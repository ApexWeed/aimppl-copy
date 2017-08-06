using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Ude;

namespace AIMPPL_Copy
{
    public class CueSheet
    {
        public string Album;
        public string Filename;
        public List<CueTrack> Tracks;
        public List<string> Media;

        public CueSheet(string filename)
        {
            Filename = filename;
            Album = Path.GetFileNameWithoutExtension(filename);
            Tracks = new List<CueTrack>();
            Media = new List<string>();

            if (File.Exists(filename))
            {
                using (var fs = File.OpenRead(filename))
                {
                    var detector = new CharsetDetector();
                    detector.Feed(fs);
                    detector.DataEnd();
                    fs.Seek(0, SeekOrigin.Begin);
                    using (var r = new StreamReader(fs, string.IsNullOrWhiteSpace(detector.Charset) ? System.Text.Encoding.UTF8 : System.Text.Encoding.GetEncoding(detector.Charset)))
                    {
                        var line = string.Empty;
                        var pos = 0L;
                        var albumPerformer = string.Empty;
                        var albumSongWriter = string.Empty;

                        while (!r.EndOfStream)
                        {
                            line = r.ReadLine().TrimStart();
                            if (line.StartsWith("FILE"))
                            {
                                Media.Add(line.Substring(6, line.LastIndexOf('\"') - 6));
                            }
                            else if (line.StartsWith("TITLE"))
                            {
                                Album = line.Substring(7, line.Length - 8);
                            }
                            else if (line.StartsWith("PERFORMER"))
                            {
                                albumPerformer = line.Substring(11, line.Length - 12);
                            }
                            else if (line.StartsWith("SONGWRITER"))
                            {
                                albumSongWriter = line.Substring(12, line.Length - 13);
                            }
                            else if (line.StartsWith("TRACK") && line.EndsWith("AUDIO"))
                            {
                                var index = line.Split(' ')[1];
                                var track = new CueTrack
                                {
                                    Id = int.Parse(index),
                                    Performer = albumPerformer,
                                    SongWriter = albumSongWriter
                                };

                                line = r.ReadLine().TrimStart();
                                while (!r.EndOfStream && !line.StartsWith("TRACK"))
                                {
                                    var parts = line.Split(' ');
                                    switch (parts[0])
                                    {
                                        case "TITLE":
                                        {
                                            track.Title = line.Substring(7, line.Length - 8);
                                            break;
                                        }
                                        case "PERFORMER":
                                        {
                                            track.Performer = line.Substring(11, line.Length - 12);
                                            break;
                                        }
                                        case "SONGWRITER":
                                        {
                                            track.SongWriter = line.Substring(12, line.Length - 13);
                                            break;
                                        }
                                        case "INDEX":
                                        {
                                            track.Indexes.Add(parts[1], parts[2]);
                                            break;
                                        }
                                    }

                                    pos = Util.GetActualPosition(r);
                                    line = r.ReadLine().TrimStart();
                                }

                                // Rewind back to start of track.
                                if (!r.EndOfStream)
                                    Util.SetActualPosition(r, pos);
                                Tracks.Add(track);
                            }
                        }
                    }
                }
            }
        }
    }
}
