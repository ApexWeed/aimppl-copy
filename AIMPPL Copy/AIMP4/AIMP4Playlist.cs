using System;
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
                                    var parts = line.Split('=');
                                    Summary.Add(parts[0], parts[1]);
                                    if (parts[0] == "Name")
                                    {
                                        Name = parts[1];
                                    }
                                    break;
                                }
                            case PlaylistMode.Settings:
                                {
                                    var parts = line.Split('=');
                                    Settings.Add(parts[0], parts[1]);
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
    }
}
