namespace AIMPPL_Copy.AIMP3
{
    public class Aimp3Song : Song
    {
        private readonly string[] _parts;

        //        0        1    2      3     4     5     6            7           8       9    10             11            12       13         14
        // #Track:IsActive|Path|Artist|Album|Genre|Title|Duration(MS)|Size(Bytes)|TrackNo|Year|SampleRate(Hz)|Bitrate(Kbps)|Channels|StreamSize|Index
        public Aimp3Song(string definition)
        {
            _parts = definition.Split('|');
        }

        public override string IsActive
        {
            get => _parts[0];
            set => _parts[0] = value;
        }

        public override string Path
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

        public override string Genre
        {
            get => _parts[4];
            set => _parts[4] = value;
        }

        public override string Title
        {
            get => _parts[5];
            set => _parts[5] = value;
        }

        public override int Duration
        {
            get => int.Parse(_parts[6]);
            set => _parts[6] = value.ToString();
        }

        public override int Size
        {
            get => int.Parse(_parts[7]);
            set => _parts[7] = value.ToString();
        }

        public override int TrackNo
        {
            get => _parts[8].Length > 0 ? int.Parse(_parts[8]) : -1;
            set => _parts[8] = value.ToString();
        }

        public override string Year
        {
            get => _parts[9];
            set => _parts[9] = value;
        }

        public override int SampleRate
        {
            get => int.Parse(_parts[10]);
            set => _parts[10] = value.ToString();
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

        public override int StreamSize
        {
            get => _parts[13].Length > 0 ? int.Parse(_parts[13]) : -1;
            set => _parts[13] = value.ToString();
        }

        public override int Index
        {
            get => int.Parse(_parts[14]);
            set => _parts[14] = value.ToString();
        }

        public override string PlaylistFormat => $"#Track:{string.Join("|", _parts)}";
    }
}
