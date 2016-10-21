using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using Aga.Controls.Tree;

namespace AIMPPL_Copy.PlaylistTree
{
    public class SongNode : PlaylistTreeNodeBase
    {
        private string sourceFilename = "";
        public string SourceFilename
        {
            get { return sourceFilename; }
            set
            {
                sourceFilename = value;
                NotifyModel();
            }
        }

        private string destinationFilename = "";
        public string DestinationFilename
        {
            get { return destinationFilename; }
            set
            {
                destinationFilename = value;
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

        private Song song;
        public Song Song
        {
            get { return song; }
            set
            {
                song = value;
                Text = value.Title;
                SourceFilename = value.Path;
            }
        }

        public override bool IsLeaf
        {
            get
            {
                return true;
            }
        }

        public SongNode(Song Song) : base(Song.Title)
        {
            this.Song = Song;
        }
    }
}
