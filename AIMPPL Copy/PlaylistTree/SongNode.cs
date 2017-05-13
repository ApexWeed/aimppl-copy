using System.Windows.Forms;

namespace AIMPPL_Copy.PlaylistTree
{
    public class SongNode : PlaylistTreeNodeBase
    {
        private string _destinationFilename = "";

        private Song _song;
        private string _sourceFilename = "";

        public SongNode(Song song) : base(song.Title)
        {
            Song = song;
        }

        public string SourceFilename
        {
            get => _sourceFilename;
            set
            {
                _sourceFilename = value;
                NotifyModel();
            }
        }

        public string DestinationFilename
        {
            get => _destinationFilename;
            set
            {
                _destinationFilename = value;
                NotifyModel();
            }
        }

        public override CheckState CheckState
        {
            get { return checkState; }
            set
            {
                if (checkState != value)
                {
                    checkState = value;
                    NotifyModel();
                    (Parent as PlaylistNode).CheckChildCheckState();
                }
            }
        }

        public Song Song
        {
            get => _song;
            set
            {
                _song = value;
                Text = value.Title;
                SourceFilename = value.Path;
            }
        }

        public override bool IsLeaf => true;
    }
}
