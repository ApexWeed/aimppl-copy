using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aga.Controls.Tree;

namespace AIMPPL_Copy.PlaylistTree
{
    public class PlaylistTreeNodeBase
    {
        #region NodeCollection

        protected class NodeCollection : Collection<PlaylistTreeNodeBase>
        {
            private PlaylistTreeNodeBase owner;

            public NodeCollection(PlaylistTreeNodeBase Owner)
            {
                this.owner = Owner;
            }

            protected override void ClearItems()
            {
                while (this.Count != 0)
                {
                    this.RemoveAt(this.Count - 1);
                }
            }

            protected override void InsertItem(int Index, PlaylistTreeNodeBase Item)
            {
                if (Item == null)
                {
                    throw new ArgumentNullException(nameof(Item));
                }

                if (Item.Parent != owner)
                {
                    if (Item.Parent != null)
                    {
                        Item.Parent.Nodes.Remove(Item);
                    }
                    Item.parent = owner;
                    base.InsertItem(Index, Item);

                    var model = owner.FindModel();
                    if (model != null)
                    {
                        model.OnNodeInserted(owner, Index, Item);
                    }
                }
            }

            protected override void RemoveItem(int Index)
            {
                var item = this[Index];
                item.parent = null;
                base.RemoveItem(Index);

                var model = owner.FindModel();
                if (model != null)
                    model.OnNodeRemoved(owner, Index, item);
            }

            protected override void SetItem(int Index, PlaylistTreeNodeBase Item)
            {
                if (Item == null)
                    throw new ArgumentNullException(nameof(Item));

                RemoveAt(Index);
                InsertItem(Index, Item);
            }
        }

        #endregion

        #region Properties

        private PlaylistTreeModel model;
        internal PlaylistTreeModel Model
        {
            get { return model; }
            set { model = value; }
        }

        private NodeCollection nodes;
        public Collection<PlaylistTreeNodeBase> Nodes
        {
            get { return nodes; }
        }

        private PlaylistTreeNodeBase parent;
        public PlaylistTreeNodeBase Parent
        {
            get { return parent; }
            set
            {
                if (value != parent)
                {
                    if (parent != null)
                        parent.Nodes.Remove(this);

                    if (value != null)
                        value.Nodes.Add(this);
                }
            }
        }

        private string name = "";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Index
        {
            get
            {
                if (parent != null)
                {
                    return parent.Nodes.IndexOf(this);
                }
                else
                {
                    return -1;
                }
            }
        }

        public PlaylistTreeNodeBase PreviousNode
        {
            get
            {
                var index = Index;
                if (index > 0)
                {
                    return parent.Nodes[index - 1];
                }
                else
                {
                    return null;
                }
            }
        }

        public PlaylistTreeNodeBase NextNode
        {
            get
            {
                var index = Index;
                if (index >= 0 && index < parent.Nodes.Count - 1)
                {
                    return parent.Nodes[index + 1];
                }
                else
                {
                    return null;
                }
            }
        }

        private string text;
        public virtual string Text
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    text = value;
                    NotifyModel();
                }
            }
        }

        private CheckState checkState;
        public virtual CheckState CheckState
        {
            get { return checkState; }
            set
            {
                if (checkState != value)
                {
                    checkState = value;
                    NotifyModel();
                }
            }
        }

        public bool IsChecked
        {
            get
            {
                return CheckState != CheckState.Unchecked;
            }
            set
            {
                if (value)
                    CheckState = CheckState.Checked;
                else
                    CheckState = CheckState.Unchecked;
            }
        }

        public virtual bool IsLeaf
        {
            get
            {
                return true;
            }
        }

        #endregion

        public PlaylistTreeNodeBase() : this(string.Empty)
        {
        }

        public PlaylistTreeNodeBase(string Text)
        {
            this.text = Text;
            nodes = new NodeCollection(this);
        }

        private PlaylistTreeModel FindModel()
        {
            var node = this;
            while (node != null)
            {
                if (node.Model != null)
                {
                    return node.Model;
                }
                node = node.Parent;
            }
            return null;
        }

        protected void NotifyModel()
        {
            var model = FindModel();
            if (model != null && Parent != null)
            {
                var path = model.GetPath(Parent);
                if (path != null)
                {
                    var args = new TreeModelEventArgs(path, new int[] { Index }, new object[] { this });
                    model.OnNodesChanged(args);
                }
            }
        }

        public void RemoveItem(int Index)
        {
            Nodes.RemoveAt(Index);
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
