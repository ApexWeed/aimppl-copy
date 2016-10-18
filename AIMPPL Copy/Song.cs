using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMPPL_Copy
{
    public class Song
    {
        public virtual string Path
        {
            get { return ""; }
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
        public virtual int TrackNo
        {
            get { return 0; }
            set { }
        }
        public virtual int DiskNo
        {
            get { return 0; }
            set { }
        }
        public virtual int SampleRate
        {
            get { return 0; }
            set { }
        }
        public virtual int Bitrate
        {
            get { return 0; }
            set { }
        }
        public virtual int Channels
        {
            get { return 0; }
            set { }
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
    }
}
