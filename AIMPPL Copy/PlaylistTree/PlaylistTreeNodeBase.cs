using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using Aga.Controls.Tree;

namespace AIMPPL_Copy.PlaylistTree
{
    public class PlaylistTreeNodeBase
    {
        public PlaylistTreeNodeBase() : this(string.Empty)
        {
        }

        public PlaylistTreeNodeBase(string text)
        {
            _text = text;
            _nodes = new NodeCollection(this);
        }

        private PlaylistTreeModel FindModel()
        {
            var node = this;
            while (node != null)
            {
                if (node.Model != null)
                    return node.Model;
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
                    var args = new TreeModelEventArgs(path, new[] {Index}, new object[] {this});
                    model.OnNodesChanged(args);
                }
            }
        }

        public void RemoveItem(int index)
        {
            Nodes.RemoveAt(index);
        }

        public override string ToString()
        {
            return Text;
        }

        #region NodeCollection

        protected class NodeCollection : Collection<PlaylistTreeNodeBase>
        {
            private readonly PlaylistTreeNodeBase _owner;

            public NodeCollection(PlaylistTreeNodeBase owner)
            {
                _owner = owner;
            }

            protected override void ClearItems()
            {
                while (Count != 0)
                    RemoveAt(Count - 1);
            }

            protected override void InsertItem(int index, PlaylistTreeNodeBase item)
            {
                if (item == null)
                    throw new ArgumentNullException(nameof(item));

                if (item.Parent != _owner)
                {
                    item.Parent?.Nodes.Remove(item);
                    item._parent = _owner;
                    base.InsertItem(index, item);

                    var model = _owner.FindModel();
                    model?.OnNodeInserted(_owner, index, item);
                }
            }

            protected override void RemoveItem(int index)
            {
                var item = this[index];
                item._parent = null;
                base.RemoveItem(index);

                var model = _owner.FindModel();
                model?.OnNodeRemoved(_owner, index, item);
            }

            protected override void SetItem(int index, PlaylistTreeNodeBase item)
            {
                if (item == null)
                    throw new ArgumentNullException(nameof(item));

                RemoveAt(index);
                InsertItem(index, item);
            }
        }

        #endregion

        #region Properties

        internal PlaylistTreeModel Model { get; set; }

        private readonly NodeCollection _nodes;

        public Collection<PlaylistTreeNodeBase> Nodes => _nodes;

        private PlaylistTreeNodeBase _parent;

        public PlaylistTreeNodeBase Parent
        {
            get { return _parent; }
            set
            {
                if (value != _parent)
                {
                    _parent?.Nodes.Remove(this);

                    value?.Nodes.Add(this);
                }
            }
        }

        public string Name { get; set; } = "";

        public int Index
        {
            get
            {
                if (_parent != null)
                    return _parent.Nodes.IndexOf(this);
                return -1;
            }
        }

        public PlaylistTreeNodeBase PreviousNode
        {
            get
            {
                var index = Index;
                if (index > 0)
                    return _parent.Nodes[index - 1];
                return null;
            }
        }

        public PlaylistTreeNodeBase NextNode
        {
            get
            {
                var index = Index;
                if (index >= 0 && index < _parent.Nodes.Count - 1)
                    return _parent.Nodes[index + 1];
                return null;
            }
        }

        private string _text;

        public virtual string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    NotifyModel();
                }
            }
        }

        protected CheckState checkState;

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
            get { return CheckState != CheckState.Unchecked; }
            set
            {
                if (value)
                    CheckState = CheckState.Checked;
                else
                    CheckState = CheckState.Unchecked;
            }
        }

        public virtual bool IsLeaf => true;

        #endregion
    }
}
