using System.Collections.Generic;

namespace AIMPPL_Copy
{
    public class CueTrack
    {
        public int Id;
        public Dictionary<string, string> Indexes;
        public string Performer;
        public string SongWriter;
        public string Title;

        public CueTrack()
        {
            Indexes = new Dictionary<string, string>();
        }

        public override string ToString() => string.IsNullOrWhiteSpace(Performer) ? Title : $"{Performer} - {Title}";

    }
}
