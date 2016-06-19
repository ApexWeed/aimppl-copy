using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMPPL_Copy
{
    public static class Util
    {
        // From https://stackoverflow.com/a/17457085
        public static long GetActualPosition(StreamReader reader)
        {
            System.Reflection.BindingFlags flags = System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetField;

            // The current buffer of decoded characters
            char[] charBuffer = (char[])reader.GetType().InvokeMember("charBuffer", flags, null, reader, null);

            // The index of the next char to be read from charBuffer
            int charPos = (int)reader.GetType().InvokeMember("charPos", flags, null, reader, null);

            // The number of decoded chars presently used in charBuffer
            int charLen = (int)reader.GetType().InvokeMember("charLen", flags, null, reader, null);

            // The current buffer of read bytes (byteBuffer.Length = 1024; this is critical).
            byte[] byteBuffer = (byte[])reader.GetType().InvokeMember("byteBuffer", flags, null, reader, null);

            // The number of bytes read while advancing reader.BaseStream.Position to (re)fill charBuffer
            int byteLen = (int)reader.GetType().InvokeMember("byteLen", flags, null, reader, null);

            // The number of bytes the remaining chars use in the original encoding.
            int numBytesLeft = reader.CurrentEncoding.GetByteCount(charBuffer, charPos, charLen - charPos);

            // For variable-byte encodings, deal with partial chars at the end of the buffer
            int numFragments = 0;
            if (byteLen > 0 && !reader.CurrentEncoding.IsSingleByte)
            {
                if (reader.CurrentEncoding.CodePage == 65001) // UTF-8
                {
                    byte byteCountMask = 0;
                    while ((byteBuffer[byteLen - numFragments - 1] >> 6) == 2) // if the byte is "10xx xxxx", it's a continuation-byte
                        byteCountMask |= (byte)(1 << ++numFragments); // count bytes & build the "complete char" mask
                    if ((byteBuffer[byteLen - numFragments - 1] >> 6) == 3) // if the byte is "11xx xxxx", it starts a multi-byte char.
                        byteCountMask |= (byte)(1 << ++numFragments); // count bytes & build the "complete char" mask
                                                                      // see if we found as many bytes as the leading-byte says to expect
                    if (numFragments > 1 && ((byteBuffer[byteLen - numFragments] >> 7 - numFragments) == byteCountMask))
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
            }
            return reader.BaseStream.Position - numBytesLeft - numFragments;
        }

        public static void SetActualPosition(StreamReader Reader, long Offset)
        {
            Reader.BaseStream.Seek(Offset, SeekOrigin.Begin);
            Reader.DiscardBufferedData();
        }

        /// <summary>
        /// Attempts to find the cover art for the specified folder. Tries common file names then resorts to any image in the folder.
        /// </summary>
        /// <param name="Path">Path to search.</param>
        /// <returns>The path to the cover or null if none are found.</returns>
        public static string FindCover(string Path)
        {
            var searchFiles = new string[] { "cover", "jacket" };
            var searchExtensions = new string[] { "jpg", "png", "bmp" };
            var files = Directory.GetFiles(Path);

            // Try common cover files.
            foreach (var file in searchFiles)
            {
                foreach (var ext in searchExtensions)
                {
                    if (files.Contains(System.IO.Path.Combine(Path, $"{file}.{ext}")))
                    {
                        return System.IO.Path.Combine(Path, $"{file}.{ext}");
                    }
                }
            }

            // Try to find some sort of image.
            foreach (var file in files)
            {
                foreach (var ext in searchExtensions)
                {
                    if (file.EndsWith(ext))
                    {
                        return file;
                    }
                }
            }

            // Give up.
            return null;
        }

        /// <summary>
        /// Attempts to find the album scans for the specified folder. Tries common folder names and then looks for images in the folder.
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static List<Scan> FindScans(string Path)
        {
            var searchExtensions = new string[] { "jpg", "png", "bmp" };
            var searchDirectories = new string[] { "scan", "scans", "Scan", "Scans", "BK", "bk", "bkv0" };
            var scans = new List<Scan>();
            var directories = Directory.GetDirectories(Path);

            // Try common directories.
            foreach (var directory in directories)
            {
                foreach (var searchDir in searchDirectories)
                {
                    if (directory.EndsWith(searchDir))
                    {
                        foreach (var file in Directory.GetFiles(directory))
                        {
                            foreach (var ext in searchExtensions)
                            {
                                if (file.EndsWith(ext))
                                {
                                    scans.Add(new Scan(file));
                                    break;
                                }
                            }
                        }
                        return scans;
                    }
                }
            }

            // Search the album folder.
            var files = Directory.GetFiles(Path);
            // Don't return the cover image as a scan.
            var cover = FindCover(Path);
            foreach (var file in files)
            {
                foreach (var ext in searchExtensions)
                {
                    if (file.EndsWith(ext))
                    {
                        if (file != cover)
                        {
                            scans.Add(new Scan(file));
                            break;
                        }
                    }
                }
            }

            // Return found scans if any.
            return scans;
        }
    }
}
