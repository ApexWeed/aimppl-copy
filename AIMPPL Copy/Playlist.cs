using System;
using System.Collections.Generic;
using System.Linq;

namespace AIMPPL_Copy
{
    public class Playlist
    {
        protected List<Cover> covers;
        public List<Group> Groups;
        public string Name;
        public string Path;

        protected List<Scan> scans;

        protected List<Song> songs;

        public List<Song> Songs
        {
            get
            {
                if (songs == null)
                {
                    songs = new List<Song>();
                    foreach (var group in Groups)
                        songs.AddRange(group.Songs);
                }
                return songs;
            }
        }

        public long Size => Groups.Sum(group => group.Size);

        public long CoverSize => Groups.Sum(group => group.Cover.Size);

        public List<Cover> Covers
        {
            get
            {
                if (covers == null)
                {
                    covers = new List<Cover>();
                    foreach (var group in Groups)
                        if (group.Cover.Size > 0)
                            covers.Add(group.Cover);
                }
                return covers;
            }
        }

        public List<Scan> Scans
        {
            get
            {
                if (scans == null)
                {
                    scans = new List<Scan>();
                    foreach (var group in Groups)
                        scans.AddRange(group.Scans);
                }
                return scans;
            }
        }

        public long ScanSize => Scans.Sum(scan => scan.Size);

        public long Duration => Groups.Sum(group => group.Duration);

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
