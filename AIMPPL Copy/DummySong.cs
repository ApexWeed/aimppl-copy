using AIMPPL_Copy.DB;

namespace AIMPPL_Copy
{
    /// <summary>
    /// Dummy song class for use in Util functions when there is no underlying playlist.
    /// </summary>
    public class DummySong : Song
    {
        public override string Album { get; set; }
        public override string Artist{ get; set; }
        public override string Title { get; set; }
        public override string Path { get; set; }
        public FilesRow Row { get; set; }

        public DummySong(string path, string title, string artist, string album, FilesRow row)
        {
            Path = path;
            Title = title;
            Artist = artist;
            Album = album;
            Row = row;
        }
    }
}
