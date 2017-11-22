using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    public interface IProbalityTable
    {
        /// <summary>
        /// Add couple probality to paticular attribute-value
        /// </summary>
        /// <param name="attr">Attribute</param>
        /// <param name="Value">Value</param>
        /// <param name="probality">cặp xác suất ứng với mỗi value trong hai trường hợp lớn hơn và nhỏ hơn 50K$</param>
        void AddProbality(AttributeCase attr, string Value, params double[] probality);
          /// <summary>
        /// Get probality of a particular attribute value.
        /// Throw exception if it don't exist
        /// </summary>
        /// <param name="attr"></param>
        /// <param name="Value">value ung voi moi attribute</param>
        /// <returns>cặp xác suất của mỗi thuộc tính ứng với hai trường hợp lớn hơn và nhơn</returns>
        double[] GetProbality(AttributeCase attr, string Value);
    }
}
