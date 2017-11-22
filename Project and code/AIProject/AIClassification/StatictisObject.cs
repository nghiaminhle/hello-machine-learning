using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    /// <summary>
    /// Dùng để lưu trữ các thông số trong quá trình thống kê.
    /// Đếm số người có thu nhập lớn hơn và nhỏ hoặc bằng 50K$
    /// </summary>
    [Serializable]
    public class StatictisObject : IStatisticObject
    {
        private double _numberGreater;
        private double _numberLessEqua;
        private double _total;

        public StatictisObject(double numberGreater, double numberLessEqua)
        {
            this._numberGreater = numberGreater;
            this._numberLessEqua = numberLessEqua;
            this._total = this._numberGreater + this._numberLessEqua;
        }
        /// <summary>
        /// Số trường hợp có thu nhập lớn hơn 50K$.
        /// Không kể trường hợp bị missing value
        /// </summary>
        public double NumberGreater
        {
            get { return this._numberGreater; }
        }
        /// <summary>
        /// Số trường hợp có thu nhập nhỏ hơn hoặc bằng 50k$.
        /// Không kể trường hợp bị missing value.
        /// </summary>
        public double NumberLessEqua
        {
            get { return this._numberLessEqua; }
        }
        /// <summary>
        /// Tổng số trường hợp.
        /// </summary>
        public double Total
        {
            get { return this._total; }
        }
    }
}
