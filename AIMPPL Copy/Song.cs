using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMPPL_Copy
{
    public class Song
    {
        public string Path;
        public string Artist;
        public string Album;
        public string Title;
        public int Duration;
        public int Size;
        public int TrackNo;
        public int SampleRate;
        public int Bitrate;
        public int Channels;

        //        0 1    2      3     4 5     6            7           8       9 10             11            12       13 14
        // #Track:1|Path|Artist|Album|?|Title|Duration(MS)|Size(Bytes)|TrackNo|?|SampleRate(Hz)|Bitrate(Kbps)|Channels|? |?
        public Song(string Definition)
        {
            var split = Definition.Split('|');
            Path = split[1];
            Artist = split[2];
            Album = split[3];
            Title = split[5];
            Duration = int.Parse(split[6]);
            Size = int.Parse(split[7]);
            TrackNo = split[8].Length > 0 ? int.Parse(split[8]) : -1;
            SampleRate = int.Parse(split[10]);
            Bitrate = int.Parse(split[11]);
            Channels = int.Parse(split[12]);
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                return System.IO.Path.GetFileNameWithoutExtension(Path);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(Artist))
                {
                    return Title;
                }
                else
                {
                    return $"{Artist} - {Title}";
                }
            }
        }
    }
}
