using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace AIClassification
{
    [Serializable]
    public class KnowledgeC45 : Knowledge
    {
        #region Private Fields

        // Luu tru nhan pho bien nhat ung voi moi attribute. 
        private double _numberValidateCase;
        private Node _c45Root;

        #endregion

        #region Constructors

        public KnowledgeC45(Node root, Dictionary<AttributeCase, object> mostCommonLabelGreatr, Dictionary<AttributeCase, object> mostCommonLabelLessEqua, double numberTrainingCase, double numberValidateCase)
            : base(mostCommonLabelGreatr, mostCommonLabelLessEqua, numberTrainingCase)
        {
            this._c45Root = root;
            this._numberValidateCase = numberValidateCase;
        }
        #endregion

        #region Public Methods

        #region Test Data
        public override double TestAccuracy(params Case[] testCases)
        {
            double numberTestCase = testCases.Length;
            double count = 0;
            double correctCase = 0;
            double accurate = 0;
            this.CleanData(testCases);

            foreach (Case ca in testCases)
            {
                count++;
                if (ca.IsGreater == this.Classify(ca))
                {
                    correctCase++;
                }
            }
            accurate = correctCase / numberTestCase;
            return accurate * 100;
        }

        public double TestAccuracy(BackgroundWorker worker, params Case[] testCases)
        {
            double numberTestCase = testCases.Length;
            double count = 0;
            double correctCase = 0;
            double accurate = 0;
            this.CleanData(testCases);

            foreach (Case ca in testCases)
            {
                count++;
                if (ca.IsGreater == this.Classify(ca))
                {
                    correctCase++;
                }
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress((int)(count * 100 / numberTestCase));
                }
            }
            accurate = correctCase / numberTestCase;
            return accurate * 100;
        }
        #endregion

        #region Private Method
        private DiscreteIndex _indexOfChild = new DiscreteIndex();
        public override bool Classify(Case ca)
        {
            Node node = this._c45Root;
            Node parent = null;
            while (!node.IsLeaf)
            {
                parent = node;
                switch (node.Label)
                {
                    #region Age
                    case AttributeCase.Age:
                        {
                            if (ca.Age >= 0)
                            {
                                if (ca.Age <= node.Threshold)
                                {
                                    node = node.ListChild[0];
                                }
                                else
                                    node = node.ListChild[1];
                            }
                        }
                        break;
                    #endregion

                    #region Capital Gain
                    case AttributeCase.CapitalGain:
                        {
                            if (ca.CapitalGain >= 0)
                            {
                                if (ca.CapitalGain <= node.Threshold)
                                {
                                    node = node.ListChild[0];
                                }
                                else
                                    node = node.ListChild[1];
                            }
                        }
                        break;
                    #endregion

                    #region Capital Loss
                    case AttributeCase.CapitalLoss:
                        {
                            if (ca.CapitalLoss >= 0)
                            {
                                if (ca.CapitalLoss <= node.Threshold)
                                {
                                    node = node.ListChild[0];
                                }
                                else
                                    node = node.ListChild[1];
                            }
                        }
                        break;
                    #endregion

                    #region Education
                    case AttributeCase.Education:
                        {
                            if (ca.Edu != Education.unknow)
                            {
                                int index = this._indexOfChild.IndexValues(AttributeCase.Education, ca.Edu);
                                node = node.ListChild[index];
                            }
                        }
                        break;
                    #endregion

                    #region Education Num
                    case AttributeCase.EducationNum:
                        {
                            if (ca.EduNum >= 0)
                            {
                                if (ca.EduNum <= node.Threshold)
                                {
                                    node = node.ListChild[0];
                                }
                                else
                                    node = node.ListChild[1];
                            }
                        }
                        break;
                    #endregion

                    #region Fnlwgt
                    case AttributeCase.Fnlwgt:
                        {
                            if (ca.Fnlwgt >= 0)
                            {
                                if (ca.Fnlwgt <= node.Threshold)
                                {
                                    node = node.ListChild[0];
                                }
                                else
                                    node = node.ListChild[1];
                            }
                        }
                        break;
                    #endregion

                    #region Hours Per Week
                    case AttributeCase.HoursPerWeek:
                        {
                            if (ca.HoursPerWeek >= 0)
                            {
                                if (ca.HoursPerWeek <= node.Threshold)
                                {
                                    node = node.ListChild[0];
                                }
                                else
                                    node = node.ListChild[1];
                            }
                        }
                        break;
                    #endregion

                    #region Marital Status
                    case AttributeCase.MaritalStatus:
                        {
                            if (ca.MariSt != MaritalStatus.unknow)
                            {
                                int index = this._indexOfChild.IndexValues(AttributeCase.MaritalStatus, ca.MariSt);
                                node = node.ListChild[index];
                            }
                        }
                        break;
                    #endregion

                    #region Native Country
                    case AttributeCase.NativeCountry:
                        {
                            if (ca.NtvCountry != NativeCountry.unknow)
                            {
                                int index = this._indexOfChild.IndexValues(AttributeCase.NativeCountry, ca.NtvCountry);
                                node = node.ListChild[index];
                            }
                        }
                        break;
                    #endregion

                    #region Occupation
                    case AttributeCase.Occupation:
                        {
                            if (ca.Occ != Occupation.unknow)
                            {
                                int index = this._indexOfChild.IndexValues(AttributeCase.Occupation, ca.Occ);
                                node = node.ListChild[index];
                            }
                        }
                        break;
                    #endregion

                    #region Race
                    case AttributeCase.Race:
                        {
                            if (ca.Ra != Race.unknow)
                            {
                                int index = this._indexOfChild.IndexValues(AttributeCase.Race, ca.Ra);
                                node = node.ListChild[index];
                            }
                        }
                        break;
                    #endregion

                    #region Relationship
                    case AttributeCase.Relationship:
                        {
                            if (ca.Reltsh != Relationship.unknow)
                            {
                                int index = this._indexOfChild.IndexValues(AttributeCase.Relationship, ca.Reltsh);
                                node = node.ListChild[index];
                            }
                        }
                        break;
                    #endregion

                    #region Sex
                    case AttributeCase.Sex:
                        {
                            if (ca.SexCase != Sex.unknow)
                            {
                                int index = this._indexOfChild.IndexValues(AttributeCase.Sex, ca.SexCase);
                                node = node.ListChild[index];
                            }
                        }
                        break;
                    #endregion

                    #region Work Class
                    case AttributeCase.WorkClass:
                        {
                            if (ca.Wcl != WorkClass.unknow)
                            {
                                int index = this._indexOfChild.IndexValues(AttributeCase.WorkClass, ca.Wcl);
                                node = node.ListChild[index];
                            }
                        }
                        break;
                    #endregion
                    default:
                        break;
                }
                if (node == null)
                {
                    if (parent.GreaterBranchNum > parent.LessEqualBranchNum)
                        return true;
                    else
                        return false;
                }
            }
            return node.Result;
        }
        #endregion

        #endregion

        #region Properties
        /// <summary>
        /// Node goc cua cay quyet dinh
        /// </summary>
        public Node C45Root
        {
            get { return this._c45Root; }
            set { this._c45Root = value; }
        }
        /// <summary>
        /// Số lượng trường hợ Validate Case
        /// </summary>
        public double NumberValidateCase
        {
            get { return _numberValidateCase; }
            set { _numberValidateCase = value; }
        }
        #endregion
    }
}