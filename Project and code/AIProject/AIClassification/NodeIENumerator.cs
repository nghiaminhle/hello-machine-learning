using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    public class NodeIENumerator : IEnumerator<Node>
    {
        #region Private fields
        private Queue<Node> queueData = null;
        private Node currentTree;
        private Node currentNode = null;
        #endregion

        #region Constructor
        public NodeIENumerator(Node tree)
        {
            this.currentTree = tree;
        }
        #endregion

        #region Private Methods
        private void Iterator(Queue<Node> enumData, Node subTree)
        {
            if (subTree != null)
            {
                // Duyet qua tat ca cac con
                if (subTree.NumberChilds != 0)
                {
                    for (int i = 0; i < subTree.NumberChilds; i++)
                    {
                        Iterator(enumData, subTree.ListChild[i]);
                    }
                }
                this.queueData.Enqueue(subTree);
            }
        }
        #endregion

        #region IEnumerator<Node>
        Node IEnumerator<Node>.Current
        {
            get
            {
                if (queueData == null)
                    throw new InvalidOperationException("Please Move Next First");
                return this.currentNode;
            }
        }
        #endregion

        #region Dispose Method
        void IDisposable.Dispose()
        {
 
        }
        #endregion

        #region IEnumerator Methods
        object System.Collections.IEnumerator.Current
        {
            get { throw new NotImplementedException(); }
        }

        bool System.Collections.IEnumerator.MoveNext()
        {
            if (this.queueData == null)
            {
                this.queueData = new Queue<Node>();
                this.Iterator(queueData, this.currentTree);
            }
            if (this.queueData.Count != 0)
            {
                this.currentNode = this.queueData.Dequeue();
                return true;
            }
            return false;
        }

        void System.Collections.IEnumerator.Reset()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
