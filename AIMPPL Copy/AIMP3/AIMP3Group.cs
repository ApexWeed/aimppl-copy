using System.Collections.Generic;
using System.IO;

namespace AIMPPL_Copy.AIMP3
{
    public class AIMP3Group : Group
    {
        private string[] parts;
        public override string Path
        {
            get { return parts[0]; }
            set { parts[0] = value; }
        }

        public override string PlaylistFormat
        {
            get
            {
                return $"#Group:{string.Join("|", parts)}";
            }
        }

        // #Group:Path|1
        public AIMP3Group(StreamReader Reader)
        {
            Songs = new List<Song>();

            var line = Reader.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
            {
                return;
            }
            var variable = line.Substring(1, line.IndexOf(':') - 1);
            var value = line.Substring(variable.Length + 2);
            // No idea what that number on the end is for.
            parts = value.Split('|');

            do
            {
                var pos = Util.GetActualPosition(Reader);
                line = Reader.ReadLine();
                variable = line.Substring(1, line.IndexOf(':') - 1);
                value = line.Substring(variable.Length + 2);

                if (variable == "Track")
                {
                    var song = new AIMP3Song(value);
                    Songs.Add(song);
                }
                else
                {
                    // Read past the end of the group, rewind stream and stop.
                    Util.SetActualPosition(Reader, pos);
                    break;
                }
            } while (!Reader.EndOfStream);
        }
    }
}
