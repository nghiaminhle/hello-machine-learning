using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    public interface ICondition
    {
        /// <summary>
        /// Attribute
        /// </summary>
        AttributeCase Attr
        {
            get;
            set;
        }
        /// <summary>
        /// Value
        /// </summary>
        string Value
        {
            get;
            set;
        }
        /// <summary>
        /// State Compare
        /// </summary>
        StateCompare Compare
        {
            get;
            set;
        }
    }
}
