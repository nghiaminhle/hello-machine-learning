using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace AIClassification
{
    public interface IFile
    {
        /// <summary>
        ///  Doc mot file tra lai mang cac case
        /// </summary>
        /// <param name="path">Duong dan</param>
        /// <returns>Mang cac case</returns>
        Case[] ReadFile(string path);
        /// <summary>
        /// Đọc dữ liệu từ một file, có xử lý progress
        /// </summary>
        /// <param name="path">Đường dẫn tới file</param>
        /// <returns>Mảng các đối tượng</returns>
        Case[] ReadFile(string path, BackgroundWorker worker, double per);
        /// <summary>
        /// Lưu một danh sách dữ liệu ra file
        /// </summary>
        /// <param name="fileName">tên đường dẫn tới file sẽ ghi ra</param>
        /// <param name="datas">List dữ liệu</param>
        void SaveCase(string path, params Case[] datas);
        /// <summary>
        /// Ghi một cây ra file để lưu trữ phục vụ phân loại về sau.
        /// </summary>
        /// <param name="path">Tên đường dẫn</param>
        /// <param name="node">Node gốc</param>
        void TreeSerialization(string path, Node root);
        /// <summary>
        /// Đọc một cây đã được serialize thành file dữ liệu
        /// </summary>
        /// <param name="path">Đường dẫn tới file dữ liệu</param>
        /// <returns>Node gốc của cây</returns>
        Node TreeDeserialization(string path);
        
        /// <summary>
        /// Ghi các tri thức tìm được ra file để lưu trữ
        /// </summary>
        /// <param name="knowledge">Tri thức cần lưu</param>
        /// <param name="path">Đường dẫn</param>
        void SaveKnowledge(KnowledgeC45 knowledge, string path);

        /// <summary>
        /// Lấy các tri thức tìm được từ file.
        /// </summary>
        /// <param name="path">Đường dẫn</param>
        /// <returns>Tri thức</returns>
        KnowledgeC45 ReadKnowledge(string path);

        /// <summary>
        /// Ghi doi tuong naive bayes ra de luu tru
        /// </summary>
        /// <param name="nb">Doi tuong nb</param>
        /// <param name="path">Duong dan</param>
        void SaveNaiveBayes(KnowledgeNaiveBayes nbKnowledge, string path);

        /// <summary>
        /// Doc doi tuong naive bayes tu file
        /// </summary>
        /// <param name="path">Duong dan</param>
        /// <returns>Doi tuong naive bayes</returns>
        KnowledgeNaiveBayes ReadNaiveBayes(string path);

        /// <summary>
        /// Sinh ngẫu nhiên từ dữ liệu gốc thành hai file dữ liệu validate và dữ
        /// liệu trainning với tỉ lệ nào đó.
        /// </summary>
        /// <param name="originalFile">File dữ liệu gốc</param>
        /// <param name="validateFile">File dữ liệu validate</param>
        /// <param name="newTrainningData">File dữ liệu trainning mới</param>
        /// <param name="percent">Phần trăm dữ liệu dùng để validate</param>
        void CreateValidateData(string originalFilePath, string validateFilePath, string newTrainningDataPath, double percent);
    }
}
