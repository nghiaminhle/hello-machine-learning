using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    public interface IDiscreteIndex
    {
        /// <summary>
        /// Trả lại index của một value bất kỳ vứng với một thuộc tính nào đó
        /// </summary>
        /// <param name="attr">Tên thuộc tính</param>
        /// <param name="discreteValue">Value</param>
        /// <returns>index-thứ tự thuộc tính đó ứng với một atribute nào đó</returns>
        int IndexValues(AttributeCase attr, object discreteValue);
        /// <summary>
        /// Number discrete values of workclass attribute.
        /// </summary>
        int WorkClassValues
        {
            get;
        }
        /// <summary>
        /// Number discrete values of education attribute.
        /// </summary>
        int EducationValues
        {
            get;
        }
        /// <summary>
        /// Number discrete values of marital attribute.
        /// </summary>
        int MaritalStatusValues
        {
            get;
        }
        /// <summary>
        /// Number discrete values of Occupation attribute.
        /// </summary>
        int OccupationValues
        {
            get;
        }
        /// <summary>
        /// Number discrete values of relationship attribute.
        /// </summary>
        int RelationshipValues
        {
            get;
        }
        /// <summary>
        /// Number discrete values of race attribute.
        /// </summary>
        int RaceValues
        {
            get;
        }
        /// <summary>
        /// Number discrete values of sex attribute.
        /// </summary>
        int SexValues
        {
            get;
        }
        /// <summary>
        /// Number discrete values of naitive country attribute.
        /// </summary>
        int NaitiveCountryValues
        {
            get;
        }
    }
}
