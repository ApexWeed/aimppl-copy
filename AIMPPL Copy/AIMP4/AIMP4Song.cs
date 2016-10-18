using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMPPL_Copy.AIMP4
{
    public class AIMP4Song : Song
    {
        private string[] parts;
        
        public override string Path
        {
            get { return parts[0]; }
            set { parts[0] = value; }
        }
        public override string Title
        {
            get { return parts[1]; }
            set { parts[1] = value; }
        }
        public override string Artist
        {
            get { return parts[2]; }
            set { parts[2] = value; }
        }
        public override string Album
        {
            get { return parts[3]; }
            set { parts[3] = value; }
        }
        public override string AlbumArtist
        {
            get { return parts[4]; }
            set { parts[4] = value; }
        }
        public override string Genre
        {
            get { return parts[5]; }
            set { parts[5] = value; }
        }
        public override string Year
        {
            get { return parts[6]; }
            set { parts[6] = value; }
        }
        public override int TrackNo
        {
            get { return parts[7].Length > 0 ? int.Parse(parts[7]) : -1; }
            set { parts[7] = value.ToString(); }
        }
        public override int DiskNo
        {
            get { return parts[8].Length > 0 ? int.Parse(parts[8]) : -1; }
            set { parts[8] = value.ToString(); }
        }
        public override int Bitrate
        {
            get { return int.Parse(parts[9]); }
            set { parts[9] = value.ToString(); }
        }
        public override int Channels
        {
            get { return int.Parse(parts[12]); }
            set { parts[12] = value.ToString(); }
        }
        public override int SampleRate
        {
            get { return int.Parse(parts[13]); }
            set { parts[13] = value.ToString(); }
        }
        public override int Duration
        {
            get { return int.Parse(parts[14]); }
            set { parts[14] = value.ToString(); }
        }
        public override int Size
        {
            get { return int.Parse(parts[15]); }
            set { parts[15] = value.ToString(); }
        }

        public override string PlaylistFormat
        {
            get
            {
                return $"{string.Join("|", parts)}";
            }
        }

        // 0    1     2      3     4           5     6    7       8      9 10 11            12       13             14           15          16 17 18 19 20
        // Path|Title|Artist|Album|AlbumArtist|Genre|Year|TrackNo|DiskNo|?|? |Bitrate(Kbps)|Channels|SampleRate(Hz)|Duration(MS)|Size(Bytes)|? |? |? |? |?
        public AIMP4Song(string Definition)
        {
            parts = Definition.Split('|');
        }
    }
}
