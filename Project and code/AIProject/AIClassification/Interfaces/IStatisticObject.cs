using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    public interface IStatisticObject
    {
        /// <summary>
        /// Số trường hợp có thu nhập lớn hơn 50K$.
        /// Không kể trường hợp bị missing value
        /// </summary>
        double NumberGreater
        {
            get;
        }
        /// <summary>
        /// Số trường hợp có thu nhập nhỏ hơn hoặc bằng 50k$.
        /// Không kể trường hợp bị missing value.
        /// </summary>
        double NumberLessEqua
        {
            get;
        }
        /// <summary>
        /// Tổng số trường hợp.
        /// </summary>
        double Total
        {
            get;
        }
    }
}
