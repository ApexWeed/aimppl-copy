using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Aga.Controls.Tree;
using Apex.Translation;

namespace AIMPPL_Copy.PlaylistTree
{
    public partial class PlaylistTreeControl : UserControl
    {
        protected string defaultDestination;
        protected string defaultName;
        protected string defaultSource;
        protected string destinationString;
        protected LanguageManager lm;
        private readonly PlaylistTreeModel _model;
        protected string nameString;
        protected string sourceString;

        public PlaylistTreeControl()
        {
            InitializeComponent();

            _model = new PlaylistTreeModel();
            treeView.Model = _model;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public LanguageManager LanguageManager
        {
            set
            {
                lm = value;
                if (lm != null)
                {
                    UpdateStrings(lm, null);
                    lm.LanguageChanged += UpdateStrings;
                }
            }
        }

        [Category("Appearance")]
        [Description("The string to retrieve from the language manager for the name column.")]
        public string NameString
        {
            get => nameString;
            set
            {
                nameString = value;
                UpdateStrings(null, null);
            }
        }

        [Category("Appearance")]
        [Description("The string to use when the language manager doesn't have a suitable string for the name column.")]
        [DefaultValue("")]
        public string DefaultName
        {
            get => defaultName;
            set => defaultName = value;
        }

        [Category("Appearance")]
        [Description("The string to retrieve from the language manager for the source column.")]
        public string SourceString
        {
            get => sourceString;
            set
            {
                sourceString = value;
                UpdateStrings(null, null);
            }
        }

        [Category("Appearance")]
        [Description(
            "The string to use when the language manager doesn't have a suitable string for the source column.")]
        [DefaultValue("")]
        public string DefaultSource
        {
            get => defaultSource;
            set => defaultSource = value;
        }

        [Category("Appearance")]
        [Description("The string to retrieve from the language manager for the destination column.")]
        public string DestinationString
        {
            get => destinationString;
            set
            {
                destinationString = value;
                UpdateStrings(null, null);
            }
        }

        [Category("Appearance")]
        [Description(
            "The string to use when the language manager doesn't have a suitable string for the destination column.")]
        [DefaultValue("")]
        public string DefaultDestination
        {
            get => defaultDestination;
            set => defaultDestination = value;
        }

        public virtual void UpdateStrings(object sender, EventArgs e)
        {
            UpdateColumn(colName, NameString, DefaultName);
            UpdateColumn(colSource, SourceString, DefaultSource);
            UpdateColumn(colDestination, DestinationString, DefaultDestination);
        }

        private void UpdateColumn(TreeColumn column, string translationString, string defaultString)
        {
            if (DesignMode)
            {
                column.Header = translationString;
            }
            else
            {
                if (lm == null)
                {
                    if (!string.IsNullOrWhiteSpace(defaultString))
                        column.Header = defaultString;
                    return;
                }
                if (string.IsNullOrWhiteSpace(translationString))
                {
                    if (string.IsNullOrWhiteSpace(defaultString))
                        return;
                    column.Header = defaultString;
                }
                if (string.IsNullOrWhiteSpace(defaultString))
                    column.Header = lm.GetString(translationString);
                else
                    column.Header = lm.GetStringDefault(translationString, defaultString);

                // Maybe later.
                //FireStringChanged(this, e);
            }
        }

        public PlaylistNode AddPlaylist(Playlist playlist, List<Song> songs, List<FormatChange> formatChanges)
        {
            var playlistNode = new PlaylistNode(playlist);
            // Put the changed formats up top.
            foreach (var formatChange in formatChanges)
            {
                var songNode = new SongNode(formatChange.Song)
                {
                    DestinationFilename = Path.ChangeExtension(formatChange.Song.Path, formatChange.NewExtension)
                };
                playlistNode.Nodes.Add(songNode);
            }
            // Followed by the regular missing songs.
            foreach (var song in songs)
            {
                var songNode = new SongNode(song);
                playlistNode.Nodes.Add(songNode);
            }

            _model.Nodes.Add(playlistNode);

            return playlistNode;
        }

        public ReadOnlyCollection<TreeNodeAdv> GetSelection()
        {
            return treeView.SelectedNodes;
        }

        public void Clear()
        {
            _model.Nodes.Clear();
        }

        public static void RemoveItem(PlaylistTreeNodeBase node, int index)
        {
            node.RemoveItem(index);
        }

        public void RemovePlaylist(PlaylistNode node)
        {
            _model.Nodes.RemoveAt(node.Index);
        }

        /*
        public void RemoveItem(int index)
        {
            RemoveItem((treeView.Root.Tag as PlaylistTreeNodeBase), index);
        }
        */

        public List<PlaylistNode> GetPlaylistNodes()
        {
            return _model.Nodes.Cast<PlaylistNode>().ToList();
        }

        private void PlaylistTreeControl_SizeChanged(object sender, EventArgs e)
        {
            colName.Width = (int)(Width * 0.3f);
            colSource.Width = (int)(Width * 0.35f);
            colDestination.Width = (int)(Width * 0.35f);
        }

        /// <summary>
        ///     Event fired when the user double clicks on the destination column.
        /// </summary>
        public event EventHandler<DestinationClickedEventArgs> DestinationClicked;

        protected void FireDestinationClicked(object sender, DestinationClickedEventArgs e)
        {
            DestinationClicked?.Invoke(sender, e);
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeAdvMouseEventArgs e)
        {
            if (e.Control != ncbCheck)
                return;

            if (e.Node.Tag.GetType() == typeof(SongNode))
            {
                var node = e.Node.Tag as SongNode;
                if (node.IsChecked)
                {
                    node.IsChecked = false;
                }
                else
                {
                    if (File.Exists(node.DestinationFilename))
                        node.IsChecked = true;
                }
            }
            else if (e.Node.Tag.GetType() == typeof(PlaylistNode))
            {
                var node = e.Node.Tag as PlaylistNode;
                if (node.IsChecked)
                {
                    node.IsChecked = false;
                    foreach (var child in node.Nodes)
                        child.IsChecked = false;
                }
                else
                {
                    var allExist = node.Nodes.All(child => File.Exists((child as SongNode).DestinationFilename));

                    if (allExist)
                    {
                        node.IsChecked = true;
                        foreach (var child in node.Nodes)
                            child.IsChecked = true;
                    }
                }
            }
        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeAdvMouseEventArgs e)
        {
            if (e.Control == ntbDestination)
                FireDestinationClicked(this, new DestinationClickedEventArgs(e.Node.Tag as SongNode));
        }

        public class DestinationClickedEventArgs : EventArgs
        {
            public SongNode Node;

            public DestinationClickedEventArgs(SongNode node)
            {
                Node = node;
            }
        }
    }
}
