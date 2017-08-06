using System;
using System.Collections.Generic;
using System.Linq;

namespace AIMPPL_Copy
{
    public class Group : IEquatable<Group>
    {
        protected Cover cover;

        protected List<Scan> scans;
        protected List<Song> songs;

        public virtual string Path
        {
            get { return ""; }
            set { }
        }

        public string Name
        {
            get
            {
                var path = Path.EndsWith(".cue") ? System.IO.Path.GetDirectoryName(Path) : Path;

                var lastIndex = path.LastIndexOf('\\');
                if (lastIndex == -1)
                    lastIndex = path.LastIndexOf('/');
                return path.Substring(lastIndex + 1);
            }
        }

        public Cover Cover => cover ?? (cover = new Cover(Util.FindCover(Path)));

        public List<Scan> Scans => scans ?? (scans = Util.FindScans(Path));

        public List<Song> Songs => songs ?? (songs = new List<Song>());

        public long ScanSize => Scans.Sum(scan => scan.Size);

        public long Size => Songs.Sum(song => song.Size);

        public long Duration => Songs.Sum(song => song.Duration);

        public virtual string PlaylistFormat => throw new NotImplementedException();

        public bool Equals(Group other)
        {
            if (ReferenceEquals(other, null))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (Path == other.Path)
                if (Songs.Count == other.Songs.Count)
                {
                    return Songs.All(song => other.Songs.Contains(song));
                }

            return false;
        }

        public override string ToString() => Name;

        public override int GetHashCode() => Songs.Aggregate(Path.GetHashCode(), (current, song) => current ^ song.GetHashCode());

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;
            if (!(obj is Group))
                return false;
            return Equals(obj as Group);
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
