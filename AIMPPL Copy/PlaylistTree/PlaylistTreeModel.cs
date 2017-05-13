using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Aga.Controls.Tree;

namespace AIMPPL_Copy.PlaylistTree
{
    public class PlaylistTreeModel : ITreeModel
    {
        public PlaylistTreeModel()
        {
            Root = new PlaylistTreeNodeBase
            {
                Model = this
            };
        }

        public PlaylistTreeNodeBase Root { get; }

        public Collection<PlaylistTreeNodeBase> Nodes => Root.Nodes;

        public TreePath GetPath(PlaylistTreeNodeBase node)
        {
            if (node == Root)
                return TreePath.Empty;
            var stack = new Stack<object>();
            while (node != Root)
            {
                stack.Push(node);
                node = node.Parent;
            }
            return new TreePath(stack.ToArray());
        }

        public PlaylistTreeNodeBase FindNode(TreePath path)
        {
            if (path.IsEmpty())
                return Root;
            return FindNode(Root, path, 0);
        }

        private static PlaylistTreeNodeBase FindNode(PlaylistTreeNodeBase root, TreePath path, int level)
        {
            foreach (var node in root.Nodes)
                if (node == path.FullPath[level])
                    if (level == path.FullPath.Length - 1)
                        return node;
                    else
                        return FindNode(node, path, level + 1);
            return null;
        }

        #region ITreeModel Members

        public IEnumerable GetChildren(TreePath treePath)
        {
            var node = FindNode(treePath);
            if (node != null)
                foreach (var n in node.Nodes)
                    yield return n;
            else
                yield break;
        }

        public bool IsLeaf(TreePath treePath)
        {
            var node = FindNode(treePath);
            if (node != null)
                return node.IsLeaf;
            throw new ArgumentException(nameof(treePath));
        }

        public event EventHandler<TreeModelEventArgs> NodesChanged;

        internal void OnNodesChanged(TreeModelEventArgs args)
        {
            NodesChanged?.Invoke(this, args);
        }

        public event EventHandler<TreePathEventArgs> StructureChanged;

        public void OnStructureChanged(TreePathEventArgs args)
        {
            StructureChanged?.Invoke(this, args);
        }

        public event EventHandler<TreeModelEventArgs> NodesInserted;

        internal void OnNodeInserted(PlaylistTreeNodeBase parent, int index, PlaylistTreeNodeBase node)
        {
            if (NodesInserted != null)
            {
                var args = new TreeModelEventArgs(GetPath(parent), new[] {index}, new object[] {node});
                NodesInserted(this, args);
            }
        }

        public event EventHandler<TreeModelEventArgs> NodesRemoved;

        internal void OnNodeRemoved(PlaylistTreeNodeBase parent, int index, PlaylistTreeNodeBase node)
        {
            if (NodesRemoved != null)
            {
                var args = new TreeModelEventArgs(GetPath(parent), new[] {index}, new object[] {node});
                NodesRemoved(this, args);
            }
        }

        #endregion
    }
}
