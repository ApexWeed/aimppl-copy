using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMPPL_Copy
{
    public class Playlist
    {
        public string Path;
        public string Name;
        public List<Group> Groups;

        private List<Song> songs;
        public List<Song> Songs
        {
            get
            {
                if (songs == null)
                {
                    songs = new List<Song>();
                    foreach (var group in Groups)
                    {
                        songs.AddRange(group.Songs);
                    }
                }
                return songs;
            }
        }

        public long Size
        {
            get
            {
                var size = 0L;
                foreach (var group in Groups)
                {
                    size += group.Size;
                }
                return size;
            }
        }

        public long CoverSize
        {
            get
            {
                var size = 0L;
                foreach (var group in Groups)
                {
                    size += group.Cover.Size;
                }
                return size;
            }
        }

        private List<Cover> covers;
        public List<Cover> Covers
        {
            get
            {
                if (covers == null)
                {
                    covers = new List<Cover>();
                    foreach (var group in Groups)
                    {
                        if (group.Cover.Size > 0)
                        {
                            covers.Add(group.Cover);
                        }
                    }
                }
                return covers;
            }
        }

        private List<Scan> scans;
        public List<Scan> Scans
        {
            get
            {
                if (scans == null)
                {
                    scans = new List<Scan>();
                    foreach (var group in Groups)
                    {
                        scans.AddRange(group.Scans);
                    }
                }
                return scans;
            }
        }

        public long ScanSize
        {
            get
            {
                var size = 0L;
                foreach (var scan in Scans)
                {
                    size += scan.Size;
                }
                return size;
            }
        }

        public long Duration
        {
            get
            {
                var duration = 0L;
                foreach (var group in Groups)
                {
                    duration += group.Duration;
                }
                return duration;
            }
        }

        public Playlist(string Path)
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
                            return;
                        }
                        var variable = line.Substring(1, line.IndexOf(':') - 1);
                        var value = line.Substring(variable.Length + 2);

                        switch (variable)
                        {
                            case "ID":
                                {
                                    // Don't care.
                                    break;
                                }
                            case "Name":
                                {
                                    Name = value;
                                    break;
                                }
                            case "Cursor":
                                {
                                    // Don't care.
                                    break;
                                }
                            case "Summary":
                                {
                                    // Don't care, we can calculate these.
                                    break;
                                }
                            case "Flags":
                                {
                                    // Don't care.
                                    break;
                                }
                            case "Group":
                                {
                                    // Rewind stream to allow group to load group info.
                                    Util.SetActualPosition(r, pos);
                                    var group = new Group(r);
                                    Groups.Add(group);
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

        public override string ToString()
        {
            return Name;
        }
    }
}
