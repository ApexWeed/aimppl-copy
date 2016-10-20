using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aga.Controls.Tree;

namespace AIMPPL_Copy.PlaylistTree
{
    public class PlaylistTreeModel : ITreeModel
    {
        private PlaylistTreeNodeBase root;
        public PlaylistTreeNodeBase Root
        {
            get { return root; }
        }

        public Collection<PlaylistTreeNodeBase> Nodes
        {
            get { return root.Nodes; }
        }

        public PlaylistTreeModel()
        {
            root = new PlaylistTreeNodeBase
            {
                Model = this
            };
        }

        public TreePath GetPath(PlaylistTreeNodeBase Node)
        {
            if (Node == root)
            {
                return TreePath.Empty;
            }
            else
            {
                var stack = new Stack<object>();
                while (Node != root)
                {
                    stack.Push(Node);
                    Node = Node.Parent;
                }
                return new TreePath(stack.ToArray());
            }
        }

        public PlaylistTreeNodeBase FindNode(TreePath Path)
        {
            if (Path.IsEmpty())
            {
                return root;
            }
            else
            {
                return FindNode(root, Path, 0);
            }
        }

        private PlaylistTreeNodeBase FindNode(PlaylistTreeNodeBase Root, TreePath Path, int Level)
        {
            foreach (PlaylistTreeNodeBase node in Root.Nodes)
            {
                if (node == Path.FullPath[Level])
                {
                    if (Level == Path.FullPath.Length - 1)
                    {
                        return node;
                    }
                    else
                    {
                        return FindNode(node, Path, Level + 1);
                    }
                }
            }
            return null;
        }

        #region ITreeModel Members

        public System.Collections.IEnumerable GetChildren(TreePath TreePath)
        {
            var node = FindNode(TreePath);
            if (node != null)
            {
                foreach (PlaylistTreeNodeBase n in node.Nodes)
                {
                    yield return n;
                }
            }
            else
            {
                yield break;
            }
        }

        public bool IsLeaf(TreePath TreePath)
        {
            var node = FindNode(TreePath);
            if (node != null)
            {
                return node.IsLeaf;
            }
            else
            {
                throw new ArgumentException(nameof(TreePath));
            }
        }

        public event EventHandler<TreeModelEventArgs> NodesChanged;
        internal void OnNodesChanged(TreeModelEventArgs Args)
        {
            NodesChanged?.Invoke(this, Args);
        }

        public event EventHandler<TreePathEventArgs> StructureChanged;
        public void OnStructureChanged(TreePathEventArgs Args)
        {
            StructureChanged?.Invoke(this, Args);
        }

        public event EventHandler<TreeModelEventArgs> NodesInserted;
        internal void OnNodeInserted(PlaylistTreeNodeBase Parent, int Index, PlaylistTreeNodeBase Node)
        {
            if (NodesInserted != null)
            {
                var args = new TreeModelEventArgs(GetPath(Parent), new int[] { Index }, new object[] { Node });
                NodesInserted(this, args);
            }

        }

        public event EventHandler<TreeModelEventArgs> NodesRemoved;
        internal void OnNodeRemoved(PlaylistTreeNodeBase Parent, int Index, PlaylistTreeNodeBase Node)
        {
            if (NodesRemoved != null)
            {
                var args = new TreeModelEventArgs(GetPath(Parent), new int[] { Index }, new object[] { Node });
                NodesRemoved(this, args);
            }
        }

        #endregion
    }
}
