using System.Reflection;

namespace AIMPPL_Copy.AIMP4
{
    public class Aimp4Song : Song
    {
        private readonly string[] _parts;

        // 0    1     2      3     4           5     6    7       8      9        10        11            12       13             14           15          16  17       18    19
        // Path|Title|Artist|Album|AlbumArtist|Genre|Year|TrackNo|DiskNo|Composer|Publisher|Bitrate(Kbps)|Channels|SampleRate(Hz)|Duration(MS)|Size(Bytes)|BPM|IsActive|Index|PluginReserved|
        public Aimp4Song(string definition)
        {
            _parts = definition.Split('|');
        }

        public Aimp4Song(Song oldSong)
        {
            _parts = new string[21];

            foreach (var propertyInfo in typeof(Song).GetProperties(BindingFlags.Public | BindingFlags.Instance))
                // Skip unsettable properties (read only macros like directory and group).
                if (propertyInfo.GetSetMethod() != null)
                    propertyInfo.SetValue(this, propertyInfo.GetValue(oldSong));
        }

        public override string Path
        {
            get => _parts[0];
            set => _parts[0] = value;
        }

        public override string Title
        {
            get => _parts[1];
            set => _parts[1] = value;
        }

        public override string Artist
        {
            get => _parts[2];
            set => _parts[2] = value;
        }

        public override string Album
        {
            get => _parts[3];
            set => _parts[3] = value;
        }

        public override string AlbumArtist
        {
            get => _parts[4];
            set => _parts[4] = value;
        }

        public override string Genre
        {
            get => _parts[5];
            set => _parts[5] = value;
        }

        public override string Year
        {
            get => _parts[6];
            set => _parts[6] = value;
        }

        public override int TrackNo
        {
            get => _parts[7].Length > 0 ? int.Parse(_parts[7]) : -1;
            set => _parts[7] = value.ToString();
        }

        public override int DiskNo
        {
            get => _parts[8].Length > 0 ? int.Parse(_parts[8]) : -1;
            set => _parts[8] = value.ToString();
        }

        public override string Composer
        {
            get => _parts[9];
            set => _parts[9] = value;
        }

        public override string Publisher
        {
            get => _parts[10];
            set => _parts[10] = value;
        }

        public override int Bitrate
        {
            get => int.Parse(_parts[11]);
            set => _parts[11] = value.ToString();
        }

        public override int Channels
        {
            get => int.Parse(_parts[12]);
            set => _parts[12] = value.ToString();
        }

        public override int SampleRate
        {
            get => int.Parse(_parts[13]);
            set => _parts[13] = value.ToString();
        }

        public override int Duration
        {
            get => int.Parse(_parts[14]);
            set => _parts[14] = value.ToString();
        }

        public override int Size
        {
            get => int.Parse(_parts[15]);
            set => _parts[15] = value.ToString();
        }

        public override int Bpm
        {
            get => int.Parse(_parts[16]);
            set => _parts[16] = value.ToString();
        }

        public override string IsActive
        {
            get => _parts[17];
            set => _parts[17] = value;
        }

        public override int Index
        {
            get => int.Parse(_parts[18]);
            set => _parts[18] = value.ToString();
        }

        public override string PluginReserved
        {
            get => _parts[18];
            set => _parts[18] = value;
        }

        public override string PlaylistFormat => $"{string.Join("|", _parts)}";
    }
}
