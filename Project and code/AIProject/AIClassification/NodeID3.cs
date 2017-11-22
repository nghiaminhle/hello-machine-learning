using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    [Serializable]
    public class NodeID3
    {
        public static int NUMOFNODE = 0;
        public static int NUMOFUNKNOWN = 0;
        public static int NUMOFTRUE = 0;
        public static int NUMOFFALSE = 0;
        
        #region Private Fields
        
        private AttributeDetail _attribute;
        private List<NodeID3> _childNodes;
        private object _value; // for a discreted value: store a value, for a continous value: store the ID of the threshold
        private bool isLeaf = false;
        private double numberCaseSatisfy;
        private double numberError;
        private double pos;
        private int result = -1;
        private NodeID3 parent;
        private double positiveration;
        #endregion

        #region Public methods
        public void SetLeaf(int value, double NumberCaseSatisfy, double NumberError)
        {
            if (value == -1)
            {
                NodeID3.NUMOFUNKNOWN++;
            }
            else if (value == 0)
            {
                NodeID3.NUMOFFALSE++;
            }
            else if (value == 1)
            {
                NodeID3.NUMOFTRUE++;
            }
            this.result = value;
            this.isLeaf = true;
            this.numberCaseSatisfy = NumberCaseSatisfy;
            this.numberError = NumberError;
        }
        #endregion

        #region Get & Set
        public int Result
        {
            get { return this.result; }
        }

        public bool IsLeaf
        {
            get { return this.isLeaf; }
            set { this.isLeaf = value; }
        }

        public double Pos
        {
            get { return this.pos; }
            set { this.pos = value; }
        }

        public double NumGreater
        {
            get 
            {
                double rv;
                if (this.isLeaf)
                {
                    rv = this.numberCaseSatisfy;
                }
                else
                {
                    rv = this.numberError;
                }
                return rv;
            }
        }

        public double NumLess
        {
            get
            {
                double rv;
                if (this.isLeaf)
                {
                    rv = this.numberError;
                }
                else
                {
                    rv = this.numberCaseSatisfy;
                }
                return rv;
            }
        }

        public AttributeDetail Attribute
        {
            get { return this._attribute; }
            set { this._attribute = value; }
        }

        public List<NodeID3> ChildNodes
        {
            get { return this._childNodes; }
        }

        public NodeID3 Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }

        public double PositiveRatio
        {
            get { return this.positiveration; }
            set { this.positiveration = value; }
        }
        /// <summary>
        /// for a discreted value: store a value, for a continous value: store the ID of the threshold
        /// </summary>
        public object Value
        {
            get { return this._value; }
            set { this._value = value; }
        }

        #endregion

        #region Constructor
        public NodeID3()
        {
            this._childNodes = new List<NodeID3>();
            this.isLeaf = false;
            NodeID3.NUMOFNODE++;
        }

        #endregion
    }
}