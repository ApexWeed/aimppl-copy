using System.Collections.Generic;
using System.IO;

namespace AIMPPL_Copy
{
    public class Group
    {
        public string Path;
        public List<Song> Songs;

        public string Name
        {
            get
            {
                var lastIndex = Path.LastIndexOf('\\');
                if (lastIndex == -1)
                {
                    lastIndex = Path.LastIndexOf('/');
                }
                return Path.Substring(lastIndex + 1);
            }
        }

        private Cover cover;
        public Cover Cover
        {
            get
            {
                if (cover == null)
                {
                    cover = new Cover(Util.FindCover(Path));
                }
                return cover;
            }
        }

        private List<Scan> scans;
        public List<Scan> Scans
        {
            get
            {
                if (scans == null)
                {
                    scans = Util.FindScans(Path);
                }
                return scans;
            }
        }

        public long ScanSize
        {
            get
            {
                var size = 0L;
                foreach (var scan in Scans)
                {
                    size += scan.Size;
                }
                return size;
            }
        }

        public long Size
        {
            get
            {
                var size = 0L;
                foreach (var song in Songs)
                {
                    size += song.Size;
                }
                return size;
            }
        }

        public long Duration
        {
            get
            {
                var duration = 0L;
                foreach (var song in Songs)
                {
                    duration += song.Duration;
                }
                return duration;
            }
        }

        // #Group:Path|1
        public Group(StreamReader Reader)
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
            Path = value.Split('|')[0];

            do
            {
                var pos = Util.GetActualPosition(Reader);
                line = Reader.ReadLine();
                variable = line.Substring(1, line.IndexOf(':') - 1);
                value = line.Substring(variable.Length + 2);

                if (variable == "Track")
                {
                    var song = new Song(value);
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

        public override string ToString()
        {
            return Name;
        }
    }
}
