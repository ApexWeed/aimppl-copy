using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMPPL_Copy.AIMP3
{
    public class AIMP3Song : Song
    {
        private string[] parts;

        public override string IsActive
        {
            get { return parts[0]; }
            set { parts[0] = value; }
        }
        public override string Path
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
        public override string Genre
        {
            get { return parts[4]; }
            set { parts[4] = value; }
        }
        public override string Title
        {
            get { return parts[5]; }
            set { parts[5] = value; }
        }
        public override int Duration
        {
            get { return int.Parse(parts[6]); }
            set { parts[6] = value.ToString(); }
        }
        public override int Size
        {
            get { return int.Parse(parts[7]); }
            set { parts[7] = value.ToString(); }
        }
        public override int TrackNo
        {
            get { return parts[8].Length > 0 ? int.Parse(parts[8]) : -1; }
            set { parts[8] = value.ToString(); }
        }
        public override string Year
        {
            get { return parts[9]; }
            set { parts[9] = value; }
        }
        public override int SampleRate
        {
            get { return int.Parse(parts[10]); }
            set { parts[10] = value.ToString(); }
        }
        public override int Bitrate
        {
            get { return int.Parse(parts[11]); }
            set { parts[11] = value.ToString(); }
        }
        public override int Channels
        {
            get { return int.Parse(parts[12]); }
            set { parts[12] = value.ToString(); }
        }
        public override int StreamSize
        {
            get { return parts[13].Length > 0 ? int.Parse(parts[13]) : -1; ; }
            set { parts[13] = value.ToString(); }
        }
        public override int Index
        {
            get { return int.Parse(parts[14]); }
            set { parts[14] = value.ToString(); }
        }

        public override string PlaylistFormat
        {
            get
            {
                return $"#Track:{string.Join("|", parts)}";
            }
        }

        //        0        1    2      3     4     5     6            7           8       9    10             11            12       13         14
        // #Track:IsActive|Path|Artist|Album|Genre|Title|Duration(MS)|Size(Bytes)|TrackNo|Year|SampleRate(Hz)|Bitrate(Kbps)|Channels|StreamSize|Index
        public AIMP3Song(string Definition)
        {
            parts = Definition.Split('|');
        }
    }
}
