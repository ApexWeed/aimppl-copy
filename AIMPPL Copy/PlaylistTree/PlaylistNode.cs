namespace AIMPPL_Copy.PlaylistTree
{
    public class PlaylistNode : PlaylistTreeNodeBase
    {
        private Playlist _playlist;

        public PlaylistNode(Playlist playlist) : base(playlist.Name)
        {
            _playlist = playlist;
        }

        public Playlist Playlist
        {
            get => _playlist;
            set
            {
                _playlist = value;
                Text = value.Name;
            }
        }

        public override bool IsLeaf => false;

        public void CheckChildCheckState()
        {
            var allChecked = true;
            foreach (var song in Nodes)
                if (!song.IsChecked)
                {
                    allChecked = false;
                    break;
                }

            IsChecked = allChecked;
        }
    }
}
