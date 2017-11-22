using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.ComponentModel;

namespace AIClassification
{
    public class Data: IEnumerable<Case>, IData
    {
        #region Private Fields
        private Case[] _listCase;
        private int _numberCase;
        private IFile _file;
        // Cau truc du lieu dung de chua thong tin Info ung voi cac attribute
        private Dictionary<AttributeCase, double> _info;
        // Cau truc du lieu dung de chua so phan tu bi missing ung voi cac attribute
        private Dictionary<AttributeCase, double> _missingNum;
        // Cau truc du lieu dung de chua mang threshold ung voi cac thuoc tinh lien tuc.
        private Dictionary<AttributeCase, double[]> _thresholdArr;
        // Luu tru nhan pho bien nhat ung voi moi attribute. 
        private Dictionary<AttributeCase, object> _mostCommonLabelGreatr;
        private Dictionary<AttributeCase, object> _mostCommonLabelLessEqua;
        #endregion
        
        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Data()
        {
            
        }
        // TODO FIXdfhgdhghghg
        /// <summary>
        /// Constructors with path to the file
        /// </summary>
        /// <param name="path">Path to the file</param>
        public Data(IFile fileProcessing)
        {
            this._file = fileProcessing;
            this._mostCommonLabelGreatr = new Dictionary<AttributeCase, object>();
            this._mostCommonLabelLessEqua = new Dictionary<AttributeCase, object>();
        }

        public Data(params Case[] arrayCase)
        {
            this._listCase = arrayCase;
            this._numberCase = arrayCase.Count();
        }
        #endregion

        #region Private Methods
        
      
        #endregion

        #region Public Methods

        #region Initial Info and Missing Num for each attribute
        /// <summary>
        /// Khởi tạo thông tin về lượng tin trung bình và số phần tử bị missing của mỗi trường hợp.
        /// Xử dụng trong các trường hợp có missing dữ liệu mà không tiền xử lý làm sạch.
        /// </summary>
        public void InitialInfo()
        {
            string[] arrName = Enum.GetNames(typeof(AttributeCase));

            this._info = new Dictionary<AttributeCase, double>();
            this._missingNum = new Dictionary<AttributeCase, double>();
            double greaterNum = 0;
            double lessEquaNum = 0;
            double missingNum = 0;
            double infoTemp = 0;
            foreach (string str in arrName)
            {
                AttributeCase attr = (AttributeCase)Enum.Parse(typeof(AttributeCase), str);

                switch (attr)
                {
                    #region Age Attribute
                    case AttributeCase.Age:
                        {
                            for (int i = 0; i < this._numberCase; i++)
                            {
                                if (this._listCase[i].IsGreater)
                                    greaterNum++;
                                else
                                    lessEquaNum++;
                            }
                            infoTemp = -((greaterNum / this._numberCase) * Math.Log(greaterNum / this._numberCase, 2)
                                + (lessEquaNum / this._numberCase) * Math.Log(lessEquaNum / this._numberCase, 2));
                            this._info.Add(AttributeCase.Age, infoTemp);
                            this._missingNum.Add(AttributeCase.Age, 0);
                            greaterNum = 0;
                            lessEquaNum = 0;
                            missingNum = 0;
                            infoTemp = 0;
                        }
                        break;
                    #endregion

                    #region Work Class
                    case AttributeCase.WorkClass:
                        {
                            for (int i = 0; i < this._numberCase; i++)
                            {
                                if (this._listCase[i].Wcl == WorkClass.unknow)
                                {
                                    missingNum++;
                                }
                                else
                                {
                                    if (this._listCase[i].IsGreater)
                                    {
                                        greaterNum++;
                                    }
                                    else
                                    {
                                        lessEquaNum++;
                                    }
                                }
                            }
                            infoTemp = -((greaterNum / (this._numberCase - missingNum)) * Math.Log((greaterNum / (this._numberCase - missingNum)), 2)
                                        + (lessEquaNum / (this._numberCase - missingNum)) * Math.Log((lessEquaNum / (this._numberCase - missingNum)), 2));
                            this._info.Add(AttributeCase.WorkClass, infoTemp);
                            this._missingNum.Add(AttributeCase.WorkClass, missingNum);
                            greaterNum = 0;
                            lessEquaNum = 0;
                            missingNum = 0;
                            infoTemp = 0;
                        }
                        break;
                    #endregion

                    #region Fnlwgt Attribute
                    case AttributeCase.Fnlwgt:
                        {
                            for (int i = 0; i < this._numberCase; i++)
                            {
                                if (this._listCase[i].IsGreater)
                                    greaterNum++;
                                else
                                    lessEquaNum++;
                            }
                            infoTemp = -((greaterNum / this._numberCase) * Math.Log(greaterNum / this._numberCase, 2)
                                + (lessEquaNum / this._numberCase) * Math.Log(lessEquaNum / this._numberCase, 2));
                            this._info.Add(AttributeCase.Fnlwgt, infoTemp);
                            this._missingNum.Add(AttributeCase.Fnlwgt, 0);
                            greaterNum = 0;
                            lessEquaNum = 0;
                            missingNum = 0;
                            infoTemp = 0;
                        }
                        break;
                    #endregion

                    #region Education
                    case AttributeCase.Education:
                        {
                            for (int i = 0; i < this._numberCase; i++)
                            {
                                if (this._listCase[i].Edu == Education.unknow)
                                {
                                    missingNum++;
                                }
                                else
                                {
                                    if (this._listCase[i].IsGreater)
                                    {
                                        greaterNum++;
                                    }
                                    else
                                    {
                                        lessEquaNum++;
                                    }
                                }
                            }
                            infoTemp = -((greaterNum / (this._numberCase - missingNum)) * Math.Log((greaterNum / (this._numberCase - missingNum)), 2)
                                        + (lessEquaNum / (this._numberCase - missingNum)) * Math.Log((lessEquaNum / (this._numberCase - missingNum)), 2));
                            this._info.Add(AttributeCase.Education, infoTemp);
                            this._missingNum.Add(AttributeCase.Education, missingNum);
                            greaterNum = 0;
                            lessEquaNum = 0;
                            missingNum = 0;
                            infoTemp = 0;
                        }
                        break;
                    #endregion

                    #region Education Num
                    case AttributeCase.EducationNum:
                        {
                            for (int i = 0; i < this._numberCase; i++)
                            {
                                if (this._listCase[i].IsGreater)
                                    greaterNum++;
                                else
                                    lessEquaNum++;
                            }
                            infoTemp = -((greaterNum / this._numberCase) * Math.Log(greaterNum / this._numberCase, 2)
                                + (lessEquaNum / this._numberCase) * Math.Log(lessEquaNum / this._numberCase, 2));
                            this._info.Add(AttributeCase.EducationNum, infoTemp);
                            this._missingNum.Add(AttributeCase.EducationNum, 0);
                            greaterNum = 0;
                            lessEquaNum = 0;
                            missingNum = 0;
                            infoTemp = 0;
                        }
                        break;
                    #endregion

                    #region Marital Status
                    case AttributeCase.MaritalStatus:
                        {
                            for (int i = 0; i < this._numberCase; i++)
                            {
                                if (this._listCase[i].MariSt == MaritalStatus.unknow)
                                {
                                    missingNum++;
                                }
                                else
                                {
                                    if (this._listCase[i].IsGreater)
                                    {
                                        greaterNum++;
                                    }
                                    else
                                    {
                                        lessEquaNum++;
                                    }
                                }
                            }
                            infoTemp = -((greaterNum / (this._numberCase - missingNum)) * Math.Log((greaterNum / (this._numberCase - missingNum)), 2)
                                        + (lessEquaNum / (this._numberCase - missingNum)) * Math.Log((lessEquaNum / (this._numberCase - missingNum)), 2));
                            this._info.Add(AttributeCase.MaritalStatus, infoTemp);
                            this._missingNum.Add(AttributeCase.MaritalStatus, missingNum);
                            greaterNum = 0;
                            lessEquaNum = 0;
                            missingNum = 0;
                            infoTemp = 0;
                        }
                        break;
                    #endregion

                    #region Occupation
                    case AttributeCase.Occupation:
                        {
                            for (int i = 0; i < this._numberCase; i++)
                            {
                                if (this._listCase[i].Occ == Occupation.unknow)
                                {
                                    missingNum++;
                                }
                                else
                                {
                                    if (this._listCase[i].IsGreater)
                                    {
                                        greaterNum++;
                                    }
                                    else
                                    {
                                        lessEquaNum++;
                                    }
                                }
                            }
                            infoTemp = -((greaterNum / (this._numberCase - missingNum)) * Math.Log((greaterNum / (this._numberCase - missingNum)), 2)
                                        + (lessEquaNum / (this._numberCase - missingNum)) * Math.Log((lessEquaNum / (this._numberCase - missingNum)), 2));
                            this._info.Add(AttributeCase.Occupation, infoTemp);
                            this._missingNum.Add(AttributeCase.Occupation, missingNum);
                            greaterNum = 0;
                            lessEquaNum = 0;
                            missingNum = 0;
                            infoTemp = 0;
                        }
                        break;
                    #endregion

                    #region Relationship
                    case AttributeCase.Relationship:
                        {
                            for (int i = 0; i < this._numberCase; i++)
                            {
                                if (this._listCase[i].Reltsh == Relationship.unknow)
                                {
                                    missingNum++;
                                }
                                else
                                {
                                    if (this._listCase[i].IsGreater)
                                    {
                                        greaterNum++;
                                    }
                                    else
                                    {
                                        lessEquaNum++;
                                    }
                                }
                            }
                            infoTemp = -((greaterNum / (this._numberCase - missingNum)) * Math.Log((greaterNum / (this._numberCase - missingNum)), 2)
                                        + (lessEquaNum / (this._numberCase - missingNum)) * Math.Log((lessEquaNum / (this._numberCase - missingNum)), 2));
                            this._info.Add(AttributeCase.Relationship, infoTemp);
                            this._missingNum.Add(AttributeCase.Relationship, missingNum);
                            greaterNum = 0;
                            lessEquaNum = 0;
                            missingNum = 0;
                            infoTemp = 0;
                        }
                        break;
                    #endregion

                    #region Race
                    case AttributeCase.Race:
                        {
                            for (int i = 0; i < this._numberCase; i++)
                            {
                                if (this._listCase[i].Ra == Race.unknow)
                                {
                                    missingNum++;
                                }
                                else
                                {
                                    if (this._listCase[i].IsGreater)
                                    {
                                        greaterNum++;
                                    }
                                    else
                                    {
                                        lessEquaNum++;
                                    }
                                }
                            }
                            infoTemp = -((greaterNum / (this._numberCase - missingNum)) * Math.Log((greaterNum / (this._numberCase - missingNum)), 2)
                                        + (lessEquaNum / (this._numberCase - missingNum)) * Math.Log((lessEquaNum / (this._numberCase - missingNum)), 2));
                            this._info.Add(AttributeCase.Race, infoTemp);
                            this._missingNum.Add(AttributeCase.Race, missingNum);
                            greaterNum = 0;
                            lessEquaNum = 0;
                            missingNum = 0;
                            infoTemp = 0;
                        }
                        break;
                    #endregion

                    #region Sex
                    case AttributeCase.Sex:
                        {
                            for (int i = 0; i < this._numberCase; i++)
                            {
                                if (this._listCase[i].SexCase == Sex.unknow)
                                {
                                    missingNum++;
                                }
                                else
                                {
                                    if (this._listCase[i].IsGreater)
                                    {
                                        greaterNum++;
                                    }
                                    else
                                    {
                                        lessEquaNum++;
                                    }
                                }
                            }
                            infoTemp = -((greaterNum / (this._numberCase - missingNum)) * Math.Log((greaterNum / (this._numberCase - missingNum)), 2)
                                        + (lessEquaNum / (this._numberCase - missingNum)) * Math.Log((lessEquaNum / (this._numberCase - missingNum)), 2));
                            this._info.Add(AttributeCase.Sex, infoTemp);
                            this._missingNum.Add(AttributeCase.Sex, missingNum);
                            greaterNum = 0;
                            lessEquaNum = 0;
                            missingNum = 0;
                            infoTemp = 0;
                        }
                        break;
                    #endregion

                    #region Capital Gain
                    case AttributeCase.CapitalGain:
                        {
                            for (int i = 0; i < this._numberCase; i++)
                            {
                                if (this._listCase[i].IsGreater)
                                    greaterNum++;
                                else
                                    lessEquaNum++;
                            }
                            infoTemp = -((greaterNum / this._numberCase) * Math.Log(greaterNum / this._numberCase, 2)
                                + (lessEquaNum / this._numberCase) * Math.Log(lessEquaNum / this._numberCase, 2));
                            this._info.Add(AttributeCase.CapitalGain, infoTemp);
                            this._missingNum.Add(AttributeCase.CapitalGain, 0);
                            greaterNum = 0;
                            lessEquaNum = 0;
                            missingNum = 0;
                            infoTemp = 0;
                        }
                        break;
                    #endregion

                    #region Capital Loss
                    case AttributeCase.CapitalLoss:
                        {
                            for (int i = 0; i < this._numberCase; i++)
                            {
                                if (this._listCase[i].IsGreater)
                                    greaterNum++;
                                else
                                    lessEquaNum++;
                            }
                            infoTemp = -((greaterNum / this._numberCase) * Math.Log(greaterNum / this._numberCase, 2)
                                + (lessEquaNum / this._numberCase) * Math.Log(lessEquaNum / this._numberCase, 2));
                            this._info.Add(AttributeCase.CapitalLoss, infoTemp);
                            this._missingNum.Add(AttributeCase.CapitalLoss, 0);
                            greaterNum = 0;
                            lessEquaNum = 0;
                            missingNum = 0;
                            infoTemp = 0;
                        }
                        break;
                    #endregion

                    #region Hours Per Week
                    case AttributeCase.HoursPerWeek:
                        {
                            for (int i = 0; i < this._numberCase; i++)
                            {
                                if (this._listCase[i].IsGreater)
                                    greaterNum++;
                                else
                                    lessEquaNum++;
                            }
                            infoTemp = -((greaterNum / this._numberCase) * Math.Log(greaterNum / this._numberCase, 2)
                                + (lessEquaNum / this._numberCase) * Math.Log(lessEquaNum / this._numberCase, 2));
                            this._info.Add(AttributeCase.HoursPerWeek, infoTemp);
                            this._missingNum.Add(AttributeCase.HoursPerWeek, 0);
                            greaterNum = 0;
                            lessEquaNum = 0;
                            missingNum = 0;
                            infoTemp = 0;
                        }
                        break;
                    #endregion

                    #region Native Country
                    case AttributeCase.NativeCountry:
                        {
                            for (int i = 0; i < this._numberCase; i++)
                            {
                                if (this._listCase[i].NtvCountry == NativeCountry.unknow)
                                {
                                    missingNum++;
                                }
                                else
                                {
                                    if (this._listCase[i].IsGreater)
                                    {
                                        greaterNum++;
                                    }
                                    else
                                    {
                                        lessEquaNum++;
                                    }
                                }
                            }
                            infoTemp = -((greaterNum / (this._numberCase - missingNum)) * Math.Log((greaterNum / (this._numberCase - missingNum)), 2)
                                        + (lessEquaNum / (this._numberCase - missingNum)) * Math.Log((lessEquaNum / (this._numberCase - missingNum)), 2));
                            this._info.Add(AttributeCase.NativeCountry, infoTemp);
                            this._missingNum.Add(AttributeCase.NativeCountry, missingNum);
                            greaterNum = 0;
                            lessEquaNum = 0;
                            missingNum = 0;
                            infoTemp = 0;
                        }
                        break;
                    #endregion

                    default:
                        break;
                }
            }

        }
        #endregion

        #region Filters
        /// <summary>
        /// Filter data with a paticular list conditions
        /// </summary>
        /// <param name="conditions">List Conditions Of Each Attribute</param>
        /// <returns>Subset Data</returns>
        public Data Filter(params Condition[] conditions)
        {
            Data subset = null;
            //List chua du lieu duoc filter. Du lieu filter theo dang loc loai bo dan dan
            List<Case> list = new List<Case>();
            List<Case> temp = null;
            bool isFirst = true;
            if (conditions.Length == 0)
            {
                return new Data(this._listCase);
            }
            foreach (Condition c in conditions)
            {
                switch (c.Attr)
                {
                    #region Attribute Age
                    // Gia tri lien tuc
                    case AttributeCase.Age:
                        {
                            double v = double.Parse(c.Value);
                            StateCompare sc = c.Compare;
                            // Truong hop la filter dau tien, thi dau du lieu vao list truoc.
                            if (isFirst)
                            {
                                foreach (Case ca in this._listCase)
                                {
                                    switch (sc)
                                    {
                                        case StateCompare.Equal:
                                            if (ca.Age == v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.Greater:
                                            if (ca.Age > v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.GreaterEqual:
                                            if (ca.Age >= v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.Less:
                                            if (ca.Age < v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.LessEqua:
                                            if (ca.Age <= v)
                                                list.Add(ca);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                isFirst = false;
                            }
                            else
                            {
                                temp = new List<Case>();
                                foreach (Case ca in list)
                                {
                                    switch (sc)
                                    {
                                        case StateCompare.Equal:
                                            if (ca.Age == v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.Greater:
                                            if (ca.Age > v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.GreaterEqual:
                                            if (ca.Age >= v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.Less:
                                            if (ca.Age < v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.LessEqua:
                                            if (ca.Age <= v)
                                                temp.Add(ca);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                list = temp;
                                temp = null;
                            }
                        }
                        break;
                    #endregion

                    #region Attribute Work Class
                    // Gia tri roi rac
                    case AttributeCase.WorkClass:
                        {
                            WorkClass wc = (WorkClass)Enum.Parse(typeof(WorkClass), c.Value);

                            if (isFirst)
                            {
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.Wcl == wc)
                                        list.Add(ca);
                                }
                                isFirst = false;
                            }
                            else
                            {
                                temp = new List<Case>();

                                foreach (Case ca in list)
                                {
                                    if (ca.Wcl == wc)
                                        temp.Add(ca);
                                }
                                list = temp;
                                temp = null; 
                            }
                        }
                        break;
                    #endregion

                    #region Attribute Fnlwgt
                    // Gia tri lien tuc
                    case AttributeCase.Fnlwgt:
                        {
                            double v = double.Parse(c.Value);
                            StateCompare sc = c.Compare;
                            if (isFirst)
                            {
                                foreach (Case ca in this._listCase)
                                {
                                    switch (sc)
                                    {
                                        case StateCompare.Equal:
                                            if (ca.Fnlwgt == v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.Greater:
                                            if (ca.Fnlwgt > v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.GreaterEqual:
                                            if (ca.Fnlwgt >= v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.Less:
                                            if (ca.Fnlwgt < v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.LessEqua:
                                            if (ca.Fnlwgt <= v)
                                                list.Add(ca);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                isFirst = false;
                            }
                            else
                            {
                                temp = new List<Case>();
                                foreach (Case ca in list)
                                {
                                    switch (sc)
                                    {
                                        case StateCompare.Equal:
                                            if (ca.Fnlwgt == v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.Greater:
                                            if (ca.Fnlwgt > v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.GreaterEqual:
                                            if (ca.Fnlwgt >= v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.Less:
                                            if (ca.Fnlwgt < v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.LessEqua:
                                            if (ca.Fnlwgt <= v)
                                                temp.Add(ca);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                list = temp;
                                temp = null; 
                            }
                        }
                        break;
                    #endregion

                    #region Attribute Education
                    // Roi rac.
                    case AttributeCase.Education:
                        {
                            Education edu = (Education)Enum.Parse(typeof(Education), c.Value);
                            if (isFirst)
                            {
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.Edu == edu)
                                        list.Add(ca);
                                }
                                isFirst = false;
                            }
                            else
                            {
                                temp = new List<Case>();
                                foreach (Case ca in list)
                                {
                                    if (ca.Edu == edu)
                                        temp.Add(ca);
                                }
                                list = temp;
                                temp = null; 
                            }
                        }
                        break;
                    #endregion

                    #region Attribute EducationNum
                    // Gia tri lien tuc
                    case AttributeCase.EducationNum:
                        {
                            double v = double.Parse(c.Value);
                            StateCompare sc = c.Compare;

                            if (isFirst)
                            {
                                foreach (Case ca in this._listCase)
                                {
                                    switch (sc)
                                    {
                                        case StateCompare.Equal:
                                            if (ca.EduNum == v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.Greater:
                                            if (ca.EduNum > v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.GreaterEqual:
                                            if (ca.EduNum >= v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.Less:
                                            if (ca.EduNum < v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.LessEqua:
                                            if (ca.EduNum <= v)
                                                list.Add(ca);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                isFirst = false;
                            }
                            else
                            {
                                temp = new List<Case>();
                              
                                foreach (Case ca in list)
                                {
                                    switch (sc)
                                    {
                                        case StateCompare.Equal:
                                            if (ca.EduNum == v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.Greater:
                                            if (ca.EduNum > v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.GreaterEqual:
                                            if (ca.EduNum >= v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.Less:
                                            if (ca.EduNum < v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.LessEqua:
                                            if (ca.EduNum <= v)
                                                temp.Add(ca);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                list = temp;
                                temp = null; 
                            }
                        }
                        break;
                    #endregion

                    #region Attribute MaritalStatus
                    // Roi rac.
                    case AttributeCase.MaritalStatus:
                        {
                            MaritalStatus mari = (MaritalStatus)Enum.Parse(typeof(MaritalStatus), c.Value);
                            if (isFirst)
                            {
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.MariSt == mari)
                                        list.Add(ca);
                                }
                                isFirst = false;
                            }
                            else
                            {
                                temp = new List<Case>();
                                foreach (Case ca in list)
                                {
                                    if (ca.MariSt == mari)
                                        temp.Add(ca);
                                }
                                list = temp;
                                temp = null; 
                            }
                        }
                        break;
                    #endregion

                    #region Attribute Occupation
                    // Roi rac
                    case AttributeCase.Occupation:
                        {
                            Occupation occ = (Occupation)Enum.Parse(typeof(Occupation), c.Value);
                            if (isFirst)
                            {
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.Occ == occ)
                                        list.Add(ca);
                                }
                                isFirst = false;
                            }
                            else
                            {
                                temp = new List<Case>();
                                foreach (Case ca in list)
                                {
                                    if (ca.Occ == occ)
                                        temp.Add(ca);
                                }
                                list = temp;
                                temp = null; 
                            }
                        }
                        break;
                    #endregion

                    #region Attribute Relationship
                    // Roi rac
                    case AttributeCase.Relationship:
                        {
                            Relationship rls = (Relationship)Enum.Parse(typeof(Relationship), c.Value);
                            if (isFirst)
                            {
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.Reltsh == rls)
                                        list.Add(ca);
                                }
                                isFirst = false;
                            }
                            else
                            {
                                temp = new List<Case>();

                                foreach (Case ca in list)
                                {
                                    if (ca.Reltsh == rls)
                                        temp.Add(ca);
                                }
                                list = temp;
                                temp = null; 
                            }
                        }
                        break;
                    #endregion

                    #region Attribute Race
                    // Roi rac
                    case AttributeCase.Race:
                        {
                            Race r = (Race)Enum.Parse(typeof(Race), c.Value);
                            if (isFirst)
                            {
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.Ra == r)
                                        list.Add(ca);
                                }
                                isFirst = false;
                            }
                            else
                            {
                                temp = new List<Case>();

                                foreach (Case ca in list)
                                {
                                    if (ca.Ra == r)
                                        temp.Add(ca);
                                }
                                list = temp;
                                temp = null; 
                            }
                        }
                        break;
                    #endregion

                    #region Attribute Sex
                    // Roi rac
                    case AttributeCase.Sex:
                        {
                            Sex s = (Sex)Enum.Parse(typeof(Sex), c.Value);

                            if (isFirst)
                            {
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.SexCase == s)
                                        list.Add(ca);
                                }
                                isFirst = false;
                            }
                            else
                            {
                                temp = new List<Case>();
                                
                                foreach (Case ca in list)
                                {
                                    if (ca.SexCase == s)
                                        temp.Add(ca);
                                }
                                list = temp;
                                temp = null; 
                            }
                        }
                        break;
                    #endregion

                    #region Attribute CapitalGain
                    // Lien tuc
                    case AttributeCase.CapitalGain:
                        {
                            double v = double.Parse(c.Value);
                            StateCompare sc = c.Compare;
                            if (isFirst)
                            {
                                foreach (Case ca in this._listCase)
                                {
                                    switch (sc)
                                    {
                                        case StateCompare.Equal:
                                            if (ca.CapitalGain == v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.Greater:
                                            if (ca.CapitalGain > v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.GreaterEqual:
                                            if (ca.CapitalGain >= v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.Less:
                                            if (ca.CapitalGain < v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.LessEqua:
                                            if (ca.CapitalGain <= v)
                                                list.Add(ca);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                isFirst = false;
                            }
                            else
                            {
                                temp = new List<Case>();
                                foreach (Case ca in list)
                                {
                                    switch (sc)
                                    {
                                        case StateCompare.Equal:
                                            if (ca.CapitalGain == v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.Greater:
                                            if (ca.CapitalGain > v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.GreaterEqual:
                                            if (ca.CapitalGain >= v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.Less:
                                            if (ca.CapitalGain < v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.LessEqua:
                                            if (ca.CapitalGain <= v)
                                                temp.Add(ca);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                list = temp;
                                temp = null; 
                            }
                        }
                        break;
                    #endregion

                    #region Attribute Capital Loss
                    // Lien tuc
                    case AttributeCase.CapitalLoss:
                        {
                            double v = double.Parse(c.Value);
                            StateCompare sc = c.Compare;
                            if (isFirst)
                            {
                                foreach (Case ca in this._listCase)
                                {
                                    switch (sc)
                                    {
                                        case StateCompare.Equal:
                                            if (ca.CapitalLoss == v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.Greater:
                                            if (ca.CapitalLoss > v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.GreaterEqual:
                                            if (ca.CapitalLoss >= v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.Less:
                                            if (ca.CapitalLoss < v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.LessEqua:
                                            if (ca.CapitalLoss <= v)
                                                list.Add(ca);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                isFirst = false;
                            }
                            else
                            {
                                temp = new List<Case>();
                                foreach (Case ca in list)
                                {
                                    switch (sc)
                                    {
                                        case StateCompare.Equal:
                                            if (ca.CapitalLoss == v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.Greater:
                                            if (ca.CapitalLoss > v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.GreaterEqual:
                                            if (ca.CapitalLoss >= v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.Less:
                                            if (ca.CapitalLoss < v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.LessEqua:
                                            if (ca.CapitalLoss <= v)
                                                temp.Add(ca);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                list = temp;
                                temp = null; 
                            }
                        }
                        break;
                    #endregion

                    #region Attribute Hours Per Week
                    // Lien tuc
                    case AttributeCase.HoursPerWeek:
                        {
                            double v = double.Parse(c.Value);
                            StateCompare sc = c.Compare;
                            if (isFirst)
                            {
                                foreach (Case ca in this._listCase)
                                {
                                    switch (sc)
                                    {
                                        case StateCompare.Equal:
                                            if (ca.HoursPerWeek == v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.Greater:
                                            if (ca.HoursPerWeek > v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.GreaterEqual:
                                            if (ca.HoursPerWeek >= v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.Less:
                                            if (ca.HoursPerWeek < v)
                                                list.Add(ca);
                                            break;
                                        case StateCompare.LessEqua:
                                            if (ca.HoursPerWeek <= v)
                                                list.Add(ca);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                isFirst = false;
                            }
                            else
                            {
                                temp = new List<Case>();
                                foreach (Case ca in list)
                                {
                                    switch (sc)
                                    {
                                        case StateCompare.Equal:
                                            if (ca.HoursPerWeek == v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.Greater:
                                            if (ca.HoursPerWeek > v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.GreaterEqual:
                                            if (ca.HoursPerWeek >= v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.Less:
                                            if (ca.HoursPerWeek < v)
                                                temp.Add(ca);
                                            break;
                                        case StateCompare.LessEqua:
                                            if (ca.HoursPerWeek <= v)
                                                temp.Add(ca);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                list = temp;
                                temp = null; 
                            }
                        }
                        break;
                    #endregion

                    #region Attribute Naitive Country
                    // Roi rac
                    case AttributeCase.NativeCountry:
                        {
                            NativeCountry n = (NativeCountry)Enum.Parse(typeof(NativeCountry), c.Value);
                            if (isFirst)
                            {
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.NtvCountry == n)
                                        list.Add(ca);
                                }
                                isFirst = false;
                            }
                            else
                            {
                                temp = new List<Case>();

                                foreach (Case ca in list)
                                {
                                    if (ca.NtvCountry == n)
                                        temp.Add(ca);
                                }
                                list = temp;
                                temp = null; 
                            }
                        }
                        break;
                    #endregion

                    #region Attribute Is Greater
                    case AttributeCase.IsGreater:
                        {
                            if (isFirst)
                            {
                                bool val = bool.Parse(c.Value);

                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.IsGreater == val)
                                        list.Add(ca);
                                }
                                isFirst = false;
                            }
                            else
                            {
                                temp = new List<Case>();
                                bool val = bool.Parse(c.Value);

                                foreach (Case ca in list)
                                {
                                    if (ca.IsGreater == val)
                                        temp.Add(ca);
                                }
                                list = temp;
                                temp = null; 
                            }
                        }
                        break;
                    #endregion
                    default:
                        break;
                }
            }
            return subset = new Data(list.ToArray());
        }
        #endregion

        #region Entropy
        /// <summary>
        /// Tính lượng tin trung bình của mỗi trường hợp.
        /// </summary>
        /// <returns>Lượng tin trung bình</returns>
        public double Entropy()
        {
            int i;
            double positive = 0;
            double p;
            for (i = 0; i < _numberCase; i++)
            {
                if (this.Cases[i].IsGreater)
                {
                    positive++;
                }
            }
            p = positive / this._numberCase;
            return -(Math.Log(p, 2) * p + Math.Log(1 - p, 2) * (1 - p));
        }
        #endregion

        #region Threshold
        /// <summary>
        /// Trả lại một mảng các thrshold của bộ dữ liệu
        /// </summary>
        /// <param name="continuousAttr">Thuộc tính liên tục cần rời rạc hóa</param>
        /// <param name="comparer">Bộ so sánh, để xác định thuộc tính liên tục nào được chọn</param>
        /// <returns>Mảng các threshold</returns>
        public double[] GetThreshold(AttributeCase continuousAttr, IComparer<Case> comparer)
        {
            List<double> thsArr = new List<double>();
            int n = this._listCase.Count();
            int i = 0;
            double begin = 0;
            double end = 0;

            if (comparer != null)
            {
                //Sắp xếp rồi bắt đầu tính khoảng.
                Array.Sort(this._listCase, comparer);
                switch (continuousAttr)
                {
                    #region Age
                    case AttributeCase.Age:
                        {
                            while (i < n - 1)
                            {
                                begin = this._listCase[i].Age;
                                i++;
                                while (i < n && this._listCase[i].Age == begin)
                                    i++;
                                if (i < n)
                                {
                                    end = this._listCase[i].Age;
                                    thsArr.Add((begin + end) / 2);
                                }
                            }
                        }
                        break; 
                    #endregion

                    #region EducationNum
                    case AttributeCase.EducationNum:
                        {
                            while (i < n - 1)
                            {
                                begin = this._listCase[i].EduNum;
                                i++;
                                while (i < n && this._listCase[i].EduNum == begin)
                                    i++;
                                if (i < n)
                                {
                                    end = this._listCase[i].EduNum;
                                    thsArr.Add((begin + end) / 2);
                                }
                            }
                        }
                        break; 
                    #endregion

                    #region CapitalGain
                    case AttributeCase.CapitalGain:
                        {
                            while (i < n - 1)
                            {
                                begin = this._listCase[i].CapitalGain;
                                i++;
                                while (i < n && this._listCase[i].CapitalGain == begin)
                                    i++;
                                if (i < n)
                                {
                                    end = this._listCase[i].CapitalGain;
                                    thsArr.Add((begin + end) / 2);
                                }
                            }
                        }
                        break; 
                    #endregion

                    #region Capital Loss
                    case AttributeCase.CapitalLoss:
                        {
                            while (i < n - 1)
                            {
                                begin = this._listCase[i].CapitalLoss;
                                i++;
                                while (i < n && this._listCase[i].CapitalLoss == begin)
                                    i++;
                                if (i < n)
                                {
                                    end = this._listCase[i].CapitalLoss;
                                    thsArr.Add((begin + end) / 2);
                                }
                            }
                        } 
                        break;
                    #endregion

                    #region Fnlwgt
                    case AttributeCase.Fnlwgt:
                        {
                            while (i < n - 1)
                            {
                                begin = this._listCase[i].Fnlwgt;
                                i++;
                                while (i < n && this._listCase[i].Fnlwgt == begin)
                                    i++;
                                if (i < n)
                                {
                                    end = this._listCase[i].Fnlwgt;
                                    thsArr.Add((begin + end) / 2);
                                }
                            }
                        }
                        break; 
                    #endregion

                    #region Hours Per Week
                    case AttributeCase.HoursPerWeek:
                        {
                            while (i < n - 1)
                            {
                                begin = this._listCase[i].HoursPerWeek;
                                i++;
                                while (i < n && this._listCase[i].HoursPerWeek == begin)
                                    i++;
                                if (i < n)
                                {
                                    end = this._listCase[i].HoursPerWeek;
                                    thsArr.Add((begin + end) / 2);
                                }
                            }
                        }
                        break; 
                    #endregion
                    default:
                        break;
                }
            }
            return thsArr.ToArray();
        }
        #endregion

        #region Missing Values Counter For Each Attribute
        /// <summary>
        /// Số trường hợp bị missing value trên một thuộc tính nào đó
        /// trên bộ dữ liệu.
        /// </summary>
        /// <param name="attr">Thuốc tính cần check</param>
        /// <returns>Số trường hợp missing value</returns>
        public double NumberMissingValues(AttributeCase attr)
        {
            double count = 0;
            switch (attr)
            {
                #region Attribute Age
                // Gia tri lien tuc
                case AttributeCase.Age:
                    {
                        for (int i = 0; i < this._numberCase; i++)
                        {
                            if (this._listCase[i].Age < 0)
                                count++;
                        }
                    }
                    break;
                #endregion

                #region Attribute Work Class
                // Gia tri roi rac
                case AttributeCase.WorkClass:
                    {
                        for (int i = 0; i < this._numberCase; i++)
                        {
                            if (this._listCase[i].Wcl == WorkClass.unknow)
                                count++;
                        }
                    }
                    break;
                #endregion

                #region Attribute Fnlwgt
                // Gia tri lien tuc
                case AttributeCase.Fnlwgt:
                    {
                        for (int i = 0; i < this._numberCase; i++)
                        {
                            if (this._listCase[i].Fnlwgt < 0)
                                count++;
                        }
                    }
                    break;
                #endregion

                #region Attribute Education
                // Roi rac.
                case AttributeCase.Education:
                    {
                        for (int i = 0; i < this._numberCase; i++)
                        {
                            if (this._listCase[i].Edu == Education.unknow)
                                count++;
                        }
                    }
                    break;
                #endregion

                #region Attribute EducationNum
                // Gia tri lien tuc
                case AttributeCase.EducationNum:
                    {
                        for (int i = 0; i < this._numberCase; i++)
                        {
                            if (this._listCase[i].EduNum < 0)
                                count++;
                        }
                    }
                    break;
                #endregion

                #region Attribute MaritalStatus
                // Roi rac.
                case AttributeCase.MaritalStatus:
                    {
                        for (int i = 0; i < this._numberCase; i++)
                        {
                            if (this._listCase[i].MariSt == MaritalStatus.unknow)
                                count++;
                        }
                    }
                    break;
                #endregion

                #region Attribute Occupation
                // Roi rac
                case AttributeCase.Occupation:
                    {
                        for (int i = 0; i < this._numberCase; i++)
                        {
                            if (this._listCase[i].Occ == Occupation.unknow)
                                count++;
                        }
                    }
                    break;
                #endregion

                #region Attribute Relationship
                // Roi rac
                case AttributeCase.Relationship:
                    {
                        for (int i = 0; i < this._numberCase; i++)
                        {
                            if (this._listCase[i].Reltsh == Relationship.unknow)
                                count++;
                        }
                    }
                    break;
                #endregion

                #region Attribute Race
                // Roi rac
                case AttributeCase.Race:
                    {
                        for (int i = 0; i < this._numberCase; i++)
                        {
                            if (this._listCase[i].Ra == Race.unknow)
                                count++;
                        }
                    }
                    break;
                #endregion

                #region Attribute Sex
                // Roi rac
                case AttributeCase.Sex:
                    {
                        for (int i = 0; i < this._numberCase; i++)
                        {
                            if (this._listCase[i].SexCase == Sex.unknow)
                                count++;
                        }
                    }
                    break;
                #endregion

                #region Attribute CapitalGain
                // Lien tuc
                case AttributeCase.CapitalGain:
                    {
                        for (int i = 0; i < this._numberCase; i++)
                        {
                            if (this._listCase[i].CapitalGain < 0)
                                count++;
                        }
                    }
                    break;
                #endregion

                #region Attribute Capital Loss
                // Lien tuc
                case AttributeCase.CapitalLoss:
                    {
                        for (int i = 0; i < this._numberCase; i++)
                        {
                            if (this._listCase[i].CapitalLoss < 0)
                                count++;
                        }
                    }
                    break;
                #endregion

                #region Attribute Hours Per Week
                // Lien tuc
                case AttributeCase.HoursPerWeek:
                    {
                        for (int i = 0; i < this._numberCase; i++)
                        {
                            if (this._listCase[i].HoursPerWeek < 0)
                                count++;
                        }
                    }
                    break;
                #endregion

                #region Attribute Naitive Country
                // Roi rac
                case AttributeCase.NativeCountry:
                    {
                        for (int i = 0; i < this._numberCase; i++)
                        {
                            if (this._listCase[i].NtvCountry == NativeCountry.unknow)
                                count++;
                        }
                    }
                    break;
                #endregion
                
                default:
                    break;
            }
            return count;
        }
        #endregion

        #region Number Missing Cases
        /// <summary>
        /// Tính tổng số phần tử bị missing
        /// </summary>
        /// <returns>Số phần từ missing</returns>
        public double TotalMissingCase()
        {
            double count = 0;
            for (int i = 0; i < this._numberCase; i++)
            {
                if (this._listCase[i].IsMissingValue)
                    count++;
            }
            return count;
        }
        #endregion

        #region Greater Number Cases
        /// <summary>
        /// Đếm số trường hợp có thu nhập lớn hơn 50k$
        /// </summary>
        /// <returns>số trường hợp</returns>
        public double NumberGreaterCase()
        {
            double count = 0;
            for (int i = 0; i < this._numberCase; i++)
            {
                if (this._listCase[i].IsGreater)
                    count++;
            }
            return count;
        }
        #endregion

        #region Statistic
        /// <summary>
        /// Thống kê số trường hợp lớn hơn và nhỏ hơn 50K$ ứng với value của một
        /// thuộc tính nào đó.
        /// </summary>
        /// <param name="attr">Thuộc tính</param>
        /// <param name="threshold">thresholde của thuộc tính liên tục</param>
        /// <returns>Mảng các đối tượng Statistic- ứng với từng value của thuộc tính rời rạc và 
        /// với thuộc tính liên tục là hai khoảng lớn hơn và nhỏ</returns>
        public StatictisObject[] Statistic(AttributeCase attr, double? threshold)
        {
            StatictisObject[] caseArr = null;
            DiscreteIndex index = new DiscreteIndex();
            switch (attr)
            {
                #region Attribute Age
                // Gia tri lien tuc
                case AttributeCase.Age:
                    {
                        double numberLessEqua1 = 0;
                        double numberGreater1 = 0;
                        // Để đếm các trường hợp có giá trị lớn hơn threshold
                        double numberLessEqual2 = 0;
                        double numberGreater2 = 0;

                        // Dem so truong hop nho hon va lon hon threshold co thu nhap lon hon va nho 50K$
                        foreach (Case item in this._listCase)
                        {
                            // Trường hợp nhỏ hơn threshold
                            if (item.Age <= threshold)
                            {
                                // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                if (item.IsGreater == false)
                                    numberLessEqua1++;
                                else // Có thu nhập lớn hơn 50K$
                                    numberGreater1++;
                            }
                            else if (item.Age > threshold)
                            {
                                // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                if (item.IsGreater == false)
                                    numberLessEqual2++;
                                else // Có thu nhập lớn 50K$
                                    numberGreater2++;
                            }
                        }

                        caseArr = new StatictisObject[2];
                        caseArr[0] = new StatictisObject(numberGreater1, numberLessEqua1);
                        caseArr[1] = new StatictisObject(numberGreater2, numberLessEqual2);
                    }
                    break;
                #endregion

                #region Attribute Work Class
                // Gia tri roi rac
                case AttributeCase.WorkClass:
                    {
                        // So value ung voi work class attribute
                        int n = index.WorkClassValues;
                        caseArr = new StatictisObject[n];
                        int i = 0;
                        string[] strArr = Enum.GetNames(typeof(WorkClass));
                        foreach (string str in strArr)
                        {
                            if (str != "unknow")
                            {
                                WorkClass wc = (WorkClass)Enum.Parse(typeof(WorkClass), str);
                                double numberLessEqua = 0;
                                double numberGreater = 0;
                                foreach (Case item in this._listCase)
                                {
                                    if (item.Wcl != WorkClass.unknow && item.Wcl == wc)
                                    {
                                        // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                        if (item.IsGreater == false)
                                            numberLessEqua++;
                                        else // Có thu nhập lớn hơn 50K$
                                            numberGreater++;
                                    }
                                }
                                StatictisObject so = new StatictisObject(numberGreater, numberLessEqua);
                                caseArr[i] = so;
                                i++;
                            }
                        }
                    }
                    break;
                #endregion

                #region Attribute Fnlwgt
                // Gia tri lien tuc
                case AttributeCase.Fnlwgt:
                    {
                        double numberLessEqua1 = 0;
                        double numberGreater1 = 0;
                        // Để đếm các trường hợp có giá trị lớn hơn threshold
                        double numberLessEqual2 = 0;
                        double numberGreater2 = 0;

                        // Dem so truong hop nho hon va lon hon threshold co thu nhap lon hon va nho 50K$
                        foreach (Case item in this._listCase)
                        {
                            // Trường hợp nhỏ hơn threshold
                            if (item.Fnlwgt <= threshold)
                            {
                                // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                if (item.IsGreater == false)
                                    numberLessEqua1++;
                                else // Có thu nhập lớn hơn 50K$
                                    numberGreater1++;
                            }
                            else if (item.Fnlwgt > threshold)
                            {
                                // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                if (item.IsGreater == false)
                                    numberLessEqual2++;
                                else // Có thu nhập lớn 50K$
                                    numberGreater2++;
                            }
                        }

                        caseArr = new StatictisObject[2];
                        caseArr[0] = new StatictisObject(numberGreater1, numberLessEqua1);
                        caseArr[1] = new StatictisObject(numberGreater2, numberLessEqual2);
                    }
                    break;
                #endregion

                #region Attribute Education
                // Roi rac.
                case AttributeCase.Education:
                    {
                        // So value ung voi work class attribute
                        int n = index.EducationValues;
                        caseArr = new StatictisObject[n];
                        int i = 0;
                        string[] strArr = Enum.GetNames(typeof(Education));
                        foreach (string str in strArr)
                        {
                            if (str != "unknow")
                            {
                                Education ed = (Education)Enum.Parse(typeof(Education), str);
                                double numberLessEqua = 0;
                                double numberGreater = 0;
                                foreach (Case item in this._listCase)
                                {
                                    if (item.Edu != Education.unknow && item.Edu == ed)
                                    {
                                        // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                        if (item.IsGreater == false)
                                            numberLessEqua++;
                                        else // Có thu nhập lớn hơn 50K$
                                            numberGreater++;
                                    }
                                }
                                StatictisObject so = new StatictisObject(numberGreater, numberLessEqua);
                                caseArr[i] = so;
                                i++;
                            }
                        }
                    }
                    break;
                #endregion

                #region Attribute EducationNum
                // Gia tri lien tuc
                case AttributeCase.EducationNum:
                    {
                        double numberLessEqua1 = 0;
                        double numberGreater1 = 0;
                        // Để đếm các trường hợp có giá trị lớn hơn threshold
                        double numberLessEqual2 = 0;
                        double numberGreater2 = 0;

                        // Dem so truong hop nho hon va lon hon threshold co thu nhap lon hon va nho 50K$
                        foreach (Case item in this._listCase)
                        {
                            // Trường hợp nhỏ hơn threshold
                            if (item.EduNum <= threshold)
                            {
                                // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                if (item.IsGreater == false)
                                    numberLessEqua1++;
                                else // Có thu nhập lớn hơn 50K$
                                    numberGreater1++;
                            }
                            else if (item.EduNum > threshold)
                            {
                                // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                if (item.IsGreater == false)
                                    numberLessEqual2++;
                                else // Có thu nhập lớn 50K$
                                    numberGreater2++;
                            }
                        }

                        caseArr = new StatictisObject[2];
                        caseArr[0] = new StatictisObject(numberGreater1, numberLessEqua1);
                        caseArr[1] = new StatictisObject(numberGreater2, numberLessEqual2);
                    }
                    break;
                #endregion

                #region Attribute MaritalStatus
                // Roi rac.
                case AttributeCase.MaritalStatus:
                    {
                        // So value ung voi work class attribute
                        int n = index.MaritalStatusValues;
                        caseArr = new StatictisObject[n];
                        int i = 0;
                        string[] strArr = Enum.GetNames(typeof(MaritalStatus));
                        foreach (string str in strArr)
                        {
                            if (str != "unknow")
                            {
                                MaritalStatus ms = (MaritalStatus)Enum.Parse(typeof(MaritalStatus), str);
                                double numberLessEqua = 0;
                                double numberGreater = 0;
                                foreach (Case item in this._listCase)
                                {
                                    if (item.MariSt != MaritalStatus.unknow && item.MariSt == ms)
                                    {
                                        // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                        if (item.IsGreater == false)
                                            numberLessEqua++;
                                        else // Có thu nhập lớn hơn 50K$
                                            numberGreater++;
                                    }
                                }
                                StatictisObject so = new StatictisObject(numberGreater, numberLessEqua);
                                caseArr[i] = so;
                                i++;
                            }
                        }
                    }
                    break;
                #endregion

                #region Attribute Occupation
                // Roi rac
                case AttributeCase.Occupation:
                    {
                        // So value ung voi work class attribute
                        int n = index.OccupationValues;
                        caseArr = new StatictisObject[n];
                        int i = 0;
                        string[] strArr = Enum.GetNames(typeof(Occupation));
                        foreach (string str in strArr)
                        {
                            if (str != "unknow")
                            {
                                Occupation occ = (Occupation)Enum.Parse(typeof(Occupation), str);
                                double numberLessEqua = 0;
                                double numberGreater = 0;
                                foreach (Case item in this._listCase)
                                {
                                    if (item.Occ != Occupation.unknow && item.Occ == occ)
                                    {
                                        // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                        if (item.IsGreater == false)
                                            numberLessEqua++;
                                        else // Có thu nhập lớn hơn 50K$
                                            numberGreater++;
                                    }
                                }
                                StatictisObject so = new StatictisObject(numberGreater, numberLessEqua);
                                caseArr[i] = so;
                                i++;
                            }
                        }
                    }
                    break;
                #endregion

                #region Attribute Relationship
                // Roi rac
                case AttributeCase.Relationship:
                    {
                        // So value ung voi work class attribute
                        int n = index.RelationshipValues;
                        caseArr = new StatictisObject[n];
                        int i = 0;
                        string[] strArr = Enum.GetNames(typeof(Relationship));
                        foreach (string str in strArr)
                        {
                            if (str != "unknow")
                            {
                                Relationship re = (Relationship)Enum.Parse(typeof(Relationship), str);
                                double numberLessEqua = 0;
                                double numberGreater = 0;
                                foreach (Case item in this._listCase)
                                {
                                    if (item.Reltsh != Relationship.unknow && item.Reltsh == re)
                                    {
                                        // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                        if (item.IsGreater == false)
                                            numberLessEqua++;
                                        else // Có thu nhập lớn hơn 50K$
                                            numberGreater++;
                                    }
                                }
                                StatictisObject so = new StatictisObject(numberGreater, numberLessEqua);
                                caseArr[i] = so;
                                i++;
                            }
                        }
                    }
                    break;
                #endregion

                #region Attribute Race
                // Roi rac
                case AttributeCase.Race:
                    {
                        // So value ung voi work class attribute
                        int n = index.RaceValues;
                        caseArr = new StatictisObject[n];
                        int i = 0;
                        string[] strArr = Enum.GetNames(typeof(Race));
                        foreach (string str in strArr)
                        {
                            if (str != "unknow")
                            {
                                Race ra = (Race)Enum.Parse(typeof(Race), str);
                                double numberLessEqua = 0;
                                double numberGreater = 0;
                                foreach (Case item in this._listCase)
                                {
                                    if (item.Ra != Race.unknow && item.Ra == ra)
                                    {
                                        // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                        if (item.IsGreater == false)
                                            numberLessEqua++;
                                        else // Có thu nhập lớn hơn 50K$
                                            numberGreater++;
                                    }
                                }
                                StatictisObject so = new StatictisObject(numberGreater, numberLessEqua);
                                caseArr[i] = so;
                                i++;
                            }
                        }
                    }
                    break;
                #endregion

                #region Attribute Sex
                // Roi rac
                case AttributeCase.Sex:
                    {
                        // So value ung voi work class attribute
                        int n = index.SexValues;
                        caseArr = new StatictisObject[n];
                        int i = 0;
                        string[] strArr = Enum.GetNames(typeof(Sex));
                        foreach (string str in strArr)
                        {
                            if (str != "unknow")
                            {
                                Sex sex = (Sex)Enum.Parse(typeof(Sex), str);
                                double numberLessEqua = 0;
                                double numberGreater = 0;
                                foreach (Case item in this._listCase)
                                {
                                    if (item.SexCase != Sex.unknow && item.SexCase == sex)
                                    {
                                        // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                        if (item.IsGreater == false)
                                            numberLessEqua++;
                                        else // Có thu nhập lớn hơn 50K$
                                            numberGreater++;
                                    }
                                }
                                StatictisObject so = new StatictisObject(numberGreater, numberLessEqua);
                                caseArr[i] = so;
                                i++;
                            }
                        }
                    }
                    break;
                #endregion

                #region Attribute CapitalGain
                // Lien tuc
                case AttributeCase.CapitalGain:
                    {
                        double numberLessEqua1 = 0;
                        double numberGreater1 = 0;
                        // Để đếm các trường hợp có giá trị lớn hơn threshold
                        double numberLessEqual2 = 0;
                        double numberGreater2 = 0;

                        // Dem so truong hop nho hon va lon hon threshold co thu nhap lon hon va nho 50K$
                        foreach (Case item in this._listCase)
                        {
                            // Trường hợp nhỏ hơn threshold
                            if (item.CapitalGain <= threshold)
                            {
                                // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                if (item.IsGreater == false)
                                    numberLessEqua1++;
                                else // Có thu nhập lớn hơn 50K$
                                    numberGreater1++;
                            }
                            else if (item.CapitalGain > threshold)
                            {
                                // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                if (item.IsGreater == false)
                                    numberLessEqual2++;
                                else // Có thu nhập lớn 50K$
                                    numberGreater2++;
                            }
                        }

                        caseArr = new StatictisObject[2];
                        caseArr[0] = new StatictisObject(numberGreater1, numberLessEqua1);
                        caseArr[1] = new StatictisObject(numberGreater2, numberLessEqual2);
                    }
                    break;
                #endregion

                #region Attribute Capital Loss
                // Lien tuc
                case AttributeCase.CapitalLoss:
                    {
                        double numberLessEqua1 = 0;
                        double numberGreater1 = 0;
                        // Để đếm các trường hợp có giá trị lớn hơn threshold
                        double numberLessEqual2 = 0;
                        double numberGreater2 = 0;

                        // Dem so truong hop nho hon va lon hon threshold co thu nhap lon hon va nho 50K$
                        foreach (Case item in this._listCase)
                        {
                            // Trường hợp nhỏ hơn threshold
                            if (item.CapitalLoss <= threshold)
                            {
                                // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                if (item.IsGreater == false)
                                    numberLessEqua1++;
                                else // Có thu nhập lớn hơn 50K$
                                    numberGreater1++;
                            }
                            else if (item.CapitalLoss > threshold)
                            {
                                // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                if (item.IsGreater == false)
                                    numberLessEqual2++;
                                else // Có thu nhập lớn 50K$
                                    numberGreater2++;
                            }
                        }

                        caseArr = new StatictisObject[2];
                        caseArr[0] = new StatictisObject(numberGreater1, numberLessEqua1);
                        caseArr[1] = new StatictisObject(numberGreater2, numberLessEqual2);
                    }
                    break;
                #endregion

                #region Attribute Hours Per Week
                // Lien tuc
                case AttributeCase.HoursPerWeek:
                    {
                        double numberLessEqua1 = 0;
                        double numberGreater1 = 0;
                        // Để đếm các trường hợp có giá trị lớn hơn threshold
                        double numberLessEqual2 = 0;
                        double numberGreater2 = 0;

                        // Dem so truong hop nho hon va lon hon threshold co thu nhap lon hon va nho 50K$
                        foreach (Case item in this._listCase)
                        {
                            // Trường hợp nhỏ hơn threshold
                            if (item.HoursPerWeek <= threshold)
                            {
                                // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                if (item.IsGreater == false)
                                    numberLessEqua1++;
                                else // Có thu nhập lớn hơn 50K$
                                    numberGreater1++;
                            }
                            else if (item.HoursPerWeek > threshold)
                            {
                                // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                if (item.IsGreater == false)
                                    numberLessEqual2++;
                                else // Có thu nhập lớn 50K$
                                    numberGreater2++;
                            }
                        }

                        caseArr = new StatictisObject[2];
                        caseArr[0] = new StatictisObject(numberGreater1, numberLessEqua1);
                        caseArr[1] = new StatictisObject(numberGreater2, numberLessEqual2);
                    }
                    break;
                #endregion

                #region Attribute Naitive Country
                // Roi rac
                case AttributeCase.NativeCountry:
                    {
                        // So value ung voi work class attribute
                        int n = index.NaitiveCountryValues;
                        caseArr = new StatictisObject[n];
                        int i = 0;
                        string[] strArr = Enum.GetNames(typeof(NativeCountry));
                        foreach (string str in strArr)
                        {
                            if (str != "unknow")
                            {
                                NativeCountry na = (NativeCountry)Enum.Parse(typeof(NativeCountry), str);
                                double numberLessEqua = 0;
                                double numberGreater = 0;
                                foreach (Case item in this._listCase)
                                {
                                    if (item.NtvCountry != NativeCountry.unknow && item.NtvCountry == na)
                                    {
                                        // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                        if (item.IsGreater == false)
                                            numberLessEqua++;
                                        else // Có thu nhập lớn hơn 50K$
                                            numberGreater++;
                                    }
                                }
                                StatictisObject so = new StatictisObject(numberGreater, numberLessEqua);
                                caseArr[i] = so;
                                i++;
                            }
                        }
                    }
                    break;
                #endregion

                default:
                    break;
            }
            return caseArr;
        }
        #endregion

        #region Get A random case
        /// <summary>
        /// Lấy random một case bất kỳ.
        /// Trả lại null nếu dữ liệu không có.
        /// </summary>
        /// <returns></returns>
        public Case GetRandomCase()
        {
            if (this._numberCase == 0)
                return null;
            Random r = new Random();
            int index = r.Next(0, this._numberCase );
            return this._listCase[index];
        }
        #endregion

        #region Clean Data
        /// <summary>
        /// Chuẩn hóa dữ liệu trong các trường hợp bị missing value
        /// </summary>
        public void CleanData()
        {
            string[] arrName = Enum.GetNames(typeof(AttributeCase));
            // Tim kiem nhan pho bien nhat ung voi moi attribute
            foreach (string str in arrName)
            {
                AttributeCase attr = (AttributeCase)Enum.Parse(typeof(AttributeCase), str);
                switch (attr)
                {
                    #region Work Class
                    case AttributeCase.WorkClass:
                        {
                            string[] valuesArr = Enum.GetNames(typeof(WorkClass));
                            Dictionary<WorkClass, int> freqLessEqua = new Dictionary<WorkClass, int>();
                            Dictionary<WorkClass, int> freqGreater = new Dictionary<WorkClass, int>();
                            foreach (string value in valuesArr)
                            {
                                if (value != "unknow")
                                {
                                    WorkClass va = (WorkClass)Enum.Parse(typeof(WorkClass),value);
                                    freqGreater.Add(va, 0);
                                    freqLessEqua.Add(va, 0);
                                }
                            }
                            foreach (Case c in this._listCase)
                            {
                                if (c.Wcl != WorkClass.unknow)
                                {
                                    if (c.IsGreater)
                                    {
                                        freqGreater[c.Wcl]++;
                                    }
                                    else
                                    {
                                        freqLessEqua[c.Wcl]++;
                                    }
                                }
                            }
                            // Tim nhan pho bien nhat.
                            WorkClass commonLessEqual = WorkClass.unknow;
                            int lessEqualNum = 0;
                            WorkClass commonGreater = WorkClass.unknow;
                            int greaterNum = 0;
                            foreach (string v in valuesArr)
                            {
                                if (v != "unknow")
                                {
                                    WorkClass key = (WorkClass)Enum.Parse(typeof(WorkClass), v);
                                    if (greaterNum < freqGreater[key])
                                    {
                                        greaterNum = freqGreater[key];
                                        commonGreater = key;
                                    }
                                    if (lessEqualNum < freqLessEqua[key])
                                    {
                                        lessEqualNum = freqLessEqua[key];
                                        commonLessEqual = key;
                                    }
                                }
                            }
                            // Luu tru cac nhan common vao tu dien
                            this._mostCommonLabelGreatr.Add(AttributeCase.WorkClass, commonGreater);
                            this._mostCommonLabelLessEqua.Add(AttributeCase.WorkClass, commonLessEqual);
                        }
                        break; 
                    #endregion

                    #region Education
                    case AttributeCase.Education:
                        {
                            string[] valuesArr = Enum.GetNames(typeof(Education));
                            Dictionary<Education, int> freqLessEqua = new Dictionary<Education, int>();
                            Dictionary<Education, int> freqGreater = new Dictionary<Education, int>();
                            foreach (string value in valuesArr)
                            {
                                if (value != "unknow")
                                {
                                    Education va = (Education)Enum.Parse(typeof(Education), value);
                                    freqGreater.Add(va, 0);
                                    freqLessEqua.Add(va, 0);
                                }
                            }
                            foreach (Case c in this._listCase)
                            {
                                if (c.Edu != Education.unknow)
                                {
                                    if (c.IsGreater)
                                    {
                                        freqGreater[c.Edu]++;
                                    }
                                    else
                                    {
                                        freqLessEqua[c.Edu]++;
                                    }
                                }
                            }
                            // Tim nhan pho bien nhat.
                            Education commonLessEqual = Education.unknow;
                            int lessEqualNum = 0;
                            Education commonGreater = Education.unknow;
                            int greaterNum = 0;
                            foreach (string v in valuesArr)
                            {
                                if (v != "unknow")
                                {
                                    Education key = (Education)Enum.Parse(typeof(Education), v);
                                    if (greaterNum < freqGreater[key])
                                    {
                                        greaterNum = freqGreater[key];
                                        commonGreater = key;
                                    }
                                    if (lessEqualNum < freqLessEqua[key])
                                    {
                                        lessEqualNum = freqLessEqua[key];
                                        commonLessEqual = key;

                                    }
                                }
                            }
                            // Luu tru cac nhan common vao tu dien
                            this._mostCommonLabelGreatr.Add(AttributeCase.Education, commonGreater);
                            this._mostCommonLabelLessEqua.Add(AttributeCase.Education, commonLessEqual);
                        }
                        break; 
                    #endregion

                    #region Marital Status
                    case AttributeCase.MaritalStatus:
                        {
                            string[] valuesArr = Enum.GetNames(typeof(MaritalStatus));
                            Dictionary<MaritalStatus, int> freqLessEqua = new Dictionary<MaritalStatus, int>();
                            Dictionary<MaritalStatus, int> freqGreater = new Dictionary<MaritalStatus, int>();
                            foreach (string value in valuesArr)
                            {
                                if (value != "unknow")
                                {
                                    MaritalStatus va = (MaritalStatus)Enum.Parse(typeof(MaritalStatus), value);
                                    freqGreater.Add(va, 0);
                                    freqLessEqua.Add(va, 0);
                                }
                            }
                            foreach (Case c in this._listCase)
                            {
                                if (c.MariSt != MaritalStatus.unknow)
                                {
                                    if (c.IsGreater)
                                    {
                                        freqGreater[c.MariSt]++;
                                    }
                                    else
                                    {
                                        freqLessEqua[c.MariSt]++;
                                    }
                                }
                            }
                            // Tim nhan pho bien nhat.
                            MaritalStatus commonLessEqual = MaritalStatus.unknow;
                            int lessEqualNum = 0;
                            MaritalStatus commonGreater = MaritalStatus.unknow;
                            int greaterNum = 0;
                            foreach (string v in valuesArr)
                            {
                                if (v != "unknow")
                                {
                                    MaritalStatus key = (MaritalStatus)Enum.Parse(typeof(MaritalStatus), v);
                                    if (greaterNum < freqGreater[key])
                                    {
                                        greaterNum = freqGreater[key];
                                        commonGreater = key;
                                    }
                                    if (lessEqualNum < freqLessEqua[key])
                                    {
                                        lessEqualNum = freqLessEqua[key];
                                        commonLessEqual = key;

                                    }
                                }
                            }
                            // Luu tru cac nhan common vao tu dien
                            this._mostCommonLabelGreatr.Add(AttributeCase.MaritalStatus, commonGreater);
                            this._mostCommonLabelLessEqua.Add(AttributeCase.MaritalStatus, commonLessEqual);
                        }
                        break; 
                    #endregion

                    #region Occupation
                    case AttributeCase.Occupation:
                        {
                            string[] valuesArr = Enum.GetNames(typeof(Occupation));
                            Dictionary<Occupation, int> freqLessEqua = new Dictionary<Occupation, int>();
                            Dictionary<Occupation, int> freqGreater = new Dictionary<Occupation, int>();
                            foreach (string value in valuesArr)
                            {
                                if (value != "unknow")
                                {
                                    Occupation va = (Occupation)Enum.Parse(typeof(Occupation), value);
                                    freqGreater.Add(va, 0);
                                    freqLessEqua.Add(va, 0);
                                }
                            }
                            foreach (Case c in this._listCase)
                            {
                                if (c.Occ != Occupation.unknow)
                                {
                                    if (c.IsGreater)
                                    {
                                        freqGreater[c.Occ]++;
                                    }
                                    else
                                    {
                                        freqLessEqua[c.Occ]++;
                                    }
                                }
                            }
                            // Tim nhan pho bien nhat.
                            Occupation commonLessEqual = Occupation.unknow;
                            int lessEqualNum = 0;
                            Occupation commonGreater = Occupation.unknow;
                            int greaterNum = 0;
                            foreach (string v in valuesArr)
                            {
                                if (v != "unknow")
                                {
                                    Occupation key = (Occupation)Enum.Parse(typeof(Occupation), v);
                                    if (greaterNum < freqGreater[key])
                                    {
                                        greaterNum = freqGreater[key];
                                        commonGreater = key;
                                    }
                                    if (lessEqualNum < freqLessEqua[key])
                                    {
                                        lessEqualNum = freqLessEqua[key];
                                        commonLessEqual = key;

                                    }
                                }
                            }
                            // Luu tru cac nhan common vao tu dien
                            this._mostCommonLabelGreatr.Add(AttributeCase.Occupation, commonGreater);
                            this._mostCommonLabelLessEqua.Add(AttributeCase.Occupation, commonLessEqual);
                        }
                        break; 
                    #endregion

                    #region Relationship
                    case AttributeCase.Relationship:
                        {
                            string[] valuesArr = Enum.GetNames(typeof(Relationship));
                            Dictionary<Relationship, int> freqLessEqua = new Dictionary<Relationship, int>();
                            Dictionary<Relationship, int> freqGreater = new Dictionary<Relationship, int>();
                            foreach (string value in valuesArr)
                            {
                                if (value != "unknow")
                                {
                                    Relationship va = (Relationship)Enum.Parse(typeof(Relationship), value);
                                    freqGreater.Add(va, 0);
                                    freqLessEqua.Add(va, 0);
                                }
                            }
                            foreach (Case c in this._listCase)
                            {
                                if (c.Reltsh != Relationship.unknow)
                                {
                                    if (c.IsGreater)
                                    {
                                        freqGreater[c.Reltsh]++;
                                    }
                                    else
                                    {
                                        freqLessEqua[c.Reltsh]++;
                                    }
                                }
                            }
                            // Tim nhan pho bien nhat.
                            Relationship commonLessEqual = Relationship.unknow;
                            int lessEqualNum = 0;
                            Relationship commonGreater = Relationship.unknow;
                            int greaterNum = 0;
                            foreach (string v in valuesArr)
                            {
                                if (v != "unknow")
                                {
                                    Relationship key = (Relationship)Enum.Parse(typeof(Relationship), v);
                                    if (greaterNum < freqGreater[key])
                                    {
                                        greaterNum = freqGreater[key];
                                        commonGreater = key;
                                    }
                                    if (lessEqualNum < freqLessEqua[key])
                                    {
                                        lessEqualNum = freqLessEqua[key];
                                        commonLessEqual = key;

                                    }
                                }
                            }
                            // Luu tru cac nhan common vao tu dien
                            this._mostCommonLabelGreatr.Add(AttributeCase.Relationship, commonGreater);
                            this._mostCommonLabelLessEqua.Add(AttributeCase.Relationship, commonLessEqual);
                        }
                        break; 
                    #endregion

                    #region Race
                    case AttributeCase.Race:
                        {
                            string[] valuesArr = Enum.GetNames(typeof(Race));
                            Dictionary<Race, int> freqLessEqua = new Dictionary<Race, int>();
                            Dictionary<Race, int> freqGreater = new Dictionary<Race, int>();
                            foreach (string value in valuesArr)
                            {
                                if (value != "unknow")
                                {
                                    Race va = (Race)Enum.Parse(typeof(Race), value);
                                    freqGreater.Add(va, 0);
                                    freqLessEqua.Add(va, 0);
                                }
                            }
                            foreach (Case c in this._listCase)
                            {
                                if (c.Ra != Race.unknow)
                                {
                                    if (c.IsGreater)
                                    {
                                        freqGreater[c.Ra]++;
                                    }
                                    else
                                    {
                                        freqLessEqua[c.Ra]++;
                                    }
                                }
                            }
                            // Tim nhan pho bien nhat.
                            Race commonLessEqual = Race.unknow;
                            int lessEqualNum = 0;
                            Race commonGreater = Race.unknow;
                            int greaterNum = 0;
                            foreach (string v in valuesArr)
                            {
                                if (v != "unknow")
                                {
                                    Race key = (Race)Enum.Parse(typeof(Race), v);
                                    if (greaterNum < freqGreater[key])
                                    {
                                        greaterNum = freqGreater[key];
                                        commonGreater = key;
                                    }
                                    if (lessEqualNum < freqLessEqua[key])
                                    {
                                        lessEqualNum = freqLessEqua[key];
                                        commonLessEqual = key;

                                    }
                                }
                            }
                            // Luu tru cac nhan common vao tu dien
                            this._mostCommonLabelGreatr.Add(AttributeCase.Race, commonGreater);
                            this._mostCommonLabelLessEqua.Add(AttributeCase.Race, commonLessEqual);
                        }
                        break; 
                    #endregion

                    #region Sex
                    case AttributeCase.Sex:
                        {
                            string[] valuesArr = Enum.GetNames(typeof(Sex));
                            Dictionary<Sex, int> freqLessEqua = new Dictionary<Sex, int>();
                            Dictionary<Sex, int> freqGreater = new Dictionary<Sex, int>();
                            foreach (string value in valuesArr)
                            {
                                if (value != "unknow")
                                {
                                    Sex va = (Sex)Enum.Parse(typeof(Sex), value);
                                    freqGreater.Add(va, 0);
                                    freqLessEqua.Add(va, 0);
                                }
                            }
                            foreach (Case c in this._listCase)
                            {
                                if (c.SexCase != Sex.unknow)
                                {
                                    if (c.IsGreater)
                                    {
                                        freqGreater[c.SexCase]++;
                                    }
                                    else
                                    {
                                        freqLessEqua[c.SexCase]++;
                                    }
                                }
                            }
                            // Tim nhan pho bien nhat.
                            Sex commonLessEqual = Sex.unknow;
                            int lessEqualNum = 0;
                            Sex commonGreater = Sex.unknow;
                            int greaterNum = 0;
                            foreach (string v in valuesArr)
                            {
                                if (v != "unknow")
                                {
                                    Sex key = (Sex)Enum.Parse(typeof(Sex), v);
                                    if (greaterNum < freqGreater[key])
                                    {
                                        greaterNum = freqGreater[key];
                                        commonGreater = key;
                                    }
                                    if (lessEqualNum < freqLessEqua[key])
                                    {
                                        lessEqualNum = freqLessEqua[key];
                                        commonLessEqual = key;

                                    }
                                }
                            }
                            // Luu tru cac nhan common vao tu dien
                            this._mostCommonLabelGreatr.Add(AttributeCase.Sex, commonGreater);
                            this._mostCommonLabelLessEqua.Add(AttributeCase.Sex, commonLessEqual);
                        }
                        break; 
                    #endregion

                    #region Naitive Country
                    case AttributeCase.NativeCountry:
                        {
                            string[] valuesArr = Enum.GetNames(typeof(NativeCountry));
                            Dictionary<NativeCountry, int> freqLessEqua = new Dictionary<NativeCountry, int>();
                            Dictionary<NativeCountry, int> freqGreater = new Dictionary<NativeCountry, int>();
                            foreach (string value in valuesArr)
                            {
                                if (value != "unknow")
                                {
                                    NativeCountry va = (NativeCountry)Enum.Parse(typeof(NativeCountry), value);
                                    freqGreater.Add(va, 0);
                                    freqLessEqua.Add(va, 0);
                                }
                            }
                            foreach (Case c in this._listCase)
                            {
                                if (c.NtvCountry != NativeCountry.unknow)
                                {
                                    if (c.IsGreater)
                                    {
                                        freqGreater[c.NtvCountry]++;
                                    }
                                    else
                                    {
                                        freqLessEqua[c.NtvCountry]++;
                                    }
                                }
                            }
                            // Tim nhan pho bien nhat.
                            NativeCountry commonLessEqual = NativeCountry.unknow;
                            int lessEqualNum = 0;
                            NativeCountry commonGreater = NativeCountry.unknow;
                            int greaterNum = 0;
                            foreach (string v in valuesArr)
                            {
                                if (v != "unknow")
                                {
                                    NativeCountry key = (NativeCountry)Enum.Parse(typeof(NativeCountry), v);
                                    if (greaterNum < freqGreater[key])
                                    {
                                        greaterNum = freqGreater[key];
                                        commonGreater = key;
                                    }
                                    if (lessEqualNum < freqLessEqua[key])
                                    {
                                        lessEqualNum = freqLessEqua[key];
                                        commonLessEqual = key;

                                    }
                                }
                            }
                            // Luu tru cac nhan common vao tu dien
                            this._mostCommonLabelGreatr.Add(AttributeCase.NativeCountry, commonGreater);
                            this._mostCommonLabelLessEqua.Add(AttributeCase.NativeCountry, commonLessEqual);
                        }
                        break; 
                    #endregion
                    default:
                        break;
                }
            }
            // Danh nhan cho cac truogn hop unknow ung voi moi attribute.
            foreach (Case ca in this._listCase)
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
        /// <summary>
        /// Chuẩn hóa dữ liệu trong các trường hợp bị missing value
        /// </summary>
        /// <param name="data">Dữ liệu cần chuẩn hóa</param>
        public void CleanData(Case[] data)
        {
            // Danh nhan cho cac truogn hop unknow ung voi moi attribute.
            foreach (Case ca in data)
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

        #region Count Cases
        /// <summary>
        /// Đếm số case có attribute nào đó có giá trị value
        /// </summary>
        /// <param name="attr">Attribute</param>
        /// <param name="value">value</param>
        /// <returns>Number Case</returns>
        public double CountCase(AttributeCase attr, object value, StateCompare compare)
        {
            double count = 0;
            switch (attr)
            {
                #region Attribute Age
                // Gia tri lien tuc
                case AttributeCase.Age:
                    {
                        double v = (double)value;
                        switch (compare)
                        {
                            case StateCompare.Greater:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.Age > v)
                                        count++;
                                }
                                break;
                            case StateCompare.Less:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.Age < v)
                                        count++;
                                }
                                break;
                            case StateCompare.GreaterEqual:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.Age >= v)
                                        count++;
                                }
                                break;
                            case StateCompare.LessEqua:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.Age <= v)
                                        count++;
                                }
                                break;
                            case StateCompare.Equal:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.Age == v)
                                        count++;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                #endregion

                #region Attribute Work Class
                // Gia tri roi rac
                case AttributeCase.WorkClass:
                    {
                        WorkClass wc = (WorkClass)value;

                        foreach (Case ca in this._listCase)
                        {
                            if (ca.Wcl == wc)
                                count++;
                        }
                    }
                    break;
                #endregion

                #region Attribute Fnlwgt
                // Gia tri lien tuc
                case AttributeCase.Fnlwgt:
                    {
                        double v = (double)value;
                        switch (compare)
                        {
                            case StateCompare.Greater:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.Fnlwgt > v)
                                        count++;
                                }
                                break;
                            case StateCompare.Less:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.Fnlwgt < v)
                                        count++;
                                }
                                break;
                            case StateCompare.GreaterEqual:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.Fnlwgt >= v)
                                        count++;
                                }
                                break;
                            case StateCompare.LessEqua:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.Fnlwgt <= v)
                                        count++;
                                }
                                break;
                            case StateCompare.Equal:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.Fnlwgt == v)
                                        count++;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                #endregion

                #region Attribute Education
                // Roi rac.
                case AttributeCase.Education:
                    {
                        Education edu = (Education)value;
                        foreach (Case ca in this._listCase)
                        {
                            if (ca.Edu == edu)
                                count++;
                        }
                    }
                    break;
                #endregion

                #region Attribute EducationNum
                // Gia tri lien tuc
                case AttributeCase.EducationNum:
                    {
                        double v = (double)value;
                        switch (compare)
                        {
                            case StateCompare.Greater:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.EduNum > v)
                                        count++;
                                }
                                break;
                            case StateCompare.Less:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.EduNum < v)
                                        count++;
                                }
                                break;
                            case StateCompare.GreaterEqual:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.EduNum >= v)
                                        count++;
                                }
                                break;
                            case StateCompare.LessEqua:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.EduNum <= v)
                                        count++;
                                }
                                break;
                            case StateCompare.Equal:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.EduNum == v)
                                        count++;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                #endregion

                #region Attribute MaritalStatus
                // Roi rac.
                case AttributeCase.MaritalStatus:
                    {
                        MaritalStatus mari = (MaritalStatus)value;
                        foreach (Case ca in this._listCase)
                        {
                            if (ca.MariSt == mari)
                                count++;
                        }
                    }
                    break;
                #endregion

                #region Attribute Occupation
                // Roi rac
                case AttributeCase.Occupation:
                    {
                        Occupation occ = (Occupation)value;
                        foreach (Case ca in this._listCase)
                        {
                            if (ca.Occ == occ)
                                count++;
                        }
                    }
                    break;
                #endregion

                #region Attribute Relationship
                // Roi rac
                case AttributeCase.Relationship:
                    {
                        Relationship rls = (Relationship)value;
                        foreach (Case ca in this._listCase)
                        {
                            if (ca.Reltsh == rls)
                                count++;
                        }
                    }
                    break;
                #endregion

                #region Attribute Race
                // Roi rac
                case AttributeCase.Race:
                    {
                        Race r = (Race)value;
                        foreach (Case ca in this._listCase)
                        {
                            if (ca.Ra == r)
                                count++;
                        }
                    }
                    break;
                #endregion

                #region Attribute Sex
                // Roi rac
                case AttributeCase.Sex:
                    {
                        Sex s = (Sex)value;
                        foreach (Case ca in this._listCase)
                        {
                            if (ca.SexCase == s)
                                count++;
                        }
                    }
                    break;
                #endregion

                #region Attribute CapitalGain
                // Lien tuc
                case AttributeCase.CapitalGain:
                    {
                        double v = (double)value;
                        switch (compare)
                        {
                            case StateCompare.Greater:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.CapitalGain > v)
                                        count++;
                                }
                                break;
                            case StateCompare.Less:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.CapitalGain < v)
                                        count++;
                                }
                                break;
                            case StateCompare.GreaterEqual:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.CapitalGain >= v)
                                        count++;
                                }
                                break;
                            case StateCompare.LessEqua:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.CapitalGain <= v)
                                        count++;
                                }
                                break;
                            case StateCompare.Equal:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.CapitalGain == v)
                                        count++;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                #endregion

                #region Attribute Capital Loss
                // Lien tuc
                case AttributeCase.CapitalLoss:
                    {
                        double v = (double)value;
                        switch (compare)
                        {
                            case StateCompare.Greater:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.CapitalLoss > v)
                                        count++;
                                }
                                break;
                            case StateCompare.Less:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.CapitalLoss < v)
                                        count++;
                                }
                                break;
                            case StateCompare.GreaterEqual:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.CapitalLoss >= v)
                                        count++;
                                }
                                break;
                            case StateCompare.LessEqua:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.CapitalLoss <= v)
                                        count++;
                                }
                                break;
                            case StateCompare.Equal:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.CapitalLoss == v)
                                        count++;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                #endregion

                #region Attribute Hours Per Week
                // Lien tuc
                case AttributeCase.HoursPerWeek:
                    {
                        double v = (double)value;
                        switch (compare)
                        {
                            case StateCompare.Greater:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.HoursPerWeek > v)
                                        count++;
                                }
                                break;
                            case StateCompare.Less:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.HoursPerWeek < v)
                                        count++;
                                }
                                break;
                            case StateCompare.GreaterEqual:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.HoursPerWeek >= v)
                                        count++;
                                }
                                break;
                            case StateCompare.LessEqua:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.HoursPerWeek <= v)
                                        count++;
                                }
                                break;
                            case StateCompare.Equal:
                                foreach (Case ca in this._listCase)
                                {
                                    if (ca.HoursPerWeek == v)
                                        count++;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                #endregion

                #region Attribute Naitive Country
                // Roi rac
                case AttributeCase.NativeCountry:
                    {
                        NativeCountry n = (NativeCountry)value;
                        foreach (Case ca in this._listCase)
                        {
                            if (ca.NtvCountry == n)
                                count++;
                        }
                    }
                    break;
                #endregion
                default:
                    break;
            }
            return count;
        } 
        #endregion
        
        #region Load Data
        public void LoadData(string path)
        {
            string[] arrName = Enum.GetNames(typeof(AttributeCase));
            this._listCase = this._file.ReadFile(path);
            this._numberCase = this._listCase.Length;

            #region Initial Threshold Array

            this._thresholdArr = new Dictionary<AttributeCase, double[]>();

            double[] thresArr = null;
            double[] pos = null;
            foreach (string str in arrName)
            {
                AttributeCase attr = (AttributeCase)Enum.Parse(typeof(AttributeCase), str);
                switch (attr)
                {
                    #region Age Attribute
                    case AttributeCase.Age:
                        {
                            thresArr = this.GetThreshold(AttributeCase.Age, new AgeComparer());
                            this._thresholdArr.Add(AttributeCase.Age, thresArr);
                            pos = new double[2] { 0, thresArr.Length };
                        }
                        break;
                    #endregion

                    #region Fnlwgt Attribute
                    case AttributeCase.Fnlwgt:
                        {
                            thresArr = this.GetThreshold(AttributeCase.Fnlwgt, new FnlwgtComparer());
                            this._thresholdArr.Add(AttributeCase.Fnlwgt, thresArr);
                            pos = new double[2] { 0, thresArr.Length };
                        }
                        break;
                    #endregion

                    #region Education Num Attribute
                    case AttributeCase.EducationNum:
                        {
                            thresArr = this.GetThreshold(AttributeCase.EducationNum, new EduNumComparer());
                            this._thresholdArr.Add(AttributeCase.EducationNum, thresArr);
                            pos = new double[2] { 0, thresArr.Length };
                        }
                        break;
                    #endregion

                    #region Capital Gain Attribute
                    case AttributeCase.CapitalGain:
                        {
                            thresArr = this.GetThreshold(AttributeCase.CapitalGain, new CapitalGainComparer());
                            this._thresholdArr.Add(AttributeCase.CapitalGain, thresArr);
                            pos = new double[2] { 0, thresArr.Length };
                        }
                        break;
                    #endregion

                    #region Capital Loss Attribute
                    case AttributeCase.CapitalLoss:
                        {
                            thresArr = this.GetThreshold(AttributeCase.CapitalLoss, new CapitalLossComparer());
                            this._thresholdArr.Add(AttributeCase.CapitalLoss, thresArr);
                            pos = new double[2] { 0, thresArr.Length };
                        }
                        break;
                    #endregion

                    #region Hours Per Week
                    case AttributeCase.HoursPerWeek:
                        {
                            thresArr = this.GetThreshold(AttributeCase.HoursPerWeek, new HoursPWComparer());
                            this._thresholdArr.Add(AttributeCase.HoursPerWeek, thresArr);
                            pos = new double[2] { 0, thresArr.Length };
                        }
                        break;
                    #endregion
                    default:
                        break;
                }
            }
            #endregion
        }
        public void LoadData(string path, BackgroundWorker worker)
        {
            string[] arrName = Enum.GetNames(typeof(AttributeCase));
            this._listCase = this._file.ReadFile(path, worker, (int)1 / 5);
            this._numberCase = this._listCase.Length;

            #region Initial Threshold Array

            this._thresholdArr = new Dictionary<AttributeCase, double[]>();

            double[] thresArr = null;
            double[] pos = null;

            double n = 5 * this._numberCase;
            double temp = 2 * n / 15;
            double count = this._numberCase;

            foreach (string str in arrName)
            {
                AttributeCase attr = (AttributeCase)Enum.Parse(typeof(AttributeCase), str);
                switch (attr)
                {
                    #region Age Attribute
                    case AttributeCase.Age:
                        {
                            thresArr = this.GetThreshold(AttributeCase.Age, new AgeComparer());
                            this._thresholdArr.Add(AttributeCase.Age, thresArr);
                            pos = new double[2] { 0, thresArr.Length };
                            // Tinh process
                            count += temp;
                            worker.ReportProgress((int)(count * 100 / n));
                        }
                        break;
                    #endregion

                    #region Fnlwgt Attribute
                    case AttributeCase.Fnlwgt:
                        {
                            thresArr = this.GetThreshold(AttributeCase.Fnlwgt, new FnlwgtComparer());
                            this._thresholdArr.Add(AttributeCase.Fnlwgt, thresArr);
                            pos = new double[2] { 0, thresArr.Length };
                            // Tinh process
                            count += temp;
                            worker.ReportProgress((int)(count * 100 / n));
                        }
                        break;
                    #endregion

                    #region Education Num Attribute
                    case AttributeCase.EducationNum:
                        {
                            thresArr = this.GetThreshold(AttributeCase.EducationNum, new EduNumComparer());
                            this._thresholdArr.Add(AttributeCase.EducationNum, thresArr);
                            pos = new double[2] { 0, thresArr.Length };
                            // Tinh process
                            count += temp;
                            worker.ReportProgress((int)(count * 100 / n));
                        }
                        break;
                    #endregion

                    #region Capital Gain Attribute
                    case AttributeCase.CapitalGain:
                        {
                            thresArr = this.GetThreshold(AttributeCase.CapitalGain, new CapitalGainComparer());
                            this._thresholdArr.Add(AttributeCase.CapitalGain, thresArr);
                            pos = new double[2] { 0, thresArr.Length };
                            // Tinh process
                            count += temp;
                            worker.ReportProgress((int)(count * 100 / n));
                        }
                        break;
                    #endregion

                    #region Capital Loss Attribute
                    case AttributeCase.CapitalLoss:
                        {
                            thresArr = this.GetThreshold(AttributeCase.CapitalLoss, new CapitalLossComparer());
                            this._thresholdArr.Add(AttributeCase.CapitalLoss, thresArr);
                            pos = new double[2] { 0, thresArr.Length };
                            // Tinh process
                            count += temp;
                            worker.ReportProgress((int)(count * 100 / n));
                        }
                        break;
                    #endregion

                    #region Hours Per Week
                    case AttributeCase.HoursPerWeek:
                        {
                            thresArr = this.GetThreshold(AttributeCase.HoursPerWeek, new HoursPWComparer());
                            this._thresholdArr.Add(AttributeCase.HoursPerWeek, thresArr);
                            pos = new double[2] { 0, thresArr.Length };
                            // Tinh process
                            count += temp;
                            worker.ReportProgress((int)(count * 100 / n));
                        }
                        break;
                    #endregion
                    default:
                        break;
                }
            }
            #endregion
        }
        #endregion

        #region IEnumerable Methods

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _listCase.GetEnumerator();
        }

        public IEnumerator<Case> GetEnumerator()
        {
            foreach (Case c in this._listCase)
            {
                yield return c;
            }
        }
        #endregion

        #region Tuan Long's Method

        #region Get NB Threshold
        /// <summary>
        /// Trả lại một mảng các thrshold của bộ dữ liệu
        /// </summary>
        /// <param name="continuousAttr">Thuộc tính liên tục cần rời rạc hóa</param>
        /// <param name="comparer">Bộ so sánh, để xác định thuộc tính liên tục nào được chọn</param>
        /// <returns>Mảng các threshold</returns>
        /// 
        public double[] GetNBThreshold(AttributeCase continuousAttr, IComparer<Case> comparer)
        {
            List<double> thsArr = new List<double>();
            int n = this._listCase.Count();
            int i = 0;
            double begin = 0;
            double end = 0;

            if (comparer != null)
            {
                //Sắp xếp rồi bắt đầu tính khoảng.
                Array.Sort(this._listCase, comparer);
                switch (continuousAttr)
                {
                    #region Age
                    case AttributeCase.Age:
                        {
                            while (i < n - 1)
                            {
                                begin = this._listCase[i].Age;
                                i++;
                                while (i < n && this._listCase[i].Age == begin)
                                    i++;
                                if (i < n)
                                {
                                    end = this._listCase[i].Age;
                                    thsArr.Add(begin);
                                }
                            }
                            thsArr.Add(end);
                        }
                        break;
                    #endregion

                    #region EducationNum
                    case AttributeCase.EducationNum:
                        {
                            while (i < n - 1)
                            {
                                begin = this._listCase[i].EduNum;
                                i++;
                                while (i < n && this._listCase[i].EduNum == begin)
                                    i++;
                                if (i < n)
                                {
                                    end = this._listCase[i].EduNum;
                                    thsArr.Add(begin);
                                }
                            }
                            thsArr.Add(end);
                        }
                        break;
                    #endregion

                    #region CapitalGain
                    case AttributeCase.CapitalGain:
                        {
                            while (i < n - 1)
                            {
                                begin = this._listCase[i].CapitalGain;
                                i++;
                                while (i < n && this._listCase[i].CapitalGain == begin)
                                    i++;
                                if (i < n)
                                {
                                    end = this._listCase[i].CapitalGain;
                                    thsArr.Add(begin);
                                }
                            }
                            thsArr.Add(end);
                        }
                        break;
                    #endregion

                    #region Capital Loss
                    case AttributeCase.CapitalLoss:
                        {
                            while (i < n - 1)
                            {
                                begin = this._listCase[i].CapitalLoss;
                                i++;
                                while (i < n && this._listCase[i].CapitalLoss == begin)
                                    i++;
                                if (i < n)
                                {
                                    end = this._listCase[i].CapitalLoss;
                                    thsArr.Add(begin);
                                }
                            }
                            thsArr.Add(end);
                        }
                        break;
                    #endregion

                    #region Fnlwgt
                    case AttributeCase.Fnlwgt:
                        {
                            while (i < n - 1)
                            {
                                begin = this._listCase[i].Fnlwgt;
                                i++;
                                while (i < n && this._listCase[i].Fnlwgt == begin)
                                    i++;
                                if (i < n)
                                {
                                    end = this._listCase[i].Fnlwgt;
                                    thsArr.Add(begin);
                                }
                            }
                            thsArr.Add(end);
                        }
                        break;
                    #endregion

                    #region Hours Per Week
                    case AttributeCase.HoursPerWeek:
                        {
                            while (i < n - 1)
                            {
                                begin = this._listCase[i].HoursPerWeek;
                                i++;
                                while (i < n && this._listCase[i].HoursPerWeek == begin)
                                    i++;
                                if (i < n)
                                {
                                    end = this._listCase[i].HoursPerWeek;
                                    thsArr.Add(begin);
                                }
                            }
                            thsArr.Add(end);
                        }
                        break;
                    #endregion
                    default:
                        break;
                }
            }
            return thsArr.ToArray();
        } 
        #endregion

        #region NB Statistic
        /// <summary>
        /// Thống kê số trường hợp lớn hơn và nhỏ hơn 50K$ ứng với value của một
        /// thuộc tính nào đó.
        /// </summary>
        /// <param name="attr">Thuộc tính</param>
        /// <param name="threshold">thresholde của thuộc tính liên tục</param>
        /// <returns>Mảng các đối tượng Statistic- ứng với từng value của thuộc tính rời rạc và 
        /// với thuộc tính liên tục là hai khoảng lớn hơn và nhỏ</returns>
        public StatictisObject[] NBStatistic(AttributeCase attr, double[] thresholds)
        {
            StatictisObject[] caseArr = null;
            DiscreteIndex index = new DiscreteIndex();
            switch (attr)
            {
                #region Attribute Age
                // Gia tri lien tuc
                case AttributeCase.Age:
                    {
                        double[] numberLessEqual = new double[thresholds.Length];
                        double[] numberGreater = new double[thresholds.Length];

                        // Dem so truong hop nho hon va lon hon threshold co thu nhap lon hon va nho 50K$
                        foreach (Case item in this._listCase)
                        {
                            // Trường hợp nhỏ hơn threshold
                            for (int i = 0; i < thresholds.Length - 1; i++)
                            {
                                if (item.Age >= thresholds[i] && item.Age < thresholds[i + 1])
                                {
                                    // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                    if (item.IsGreater == false)
                                        numberLessEqual[i]++;
                                    else // Có thu nhập lớn hơn 50K$
                                        numberGreater[i]++;
                                    break;
                                }
                                if (item.Age == thresholds[thresholds.Length - 1])
                                {
                                    // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                    if (item.IsGreater == false)
                                        numberLessEqual[thresholds.Length - 1]++;
                                    else // Có thu nhập lớn 50K$
                                        numberGreater[thresholds.Length - 1]++;
                                }
                            }
                        }

                        caseArr = new StatictisObject[thresholds.Length];
                        for (int i = 0; i <= thresholds.Length - 1; i++)
                            caseArr[i] = new StatictisObject(numberGreater[i], numberLessEqual[i]);
                    }
                    break;
                #endregion

                #region Attribute Work Class
                // Gia tri roi rac
                case AttributeCase.WorkClass:
                    {
                        // So value ung voi work class attribute
                        int n = index.WorkClassValues;
                        caseArr = new StatictisObject[n];
                        int i = 0;
                        string[] strArr = Enum.GetNames(typeof(WorkClass));
                        foreach (string str in strArr)
                        {
                            if (str != "unknow")
                            {
                                WorkClass wc = (WorkClass)Enum.Parse(typeof(WorkClass), str);
                                double numberLessEqua = 0;
                                double numberGreater = 0;
                                foreach (Case item in this._listCase)
                                {
                                    if (item.Wcl != WorkClass.unknow && item.Wcl == wc)
                                    {
                                        // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                        if (item.IsGreater == false)
                                            numberLessEqua++;
                                        else // Có thu nhập lớn hơn 50K$
                                            numberGreater++;
                                    }
                                }
                                StatictisObject so = new StatictisObject(numberGreater, numberLessEqua);
                                caseArr[i] = so;
                                i++;
                            }
                        }
                    }
                    break;
                #endregion

                #region Attribute Fnlwgt
                // Gia tri lien tuc
                case AttributeCase.Fnlwgt:
                    {
                        double[] numberLessEqual = new double[thresholds.Length];
                        double[] numberGreater = new double[thresholds.Length];

                        // Dem so truong hop nho hon va lon hon threshold co thu nhap lon hon va nho 50K$
                        foreach (Case item in this._listCase)
                        {
                            // Trường hợp nhỏ hơn threshold
                            for (int i = 0; i < thresholds.Length - 1; i++)
                            {
                                if (item.Fnlwgt >= thresholds[i] && item.Fnlwgt < thresholds[i + 1])
                                {
                                    // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                    if (item.IsGreater == false)
                                        numberLessEqual[i]++;
                                    else // Có thu nhập lớn hơn 50K$
                                        numberGreater[i]++;
                                }
                                if (item.Fnlwgt == thresholds[thresholds.Length - 1])
                                {
                                    // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                    if (item.IsGreater == false)
                                        numberLessEqual[thresholds.Length - 1]++;
                                    else // Có thu nhập lớn 50K$
                                        numberGreater[thresholds.Length - 1]++;
                                }
                            }
                        }

                        caseArr = new StatictisObject[thresholds.Length];
                        for (int i = 0; i <= thresholds.Length - 1; i++)
                            caseArr[i] = new StatictisObject(numberGreater[i], numberLessEqual[i]);
                    }
                    break;
                #endregion

                #region Attribute Education
                // Roi rac.
                case AttributeCase.Education:
                    {
                        // So value ung voi work class attribute
                        int n = index.EducationValues;
                        caseArr = new StatictisObject[n];
                        int i = 0;
                        string[] strArr = Enum.GetNames(typeof(Education));
                        foreach (string str in strArr)
                        {
                            if (str != "unknow")
                            {
                                Education ed = (Education)Enum.Parse(typeof(Education), str);
                                double numberLessEqua = 0;
                                double numberGreater = 0;
                                foreach (Case item in this._listCase)
                                {
                                    if (item.Edu != Education.unknow && item.Edu == ed)
                                    {
                                        // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                        if (item.IsGreater == false)
                                            numberLessEqua++;
                                        else // Có thu nhập lớn hơn 50K$
                                            numberGreater++;
                                    }
                                }
                                StatictisObject so = new StatictisObject(numberGreater, numberLessEqua);
                                caseArr[i] = so;
                                i++;
                            }
                        }
                    }
                    break;
                #endregion

                #region Attribute EducationNum
                // Gia tri lien tuc
                case AttributeCase.EducationNum:
                    {
                        double[] numberLessEqual = new double[thresholds.Length];
                        double[] numberGreater = new double[thresholds.Length];

                        // Dem so truong hop nho hon va lon hon threshold co thu nhap lon hon va nho 50K$
                        foreach (Case item in this._listCase)
                        {
                            // Trường hợp nhỏ hơn threshold
                            for (int i = 0; i < thresholds.Length - 1; i++)
                            {
                                if (item.EduNum >= thresholds[i] && item.EduNum < thresholds[i + 1])
                                {
                                    // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                    if (item.IsGreater == false)
                                        numberLessEqual[i]++;
                                    else // Có thu nhập lớn hơn 50K$
                                        numberGreater[i]++;
                                }
                                if (item.EduNum == thresholds[thresholds.Length - 1])
                                {
                                    // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                    if (item.IsGreater == false)
                                        numberLessEqual[thresholds.Length - 1]++;
                                    else // Có thu nhập lớn 50K$
                                        numberGreater[thresholds.Length - 1]++;

                                }
                            }
                        }

                        caseArr = new StatictisObject[thresholds.Length];
                        for (int i = 0; i <= thresholds.Length - 1; i++)
                            caseArr[i] = new StatictisObject(numberGreater[i], numberLessEqual[i]);
                    }
                    break;
                #endregion

                #region Attribute MaritalStatus
                // Roi rac.
                case AttributeCase.MaritalStatus:
                    {
                        // So value ung voi work class attribute
                        int n = index.MaritalStatusValues;
                        caseArr = new StatictisObject[n];
                        int i = 0;
                        string[] strArr = Enum.GetNames(typeof(MaritalStatus));
                        foreach (string str in strArr)
                        {
                            if (str != "unknow")
                            {
                                MaritalStatus ms = (MaritalStatus)Enum.Parse(typeof(MaritalStatus), str);
                                double numberLessEqua = 0;
                                double numberGreater = 0;
                                foreach (Case item in this._listCase)
                                {
                                    if (item.MariSt != MaritalStatus.unknow && item.MariSt == ms)
                                    {
                                        // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                        if (item.IsGreater == false)
                                            numberLessEqua++;
                                        else // Có thu nhập lớn hơn 50K$
                                            numberGreater++;
                                    }
                                }
                                StatictisObject so = new StatictisObject(numberGreater, numberLessEqua);
                                caseArr[i] = so;
                                i++;
                            }
                        }
                    }
                    break;
                #endregion

                #region Attribute Occupation
                // Roi rac
                case AttributeCase.Occupation:
                    {
                        // So value ung voi work class attribute
                        int n = index.OccupationValues;
                        caseArr = new StatictisObject[n];
                        int i = 0;
                        string[] strArr = Enum.GetNames(typeof(Occupation));
                        foreach (string str in strArr)
                        {
                            if (str != "unknow")
                            {
                                Occupation occ = (Occupation)Enum.Parse(typeof(Occupation), str);
                                double numberLessEqua = 0;
                                double numberGreater = 0;
                                foreach (Case item in this._listCase)
                                {
                                    if (item.Occ != Occupation.unknow && item.Occ == occ)
                                    {
                                        // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                        if (item.IsGreater == false)
                                            numberLessEqua++;
                                        else // Có thu nhập lớn hơn 50K$
                                            numberGreater++;
                                    }
                                }
                                StatictisObject so = new StatictisObject(numberGreater, numberLessEqua);
                                caseArr[i] = so;
                                i++;
                            }
                        }
                    }
                    break;
                #endregion

                #region Attribute Relationship
                // Roi rac
                case AttributeCase.Relationship:
                    {
                        // So value ung voi work class attribute
                        int n = index.RelationshipValues;
                        caseArr = new StatictisObject[n];
                        int i = 0;
                        string[] strArr = Enum.GetNames(typeof(Relationship));
                        foreach (string str in strArr)
                        {
                            if (str != "unknow")
                            {
                                Relationship re = (Relationship)Enum.Parse(typeof(Relationship), str);
                                double numberLessEqua = 0;
                                double numberGreater = 0;
                                foreach (Case item in this._listCase)
                                {
                                    if (item.Reltsh != Relationship.unknow && item.Reltsh == re)
                                    {
                                        // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                        if (item.IsGreater == false)
                                            numberLessEqua++;
                                        else // Có thu nhập lớn hơn 50K$
                                            numberGreater++;
                                    }
                                }
                                StatictisObject so = new StatictisObject(numberGreater, numberLessEqua);
                                caseArr[i] = so;
                                i++;
                            }
                        }
                    }
                    break;
                #endregion

                #region Attribute Race
                // Roi rac
                case AttributeCase.Race:
                    {
                        // So value ung voi work class attribute
                        int n = index.RaceValues;
                        caseArr = new StatictisObject[n];
                        int i = 0;
                        string[] strArr = Enum.GetNames(typeof(Race));
                        foreach (string str in strArr)
                        {
                            if (str != "unknow")
                            {
                                Race ra = (Race)Enum.Parse(typeof(Race), str);
                                double numberLessEqua = 0;
                                double numberGreater = 0;
                                foreach (Case item in this._listCase)
                                {
                                    if (item.Ra != Race.unknow && item.Ra == ra)
                                    {
                                        // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                        if (item.IsGreater == false)
                                            numberLessEqua++;
                                        else // Có thu nhập lớn hơn 50K$
                                            numberGreater++;
                                    }
                                }
                                StatictisObject so = new StatictisObject(numberGreater, numberLessEqua);
                                caseArr[i] = so;
                                i++;
                            }
                        }
                    }
                    break;
                #endregion

                #region Attribute Sex
                // Roi rac
                case AttributeCase.Sex:
                    {
                        // So value ung voi work class attribute
                        int n = index.SexValues;
                        caseArr = new StatictisObject[n];
                        int i = 0;
                        string[] strArr = Enum.GetNames(typeof(Sex));
                        foreach (string str in strArr)
                        {
                            if (str != "unknow")
                            {
                                Sex sex = (Sex)Enum.Parse(typeof(Sex), str);
                                double numberLessEqua = 0;
                                double numberGreater = 0;
                                foreach (Case item in this._listCase)
                                {
                                    if (item.SexCase != Sex.unknow && item.SexCase == sex)
                                    {
                                        // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                        if (item.IsGreater == false)
                                            numberLessEqua++;
                                        else // Có thu nhập lớn hơn 50K$
                                            numberGreater++;
                                    }
                                }
                                StatictisObject so = new StatictisObject(numberGreater, numberLessEqua);
                                caseArr[i] = so;
                                i++;
                            }
                        }
                    }
                    break;
                #endregion

                #region Attribute CapitalGain
                // Lien tuc
                case AttributeCase.CapitalGain:
                    {
                        double[] numberLessEqual = new double[thresholds.Length];
                        double[] numberGreater = new double[thresholds.Length];

                        // Dem so truong hop nho hon va lon hon threshold co thu nhap lon hon va nho 50K$
                        foreach (Case item in this._listCase)
                        {
                            // Trường hợp nhỏ hơn threshold
                            for (int i = 0; i < thresholds.Length - 1; i++)
                            {
                                if (item.CapitalGain >= thresholds[i] && item.CapitalGain < thresholds[i + 1])
                                {
                                    // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                    if (item.IsGreater == false)
                                        numberLessEqual[i]++;
                                    else // Có thu nhập lớn hơn 50K$
                                        numberGreater[i]++;
                                }
                                if (item.CapitalGain == thresholds[thresholds.Length - 1])
                                {
                                    // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                    if (item.IsGreater == false)
                                        numberLessEqual[thresholds.Length - 1]++;
                                    else // Có thu nhập lớn 50K$
                                        numberGreater[thresholds.Length - 1]++;
                                }
                            }
                        }

                        caseArr = new StatictisObject[thresholds.Length];
                        for (int i = 0; i <= thresholds.Length - 1; i++)
                            caseArr[i] = new StatictisObject(numberGreater[i], numberLessEqual[i]);
                    }
                    break;
                #endregion

                #region Attribute Capital Loss
                // Lien tuc
                case AttributeCase.CapitalLoss:
                    {
                        double[] numberLessEqual = new double[thresholds.Length];
                        double[] numberGreater = new double[thresholds.Length];

                        // Dem so truong hop nho hon va lon hon threshold co thu nhap lon hon va nho 50K$
                        foreach (Case item in this._listCase)
                        {
                            // Trường hợp nhỏ hơn threshold
                            for (int i = 0; i < thresholds.Length - 1; i++)
                            {
                                if (item.CapitalLoss >= thresholds[i] && item.CapitalLoss < thresholds[i + 1])
                                {
                                    // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                    if (item.IsGreater == false)
                                        numberLessEqual[i]++;
                                    else // Có thu nhập lớn hơn 50K$
                                        numberGreater[i]++;
                                }
                                if (item.CapitalLoss == thresholds[thresholds.Length - 1])
                                {
                                    // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                    if (item.IsGreater == false)
                                        numberLessEqual[thresholds.Length - 1]++;
                                    else // Có thu nhập lớn 50K$
                                        numberGreater[thresholds.Length - 1]++;
                                }
                            }
                        }

                        caseArr = new StatictisObject[thresholds.Length];
                        for (int i = 0; i <= thresholds.Length - 2; i++)
                            caseArr[i] = new StatictisObject(numberGreater[i], numberLessEqual[i]);
                    }
                    break;
                #endregion

                #region Attribute Hours Per Week
                // Lien tuc
                case AttributeCase.HoursPerWeek:
                    {
                        double[] numberLessEqual = new double[thresholds.Length];
                        double[] numberGreater = new double[thresholds.Length];

                        // Dem so truong hop nho hon va lon hon threshold co thu nhap lon hon va nho 50K$
                        foreach (Case item in this._listCase)
                        {
                            // Trường hợp nhỏ hơn threshold
                            for (int i = 0; i < thresholds.Length - 1; i++)
                            {
                                if (item.HoursPerWeek >= thresholds[i] && item.HoursPerWeek < thresholds[i + 1])
                                {
                                    // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                    if (item.IsGreater == false)
                                        numberLessEqual[i]++;
                                    else // Có thu nhập lớn hơn 50K$
                                        numberGreater[i]++;
                                }
                                if (item.HoursPerWeek == thresholds[thresholds.Length - 1])
                                {
                                    // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                    if (item.IsGreater == false)
                                        numberLessEqual[thresholds.Length - 1]++;
                                    else // Có thu nhập lớn 50K$
                                        numberGreater[thresholds.Length - 1]++;
                                }
                            }
                        }

                        caseArr = new StatictisObject[thresholds.Length];
                        for (int i = 0; i <= thresholds.Length - 1; i++)
                            caseArr[i] = new StatictisObject(numberGreater[i], numberLessEqual[i]);
                    }
                    break;
                #endregion

                #region Attribute Naitive Country
                // Roi rac
                case AttributeCase.NativeCountry:
                    {
                        // So value ung voi work class attribute
                        int n = index.NaitiveCountryValues;
                        caseArr = new StatictisObject[n];
                        int i = 0;
                        string[] strArr = Enum.GetNames(typeof(NativeCountry));
                        foreach (string str in strArr)
                        {
                            if (str != "unknow")
                            {
                                NativeCountry na = (NativeCountry)Enum.Parse(typeof(NativeCountry), str);
                                double numberLessEqua = 0;
                                double numberGreater = 0;
                                foreach (Case item in this._listCase)
                                {
                                    if (item.NtvCountry != NativeCountry.unknow && item.NtvCountry == na)
                                    {
                                        // Có thu nhập nhỏ hơn hoặc bằng 50K$
                                        if (item.IsGreater == false)
                                            numberLessEqua++;
                                        else // Có thu nhập lớn hơn 50K$
                                            numberGreater++;
                                    }
                                }
                                StatictisObject so = new StatictisObject(numberGreater, numberLessEqua);
                                caseArr[i] = so;
                                i++;
                            }
                        }
                    }
                    break;
                #endregion

                default:
                    break;
            }
            return caseArr;
        }
        #endregion

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Number case of the data set
        /// </summary>
        public int NumberCase
        {
            get { return this._numberCase; }
        }
        /// <summary>
        /// Array of cases
        /// </summary>
        /// <param name="i">index of certain case</param>
        /// <returns>Case</returns>
        public Case this[int i]
        {
            get { return this._listCase[i]; }
        }
        /// <summary>
        /// Array Cases
        /// </summary>
        public Case[] Cases
        {
            get { return this._listCase; }
        }
        /// <summary>
        /// lay gia tri info S de tinh gain ung voi mot thuoc tinh nao do
        /// </summary>
        public Dictionary<AttributeCase, double> Info
        {
            get { return _info; }
        }
        /// <summary>
        /// Lấy giá trị bị missing ở một attribute nào đó
        /// </summary>
        public Dictionary<AttributeCase, double> MissingNum
        {
            get { return _missingNum; }
        }
        /// <summary>
        /// Lay mang threshold ung voi mot attribute nao do
        /// </summary>
        public Dictionary<AttributeCase, double[]> ThresholdArr
        {
            get { return _thresholdArr; }
        }
        /// <summary>
        /// Nhãn phổ biến của một attribute nào đó ứng với trường hợp lớn hơn 50k$
        /// </summary>
        public Dictionary<AttributeCase, object> MostCommonLabelGreatr
        {
            get { return _mostCommonLabelGreatr; }
        }
        /// <summary>
        /// Nhãn phổ biến của một attribute nào đó ứng với trường hợp nhỏ hơn hoặc bằng 50k$
        /// </summary>
        public Dictionary<AttributeCase, object> MostCommonLabelLessEqua
        {
            get { return _mostCommonLabelLessEqua; }
        }
        #endregion
    }
}