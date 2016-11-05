using System;
using System.Collections.Generic;
using System.IO;

namespace AIMPPL_Copy
{
    public class Group : IEquatable<Group>
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

        public override int GetHashCode()
        {
            var code = Path.GetHashCode();
            
            foreach (var song in Songs)
            {
                code ^= song.GetHashCode();
            }

            return code;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }
            else if (!(obj is Group))
            {
                return false;
            }
            else
            {
                return this.Equals(obj as Group);
            }
        }

        public bool Equals(Group other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            else if (ReferenceEquals(this, other))
            {
                return true;
            }
            else
            {
                if (Path == other.Path)
                {
                    if (Songs.Count == other.Songs.Count)
                    {
                        foreach (var song in Songs)
                        {
                            if (!other.Songs.Contains(song))
                            {
                                return false;
                            }
                        }

                        return true;
                    }
                }

                return false;
            }
        }

        public static bool operator ==(Group a, Group b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Group a, Group b)
        {
            return !a.Equals(b);
        }
    }
}
