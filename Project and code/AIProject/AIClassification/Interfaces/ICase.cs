using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    public interface ICase
    {
        /// <summary>
        /// Age of case-Continuous Values
        /// </summary>
        double Age
        {
            get;
            set;
        }
        /// <summary>
        /// WorkClass-Discrete Value
        /// </summary>
        WorkClass Wcl
        {
            get;
            set;
        }
        /// <summary>
        /// fnlwgt- Continous Values
        /// </summary>
        double Fnlwgt
        {
            get;
            set;
        }
        /// <summary>
        /// Education
        /// </summary>
        Education Edu
        {
            get;
            set;
        }
        /// <summary>
        /// Education Number - Continuous Value
        /// </summary>
        double EduNum
        {
            get;
            set;
        }
        /// <summary>
        /// Marital Status
        /// </summary>
        MaritalStatus MariSt
        {
            get;
            set;
        }
        /// <summary>
        /// Occupation
        /// </summary>
        Occupation Occ
        {
            get;
            set; 
        }
        /// <summary>
        /// Relationship
        /// </summary>
        Relationship Reltsh
        {
            get;
            set;
        }
        /// <summary>
        /// Race
        /// </summary>
        Race Ra
        {
            get;
            set;
        }
        /// <summary>
        /// Sex
        /// </summary>
        Sex SexCase
        {
            get;
            set;
        }
        /// <summary>
        /// Capital Gain - Continuous Value
        /// </summary>
        double CapitalGain
        {
            get;
            set;
        }
        /// <summary>
        /// Capital Loss -Continuous Value
        /// </summary>
        double CapitalLoss
        {
            get;
            set;
        }
        /// <summary>
        /// Hours Per Week-Continous Value
        /// </summary>
        double HoursPerWeek
        {
            get;
            set;
        }
        /// <summary>
        /// Native Country
        /// </summary>
        NativeCountry NtvCountry
        {
            get;
            set;
        }
        /// <summary>
        /// Use to determine when the case is missed value
        /// </summary>
        bool IsMissingValue
        {
            get;
            set;
        }
        /// <summary>
        /// Play of State of each case
        /// </summary>
        bool IsGreater
        {
            get;
            set;
        }
    }
}
