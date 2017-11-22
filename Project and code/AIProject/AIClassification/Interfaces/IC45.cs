using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    public interface IC45
    {
        /// <summary>
        /// Xây dựng cây.
        /// </summary>
        void ConstructTree();
        /// <summary>
        /// Xác định xem cây đã được xây dựng xong chưa
        /// </summary>
        bool IsConstructed
        {
            get;
        }
        /// <summary>
        /// Trả lại node gốc của cây quyết định
        /// </summary>
        Node Root
        {
            get;
        }
    }
}
