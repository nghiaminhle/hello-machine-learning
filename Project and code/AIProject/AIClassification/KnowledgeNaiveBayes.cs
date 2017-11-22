using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace AIClassification
{
    [Serializable]
    public class KnowledgeNaiveBayes : Knowledge
    {
        #region Private Fields

        // Luu tru nhan pho bien nhat ung voi moi attribute. 
        private NaiveBayes _nbObject;

        #endregion

        public KnowledgeNaiveBayes(NaiveBayes nbObject, Dictionary<AttributeCase, object> mostCommonLabelGreatr, Dictionary<AttributeCase, object> mostCommonLabelLessEqua, double numberTrainingCase)
            : base(mostCommonLabelGreatr, mostCommonLabelLessEqua, numberTrainingCase)
        {
            this._nbObject = nbObject;
        }

        #region Public Method

        #region Classify
        public override bool Classify(Case ca)
        {
            return this._nbObject.Classify(ca);
        }
        #endregion

        #region TestAccuracy
        public override double TestAccuracy(params Case[] testCases)
        {
            double numberTestCase = testCases.Length;
            double correctCase = 0;
            double accurate = 0;
            this.CleanData(testCases);

            foreach (Case ca in testCases)
            {
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

        #region Clean Data

        #endregion
        #endregion

        #region Property

        public NaiveBayes NbObject
        {
            get { return _nbObject; }
            set { _nbObject = value; }
        }
        #endregion
    }
}
