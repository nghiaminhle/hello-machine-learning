using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    #region enum
    public enum AttributeCase
    {
        /// <summary>
        /// Continuous
        /// </summary>
        Age, 
        /// <summary>
        /// Discrete value
        /// </summary>
        WorkClass,
        /// <summary>
        /// Continuous value
        /// </summary>
        Fnlwgt,
        /// <summary>
        /// Discrete value
        /// </summary>
        Education,
        /// <summary>
        /// Continuous value
        /// </summary>
        EducationNum,
        /// <summary>
        /// Discrete Value
        /// </summary>
        MaritalStatus,
        /// <summary>
        /// Discrete value
        /// </summary>
        Occupation,
        /// <summary>
        /// Discrete value
        /// </summary>
        Relationship,
        /// <summary>
        /// Discrete value
        /// </summary>
        Race,
        /// <summary>
        /// Discrete value
        /// </summary>
        Sex,
        /// <summary>
        /// Continuous
        /// </summary>
        CapitalGain,
        /// <summary>
        /// Continuous
        /// </summary>
        CapitalLoss,
        /// <summary>
        /// Continuous
        /// </summary>
        HoursPerWeek,
        /// <summary>
        /// Discrete
        /// </summary>
        NativeCountry,
        /// <summary>
        /// Lon hon 50k$ hoac nho hon 50K$
        /// </summary>
        IsGreater,
        unknow
    }

    public enum Sex
    {
        Female,
        Male,
        unknow
    }

    public enum WorkClass
    {
        Private,
        Self_emp_not_inc,
        Self_emp_inc,
        Federal_gov,
        Local_gov,
        State_gov,
        Without_pay,
        Never_worked,
        unknow
    }

    public enum Education
    {
        Bachelors,
        Some_college,
        Eleventh,
        HS_grad,
        Prof_school,
        Assoc_acdm,
        Assoc_voc,
        Ninth,
        Seventh_Eighth,
        Twelfth,
        Masters,
        First_Fourth,
        Tenth,
        Doctorate,
        Fith_Sixth,
        Preschool,
        unknow
    }

    public enum MaritalStatus
    {
        Married_civ_spouse,
        Divorced,
        Never_married,
        Separated,
        Widowed,
        Married_spouse_absent,
        Married_AF_spouse,
        unknow
    }

    public enum Occupation
    {
        Tech_support,
        Craft_repair,
        Other_service,
        Sales,
        Exec_managerial,
        Prof_specialty,
        Handlers_cleaners,
        Machine_op_inspct,
        Adm_clerical,
        Farming_fishing,
        Transport_moving,
        Priv_house_serv,
        Protective_serv,
        Armed_Forces,
        unknow
    }

    public enum Relationship
    {
        Wife,
        Own_child,
        Husband,
        Not_in_family,
        Other_relative,
        Unmarried,
        unknow
    }

    public enum Race
    {
        White,
        Asian_Pac_Islander,
        Amer_Indian_Eskimo,
        Other,
        Black,
        unknow
    }

    public enum NativeCountry
    {
        United_States,
        Cambodia,
        England,
        Puerto_Rico,
        Canada, Germany,
        Outlying_US,
        India,
        Japan,
        Greece,
        South,
        China,
        Cuba,
        Iran,
        Honduras,
        Philippines,
        Italy,
        Poland,
        Jamaica,
        Vietnam,
        Mexico,
        Portugal,
        Ireland,
        France,
        Dominican_Republic,
        Laos,
        Ecuador,
        Taiwan,
        Haiti,
        Columbia,
        Hungary,
        Guatemala,
        Nicaragua,
        Scotland,
        Thailand,
        Yugoslavia,
        El_Salvador,
        TrinadadTobago,
        Peru,
        Hong,
        Holand_Netherlands,
        unknow
    }

    public enum StateCompare
    {
        Greater,
        Less,
        GreaterEqual,
        LessEqua,
        Equal
    }

    public enum Algorithm
    {
        C45,
        ID3,
        NaiveBayes
    }
    #endregion
}
