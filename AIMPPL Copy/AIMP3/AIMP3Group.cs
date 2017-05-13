using System.Collections.Generic;
using System.IO;

namespace AIMPPL_Copy.AIMP3
{
    public class Aimp3Group : Group
    {
        private readonly string[] _parts;

        // #Group:Path|1
        public Aimp3Group(StreamReader reader)
        {
            var line = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
                return;
            var variable = line.Substring(1, line.IndexOf(':') - 1);
            var value = line.Substring(variable.Length + 2);
            // No idea what that number on the end is for.
            _parts = value.Split('|');

            do
            {
                var pos = Util.GetActualPosition(reader);
                line = reader.ReadLine();
                variable = line.Substring(1, line.IndexOf(':') - 1);
                value = line.Substring(variable.Length + 2);

                if (variable == "Track")
                {
                    var song = new Aimp3Song(value);
                    Songs.Add(song);
                }
                else
                {
                    // Read past the end of the group, rewind stream and stop.
                    Util.SetActualPosition(reader, pos);
                    break;
                }
            } while (!reader.EndOfStream);
        }

        public Aimp3Group(string path, List<Song> songs)
        {
            Path = path;
            this.songs = songs;
        }

        public override string Path
        {
            get => _parts[0];
            set => _parts[0] = value;
        }

        public override string PlaylistFormat => $"#Group:{string.Join("|", _parts)}";
    }
}
