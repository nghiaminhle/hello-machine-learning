using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    public interface INode
    {
        /// <summary>
        /// Đánh nhãn thuộc tính cho mỗi node trong (không phải là node lá).
        /// </summary>
        /// <param name="attr">Tên thuộc tính</param>
        /// <param name="threshold">Khoảng giá trị nếu đấy là thuộc tính liên tục</param>
        void MakeLabel(AttributeCase attr, double? threshold, double numberCase, double greaterNum, double lessEqualNum);
         /// <summary>
        /// Xét trạng thái của một node là lá. 
        /// </summary>
        /// <param name="result">Kết quả là lớn hơn hay nhỏ 50$. True- Lớn hơn 50K$; Fasle-Nhỏ hơn hoặc bằng 50K$</param>
        void SetLeaf(bool result, double numberCase, double numberError);
         /// <summary>
        /// Xác định mộ trường hợp đưa vào thuộc nhóm nào
        /// </summary>
        /// <param name="ca">Một trường hợp bất kỳ</param>
        /// <returns>Nhóm phân loại</returns>
        bool Classify(Case ca);
          /// <summary>
        /// Lấy một node con ứng với nhãn là một thuộc tính nào đó
        /// </summary>
        /// <param name="attr">Thuộc tính nhãn của một node</param>
        /// <param name="value">Giá trị ứng với thuộc tính của node</param>
        /// <returns>Một node con phù hợp</returns>
        Node GetChildNode(AttributeCase attr, object value);
       
        /// <summary>
        /// Nhãn thuộc tính của một node.
        /// </summary>
        AttributeCase Label
        {
            get;
            set;
        }
        /// <summary>
        /// Xác định trạng thái của một node có phải là mức lá hay không?
        /// </summary>
        bool IsLeaf
        {
            get;
            set;
        }
        /// <summary>
        /// Số trường hợp khi xét ở mức lá
        /// </summary>
        double NumberCase
        {
            get;
            set;
        }
        /// <summary>
        /// Số trường hợp bị lỗi khi xét tới mức lá.
        /// </summary>
        double NumberError
        {
            get;
            set;
        }
        /// <summary>
        /// Trạng thái của một node lá: true- lớn hơn 50K$ và false-nhỏ hơn hoặc bằng 50k$.
        /// </summary>
        bool Result
        {
            get;
            set;
        }
        /// <summary>
        /// Number of child nodes
        /// </summary>
        int NumberChilds
        {
            get;
        }
        /// <summary>
        /// Node of parent;
        /// </summary>
        Node Parent
        {
            get;
            set;
        }
        /// <summary>
        /// List child node
        /// </summary>
        Node[] ListChild
        {
            get;
            set;
        }
         /// <summary>
        /// Số trường hợp tới khi node được xét đánh nhãn có thu nhập lớn hơn 50K$
        /// </summary>
        double GreaterNumber
        {
            get;
            set;
        }
        /// <summary>
        /// Số trường hợp tới khi node được xét đánh nhãn có thu nhập nhỏ hoặc bằng 50K$
        /// </summary>
        double LessEquaNumber
        {
            get;
            set;
        }
    }
}
