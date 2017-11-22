using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{    
    [Serializable]
    public class AttributeDetail
    {
        #region Private attribute
        private AttributeCase type;
        private List<object> typeValue = new List<object>();
        private bool isContinous = false;
        #endregion

        #region Constructors
        /// <summary>
        /// Creating an attribute detail, using for the tree
        /// </summary>
        /// <param name="ac"></param>
        public AttributeDetail(AttributeCase ac)
        {
            this.type = ac;
            typeValue = new List<object>();
            this.SetTypeValue(ac);
        }
        #endregion

        /// <summary>
        /// Hàm trả về trạng thái liên tục hay không liên tục của một thuộc tính.
        /// </summary>
        /// <param name="attr">Attribute cần kiểm tra</param>
        /// <returns>false: liên tục, true: rời rạc</returns>
        public static bool IsContinuosAttr(AttributeCase attr)
        {
            bool state = false;
            switch (attr)
            {
                case AttributeCase.Age:
                    {
                        state = true;
                    }
                    break;
                case AttributeCase.CapitalGain:
                    {
                        state = true;
                    }
                    break;
                case AttributeCase.CapitalLoss:
                    {
                        state = true;
                    }
                    break;
                case AttributeCase.Education:
                    {
                        state = false;
                    }
                    break;
                case AttributeCase.EducationNum:
                    {
                        state = true;
                    }
                    break;
                case AttributeCase.Fnlwgt:
                    {
                        state = true;
                    }
                    break;
                case AttributeCase.HoursPerWeek:
                    {
                        state = true;
                    }
                    break;
                case AttributeCase.MaritalStatus:
                    {
                        state = false;
                    }
                    break;
                case AttributeCase.NativeCountry:
                    {
                        state = false;
                    }
                    break;
                case AttributeCase.Occupation:
                    {
                        state = false;
                    }
                    break;
                case AttributeCase.Race:
                    {
                        state = false;
                    }
                    break;
                case AttributeCase.Relationship:
                    {
                        state = false;
                    }
                    break;
                case AttributeCase.Sex:
                    {
                        state = false;
                    }
                    break;
                case AttributeCase.WorkClass:
                    {
                        state = false;
                    }
                    break;
                default:
                    break;
            }
            return state;
        }

        #region Private methods
        /// <summary>
        /// Initialize the value of `tyleValue`
        /// </summary>
        /// <param name="ac"></param>
        /// 
        public void SetTypeValue(AttributeCase ac)
        {
            switch (ac)
            {
                case AttributeCase.Age:
                    isContinous = true;
                    break;
                case AttributeCase.CapitalGain:
                    isContinous = true;
                    break;
                case AttributeCase.CapitalLoss:
                    isContinous = true;
                    break;
                case AttributeCase.Education:
                    isContinous = false;
                    typeValue.Add(Education.Assoc_acdm);
                    typeValue.Add(Education.Assoc_voc);
                    typeValue.Add(Education.Bachelors);
                    typeValue.Add(Education.Doctorate);
                    typeValue.Add(Education.Eleventh);
                    typeValue.Add(Education.First_Fourth);
                    typeValue.Add(Education.Fith_Sixth);
                    typeValue.Add(Education.HS_grad);
                    typeValue.Add(Education.Masters);
                    typeValue.Add(Education.Ninth);
                    typeValue.Add(Education.Preschool);
                    typeValue.Add(Education.Prof_school);
                    typeValue.Add(Education.Seventh_Eighth);
                    typeValue.Add(Education.Some_college);
                    typeValue.Add(Education.Tenth);
                    typeValue.Add(Education.Twelfth);
                    typeValue.Add(Education.unknow);
                    break;
                case AttributeCase.EducationNum:
                    isContinous = true;
                    break;
                case AttributeCase.Fnlwgt:
                    isContinous = true;
                    break;
                case AttributeCase.HoursPerWeek:
                    isContinous = true;
                    break;
                case AttributeCase.MaritalStatus:
                    isContinous = false;
                    typeValue.Add(MaritalStatus.Divorced);
                    typeValue.Add(MaritalStatus.Married_AF_spouse);
                    typeValue.Add(MaritalStatus.Married_civ_spouse);
                    typeValue.Add(MaritalStatus.Married_spouse_absent);
                    typeValue.Add(MaritalStatus.Never_married);
                    typeValue.Add(MaritalStatus.Separated);
                    typeValue.Add(MaritalStatus.Widowed);
                    typeValue.Add(MaritalStatus.unknow);
                    break;
                case AttributeCase.NativeCountry:
                    isContinous = false;
                    typeValue.Add(NativeCountry.Cambodia);
                    typeValue.Add(NativeCountry.Canada);
                    typeValue.Add(NativeCountry.Columbia);
                    typeValue.Add(NativeCountry.Cuba);
                    typeValue.Add(NativeCountry.China);
                    typeValue.Add(NativeCountry.Dominican_Republic);
                    typeValue.Add(NativeCountry.Ecuador);
                    typeValue.Add(NativeCountry.El_Salvador);
                    typeValue.Add(NativeCountry.England);
                    typeValue.Add(NativeCountry.France);
                    typeValue.Add(NativeCountry.Germany);
                    typeValue.Add(NativeCountry.Greece);
                    typeValue.Add(NativeCountry.Guatemala);
                    typeValue.Add(NativeCountry.Haiti);
                    typeValue.Add(NativeCountry.Holand_Netherlands);
                    typeValue.Add(NativeCountry.Honduras);
                    typeValue.Add(NativeCountry.Hong);
                    typeValue.Add(NativeCountry.Hungary);
                    typeValue.Add(NativeCountry.India);
                    typeValue.Add(NativeCountry.Iran);
                    typeValue.Add(NativeCountry.Ireland);
                    typeValue.Add(NativeCountry.Italy);
                    typeValue.Add(NativeCountry.Jamaica);
                    typeValue.Add(NativeCountry.Japan);
                    typeValue.Add(NativeCountry.Laos);
                    typeValue.Add(NativeCountry.Mexico);
                    typeValue.Add(NativeCountry.Nicaragua);
                    typeValue.Add(NativeCountry.Outlying_US);
                    typeValue.Add(NativeCountry.Peru);
                    typeValue.Add(NativeCountry.Poland);
                    typeValue.Add(NativeCountry.Portugal);
                    typeValue.Add(NativeCountry.Puerto_Rico);
                    typeValue.Add(NativeCountry.Philippines);
                    typeValue.Add(NativeCountry.Scotland);
                    typeValue.Add(NativeCountry.South);
                    typeValue.Add(NativeCountry.Taiwan);
                    typeValue.Add(NativeCountry.Thailand);
                    typeValue.Add(NativeCountry.TrinadadTobago);
                    typeValue.Add(NativeCountry.United_States);
                    typeValue.Add(NativeCountry.Vietnam);
                    typeValue.Add(NativeCountry.Yugoslavia);
                    break;
                case AttributeCase.Occupation:
                    isContinous = false;
                    typeValue.Add(Occupation.Adm_clerical);
                    typeValue.Add(Occupation.Armed_Forces);
                    typeValue.Add(Occupation.Craft_repair);
                    typeValue.Add(Occupation.Exec_managerial);
                    typeValue.Add(Occupation.Farming_fishing);
                    typeValue.Add(Occupation.Handlers_cleaners);
                    typeValue.Add(Occupation.Machine_op_inspct);
                    typeValue.Add(Occupation.Priv_house_serv);
                    typeValue.Add(Occupation.Prof_specialty);
                    typeValue.Add(Occupation.Protective_serv);
                    typeValue.Add(Occupation.Sales);
                    typeValue.Add(Occupation.Tech_support);
                    typeValue.Add(Occupation.Transport_moving);
                    typeValue.Add(Occupation.unknow);
                    break;
                case AttributeCase.Race:
                    isContinous = false;
                    typeValue.Add(Race.Amer_Indian_Eskimo);
                    typeValue.Add(Race.Asian_Pac_Islander);
                    typeValue.Add(Race.Black);
                    typeValue.Add(Race.Other);
                    typeValue.Add(Race.White);
                    typeValue.Add(Race.unknow);
                    break;
                case AttributeCase.Relationship:
                    isContinous = false;
                    typeValue.Add(Relationship.Husband);
                    typeValue.Add(Relationship.Not_in_family);
                    typeValue.Add(Relationship.Other_relative);
                    typeValue.Add(Relationship.Own_child);
                    typeValue.Add(Relationship.Unmarried);
                    typeValue.Add(Relationship.Wife);
                    typeValue.Add(Relationship.unknow);
                    break;
                case AttributeCase.Sex:
                    isContinous = false;
                    typeValue.Add(Sex.Female);
                    typeValue.Add(Sex.Male);
                    typeValue.Add(Sex.unknow);
                    break;
                case AttributeCase.WorkClass:
                    isContinous = false;
                    typeValue.Add(WorkClass.Federal_gov);
                    typeValue.Add(WorkClass.Local_gov);
                    typeValue.Add(WorkClass.Never_worked);
                    typeValue.Add(WorkClass.Private);
                    typeValue.Add(WorkClass.Self_emp_inc);
                    typeValue.Add(WorkClass.Self_emp_not_inc);
                    typeValue.Add(WorkClass.State_gov);
                    typeValue.Add(WorkClass.Without_pay);
                    typeValue.Add(WorkClass.unknow);
                    break;
            }
        }
        #endregion

        #region Gets & sets
        public AttributeCase Type
        {
            get { return this.type; }
            set
            {
                this.type = value;
                SetTypeValue(value);
            }
        }

        public List<object> TypeValue
        {
            get { return this.typeValue; }
        }

        public bool IsContinous
        {
            get { return this.isContinous; }
        }
        #endregion
    }
}
