using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    /// <summary>
    /// Xác định vị trí một giá trị trong tập các giá trị rời rạc của một thuộc tính rời rạc nào đó.
    /// </summary>
    [Serializable]
    public class DiscreteIndex:IDiscreteIndex
    {
        #region Private Index
        private Dictionary<WorkClass, int> workClassIndex;
        private Dictionary<Education, int> educationIndex;
        private Dictionary<MaritalStatus, int> maritalStatusIndex;
        private Dictionary<Occupation, int> occupationIndex;
        private Dictionary<Relationship, int> relationshipIndex;
        private Dictionary<Race, int> raceIndex;
        private Dictionary<Sex, int> sexIndex;
        private Dictionary<NativeCountry, int> naitiveCountryIndex;
        #endregion

        #region Constructor
        public DiscreteIndex()
        {
            int n = 0;
            int i = 0;
            #region Work Class
            
            this.workClassIndex = new Dictionary<WorkClass, int>();
            string[] nameArr = Enum.GetNames(typeof(WorkClass));
            n = nameArr.Length;

            for (i = 0; i < n; i++)
            {
                if (nameArr[i]!="unknow")
                {
                    WorkClass wc = (WorkClass)Enum.Parse(typeof(WorkClass), nameArr[i]);
                    this.workClassIndex.Add(wc, i); 
                }
            }
            
            #endregion

            #region Education

            this.educationIndex = new Dictionary<Education, int>();
            nameArr = Enum.GetNames(typeof(Education));
            n = nameArr.Length;

            for (i = 0; i < n; i++)
            {
                if (nameArr[i]!="unknow")
                {
                    Education edu = (Education)Enum.Parse(typeof(Education), nameArr[i]);
                    this.educationIndex.Add(edu, i); 
                }
            }
            
            #endregion

            #region Marital Status
            
            this.maritalStatusIndex = new Dictionary<MaritalStatus, int>();

            nameArr = Enum.GetNames(typeof(MaritalStatus));
            n = nameArr.Length;

            for (i = 0; i < n; i++)
            {
                if (nameArr[i]!="unknow")
                {
                    MaritalStatus mrs = (MaritalStatus)Enum.Parse(typeof(MaritalStatus), nameArr[i]);
                    this.maritalStatusIndex.Add(mrs, i); 
                }
            }

            #endregion

            #region Occupation
            
            this.occupationIndex = new Dictionary<Occupation, int>();
            nameArr = Enum.GetNames(typeof(Occupation));
            n = nameArr.Length;

            for (i = 0; i < n; i++)    
            {
                if (nameArr[i]!="unknow")
                {
                    Occupation occ = (Occupation)Enum.Parse(typeof(Occupation), nameArr[i]);
                    this.occupationIndex.Add(occ, i); 
                }
            }

            #endregion

            #region Ralationship

            this.relationshipIndex = new Dictionary<Relationship, int>();
            nameArr = Enum.GetNames(typeof(Relationship));
            n = nameArr.Length;

            for (i = 0; i < n; i++)
            {
                if (nameArr[i]!="unknow")
                {
                    Relationship rls = (Relationship)Enum.Parse(typeof(Relationship), nameArr[i]);
                    this.relationshipIndex.Add(rls, i);  
                }
            }
            
            #endregion

            #region Race

            this.raceIndex = new Dictionary<Race, int>();
            nameArr = Enum.GetNames(typeof(Race));
            n = nameArr.Length;

            for (i = 0; i < n; i++)
            {
                if (nameArr[i]!="unknow")
                {
                    Race ra = (Race)Enum.Parse(typeof(Race), nameArr[i]);
                    this.raceIndex.Add(ra, i); 
                }
            }
            
            #endregion

            #region Sex

            this.sexIndex = new Dictionary<Sex, int>();
            nameArr = Enum.GetNames(typeof(Sex));
            n = nameArr.Length;

            for (i = 0; i < n; i++)
            {
                if (nameArr[i]!="unknow")
                {
                    Sex sex = (Sex)Enum.Parse(typeof(Sex), nameArr[i]);
                    this.sexIndex.Add(sex, i);  
                }
            }

            #endregion

            #region Naitive Country
            this.naitiveCountryIndex = new Dictionary<NativeCountry, int>();
            nameArr = Enum.GetNames(typeof(NativeCountry));
            n = nameArr.Length;

            for (i = 0; i < n; i++)
            {
                if (nameArr[i]!="unknow")
                {
                    NativeCountry na = (NativeCountry)Enum.Parse(typeof(NativeCountry), nameArr[i]);
                    this.naitiveCountryIndex.Add(na, i); 
                }
            }
            
            #endregion
        }
        #endregion

        #region Public Methods

        #region Index of value attributes
        /// <summary>
        /// Trả lại index của một value bất kỳ vứng với một thuộc tính nào đó
        /// </summary>
        /// <param name="attr">Tên thuộc tính</param>
        /// <param name="discreteValue">Value</param>
        /// <returns>index-thứ tự thuộc tính đó ứng với một atribute nào đó</returns>
        public int IndexValues(AttributeCase attr, object discreteValue)
        {
            int index = -1;
            switch (attr)
            {
                case AttributeCase.Education:
                    {
                        Education value = (Education)discreteValue;
                        index = this.educationIndex[value];
                    }
                    break;
                case AttributeCase.MaritalStatus:
                    {
                        MaritalStatus value = (MaritalStatus)discreteValue;
                        index = this.maritalStatusIndex[value];
                    }
                    break;
                case AttributeCase.NativeCountry:
                    {
                        NativeCountry value = (NativeCountry)discreteValue;
                        index = this.naitiveCountryIndex[value];
                    }
                    break;
                case AttributeCase.Occupation:
                    {
                        Occupation value = (Occupation)discreteValue;
                        index = this.occupationIndex[value];
                    }
                    break;
                case AttributeCase.Race:
                    {
                        Race value = (Race)discreteValue;
                        index = this.raceIndex[value];
                    }
                    break;
                case AttributeCase.Relationship:
                    {
                        Relationship value = (Relationship)discreteValue;
                        index = this.relationshipIndex[value];
                    }
                    break;
                case AttributeCase.Sex:
                    {
                        Sex value = (Sex)discreteValue;
                        index = this.sexIndex[value];
                    }
                    break;
                case AttributeCase.WorkClass:
                    {
                        WorkClass value = (WorkClass)discreteValue;
                        index = this.workClassIndex[value];
                    }
                    break;
                default:
                    break;
            }
            return index;
        } 
        #endregion
        #endregion

        #region Properties

        /// <summary>
        /// Number discrete values of workclass attribute.
        /// </summary>
        public int WorkClassValues
        {
            get { return this.workClassIndex.Count; }
        }
        /// <summary>
        /// Number discrete values of education attribute.
        /// </summary>
        public int EducationValues
        {
            get { return this.educationIndex.Count; }
        }
        /// <summary>
        /// Number discrete values of marital attribute.
        /// </summary>
        public int MaritalStatusValues
        {
            get { return this.maritalStatusIndex.Count; }
        }
        /// <summary>
        /// Number discrete values of Occupation attribute.
        /// </summary>
        public int OccupationValues
        {
            get { return this.occupationIndex.Count; }
        }
        /// <summary>
        /// Number discrete values of relationship attribute.
        /// </summary>
        public int RelationshipValues
        {
            get { return this.relationshipIndex.Count; }
        }
        /// <summary>
        /// Number discrete values of race attribute.
        /// </summary>
        public int RaceValues
        {
            get { return this.raceIndex.Count; }
        }
        /// <summary>
        /// Number discrete values of sex attribute.
        /// </summary>
        public int SexValues
        {
            get { return this.sexIndex.Count; }
        }
        /// <summary>
        /// Number discrete values of naitive country attribute.
        /// </summary>
        public int NaitiveCountryValues
        {
            get { return this.naitiveCountryIndex.Count; }
        }
        #endregion
    }
}
