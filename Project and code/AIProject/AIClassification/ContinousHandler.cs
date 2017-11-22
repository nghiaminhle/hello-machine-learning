using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    public class ContinousHandler
    {
        public static double NUM_OF_RANGE = 3;
        private Data dt;
        /*
         * Must be ordered
         * */
        public List<double[]> AgeThresholds;
        public List<double[]> FnlwgtThresholds;
        public List<double[]> EducationNumThresholds;
        public List<double[]> CapitalGainThresholds;
        public List<double[]> CapitalLossThresholds;
        public List<double[]> HoursPerWeekThresholds;

        /// <summary>
        /// Should use Singleton model, this object should be created only once !
        /// Do it later on :-), we're aigle-ing :-p
        /// </summary>
        /// <param name="dt"></param>
        public ContinousHandler(Data dt)
        {
            this.dt = dt;
            AgeThresholds = new List<double[]>();
            FnlwgtThresholds = new List<double[]>();
            EducationNumThresholds = new List<double[]>();
            CapitalGainThresholds = new List<double[]>();
            CapitalLossThresholds = new List<double[]>();
            HoursPerWeekThresholds = new List<double[]>();
            MostPopularValues(AttributeCase.Age);
            MostPopularValues(AttributeCase.Fnlwgt);
            MostPopularValues(AttributeCase.EducationNum);
            MostPopularValues(AttributeCase.CapitalGain);
            MostPopularValues(AttributeCase.CapitalLoss);
            MostPopularValues(AttributeCase.HoursPerWeek);
            // Finish initialize thresholds
        }
        /// <summary>
        /// Re-initialize all thresholds
        /// </summary>
        public void Restart()
        {
            AgeThresholds = new List<double[]>();
            FnlwgtThresholds = new List<double[]>();
            EducationNumThresholds = new List<double[]>();
            CapitalGainThresholds = new List<double[]>();
            CapitalLossThresholds = new List<double[]>();
            HoursPerWeekThresholds = new List<double[]>();
            MostPopularValues(AttributeCase.Age);
            MostPopularValues(AttributeCase.Fnlwgt);
            MostPopularValues(AttributeCase.EducationNum);
            MostPopularValues(AttributeCase.CapitalGain);
            MostPopularValues(AttributeCase.CapitalLoss);
            MostPopularValues(AttributeCase.HoursPerWeek);
        }
        /// <summary>
        /// Return a list of threshold values, still can refact and make it better !
        /// </summary>
        /// <param name="ac"></param>
        /// <returns></returns>
        private void MostPopularValues(AttributeCase ac)
        {
            int i,j,unitInRange;
            double average;
            Dictionary<double, int> values = new Dictionary<double,int>();
            var items = (from entry in values orderby entry.Key ascending select entry);
            double smallestKey;
            double maxKey;
            double firstnum;
            double lastnum;
            double[] thrs;
            switch (ac)
            {
                // not very smart actually; Age, Fnlgwt and the others are the same double
                // we can figure out some way else to do it instead of this copy and paste
                case AttributeCase.Age:
                    values = new Dictionary<double, int>();
                    unitInRange = 0;
                    smallestKey = Double.MaxValue;
                    maxKey = 0;
                    
                    for (i = 0; i <  dt.NumberCase; i++)
                    {
                        if (values.ContainsKey(dt[i].Age))
                        {
                            values[dt[i].Age]++;
                        }
                        else
                        {
                            if (dt[i].Age < smallestKey)
                            {
                                smallestKey = dt[i].Age;
                            }
                            if (dt[i].Age >= maxKey)
                            {
                                maxKey = dt[i].Age;
                            }
                            values[dt[i].Age] = 1;
                        }
                    }
                    average = (double)dt.NumberCase / ContinousHandler.NUM_OF_RANGE;
                    items = (from entry in values orderby entry.Key ascending select entry);
                    thrs = new double[2];
                    firstnum = smallestKey;
                    thrs[0] = firstnum;
                    
                    for (j = 0; j < items.Count(); j++)
                    {
                        unitInRange += items.ElementAt(j).Value;
                        if (unitInRange > average)
                        {
                            lastnum = items.ElementAt(j - 1).Key;
                            thrs[1] = lastnum;
                            AgeThresholds.Add(thrs);
                            thrs = new double[2];
                            unitInRange = 0;
                            firstnum = items.ElementAt(j).Key;
                            thrs[0] = firstnum;
                        }
                    }
                    if (thrs[1] == 0)
                    {
                        thrs[1] = items.ElementAt(items.Count() - 1).Key;
                        AgeThresholds.Add(thrs);
                    }
                    break;
                case AttributeCase.Fnlwgt:
                    values = new Dictionary<double, int>();
                    unitInRange = 0;
                    smallestKey = Double.MaxValue;
                    maxKey = 0;

                    for (i = 0; i < dt.NumberCase; i++)
                    {
                        if (values.ContainsKey(dt[i].Fnlwgt))
                        {
                            values[dt[i].Fnlwgt]++;
                        }
                        else
                        {
                            if (dt[i].Fnlwgt < smallestKey)
                            {
                                smallestKey = dt[i].Fnlwgt;
                            }
                            if (dt[i].Fnlwgt >= maxKey)
                            {
                                maxKey = dt[i].Fnlwgt;
                            }
                            values[dt[i].Fnlwgt] = 1;
                        }
                    }
                    average = (double)dt.NumberCase / ContinousHandler.NUM_OF_RANGE;
                    items = (from entry in values orderby entry.Key ascending select entry);
                    thrs = new double[2];
                    firstnum = smallestKey;
                    thrs[0] = firstnum;
                    for (j = 0; j < items.Count(); j++)
                    {
                        unitInRange += items.ElementAt(j).Value;
                        if (unitInRange > average)
                        {
                            lastnum = items.ElementAt(j - 1).Key;
                            thrs[1] = lastnum;
                            FnlwgtThresholds.Add(thrs);
                            thrs = new double[2];
                            unitInRange = 0;
                            firstnum = items.ElementAt(j).Key;
                            thrs[0] = firstnum;
                        }
                    }
                    if (thrs[1] == 0)
                    {
                        thrs[1] = items.ElementAt(items.Count() - 1).Key;
                        FnlwgtThresholds.Add(thrs);
                    }
                    break;
                case AttributeCase.EducationNum:
                    values = new Dictionary<double, int>();
                    unitInRange = 0;
                    smallestKey = Double.MaxValue;
                    maxKey = 0;

                    for (i = 0; i < dt.NumberCase; i++)
                    {
                        if (values.ContainsKey(dt[i].EduNum))
                        {
                            values[dt[i].EduNum]++;
                        }
                        else
                        {
                            if (dt[i].EduNum < smallestKey)
                            {
                                smallestKey = dt[i].EduNum;
                            }
                            if (dt[i].EduNum >= maxKey)
                            {
                                maxKey = dt[i].EduNum;
                            }
                            values[dt[i].EduNum] = 1;
                        }
                    }
                    average = (double)dt.NumberCase / ContinousHandler.NUM_OF_RANGE;
                    items = (from entry in values orderby entry.Key ascending select entry);
                    thrs = new double[2];
                    firstnum = smallestKey;
                    thrs[0] = firstnum;
                    for (j = 0; j < items.Count(); j++)
                    {
                        unitInRange += items.ElementAt(j).Value;
                        if (unitInRange > average)
                        {
                            lastnum = items.ElementAt(j - 1).Key;
                            thrs[1] = lastnum;
                            EducationNumThresholds.Add(thrs);
                            thrs = new double[2];
                            unitInRange = 0;
                            firstnum = items.ElementAt(j).Key;
                            thrs[0] = firstnum;
                        }
                    }
                    if (thrs[1] == 0)
                    {
                        thrs[1] = items.ElementAt(items.Count() - 1).Key;
                        EducationNumThresholds.Add(thrs);
                    }
                    break;
                case AttributeCase.CapitalGain:
                    values = new Dictionary<double, int>();
                    unitInRange = 0;
                    smallestKey = Double.MaxValue;
                    maxKey = 0;

                    for (i = 0; i < dt.NumberCase; i++)
                    {
                        if (values.ContainsKey(dt[i].CapitalGain))
                        {
                            values[dt[i].CapitalGain]++;
                        }
                        else
                        {
                            if (dt[i].CapitalGain < smallestKey)
                            {
                                smallestKey = dt[i].CapitalGain;
                            }
                            if (dt[i].CapitalGain >= maxKey)
                            {
                                maxKey = dt[i].CapitalGain;
                            }
                            values[dt[i].CapitalGain] = 1;
                        }
                    }
                    average = (double)dt.NumberCase / ContinousHandler.NUM_OF_RANGE;
                    items = (from entry in values orderby entry.Key ascending select entry);
                    thrs = new double[2];
                    firstnum = smallestKey;
                    thrs[0] = firstnum;
                    for (j = 0; j < items.Count(); j++)
                    {
                        unitInRange += items.ElementAt(j).Value;
                        if (unitInRange > average)
                        {
                            if (j == 0)
                            {
                                lastnum = items.ElementAt(0).Key;
                                thrs[1] = lastnum;
                                CapitalGainThresholds.Add(thrs);
                                thrs = new double[2];
                                unitInRange = 0;
                                firstnum = items.ElementAt(1).Key;
                                thrs[0] = firstnum;
                            }
                            else
                            {
                                lastnum = items.ElementAt(j - 1).Key;
                                thrs[1] = lastnum;
                                CapitalGainThresholds.Add(thrs);
                                thrs = new double[2];
                                unitInRange = 0;
                                firstnum = items.ElementAt(j).Key;
                                thrs[0] = firstnum;
                            }
                        }
                    }
                    if (thrs[1] == 0)
                    {
                        thrs[1] = items.ElementAt(items.Count() - 1).Key;
                        CapitalGainThresholds.Add(thrs);
                    }
                    break;
                case AttributeCase.CapitalLoss:
                    values = new Dictionary<double, int>();
                    unitInRange = 0;
                    smallestKey = Double.MaxValue;
                    maxKey = 0;

                    for (i = 0; i < dt.NumberCase; i++)
                    {
                        if (values.ContainsKey(dt[i].CapitalLoss))
                        {
                            values[dt[i].CapitalLoss]++;
                        }
                        else
                        {
                            if (dt[i].CapitalLoss < smallestKey)
                            {
                                smallestKey = dt[i].CapitalLoss;
                            }
                            if (dt[i].CapitalLoss >= maxKey)
                            {
                                maxKey = dt[i].CapitalLoss;
                            }
                            values[dt[i].CapitalLoss] = 1;
                        }
                    }
                    average = (double)dt.NumberCase / ContinousHandler.NUM_OF_RANGE;
                    items = (from entry in values orderby entry.Key ascending select entry);
                    thrs = new double[2];
                    firstnum = smallestKey;
                    thrs[0] = firstnum;
                    for (j = 0; j < items.Count(); j++)
                    {
                        unitInRange += items.ElementAt(j).Value;
                        if (unitInRange > average)
                        {
                            if (j == 0)
                            {
                                lastnum = items.ElementAt(0).Key;
                                thrs[1] = lastnum;
                                CapitalLossThresholds.Add(thrs);
                                thrs = new double[2];
                                unitInRange = 0;
                                firstnum = items.ElementAt(1).Key;
                                thrs[0] = firstnum;
                            }
                            else
                            {
                                lastnum = items.ElementAt(j - 1).Key;
                                thrs[1] = lastnum;
                                CapitalLossThresholds.Add(thrs);
                                thrs = new double[2];
                                unitInRange = 0;
                                firstnum = items.ElementAt(j).Key;
                                thrs[0] = firstnum;
                            }
                        }
                    }
                    if (thrs[1] == 0)
                    {
                        thrs[1] = items.ElementAt(items.Count() - 1).Key;
                        CapitalLossThresholds.Add(thrs);
                    }
                    break;
                case AttributeCase.HoursPerWeek:
                    values = new Dictionary<double, int>();
                    unitInRange = 0;
                    smallestKey = Double.MaxValue;
                    maxKey = 0;

                    for (i = 0; i < dt.NumberCase; i++)
                    {
                        if (values.ContainsKey(dt[i].HoursPerWeek))
                        {
                            values[dt[i].HoursPerWeek]++;
                        }
                        else
                        {
                            if (dt[i].HoursPerWeek < smallestKey)
                            {
                                smallestKey = dt[i].HoursPerWeek;
                            }
                            if (dt[i].HoursPerWeek >= maxKey)
                            {
                                maxKey = dt[i].HoursPerWeek;
                            }
                            values[dt[i].HoursPerWeek] = 1;
                        }
                    }
                    average = (double)dt.NumberCase / ContinousHandler.NUM_OF_RANGE;
                    items = (from entry in values orderby entry.Key ascending select entry);
                    thrs = new double[2];
                    firstnum = smallestKey;
                    thrs[0] = firstnum;
                    for (j = 0; j < items.Count(); j++)
                    {
                        unitInRange += items.ElementAt(j).Value;
                        if (unitInRange > average)
                        {
                            lastnum = items.ElementAt(j - 1).Key;
                            thrs[1] = lastnum;
                            HoursPerWeekThresholds.Add(thrs);
                            thrs = new double[2];
                            unitInRange = 0;
                            firstnum = items.ElementAt(j).Key;
                            thrs[0] = firstnum;
                        }
                    }
                    if (thrs[1] == 0)
                    {
                        thrs[1] = items.ElementAt(items.Count() - 1).Key;
                        HoursPerWeekThresholds.Add(thrs);
                    }
                    break;
                default:
                    return;
            }
        }
    }
}
