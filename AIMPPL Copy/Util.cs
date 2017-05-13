using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Apex;
using TagLib;
using File = System.IO.File;

namespace AIMPPL_Copy
{
    public static class Util
    {
        private static Dictionary<string, bool> _fileExistenceCache;
        private static HashSet<string> _folderScanCache;
        private static Dictionary<string, CueSheet> _cueSheetCache;

        private static readonly string[] MusicExtensions =
        {
            "flac",
            "mp3",
            "alac",
            "tak",
            "ape",
            "wav",
            "ogg",
            "m4a"
        };

        private static Dictionary<string, Tag> _tagCache;

        // From https://stackoverflow.com/a/17457085
        public static long GetActualPosition(StreamReader reader)
        {
            var flags = BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance |
                        BindingFlags.GetField;

            // The current buffer of decoded characters
            var charBuffer = (char[])reader.GetType().InvokeMember("charBuffer", flags, null, reader, null);

            // The index of the next char to be read from charBuffer
            var charPos = (int)reader.GetType().InvokeMember("charPos", flags, null, reader, null);

            // The number of decoded chars presently used in charBuffer
            var charLen = (int)reader.GetType().InvokeMember("charLen", flags, null, reader, null);

            // The current buffer of read bytes (byteBuffer.Length = 1024; this is critical).
            var byteBuffer = (byte[])reader.GetType().InvokeMember("byteBuffer", flags, null, reader, null);

            // The number of bytes read while advancing reader.BaseStream.Position to (re)fill charBuffer
            var byteLen = (int)reader.GetType().InvokeMember("byteLen", flags, null, reader, null);

            // The number of bytes the remaining chars use in the original encoding.
            var numBytesLeft = reader.CurrentEncoding.GetByteCount(charBuffer, charPos, charLen - charPos);

            // For variable-byte encodings, deal with partial chars at the end of the buffer
            var numFragments = 0;
            if (byteLen > 0 && !reader.CurrentEncoding.IsSingleByte)
                if (reader.CurrentEncoding.CodePage == 65001) // UTF-8
                {
                    byte byteCountMask = 0;
                    while (byteBuffer[byteLen - numFragments - 1] >> 6 == 2
                    ) // if the byte is "10xx xxxx", it's a continuation-byte
                        byteCountMask |= (byte)(1 << ++numFragments); // count bytes & build the "complete char" mask
                    if (byteBuffer[byteLen - numFragments - 1] >> 6 == 3
                    ) // if the byte is "11xx xxxx", it starts a multi-byte char.
                        byteCountMask |= (byte)(1 << ++numFragments); // count bytes & build the "complete char" mask
                    // see if we found as many bytes as the leading-byte says to expect
                    if (numFragments > 1 && byteBuffer[byteLen - numFragments] >> (7 - numFragments) == byteCountMask)
                        numFragments = 0; // no partial-char in the byte-buffer to account for
                }
                else if (reader.CurrentEncoding.CodePage == 1200) // UTF-16LE
                {
                    if (byteBuffer[byteLen - 1] >= 0xd8) // high-surrogate
                        numFragments = 2; // account for the partial character
                }
                else if (reader.CurrentEncoding.CodePage == 1201) // UTF-16BE
                {
                    if (byteBuffer[byteLen - 2] >= 0xd8) // high-surrogate
                        numFragments = 2; // account for the partial character
                }
            return reader.BaseStream.Position - numBytesLeft - numFragments;
        }

        public static void SetActualPosition(StreamReader reader, long offset)
        {
            reader.BaseStream.Seek(offset, SeekOrigin.Begin);
            reader.DiscardBufferedData();
        }

        /// <summary>
        ///     Attempts to find the cover art for the specified folder. Tries common file names then resorts to any image in the
        ///     folder.
        /// </summary>
        /// <param name="path">Path to search.</param>
        /// <returns>The path to the cover or null if none are found.</returns>
        public static string FindCover(string path)
        {
            // Account for non existant folders.
            if (!Directory.Exists(path))
                return null;

            var searchFiles = new[] {"cover", "jacket", "folder"};
            var searchExtensions = new[] {"jpg", "png", "jpeg", "bmp"};
            var files = Directory.GetFiles(path);

            // Try common cover files.
            foreach (var file in searchFiles)
                foreach (var ext in searchExtensions)
                    if (files.Contains(Path.Combine(path, $"{file}.{ext}")))
                        return Path.Combine(path, $"{file}.{ext}");

            // Try to find some sort of image.
            foreach (var file in files)
                if (searchExtensions.Any(ext => file.EndsWith(ext)))
                    return file;

            // Give up.
            return null;
        }

        /// <summary>
        ///     Attempts to find the album scans for the specified folder. Tries common folder names and then looks for images in
        ///     the folder.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<Scan> FindScans(string path)
        {
            // Account for non existant folders.
            if (!Directory.Exists(path))
                return new List<Scan>();

            var searchExtensions = new[] {"jpg", "png", "bmp"};
            var searchDirectories = new[] {"scan", "scans", "Scan", "Scans", "BK", "bk", "bkv0"};
            var directories = Directory.GetDirectories(path);

            // Try common directories.
            foreach (var directory in directories)
            {
                if (searchDirectories.Any(searchDir => directory.EndsWith(searchDir)))
                {
                    return (from file in Directory.GetFiles(directory)
                        where searchExtensions.Any(file.EndsWith)
                        select new Scan(file)).ToList();
                }
            }

            // Search the album folder.
            var files = Directory.GetFiles(path);
            // Don't return the cover image as a scan.
            var cover = FindCover(path);

            // Return found scans if any.
            return (from file in files
                    where searchExtensions.Where(file.EndsWith).Any(ext => file != cover)
                    select new Scan(file)).ToList();
        }

        /// <summary>
        ///     Returns whether a file exists on disk or not, uses a cache and scans the whole folder if
        ///     the file is not in the cache already.
        /// </summary>
        /// <param name="path">Path to check for existance.</param>
        /// <returns>Whether the file exists.</returns>
        public static bool FileExists(string path)
        {
            if (_fileExistenceCache == null)
            {
                _fileExistenceCache = new Dictionary<string, bool>();
                _folderScanCache = new HashSet<string>();
            }

            if (!_fileExistenceCache.ContainsKey(path))
            {
                var dirPath = Path.GetDirectoryName(path);

                // Account for non existant folders.
                if (!Directory.Exists(dirPath))
                {
                    _fileExistenceCache.Add(path, false);
                    return false;
                }

                if (_folderScanCache.Contains(dirPath))
                {
                    _fileExistenceCache.Add(path, false);
                }
                else
                {
                    _folderScanCache.Add(dirPath);
                    var dirFiles = Directory.GetFiles(dirPath);
                    foreach (var file in dirFiles)
                        _fileExistenceCache.Add(file, true);
                    if (!dirFiles.Contains(path))
                        _fileExistenceCache.Add(path, false);
                }
            }

            return _fileExistenceCache[path];
        }

        public static bool SongExists(string path)
        {
            if (_cueSheetCache == null)
                _cueSheetCache = new Dictionary<string, CueSheet>();

            // Only load a CUE if the song is actually loaded from one.
            if (path.Contains(".cue:"))
            {
                var parts = path.Split(':');
                var file = $"{parts[0]}:{parts[1]}";
                // AIMP stores indexes zero based, CUE is one based.
                var index = int.Parse(parts[2]) + 1;

                // Cache the CUE sheet.
                if (!_cueSheetCache.ContainsKey(file))
                    _cueSheetCache.Add(file, new CueSheet(file));

                var sheet = _cueSheetCache[file];

                return sheet.Tracks.Any(x => x.Id == index);
            }
            // Proceed as normal.
            return FileExists(path);
        }

        public static Tag GetTags(string filename)
        {
            if (_tagCache == null)
                _tagCache = new Dictionary<string, Tag>();

            if (!_tagCache.ContainsKey(filename))
                if (File.Exists(filename))
                    _tagCache.Add(filename, TagLib.File.Create(filename).Tag);
                else
                    _tagCache.Add(filename, null);

            return _tagCache[filename];
        }

        /// <summary>
        ///     Finds missing songs or songs with changed filetypes in the specified playlist.
        /// </summary>
        /// <param name="playlist">Playlist to search.</param>
        /// <param name="missing">List to store the missing songs.</param>
        /// <param name="formatChanged">List to store the songs with changed filetypes.</param>
        /// <returns>True if songs were missing, false if not.</returns>
        public static bool FindMissing(Playlist playlist, out List<Song> missing, out List<FormatChange> formatChanged)
        {
            var found = false;
            missing = new List<Song>();
            formatChanged = new List<FormatChange>();
            var songs = playlist.Songs;

            foreach (var song in songs)
                if (!SongExists(song.Path))
                {
                    var changed = false;
                    foreach (var extension in MusicExtensions)
                    {
                        var newPath = Path.Combine(Path.GetDirectoryName(song.Path),
                            Path.ChangeExtension(song.Path, extension));
                        // No point checking for CUE sheets here.
                        if (FileExists(newPath))
                        {
                            formatChanged.Add(new FormatChange(song, extension));
                            changed = true;
                            found = true;
                            break;
                        }
                    }
                    if (!changed)
                    {
                        missing.Add(song);
                        found = true;
                    }
                }

            return found;
        }

        /// <summary>
        ///     Searches for missing songs.
        /// </summary>
        /// <param name="missingSongs">List of songs to search for.</param>
        /// <param name="directory">Directory to search in.</param>
        /// <param name="searchTags">Whether to scan tags if filename search fails.</param>
        /// <param name="searchCue">Whether to scan cue files if filename search fails.</param>
        /// <returns>Tuple of songs and found filepaths.</returns>
        public static List<Tuple<Song, string>> SearchSongs(List<Song> missingSongs, string directory, bool searchTags,
            bool searchCue)
        {
            var foundSongs = new List<Tuple<Song, string>>();
            var files = FileUtil.GetFiles(directory);

            // Try to find the song in the search directory.
            foreach (var song in missingSongs)
            {
                var found = false;
                // In theory, FLAC and MP3 files are much more likely to be found so prioritise searching by
                // extension rather than trying each extension on each file one after the other.
                foreach (var extension in MusicExtensions)
                {
                    if (found)
                        continue;

                    foreach (var file in files)
                    {
                        if (found)
                            continue;

                        // Update the data grid with the new filename if we found it.
                        if (Path.GetFileName(Path.ChangeExtension(song.Path, extension)) == Path.GetFileName(file))
                        {
                            foundSongs.Add(new Tuple<Song, string>(song, file));
                            found = true;
                        }
                    }
                }
            }

            if (searchCue)
            {
                foreach (var song in foundSongs.Select(x => x.Item1))
                    missingSongs.Remove(song);

                if (missingSongs.Count > 0)
                {
                    var filteredFiles = files.Where(x => String.Compare(Path.GetExtension(x), ".cue", StringComparison.OrdinalIgnoreCase) == 0);

                    foreach (var cueFile in filteredFiles)
                    {
                        if (missingSongs.Count == 0)
                            break;

                        var cueSheet = new CueSheet(cueFile);

                        for (var i = 0; i < missingSongs.Count; i++)
                        {
                            var song = missingSongs[i];
                            if (String.Compare(song.Album, cueSheet.Album, StringComparison.OrdinalIgnoreCase) == 0)
                                for (var j = 0; j < cueSheet.Tracks.Count; j++)
                                    if (String.Compare(song.Title, cueSheet.Tracks[j].Title, StringComparison.OrdinalIgnoreCase) == 0)
                                    {
                                        foundSongs.Add(new Tuple<Song, string>(song, $"{cueFile}:{j}"));
                                        missingSongs.RemoveAt(i);
                                        i--;
                                        break;
                                    }
                        }
                    }
                }
            }

            if (searchTags)
            {
                // As the list might have been pruned in the cue search, we can't just remove
                // all the entries without checking first.
                foreach (var song in foundSongs.Select(x => x.Item1))
                    if (missingSongs.Contains(song))
                        missingSongs.Remove(song);

                // Songs that couldn't be found from path alone.
                if (missingSongs.Count > 0)
                {
                    var filteredFiles = (from file in files
                                         let extension = Path.GetExtension(file)
                                         where extension.Length > 1
                                         where MusicExtensions.Contains(extension.Substring(1))
                                         select file).ToList();

                    // Search through every music file found.
                    foreach (var file in filteredFiles)
                    {
                        // No point loading tags if all songs have been found already.
                        if (missingSongs.Count == 0)
                            break;

                        var tag = GetTags(file);

                        for (var i = 0; i < missingSongs.Count; i++)
                        {
                            var song = missingSongs[i];
                            if (string.Compare(song.Title, tag.Title, StringComparison.OrdinalIgnoreCase) == 0 &&
                                string.Compare(song.Album, tag.Album, StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                // That's probably the right song.
                                foundSongs.Add(new Tuple<Song, string>(song, file));
                                missingSongs.RemoveAt(i);
                                break;
                            }
                        }
                    }
                }
            }

            return foundSongs;
        }
    }
}
