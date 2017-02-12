using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMPPL_Copy
{
    public class Song : IEquatable<Song>
    {
        public virtual string Path
        {
            get { return ""; }
            set { }
        }
        public virtual string IsActive
        {
            get { return "1"; }
            set { }
        }
        public virtual string Artist
        {
            get { return ""; }
            set { }
        }
        public virtual string Album
        {
            get { return ""; }
            set { }
        }
        public virtual string AlbumArtist
        {
            get { return ""; }
            set { }
        }
        public virtual string Title
        {
            get { return ""; }
            set { }
        }
        public virtual string Genre
        {
            get { return ""; }
            set { }
        }
        public virtual string Year
        {
            get { return ""; }
            set { }
        }
        public virtual int Duration
        {
            get { return 0; }
            set { }
        }
        public virtual int Size
        {
            get { return 0; }
            set { }
        }
        public virtual int BPM
        {
            get { return 0; }
            set { }
        }
        public virtual int TrackNo
        {
            get { return 1; }
            set { }
        }
        public virtual int DiskNo
        {
            get { return 1; }
            set { }
        }
        public virtual string Composer
        {
            get { return ""; }
            set { }
        }
        public virtual string Publisher
        {
            get { return ""; }
            set { }
        }
        public virtual int SampleRate
        {
            get { return 44100; }
            set { }
        }
        public virtual int Bitrate
        {
            get { return 0; }
            set { }
        }
        public virtual int Channels
        {
            get { return 2; }
            set { }
        }
        public virtual int Index
        {
            get { return 0; }
            set { }
        }
        public virtual int StreamSize
        {
            get { return 0; }
            set { }
        }
        public virtual string PluginReserved
        {
            get { return ""; }
            set { }
        }

        public virtual string PlaylistFormat
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual string Directory
        {
            get
            {
                if (Path.LastIndexOf(':') > 1)
                {
                    return Path.Substring(0, Path.LastIndexOf(':'));
                }
                else
                {
                    return System.IO.Path.GetDirectoryName(Path);
                }
            }
        }

        public virtual string Group
        {
            get
            {
                var directory = Directory;
                var lastIndex = directory.LastIndexOf('\\');
                if (lastIndex == -1)
                {
                    lastIndex = directory.LastIndexOf('/');
                }
                return directory.Substring(lastIndex + 1);
            }
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                return System.IO.Path.GetFileNameWithoutExtension(Path);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(Artist))
                {
                    return Title;
                }
                else
                {
                    return $"{Artist} - {Title}";
                }
            }
        }

        public override int GetHashCode()
        {
            return Path.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }
            else if (!(obj is Song))
            {
                return false;
            }
            else
            {
                return this.Equals(obj as Song);
            }
        }

        public bool Equals(Song other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            else
            {
                return Path == other.Path;
            }
        }

        public static bool operator ==(Song a, Song b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Song a, Song b)
        {
            return !a.Equals(b);
        }
    }
}
