using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    public class NBTree : IClassification
    {
        /// <summary>
        /// Phân loại một trường hợp
        /// </summary>
        /// <param name="ca">Một trường hợp bất kỳ</param>
        /// <returns>true: lon hon 50k$, false neu nho hon hoac bang 50k$</returns>
        public bool Classify(Case ca)
        {
            return true;
        }

        #region IClassification Members


        public bool IsConstructed
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
