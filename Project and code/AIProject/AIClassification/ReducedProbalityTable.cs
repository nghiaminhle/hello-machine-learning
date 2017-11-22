using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    [Serializable]
    public class ReducedProbalityTable
    {  
        #region Constructor
        /*
         * It's also the way to build classifiers
         */
        public ReducedProbalityTable(Dictionary<AttributeCase, Dictionary<string, StatictisObject>> aProStore, Dictionary<AttributeCase,double[]> aThresholds)
        {
            this.ProbStore = aProStore;
            this.Thresholds = aThresholds;    
        }
        
        #endregion

        #region public methods

        public void AddProbality(AttributeCase attr, string Value, StatictisObject probality)
        {
            ProbStore[attr][Value]=probality;
        }
        /// <summary>
        /// Get probality of a particular attribute value.
        /// Throw exception if it don't exist
        /// </summary>
        /// <param name="attr"></param>
        /// <param name="Value">value ung voi moi attribute</param>
        /// <returns>cặp xác suất của mỗi thuộc tính ứng với hai trường hợp lớn hơn và nho hon</returns>
        public StatictisObject GetProbality(AttributeCase attr, double Value)
        {
            double[] temp;
            temp = GetThreshold(attr);
            for (int i=0; i<temp.Length-1; i++) //notice
            {
                if (Value>=temp[i] && Value<temp[i+1])
                {
                    return ProbStore[attr][temp[i].ToString()];
                }
            }
            return ProbStore[attr][temp[temp.Length-2].ToString()];
        }

        public StatictisObject GetProbality(AttributeCase attr, string Value)
        {
            return ProbStore[attr][Value];
        }

        #endregion

        #region Get Threshold

        public double[] GetThreshold(AttributeCase att)
        {
            return Thresholds[att]; 
        }
        #endregion

        #region properties
        private Dictionary<AttributeCase,double[]> Thresholds = null;
        private Dictionary<AttributeCase, Dictionary<string, StatictisObject>> ProbStore = new Dictionary<AttributeCase, Dictionary<string, StatictisObject>>();
        #endregion
    }
}