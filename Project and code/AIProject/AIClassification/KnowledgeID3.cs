using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace AIClassification
{
    [Serializable]
    public class KnowledgeID3 : Knowledge
    {
        #region Private Fields

        // Luu tru nhan pho bien nhat ung voi moi attribute. 
        private double _numberValidateCase;
        private NodeID3 _id3Root;

        #endregion

        #region Constructors

        public KnowledgeID3(NodeID3 root, Dictionary<AttributeCase, object> mostCommonLabelGreatr, Dictionary<AttributeCase, object> mostCommonLabelLessEqua, double numberTrainingCase, double numberValidateCase)
            : base(mostCommonLabelGreatr, mostCommonLabelLessEqua, numberTrainingCase)
        {
            this._id3Root = root;
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

        private DiscreteIndex _indexOfChild = new DiscreteIndex();
        public override bool Classify(Case ca)
        {
            return true;
        }

        #endregion

        #region Private Method

        #endregion

        #region Properties
        /// <summary>
        /// Node goc cua cay quyet dinh
        /// </summary>
        public NodeID3 id3Root
        {
            get { return this._id3Root; }
            set { this._id3Root = value; }
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
