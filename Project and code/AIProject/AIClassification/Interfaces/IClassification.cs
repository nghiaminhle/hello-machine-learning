using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    public interface IClassification
    {
        /// <summary>
        /// Phân loại một trường hợp
        /// </summary>
        /// <param name="ca">Một trường hợp bất kỳ</param>
        /// <returns>true: lon hon 50k$, false neu nho hon hoac bang 50k$</returns>
        bool Classify(Case ca);
        /// <summary>
        /// Xác định xem cây đã được xây dựng xong chưa
        /// </summary>
        bool IsConstructed
        {
            get;
        }
    }
}
