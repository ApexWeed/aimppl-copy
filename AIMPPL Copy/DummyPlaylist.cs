namespace AIMPPL_Copy
{
    /// <summary>
    /// Dummy playlist class for use in Util functions when there is no underlying playlist.
    /// </summary>
    public class DummyPlaylist : Playlist
    {
        public DummyPlaylist(string name)
        {
            Name = name;
        }
    }
}
