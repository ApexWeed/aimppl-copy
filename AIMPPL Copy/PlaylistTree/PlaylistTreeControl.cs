using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Apex.Translation;
using Aga.Controls.Tree;
using System.IO;

namespace AIMPPL_Copy.PlaylistTree
{
    public partial class PlaylistTreeControl : UserControl
    {
        private PlaylistTreeModel model;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public LanguageManager LanguageManager
        {
            set
            {
                LM = value;
                if (LM != null)
                {
                    UpdateStrings(LM, null);
                    LM.LanguageChanged += UpdateStrings;
                }
            }
        }
        protected LanguageManager LM;

        [Category("Appearance")]
        [Description("The string to retrieve from the language manager for the name column.")]
        public string NameString
        {
            get { return nameString; }
            set
            {
                nameString = value;
                UpdateStrings(null, null);
            }
        }
        protected string nameString;

        [Category("Appearance")]
        [Description("The string to use when the language manager doesn't have a suitable string for the name column.")]
        [DefaultValue("")]
        public string DefaultName
        {
            get { return defaultName; }
            set { defaultName = value; }
        }
        protected string defaultName;

        [Category("Appearance")]
        [Description("The string to retrieve from the language manager for the source column.")]
        public string SourceString
        {
            get { return sourceString; }
            set
            {
                sourceString = value;
                UpdateStrings(null, null);
            }
        }
        protected string sourceString;

        [Category("Appearance")]
        [Description("The string to use when the language manager doesn't have a suitable string for the source column.")]
        [DefaultValue("")]
        public string DefaultSource
        {
            get { return defaultSource; }
            set { defaultSource = value; }
        }
        protected string defaultSource;

        [Category("Appearance")]
        [Description("The string to retrieve from the language manager for the destination column.")]
        public string DestinationString
        {
            get { return destinationString; }
            set
            {
                destinationString = value;
                UpdateStrings(null, null);
            }
        }
        protected string destinationString;

        [Category("Appearance")]
        [Description("The string to use when the language manager doesn't have a suitable string for the destination column.")]
        [DefaultValue("")]
        public string DefaultDestination
        {
            get { return defaultDestination; }
            set { defaultDestination = value; }
        }
        protected string defaultDestination;

        public class DestinationClickedEventArgs : EventArgs
        {
            public SongNode Node;

            public DestinationClickedEventArgs(SongNode Node)
            {
                this.Node = Node;
            }
        }

        public PlaylistTreeControl()
        {
            InitializeComponent();

            model = new PlaylistTreeModel();
            treeView.Model = model;
        }

        public virtual void UpdateStrings(object sender, EventArgs e)
        {
            UpdateColumn(colName, NameString, DefaultName);
            UpdateColumn(colSource, SourceString, DefaultSource);
            UpdateColumn(colDestination, DestinationString, DefaultDestination);
        }

        private void UpdateColumn(TreeColumn Column, string TranslationString, string DefaultString)
        {
            if (DesignMode)
            {
                Column.Header = TranslationString;
            }
            else
            {
                if (LM == null)
                {
                    if (!string.IsNullOrWhiteSpace(DefaultString))
                    {
                        Column.Header = DefaultString;
                    }
                    return;
                }
                if (string.IsNullOrWhiteSpace(TranslationString))
                {
                    if (string.IsNullOrWhiteSpace(DefaultString))
                    {
                        return;
                    }
                    Column.Header = DefaultString;
                }
                if (string.IsNullOrWhiteSpace(DefaultString))
                {
                    Column.Header = LM.GetString(TranslationString);
                }
                else
                {
                    Column.Header = LM.GetStringDefault(TranslationString, DefaultString);
                }

                // Maybe later.
                //FireStringChanged(this, e);
            }
        }

        public void AddPlaylist(Playlist Playlist, List<Song> Songs, List<FormatChange> FormatChanges)
        {
            var playlistNode = new PlaylistNode(Playlist)
            {
                Name = Playlist.Path
            };
            // Put the changed formats up top.
            foreach (var formatChange in FormatChanges)
            {
                var songNode = new SongNode(formatChange.Song)
                {
                    DestinationFilename = Path.ChangeExtension(formatChange.Song.Path, formatChange.NewExtension)
                };
                playlistNode.Nodes.Add(songNode);
            }
            // Followed by the regular missing songs.
            foreach (var song in Songs)
            {
                var songNode = new SongNode(song);
                playlistNode.Nodes.Add(songNode);
            }

            model.Nodes.Add(playlistNode);
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<TreeNodeAdv> GetSelection()
        {
            return treeView.SelectedNodes;
        }

        public void Clear()
        {
            model.Nodes.Clear();
        }

        public static void RemoveItem(PlaylistTreeNodeBase node, int index)
        {
            node.RemoveItem(index);
        }

        public void RemovePlaylist(PlaylistNode Node)
        {
            model.Nodes.RemoveAt(Node.Index);
        }

        /*
        public void RemoveItem(int index)
        {
            RemoveItem((treeView.Root.Tag as PlaylistTreeNodeBase), index);
        }
        */

        public List<PlaylistNode> GetPlaylistNodes()
        {
            return model.Nodes.Cast<PlaylistNode>().ToList();
        }

        private void PlaylistTreeControl_SizeChanged(object sender, EventArgs e)
        {
            colName.Width = (int)(Width * 0.3f);
            colSource.Width = (int)(Width * 0.35f);
            colDestination.Width = (int)(Width * 0.35f);
        }

        /// <summary>
        /// Event fired when the user double clicks on the destination column.
        /// </summary>
        public event EventHandler<DestinationClickedEventArgs> DestinationClicked;
        protected void FireDestinationClicked(object sender, DestinationClickedEventArgs e)
        {
            DestinationClicked?.Invoke(sender, e);
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeAdvMouseEventArgs e)
        {
            if (e.Control == ncbCheck)
            {
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
                        {
                            node.IsChecked = true;
                        }
                    }
                }
                else if (e.Node.Tag.GetType() == typeof(PlaylistNode))
                {
                    var node = e.Node.Tag as PlaylistNode;
                    if (node.IsChecked)
                    {
                        node.IsChecked = false;
                        foreach (var child in node.Nodes)
                        {
                            child.IsChecked = false;
                        }
                    }
                    else
                    {
                        var allExist = true;
                        foreach (var child in node.Nodes)
                        {
                            if (!File.Exists((child as SongNode).DestinationFilename))
                            {
                                allExist = false;
                                break;
                            }
                        }

                        if (allExist)
                        {
                            node.IsChecked = true;
                            foreach (var child in node.Nodes)
                            {
                                child.IsChecked = true;
                            }
                        }
                    }
                }
            }
        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeAdvMouseEventArgs e)
        {
            if (e.Control == ntbDestination)
            {
                FireDestinationClicked(this, new DestinationClickedEventArgs(e.Node.Tag as SongNode));
            }
        }
    }
}
