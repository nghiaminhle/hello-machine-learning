using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    public class Case : ICase
    {
        #region Fields

        private double age;
        private WorkClass wcl;
        private double fnlwgt;
        private Education education;
        private double eduNum;
        private MaritalStatus mariSt;
        private Occupation occ;
        private Relationship reltsh;
        private Race ra;
        private Sex sexCase;
        private double capitalGain;
        private double capitalLoss;
        private double hoursPerWeek;
        private NativeCountry ntvCountry;
        private bool isMissingValue;
        private bool isGreater;

        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Case()
        { }

        /// <summary>
        /// Constructor with full parameters
        /// </summary>
        /// <param name="age">Age</param>
        /// <param name="fnlwgt">fnlwgt</param>
        /// <param name="edu">Education</param>
        /// <param name="eduNum">EduNum</param>
        /// <param name="mts">Marital Status</param>
        /// <param name="occ">Occupation</param>
        /// <param name="re">Relationship</param>
        /// <param name="ra">Race</param>
        /// <param name="sexCase">Sex</param>
        /// <param name="capitalGain">Capital Gain</param>
        /// <param name="capitalLoss">Capital Loss</param>
        /// <param name="hoursPerWeek">Hours Per Week</param>
        /// <param name="nc">Native Country</param>
        public Case(double age, WorkClass wcl, double fnlwgt, Education edu, double eduNum,
            MaritalStatus mts, Occupation occ, Relationship re, Race ra,
            Sex sexCase, double capitalGain, double capitalLoss, double hoursPerWeek,
            NativeCountry nc, bool isGreater)
        {
            this.isMissingValue = false;
            
            this.age = age;

            this.wcl = wcl;
            if (this.wcl == WorkClass.unknow)
            {
                this.isMissingValue = true;
            }

            this.fnlwgt = fnlwgt;

            this.education = edu;
            if (this.education == Education.unknow)
            {
                this.isMissingValue = true;
            }

            this.eduNum = eduNum;

            this.mariSt = mts;
            if (this.mariSt == MaritalStatus.unknow)
            {
                this.isMissingValue = true;
            }

            this.occ = occ;
            if (this.occ == Occupation.unknow)
            {
                this.isMissingValue = true;
            }

            this.reltsh = re;
            if (this.reltsh == Relationship.unknow)
            {
                this.isMissingValue = true;
            }

            this.ra = ra;
            if (this.ra == Race.unknow)
            {
                this.isMissingValue = true;
            }

            this.sexCase = sexCase;
            if (this.sexCase == Sex.unknow)
            {
                this.isMissingValue = true;
            }

            this.capitalGain = capitalGain;
            this.capitalLoss = capitalLoss;
            this.hoursPerWeek = hoursPerWeek;

            this.ntvCountry = nc;
            if (this.ntvCountry == NativeCountry.unknow)
            {
                this.isMissingValue = true;
            }

            this.isGreater = isGreater;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Age-Continuous Value
        /// </summary>
        public double Age
        {
            get { return age; }
            set { age = value; }
        }

        /// <summary>
        /// Work Class
        /// </summary>
        public WorkClass Wcl
        {
            get { return this.wcl; }
            set { this.wcl = value; }
        }

        /// <summary>
        /// fnlwgt- Continous Values
        /// </summary>
        public double Fnlwgt
        {
            get { return fnlwgt; }
            set { fnlwgt = value; }
        }

        /// <summary>
        /// Education
        /// </summary>
        public Education Edu
        {
            get { return education; }
            set { education = value; }
        }

        /// <summary>
        /// Education Number - Continuous Value
        /// </summary>
        public double EduNum
        {
            get { return eduNum; }
            set { eduNum = value; }
        }

        /// <summary>
        /// Marital Status
        /// </summary>
        public MaritalStatus MariSt
        {
            get { return mariSt; }
            set { mariSt = value; }
        }

        /// <summary>
        /// Occupation
        /// </summary>
        public Occupation Occ
        {
            get { return occ; }
            set { occ = value; }
        }

        /// <summary>
        /// Relationship
        /// </summary>
        public Relationship Reltsh
        {
            get { return reltsh; }
            set { reltsh = value; }
        }

        /// <summary>
        /// Race
        /// </summary>
        public Race Ra
        {
            get { return ra; }
            set { ra = value; }
        }

        /// <summary>
        /// Sex
        /// </summary>
        public Sex SexCase
        {
            get { return sexCase; }
            set { sexCase = value; }
        }

        /// <summary>
        /// Capital Gain - Continuous Value
        /// </summary>
        public double CapitalGain
        {
            get { return capitalGain; }
            set { capitalGain = value; }
        }

        /// <summary>
        /// Capital Loss -Continuous Value
        /// </summary>
        public double CapitalLoss
        {
            get { return capitalLoss; }
            set { capitalLoss = value; }
        }

        /// <summary>
        /// Hours Per Week-Continous Value
        /// </summary>
        public double HoursPerWeek
        {
            get { return hoursPerWeek; }
            set { hoursPerWeek = value; }
        }

        /// <summary>
        /// Native Country
        /// </summary>
        public NativeCountry NtvCountry
        {
            get { return ntvCountry; }
            set { ntvCountry = value; }
        }

        /// <summary>
        /// Use to determine when the case is missed value
        /// </summary>
        public bool IsMissingValue
        {
            get { return isMissingValue; }
            set { isMissingValue = value; }
        }

        /// <summary>
        /// Play of State of each case
        /// </summary>
        public bool IsGreater
        {
            get { return isGreater; }
            set { isGreater = value; }
        }
        #endregion
    }
}
