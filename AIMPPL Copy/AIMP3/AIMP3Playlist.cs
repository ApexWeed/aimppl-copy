using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMPPL_Copy.AIMP3
{
    public class AIMP3Playlist : Playlist
    {
        public string ID;
        public string Cursor;
        public string Summary;
        public string Flags;

        public AIMP3Playlist(string Path)
        {
            this.Path = Path;
            Groups = new List<Group>();

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
                        var variable = line.Substring(1, line.IndexOf(':') - 1);
                        var value = line.Substring(variable.Length + 2);

                        switch (variable)
                        {
                            case "ID":
                                {
                                    ID = value;
                                    break;
                                }
                            case "Name":
                                {
                                    Name = value;
                                    break;
                                }
                            case "Cursor":
                                {
                                    Cursor = value;
                                    break;
                                }
                            case "Summary":
                                {
                                    Summary = value;
                                    break;
                                }
                            case "Flags":
                                {
                                    Flags = value;
                                    break;
                                }
                            case "Group":
                                {
                                    // Rewind stream to allow group to load group info.
                                    Util.SetActualPosition(r, pos);
                                    var group = new AIMP3Group(r);
                                    if (group.Songs.Count > 0)
                                    {
                                        Groups.Add(group);
                                    }
                                    break;
                                }
                            default:
                                {
                                    throw new NotSupportedException($"Unknown playlist variable \"{variable}\".");
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
                    w.WriteLine($"#ID:{ID}");
                    w.WriteLine($"#Name:{Name}");
                    w.WriteLine($"#Cursor:{Cursor}");
                    w.WriteLine($"#Summary:{Summary}");
                    w.WriteLine($"#Flags:{Flags}");
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
                        var newGroup = new AIMP3Group(directory, songs);
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
