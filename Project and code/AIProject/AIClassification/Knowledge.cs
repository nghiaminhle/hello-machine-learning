using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    [Serializable]
    public class Knowledge
    {
        #region Private Fields

        // Luu tru nhan pho bien nhat ung voi moi attribute. 
        private Dictionary<AttributeCase, object> _mostCommonLabelGreatr;
        private Dictionary<AttributeCase, object> _mostCommonLabelLessEqua;
        private double _numberTrainingCase;

        #endregion

        public Knowledge(Dictionary<AttributeCase, object> mostCommonLabelGreatr, Dictionary<AttributeCase, object> mostCommonLabelLessEqua, double numberTrainingCase)
        {
            this._mostCommonLabelGreatr = mostCommonLabelGreatr;
            this._mostCommonLabelLessEqua = mostCommonLabelLessEqua;
            this._numberTrainingCase = numberTrainingCase;
        }

        #region Public Methods
        #region Classify
        /// <summary>
        /// Phương thức phân loại
        /// </summary>
        /// <param name="ca">Trường hợp cần phân loại</param>
        /// <returns>phân loại</returns>
        public virtual bool Classify(Case ca)
        {
            return true;
        } 
        #endregion

        #region Test Accuracy
        /// <summary>
        /// Đáng giá độ chính xác của một bộ dữ liệu
        /// </summary>
        /// <param name="ca">mang cac test case</param>
        /// <returns></returns>
        public virtual double TestAccuracy(params Case[] testCases)
        {
            return 1;
        } 
        #endregion

        #region Clean Data
        /// <summary>
        /// Xử lý các trường hợp bị missing value
        /// </summary>
        /// <param name="cases">Mảng dữ liệu cần clean</param>
        public virtual void CleanData(params Case[] cases)
        {
            // Danh nhan cho cac truogn hop unknow ung voi moi attribute.
            foreach (Case ca in cases)
            {
                if (ca.IsMissingValue)
                {
                    if (ca.Wcl == WorkClass.unknow)
                    {
                        if (ca.IsGreater)
                        {
                            ca.Wcl = (WorkClass)this._mostCommonLabelGreatr[AttributeCase.WorkClass];
                        }
                        else
                        {
                            ca.Wcl = (WorkClass)this._mostCommonLabelLessEqua[AttributeCase.WorkClass];
                        }
                    }
                    if (ca.Edu == Education.unknow)
                    {
                        if (ca.IsGreater)
                        {
                            ca.Edu = (Education)this._mostCommonLabelGreatr[AttributeCase.Education];
                        }
                        else
                        {
                            ca.Edu = (Education)this._mostCommonLabelLessEqua[AttributeCase.Education];
                        }
                    }
                    if (ca.MariSt == MaritalStatus.unknow)
                    {
                        if (ca.IsGreater)
                        {
                            ca.MariSt = (MaritalStatus)this._mostCommonLabelGreatr[AttributeCase.MaritalStatus];
                        }
                        else
                        {
                            ca.MariSt = (MaritalStatus)this._mostCommonLabelLessEqua[AttributeCase.MaritalStatus];
                        }
                    }
                    if (ca.Occ == Occupation.unknow)
                    {
                        if (ca.IsGreater)
                        {
                            ca.Occ = (Occupation)this._mostCommonLabelGreatr[AttributeCase.Occupation];
                        }
                        else
                        {
                            ca.Occ = (Occupation)this._mostCommonLabelLessEqua[AttributeCase.Occupation];
                        }
                    }
                    if (ca.Reltsh == Relationship.unknow)
                    {
                        if (ca.IsGreater)
                        {
                            ca.Reltsh = (Relationship)this._mostCommonLabelGreatr[AttributeCase.Relationship];
                        }
                        else
                        {
                            ca.Reltsh = (Relationship)this._mostCommonLabelLessEqua[AttributeCase.Relationship];
                        }
                    }
                    if (ca.Ra == Race.unknow)
                    {
                        if (ca.IsGreater)
                        {
                            ca.Ra = (Race)this._mostCommonLabelGreatr[AttributeCase.Race];
                        }
                        else
                        {
                            ca.Ra = (Race)this._mostCommonLabelLessEqua[AttributeCase.Race];
                        }
                    }
                    if (ca.SexCase == Sex.unknow)
                    {
                        if (ca.IsGreater)
                        {
                            ca.SexCase = (Sex)this._mostCommonLabelGreatr[AttributeCase.Sex];
                        }
                        else
                        {
                            ca.SexCase = (Sex)this._mostCommonLabelLessEqua[AttributeCase.Sex];
                        }
                    }
                    if (ca.NtvCountry == NativeCountry.unknow)
                    {
                        if (ca.IsGreater)
                        {
                            ca.NtvCountry = (NativeCountry)this._mostCommonLabelGreatr[AttributeCase.NativeCountry];
                        }
                        else
                        {
                            ca.NtvCountry = (NativeCountry)this._mostCommonLabelLessEqua[AttributeCase.NativeCountry];
                        }
                    }
                    ca.IsMissingValue = false;
                }
            }
        } 
        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Trả lại nhãn phổ biến nhất trong trưởng hợp greater ứng với một attribute nào đó.
        /// </summary>
        public Dictionary<AttributeCase, object> MostCommonLabelGreatr
        {
            get { return _mostCommonLabelGreatr; }
            set { _mostCommonLabelGreatr = value; }
        }
        /// <summary>
        /// Trả lại nhãn phổ biến nhất trong trường hợp less equal ứng với một attribute nào đó
        /// </summary>
        public Dictionary<AttributeCase, object> MostCommonLabelLessEqua
        {
            get { return _mostCommonLabelLessEqua; }
            set { _mostCommonLabelLessEqua = value; }
        }
        /// <summary>
        /// Số lượng training case
        /// </summary>
        public double NumberTrainingCase
        {
            get { return _numberTrainingCase; }
            set { _numberTrainingCase = value; }
        }
        #endregion
    }
}
