using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    [Serializable]
    public class Condition:ICondition
    {
        #region Private Fields
        private AttributeCase attr;
        private string val;
        private StateCompare compare;
        #endregion

        #region Constructors
        public Condition()
        {
 
        }

        public Condition(AttributeCase attr, string value, StateCompare compareState)
        {
            this.attr = attr;
            this.val = value;
            this.compare = compareState;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Attribute
        /// </summary>
        public AttributeCase Attr
        {
            get { return this.attr; }
            set { this.attr = value; }
        }
        /// <summary>
        /// Value
        /// </summary>
        public string Value
        {
            get { return this.val; }
            set { this.val = value; }
        }
        /// <summary>
        /// State Compare
        /// </summary>
        public StateCompare Compare
        {
            get { return this.compare; }
            set { this.compare = value; }
        }
        #endregion
    }
}
