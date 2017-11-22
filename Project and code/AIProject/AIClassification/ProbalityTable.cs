using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    public class ProbalityTable
    {  

        #region Constructor
        /*
         * It's also the way to build classifiers
         */
        public ProbalityTable(Data dat)
        {
            this.data = dat;
        }

        public void ComputeProbabilityTab()
        {
            InitThreshold();

            Dictionary<string, StatictisObject> dict;

            foreach (AttributeCase j in System.Enum.GetValues(typeof(AIClassification.AttributeCase)))
            {
                StatictisObject[] stat;
                string strIndex;
                string value;
                Array values;
                dict = new Dictionary<string, StatictisObject>();
                switch (j)
                {
                    #region Continuous value
                    case AttributeCase.Age:
                    case AttributeCase.CapitalGain:
                    case AttributeCase.CapitalLoss:
                    case AttributeCase.EducationNum:
                    case AttributeCase.HoursPerWeek:
                    case AttributeCase.Fnlwgt:
                        double[] temp = GetThreshold(j);
                        stat = data.NBStatistic(j, temp);
                        for (int i = 0; i <= temp.Length - 1; i++)
                        {
                            dict.Add(temp[i].ToString(), stat[i]);
                        }
                        break;
                    #endregion
                    #region discrete values

                    case AttributeCase.Education:
                        stat = data.NBStatistic(j, null);
                        values = Enum.GetValues(typeof(Education));
                        for (int i = 0; i <= values.Length - 1; i++)
                        {
                            strIndex = i.ToString();
                            value = values.GetValue(Convert.ToInt32(strIndex)).ToString();

                            if (value != Education.unknow.ToString())
                            {
                                dict.Add(value, stat[i]);
                            }
                        }
                        break;
                    case AttributeCase.MaritalStatus:
                        stat = data.NBStatistic(j, null);
                        values = Enum.GetValues(typeof(MaritalStatus));
                        for (int i = 0; i <= values.Length - 1; i++)
                        {
                            strIndex = i.ToString();
                            value = values.GetValue(Convert.ToInt32(strIndex)).ToString();
                            if (value != MaritalStatus.unknow.ToString())
                            {
                                dict.Add(value, stat[i]);
                            }
                        }
                        break;
                    case AttributeCase.NativeCountry:
                        stat = data.NBStatistic(j, null);
                        values = Enum.GetValues(typeof(NativeCountry));
                        for (int i = 0; i <= values.Length - 1; i++)
                        {
                            strIndex = i.ToString();
                            value = values.GetValue(Convert.ToInt32(strIndex)).ToString();
                            if (value != NativeCountry.unknow.ToString())
                            {
                                dict.Add(value, stat[i]);
                            }
                        }
                        break;
                    case AttributeCase.Occupation:
                        stat = data.NBStatistic(j, null);
                        values = Enum.GetValues(typeof(Occupation));
                        for (int i = 0; i <= values.Length - 1; i++)
                        {
                            strIndex = i.ToString();
                            value = values.GetValue(Convert.ToInt32(strIndex)).ToString();
                            if (value != Occupation.unknow.ToString())
                            {
                                dict.Add(value, stat[i]);
                            }
                        }
                        break;
                    case AttributeCase.Race:
                        stat = data.NBStatistic(j, null);
                        values = Enum.GetValues(typeof(Race));
                        for (int i = 0; i <= values.Length - 1; i++)
                        {
                            strIndex = i.ToString();
                            value = values.GetValue(Convert.ToInt32(strIndex)).ToString();
                            if (value != Race.unknow.ToString())
                            {
                                dict.Add(value, stat[i]);
                            }
                        }
                        break;
                    case AttributeCase.Relationship:
                        stat = data.NBStatistic(j, null);
                        values = Enum.GetValues(typeof(Relationship));
                        for (int i = 0; i <= values.Length - 1; i++)
                        {
                            strIndex = i.ToString();
                            value = values.GetValue(Convert.ToInt32(strIndex)).ToString();
                            if (value != Relationship.unknow.ToString())
                            {
                                dict.Add(value, stat[i]);
                            }
                        }
                        break;
                    case AttributeCase.Sex:
                        stat = data.NBStatistic(j, null);
                        values = Enum.GetValues(typeof(Sex));
                        for (int i = 0; i <= values.Length - 1; i++)
                        {
                            strIndex = i.ToString();
                            value = values.GetValue(Convert.ToInt32(strIndex)).ToString();
                            if (value != Sex.unknow.ToString())
                            {
                                dict.Add(value, stat[i]);
                            }
                        }
                        break;
                    case AttributeCase.WorkClass:
                        stat = data.NBStatistic(j, null);
                        values = Enum.GetValues(typeof(WorkClass));
                        for (int i = 0; i <= values.Length - 1; i++)
                        {
                            strIndex = i.ToString();
                            value = values.GetValue(Convert.ToInt32(strIndex)).ToString();
                            if (value != WorkClass.unknow.ToString())
                            {
                                dict.Add(value, stat[i]);
                            }
                        }
                        break;
                    #endregion
                }
                ProbStore.Add((AttributeCase)j, dict);
            }
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


        private void InitThreshold()
        {
            Thresholds = new Dictionary<AttributeCase, double[]>();
            Thresholds.Add(AttributeCase.Age, data.GetNBThreshold(AttributeCase.Age, new AgeComparer()));
            Thresholds.Add(AttributeCase.CapitalGain, data.GetNBThreshold(AttributeCase.CapitalGain, new CapitalGainComparer()));
            Thresholds.Add(AttributeCase.CapitalLoss, data.GetNBThreshold(AttributeCase.CapitalLoss, new CapitalLossComparer()));
            Thresholds.Add(AttributeCase.EducationNum, data.GetNBThreshold(AttributeCase.EducationNum, new EduNumComparer()));
            Thresholds.Add(AttributeCase.Fnlwgt, data.GetNBThreshold(AttributeCase.Fnlwgt, new FnlwgtComparer()));
            Thresholds.Add(AttributeCase.HoursPerWeek, data.GetNBThreshold(AttributeCase.HoursPerWeek, new HoursPWComparer()));
        }

        public double[] GetThreshold(AttributeCase att)
        {
            return Thresholds[att]; 
        }

        public Dictionary<AttributeCase, double[]> getThresholdsArr()
        {
            return Thresholds;
        }

        public Dictionary<AttributeCase, Dictionary<string, StatictisObject>> getProTab()
        {
            return ProbStore;
        }
        #endregion

        #region properties
        private Dictionary<AttributeCase, Dictionary<string, StatictisObject>> ProbStore = new Dictionary<AttributeCase, Dictionary<string, StatictisObject>>();
        private Data data; 
        private Dictionary<AttributeCase,double[]> Thresholds = null;
        #endregion
    }
}