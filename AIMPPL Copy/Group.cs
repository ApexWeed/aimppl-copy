using System;
using System.Collections.Generic;
using System.IO;

namespace AIMPPL_Copy
{
    public class Group
    {
        public virtual string Path
        {
            get { return ""; }
            set { }
        }
        public List<Song> Songs;

        public string Name
        {
            get
            {
                var lastIndex = Path.LastIndexOf('\\');
                if (lastIndex == -1)
                {
                    lastIndex = Path.LastIndexOf('/');
                }
                return Path.Substring(lastIndex + 1);
            }
        }

        protected Cover cover;
        public Cover Cover
        {
            get
            {
                if (cover == null)
                {
                    cover = new Cover(Util.FindCover(Path));
                }
                return cover;
            }
        }

        protected List<Scan> scans;
        public List<Scan> Scans
        {
            get
            {
                if (scans == null)
                {
                    scans = Util.FindScans(Path);
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

        public long Size
        {
            get
            {
                var size = 0L;
                foreach (var song in Songs)
                {
                    size += song.Size;
                }
                return size;
            }
        }

        public long Duration
        {
            get
            {
                var duration = 0L;
                foreach (var song in Songs)
                {
                    duration += song.Duration;
                }
                return duration;
            }
        }

        public virtual string PlaylistFormat
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
