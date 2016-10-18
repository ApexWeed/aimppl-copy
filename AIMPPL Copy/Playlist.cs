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

        protected List<Song> songs;
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

        protected List<Cover> covers;
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

        protected List<Scan> scans;
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

        public virtual void Save()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
