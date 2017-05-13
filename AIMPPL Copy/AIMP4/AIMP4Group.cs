using System.Collections.Generic;
using System.IO;

namespace AIMPPL_Copy.AIMP4
{
    public class Aimp4Group : Group
    {
        private string _path;

        // -Path
        public Aimp4Group(StreamReader reader)
        {
            var line = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
                return;
            if (line.StartsWith("-"))
                _path = line.Substring(1);

            do
            {
                var pos = Util.GetActualPosition(reader);
                line = reader.ReadLine();

                if (line.StartsWith("-"))
                {
                    // Read past the end of the group, rewind stream and stop.
                    Util.SetActualPosition(reader, pos);
                    break;
                }
                var song = new Aimp4Song(line);
                Songs.Add(song);
            } while (!reader.EndOfStream);
        }

        public Aimp4Group(string path, List<Song> songs)
        {
            this.Path = path;
            this.songs = songs;
        }

        public override string Path
        {
            get => _path;
            set => _path = value;
        }

        public override string PlaylistFormat => $"-{Path}";
    }
}
