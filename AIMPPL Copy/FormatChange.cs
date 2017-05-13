using System.IO;

namespace AIMPPL_Copy
{
    public class FormatChange
    {
        public string NewExtension;
        public Song Song;

        public FormatChange(Song song, string extension)
        {
            Song = song;
            NewExtension = extension;
        }

        public override string ToString() => $"{Song} ({Path.GetExtension(Song.Path).Substring(1)} -> {NewExtension})";
    }
}
