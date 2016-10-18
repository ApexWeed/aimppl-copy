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

        public override string ToString()
        {
            return Name;
        }
    }
}
