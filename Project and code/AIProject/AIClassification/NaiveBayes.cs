using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    [Serializable]
    public class NaiveBayes
    {
        #region Constructor

        public NaiveBayes(ReducedProbalityTable aProTab, double aNumberCase, double aNumberGreaterCase)
        {
            this.ProTab = aProTab;
            this.NumberCase = aNumberCase;
            this.NumberGreaterCase = aNumberGreaterCase;
        }

        #endregion

        #region public function
        /// <summary>
        /// Phân loại một trường hợp
        /// </summary>
        /// <param name="ca">Một trường hợp bất kỳ</param>
        /// <returns>true: lon hon 50k$, false neu nho hon hoac bang 50k$</returns>
        public bool Classify(Case ca)
        {
            double m_test = 10;
            StatictisObject elementProb = null;
            bool ok = true;
            double greaterProb = 1;
            double lessProb = 1;
            double greaterProb1 = 1;
            double lessProb1 = 1;
            foreach (var i in System.Enum.GetValues(typeof(AttributeCase)))
            {
                AttributeCase x = (AttributeCase)i;
                if (AttributeDetail.IsContinuosAttr(x))
                {
                    switch (x)
                    {
                        case AttributeCase.Age:
                            {
                                elementProb = ProTab.GetProbality(x, ca.Age);
                                greaterProb1 = elementProb.NumberGreater;
                                lessProb1 = elementProb.NumberLessEqua;
                                break;
                            }
                        case AttributeCase.CapitalGain:
                            {
                                elementProb = ProTab.GetProbality(x, ca.CapitalGain);
                                greaterProb1 = elementProb.NumberGreater;
                                lessProb1 = elementProb.NumberLessEqua;
                                break;
                            }
                        case AttributeCase.CapitalLoss:
                            {
                                elementProb = ProTab.GetProbality(x, ca.CapitalLoss);
                                greaterProb1 = elementProb.NumberGreater;
                                lessProb1 = elementProb.NumberLessEqua;
                                break;
                            }
                        case AttributeCase.EducationNum:
                            {
                                elementProb = ProTab.GetProbality(x, ca.EduNum);
                                greaterProb1 = elementProb.NumberGreater;
                                lessProb1 = elementProb.NumberLessEqua;
                                break;
                            }
                        case AttributeCase.Fnlwgt:
                            {
                                elementProb = ProTab.GetProbality(x, ca.Fnlwgt);
                                greaterProb1 = elementProb.NumberGreater;
                                lessProb1 = elementProb.NumberLessEqua;
                                break;
                            }
                        case AttributeCase.HoursPerWeek:
                            {
                                elementProb = ProTab.GetProbality(x, ca.HoursPerWeek);
                                greaterProb1 = elementProb.NumberGreater;
                                lessProb1 = elementProb.NumberLessEqua;
                                break;
                            }
                        default: 
                            ok = false;
                            break;
                    }
                    //Truong hop xac suat = 0
                    double sl = ProTab.GetThreshold(AttributeCase.Age).Length;
                    double n;
                    double nc = 0;

                    switch (x)
                    {
                        case AttributeCase.Age:
                            
                            nc = ProTab.GetProbality(AttributeCase.Age, ca.Age).Total;
                            break;
                        case AttributeCase.CapitalGain:
                            nc = ProTab.GetProbality(AttributeCase.CapitalGain, ca.CapitalGain).Total;
                            break;
                        case AttributeCase.CapitalLoss:
                            nc = ProTab.GetProbality(AttributeCase.CapitalLoss, ca.CapitalLoss).Total;
                            break;
                        case AttributeCase.EducationNum:
                            nc = ProTab.GetProbality(AttributeCase.EducationNum, ca.EduNum).Total;
                            break;
                        case AttributeCase.Fnlwgt:
                            nc = ProTab.GetProbality(AttributeCase.Fnlwgt, ca.Fnlwgt).Total;
                            break;
                        case AttributeCase.HoursPerWeek:
                            nc = ProTab.GetProbality(AttributeCase.HoursPerWeek, ca.HoursPerWeek).Total;
                            break;
                    }

                    double p = (double)1 / sl;
                    double m = m_test;

                    if (greaterProb1 == 0)
                    {
                        NaiveBayes.co++;
                        n = NumberGreaterCase;
                        //greaterProb1 = (int)(NumberCase / sl);
                        greaterProb *= (nc + m * p) / (n + m);
                    }
                    if (lessProb1 == 0)
                    {
                        NaiveBayes.co++;
                        n = NumberCase - NumberGreaterCase;
                        lessProb *= (nc + m * p) / (n + m);
                    }
                }
                else
                {
                    switch (x)
                    {
                        case AttributeCase.Education:
                            elementProb = ProTab.GetProbality(x, ca.Edu.ToString());
                            greaterProb1 = elementProb.NumberGreater;
                            lessProb1 = elementProb.NumberLessEqua;
                            break;
                        case AttributeCase.MaritalStatus:
                            elementProb = ProTab.GetProbality(x, ca.MariSt.ToString());
                            greaterProb1 = elementProb.NumberGreater;
                            lessProb1 = elementProb.NumberLessEqua;
                            break;
                        case AttributeCase.NativeCountry:
                            elementProb = ProTab.GetProbality(x, ca.NtvCountry.ToString());
                            greaterProb1 = elementProb.NumberGreater;
                            lessProb1 = elementProb.NumberLessEqua;
                            break;
                        case AttributeCase.Occupation:
                            elementProb = ProTab.GetProbality(x, ca.Occ.ToString());
                            greaterProb1 = elementProb.NumberGreater;
                            lessProb1 = elementProb.NumberLessEqua;
                            break;
                        case AttributeCase.Race:
                            elementProb = ProTab.GetProbality(x, ca.Ra.ToString());
                            greaterProb1 = elementProb.NumberGreater;
                            lessProb1 = elementProb.NumberLessEqua;
                            break;
                        case AttributeCase.Relationship:
                            elementProb = ProTab.GetProbality(x, ca.Reltsh.ToString());
                            greaterProb1 = elementProb.NumberGreater;
                            lessProb1 = elementProb.NumberLessEqua;
                            break;
                        case AttributeCase.Sex:
                            elementProb = ProTab.GetProbality(x, ca.SexCase.ToString());
                            greaterProb1 = elementProb.NumberGreater;
                            lessProb1 = elementProb.NumberLessEqua;
                            break;
                        case AttributeCase.WorkClass:
                            elementProb = ProTab.GetProbality(x, ca.Wcl.ToString());
                            greaterProb1 = elementProb.NumberGreater;
                            lessProb1 = elementProb.NumberLessEqua;
                            break;
                        default:
                            ok = false;
                            break;                       
                    }
                    //Truong hop xac suat = 0
                    double sl = 0;
                    double n;
                    double nc = 0;

                    switch (x)
                    {
                        case AttributeCase.Education:
                            nc = ProTab.GetProbality(AttributeCase.Education, ca.Edu.ToString()).Total;
                            sl = Enum.GetValues(typeof(Education)).Length;
                            break;
                        case AttributeCase.MaritalStatus:
                            nc = ProTab.GetProbality(AttributeCase.MaritalStatus, ca.MariSt.ToString()).Total;
                            sl = Enum.GetValues(typeof(MaritalStatus)).Length;
                            break;
                        case AttributeCase.NativeCountry:
                            nc = ProTab.GetProbality(AttributeCase.NativeCountry, ca.NtvCountry.ToString()).Total;
                            sl = Enum.GetValues(typeof(NativeCountry)).Length;
                            break;
                        case AttributeCase.Occupation:
                            nc = ProTab.GetProbality(AttributeCase.Occupation, ca.Occ.ToString()).Total;
                            sl = Enum.GetValues(typeof(Occupation)).Length;
                            break;
                        case AttributeCase.Race:
                            nc = ProTab.GetProbality(AttributeCase.Race, ca.Ra.ToString()).Total;
                            sl = Enum.GetValues(typeof(Race)).Length;
                            break;
                        case AttributeCase.Relationship:
                            nc = ProTab.GetProbality(AttributeCase.Relationship, ca.Reltsh.ToString()).Total;
                            sl = Enum.GetValues(typeof(Relationship)).Length;
                            break;
                        case AttributeCase.Sex:
                            nc = ProTab.GetProbality(AttributeCase.Sex, ca.SexCase.ToString()).Total;
                            sl = Enum.GetValues(typeof(Sex)).Length;
                            break;
                        case AttributeCase.WorkClass:
                            nc = ProTab.GetProbality(AttributeCase.WorkClass, ca.Wcl.ToString()).Total;
                            sl = Enum.GetValues(typeof(WorkClass)).Length;
                            break;
                    }
                    double p = (double)1 / sl;
                    double m = m_test;
                                        
                    if (greaterProb1 == 0) 
                    {
                        NaiveBayes.co++;
                        n = NumberGreaterCase;
                        //greaterProb1 = (int)(NumberCase / sl);
                        greaterProb *= (nc + m * p) / (n + m);
                    }
                    if (lessProb1 == 0)
                    {
                        NaiveBayes.co++;
                        n = NumberCase - NumberGreaterCase;
                        lessProb *= (nc + m * p) / (n + m);
                    }
                }
                if (ok)
                {
                    if (elementProb != null)
                    {
                        //TODO Process the problem when we have met probability = 0;
                        if (greaterProb1!=0)
                            greaterProb *= (double) greaterProb1/NumberGreaterCase;
                        if (lessProb1!=0)
                            lessProb *= (double) lessProb1/(NumberCase - NumberGreaterCase);
                    }
                    greaterProb1 = 1;
                    lessProb1 = 1;
                    ok = true;
                }
            }
            greaterProb *= NumberGreaterCase;
            lessProb *= ((int)NumberCase - (int)NumberGreaterCase);
            Console.WriteLine(greaterProb);
            Console.WriteLine(lessProb);
            Console.WriteLine(ca.IsGreater);
            if (greaterProb > lessProb)
                return true;
            else return false;
        }

        public bool IsConstructed()
        {
            return true;
        }

        public void initCountCase()
        {
            
        }
        
        #endregion

        #region properties
        private ReducedProbalityTable ProTab;
        private double NumberCase;
        private double NumberGreaterCase;
        public static int co = 0;
        public static int difference = 0;
        #endregion
    }
}