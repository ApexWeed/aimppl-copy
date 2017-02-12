using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AIMPPL_Copy
{
    public class CueSheet
    {
        public string Filename;
        public string Album;
        public List<CueTrack> Tracks;

        public CueSheet(string Filename)
        {
            this.Filename = Filename;
            Album = Path.GetFileNameWithoutExtension(Filename);
            Tracks = new List<CueTrack>();

            if (File.Exists(Filename))
            {
                using (var r = new StreamReader(Filename))
                {
                    var line = string.Empty;
                    var pos = 0L;

                    while (!r.EndOfStream)
                    {
                        line = r.ReadLine();
                        if (line.StartsWith("TITLE"))
                        {
                            Album = line.Substring(7, line.Length - 8);
                        }
                        else if (line.StartsWith("  TRACK") && line.EndsWith("AUDIO"))
                        {
                            var index = line.Split(' ')[3];
                            var track = new CueTrack
                            {
                                ID = int.Parse(index)
                            };

                            line = r.ReadLine();
                            while (!r.EndOfStream && line.StartsWith("    "))
                            {
                                var parts = line.Split(' ');
                                switch (parts[4])
                                {
                                    case "TITLE":
                                        {
                                            track.Title = line.Substring(11, line.Length - 12);
                                            break;
                                        }
                                    case "PERFORMER":
                                        {
                                            track.Performer = line.Substring(15, line.Length - 16);
                                            break;
                                        }
                                    case "SONGWRITER":
                                        {
                                            track.SongWriter = line.Substring(16, line.Length - 17);
                                            break;
                                        }
                                    case "INDEX":
                                        {
                                            track.Indexes.Add(parts[5], parts[6]);
                                            break;
                                        }
                                }

                                pos = Util.GetActualPosition(r);
                                line = r.ReadLine();
                            }

                            // Rewind back to start of track.
                            Util.SetActualPosition(r, pos);
                            Tracks.Add(track);
                        }
                    }
                }
            }
        }
    }
}
