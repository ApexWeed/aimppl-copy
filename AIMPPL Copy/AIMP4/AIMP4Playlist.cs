﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMPPL_Copy.AIMP4
{
    public class AIMP4Playlist : Playlist
    {
        private enum PlaylistMode
        {
            Summary,
            Settings,
            Content
        }

        public Dictionary<string, string> Summary;
        public Dictionary<string, string> Settings;

        private AIMP4Playlist()
        {
            Groups = new List<Group>();
            Summary = new Dictionary<string, string>();
            Settings = new Dictionary<string, string>();
        }

        public AIMP4Playlist(string Path)
        {
            this.Path = Path;
            Groups = new List<Group>();
            Summary = new Dictionary<string, string>();
            Settings = new Dictionary<string, string>();

            var mode = PlaylistMode.Summary;
            using (var fs = File.Open(Path, FileMode.Open))
            {
                using (var r = new StreamReader(fs))
                {
                    if (!r.BaseStream.CanSeek)
                    {
                        throw new NotSupportedException("Stream doesn't support seeking, cannot load playlist.");
                    }
                    while (!r.EndOfStream)
                    {
                        var pos = Util.GetActualPosition(r);
                        var line = r.ReadLine();
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        if (line == "#-----SUMMARY-----#")
                        {
                            mode = PlaylistMode.Summary;
                            continue;
                        }
                        else if (line == "#-----SETTINGS-----#")
                        {
                            mode = PlaylistMode.Settings;
                            continue;
                        }
                        else if (line == "#-----CONTENT-----#")
                        {
                            mode = PlaylistMode.Content;
                            continue;
                        }

                        switch (mode)
                        {
                            case PlaylistMode.Summary:
                                {
                                    var split = line.IndexOf('=');
                                    var variable = line.Substring(0, split);
                                    var value = line.Substring(split + 1);
                                    Summary.Add(variable, value);
                                    if (variable == "Name")
                                    {
                                        Name = value;
                                    }
                                    break;
                                }
                            case PlaylistMode.Settings:
                                {
                                    var split = line.IndexOf('=');
                                    var variable = line.Substring(0, split);
                                    var value = line.Substring(split + 1);
                                    Settings.Add(variable, value);
                                    break;
                                }
                            case PlaylistMode.Content:
                                {
                                    // Rewind stream to allow group to load group info.
                                    Util.SetActualPosition(r, pos);
                                    var group = new AIMP4Group(r);
                                    if (group.Songs.Count > 0)
                                    {
                                        Groups.Add(group);
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
        }

        public override void Save()
        {
            CorrectGroups();

            using (var fs = File.Create(Path))
            {
                using (var w = new StreamWriter(fs, Encoding.Unicode))
                {
                    w.WriteLine("#-----SUMMARY-----#");
                    foreach (var summaryValue in Summary)
                    {
                        w.WriteLine($"{summaryValue.Key}={summaryValue.Value}");
                    }
                    w.WriteLine("\n#-----SETTINGS-----#");
                    foreach (var settingsValue in Settings)
                    {
                        w.WriteLine($"{settingsValue.Key}={settingsValue.Value}");
                    }
                    w.WriteLine("\n#-----CONTENT-----#");
                    foreach (var group in Groups)
                    {
                        w.WriteLine(group.PlaylistFormat);
                        foreach (var song in group.Songs)
                        {
                            w.WriteLine(song.PlaylistFormat);
                        }
                    }
                }
            }
        }

        public void CorrectGroups()
        {
            for (int i = 0; i < Groups.Count; i++)
            {
                var group = Groups[i];
                var correct = true;
                var directories = new HashSet<string>();

                foreach (var song in group.Songs)
                {
                    if (!directories.Contains(song.Directory))
                    {
                        directories.Add(song.Directory);
                    }

                    if (song.Directory != group.Path)
                    {
                        correct = false;
                    }
                }

                // The group contains files in multiple directories, they need to be split into seperate groups.
                if (directories.Count > 1)
                {
                    var newGroups = new List<Group>();
                    foreach (var directory in directories)
                    {
                        var songs = group.Songs.Where((x) => x.Directory == directory).ToList();
                        var newGroup = new AIMP4Group(directory, songs);
                        newGroups.Add(newGroup);
                    }

                    Groups.RemoveAt(i);
                    Groups.InsertRange(i, newGroups);
                    i += directories.Count - 1;
                }
                // The group has the wrong path.
                else if (!correct)
                {
                    group.Path = directories.First();
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public static AIMP4Playlist UpgradePlaylist(AIMP3.AIMP3Playlist OldPlaylist)
        {
            var newPlaylist = new AIMP4Playlist()
            {
                Name = OldPlaylist.Name,
                Path = System.IO.Path.ChangeExtension(OldPlaylist.Path, "aimppl4")
            };

            var contentDuration = 0;
            var contentFiles = 0;
            var contentSize = 0;
            foreach (var oldGroup in OldPlaylist.Groups)
            {
                var newSongs = new List<Song>();
                foreach (var oldSong in oldGroup.Songs)
                {
                    var newSong = new AIMP4Song(oldSong);
                    newSongs.Add(newSong);
                    contentDuration += newSong.Duration;
                    contentFiles++;
                    contentSize += newSong.Size;
                }
                newPlaylist.Groups.Add(new AIMP4Group(oldGroup.Path, newSongs));
            }

            newPlaylist.Summary.Add("ID", OldPlaylist.ID);
            newPlaylist.Summary.Add("Name", OldPlaylist.Name);
            newPlaylist.Summary.Add("NameIsAutoSet", "0");
            newPlaylist.Summary.Add("ContentDuration", contentDuration.ToString());
            newPlaylist.Summary.Add("ContentFiles", contentFiles.ToString());
            newPlaylist.Summary.Add("ContentSize", contentSize.ToString());
            newPlaylist.Summary.Add("PlaybackCursor", OldPlaylist.Cursor);

            newPlaylist.Settings.Add("Flags", OldPlaylist.Flags);
            newPlaylist.Settings.Add("FormatMainLine", "%IF(%R,%R - %T,%T)");
            newPlaylist.Settings.Add("FormatSecondLine", "%E :: %H, %B, %S");
            newPlaylist.Settings.Add("GroupFormatLine", "%D");

            return newPlaylist;
        }
    }
}
