using System.Collections.Generic;
using System.IO;

namespace AIMPPL_Copy.AIMP4
{
    public class AIMP4Group : Group
    {
        private string path;
        public override string Path
        {
            get { return path; }
            set { path = value; }
        }

        public override string PlaylistFormat
        {
            get
            {
                return $"-{Path}";
            }
        }

        // -Path
        public AIMP4Group(StreamReader Reader)
        {
            Songs = new List<Song>();

            var line = Reader.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
            {
                return;
            }
            if (line.StartsWith("-"))
            {
                path = line.Substring(1);
            }

            do
            {
                var pos = Util.GetActualPosition(Reader);
                line = Reader.ReadLine();
                
                if (line.StartsWith("-"))
                {
                    // Read past the end of the group, rewind stream and stop.
                    Util.SetActualPosition(Reader, pos);
                    break;
                }
                else
                {
                    var song = new AIMP4Song(line);
                    Songs.Add(song);
                }
            } while (!Reader.EndOfStream);
        }

        public AIMP4Group(string Path, List<Song> Songs)
        {
            this.Path = Path;
            this.Songs = Songs;
        }
    }
}
