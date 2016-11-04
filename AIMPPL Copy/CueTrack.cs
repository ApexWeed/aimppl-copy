using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMPPL_Copy
{
    public class CueTrack
    {
        public int ID;
        public string Title;
        public string Performer;
        public string SongWriter;
        public Dictionary<string, string> Indexes;

        public CueTrack()
        {
            Indexes = new Dictionary<string, string>();
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Performer))
            {
                return Title;
            }
            else
            {
                return $"{Performer} - {Title}";
            }
        }
    }
}
