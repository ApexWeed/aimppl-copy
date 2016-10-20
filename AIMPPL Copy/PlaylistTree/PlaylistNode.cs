using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using Aga.Controls.Tree;

namespace AIMPPL_Copy.PlaylistTree
{
    public class PlaylistNode : PlaylistTreeNodeBase
    {
        private Playlist playlist;
        public Playlist Playlist
        {
            get { return playlist; }
            set
            {
                playlist = value;
                Text = value.Name;
            }
        }

        public override bool IsLeaf
        {
            get
            {
                return false;
            }
        }

        public PlaylistNode(Playlist Playlist) : base(Playlist.Name)
        {
            playlist = Playlist;
        }
    }
}
