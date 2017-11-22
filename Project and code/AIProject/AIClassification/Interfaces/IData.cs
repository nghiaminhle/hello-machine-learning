using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    public interface IData
    {
        /// <summary>
        /// Filter data with a paticular list conditions
        /// </summary>
        /// <param name="conditions">List Conditions Of Each Attribute</param>
        /// <returns>Subset Data</returns>
        Data Filter(params Condition[] conditions);
        
        /// <summary>
        /// Tính lượng tin trung bình của mỗi trường hợp.
        /// </summary>
        /// <returns>Lượng tin trung bình</returns>
        double Entropy();
        
        /// <summary>
        /// Trả lại một mảng các thrshold của bộ dữ liệu
        /// </summary>
        /// <param name="continuousAttr">Thuộc tính liên tục cần rời rạc hóa</param>
        /// <param name="comparer">Bộ so sánh, để xác định thuộc tính liên tục nào được chọn</param>
        /// <returns>Mảng các threshold</returns>
        double[] GetThreshold(AttributeCase continuousAttr, IComparer<Case> comparer);
        
        /// <summary>
        /// Số trường hợp bị missing value trên một thuộc tính nào đó
        /// trên bộ dữ liệu.
        /// </summary>
        /// <param name="attr">Thuốc tính cần check</param>
        /// <returns>Số trường hợp missing value</returns>
        double NumberMissingValues(AttributeCase attr);
        
        /// <summary>
        /// Đếm số trường hợp có thu nhập lớn hơn 50k$
        /// </summary>
        /// <returns>số trường hợp</returns>
        double NumberGreaterCase();
        
        /// <summary>
        /// Lấy random một case bất kỳ.
        /// Trả lại null nếu dữ liệu không có.
        /// </summary>
        /// <returns></returns>
        Case GetRandomCase();
        
        /// <summary>
        /// Thống kê số trường hợp lớn hơn và nhỏ hơn 50K$ ứng với value của một
        /// thuộc tính nào đó.
        /// </summary>
        /// <param name="attr">Thuộc tính</param>
        /// <param name="threshold">thresholde của thuộc tính liên tục</param>
        /// <returns>Mảng các đối tượng Statistic- ứng với từng value của thuộc tính rời rạc và 
        /// với thuộc tính liên tục là hai khoảng lớn hơn và nhỏ</returns>
        StatictisObject[] Statistic(AttributeCase attr, double? threshold);
        
        /// <summary>
        /// Khởi tạo thông tin về lượng tin trung bình và số phần tử bị missing của mỗi trường hợp.
        /// Xử dụng trong các trường hợp có missing dữ liệu mà không tiền xử lý làm sạch.
        /// </summary>
        void InitialInfo();
        
        /// <summary>
        /// Tính tổng số phần tử bị missing
        /// </summary>
        /// <returns>Số phần từ missing</returns>
        double TotalMissingCase();

        /// <summary>
        /// Chuẩn hóa dữ liệu trong các trường hợp bị missing value
        /// </summary>
        void CleanData();

        /// <summary>
        /// Number case of the data set
        /// </summary>
        int NumberCase
        {
            get;
        }
        /// <summary>
        /// Array of cases
        /// </summary>
        /// <param name="i">index of certain case</param>
        /// <returns>Case</returns>
        Case this[int i]
        {
            get;
        }
        /// <summary>
        /// Array Cases
        /// </summary>
        Case[] Cases
        {
            get;
        }

        /// <summary>
        /// Nhãn phổ biến của một attribute nào đó ứng với trường hợp lớn hơn 50k$
        /// </summary>
        Dictionary<AttributeCase, object> MostCommonLabelGreatr
        {
            get;
        }
        /// <summary>
        /// Nhãn phổ biến của một attribute nào đó ứng với trường hợp nhỏ hơn hoặc bằng 50k$
        /// </summary>
        Dictionary<AttributeCase, object> MostCommonLabelLessEqua
        { get; }
    }
}
