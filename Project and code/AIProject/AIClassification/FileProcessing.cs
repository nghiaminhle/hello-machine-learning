using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;

namespace AIClassification
{
    public class FileProcessing : IFile
    {
        #region Public Methods

        #region Read File
        /// <summary>
        /// Đọc dữ liệu từ một file
        /// </summary>
        /// <param name="path">Đường dẫn tới file</param>
        /// <returns>Mảng các đối tượng</returns>
        public Case[] ReadFile(string path)
        {
            Case[] listCase = null;
            using (StreamReader reader = File.OpenText(path))
            {
                string input = null;
                List<string> list = new List<string>();

                while ((input = reader.ReadLine()) != null)
                {
                    list.Add(input);
                }
                if (list.Count != 0)
                {
                    listCase = new Case[list.Count];
                    // xử dụng biểu thức chính quy để phân tách thành các sâu con.
                    Regex regex = new Regex(", ");

                    #region Fields
                    double age = -1;
                    WorkClass wc = WorkClass.unknow;
                    double fnlwgt = -1;
                    Education edu = Education.unknow;
                    double eduNum = -1;
                    MaritalStatus mrt = MaritalStatus.unknow;
                    Occupation occ = Occupation.unknow;
                    Relationship rls = Relationship.unknow;
                    Race race = Race.unknow;
                    Sex sex = Sex.unknow;
                    double capitalGain = -1;
                    double capitalLoss = -1;
                    double hoursPerWeek = -1;
                    NativeCountry ntc = NativeCountry.unknow;
                    bool isGreater = true;
                    #endregion

                    int index = 0;
                    foreach (string s in list)
                    {
                        int pos = 0;
                        #region Tách và chuẩn hóa các substring.
                        foreach (string subStr in regex.Split(s))
                        {
                            // Chuẩn hóa sâu, thay các kí tứ '-' thành kí tự '_'
                            Regex r1 = new Regex("-");
                            string tempStr = r1.Replace(subStr, "_");
                            switch (tempStr)
                            {
                                case "11th":
                                    tempStr = "Eleventh";
                                    break;
                                case "9th":
                                    tempStr = "Ninth";
                                    break;
                                case "7th_8th":
                                    tempStr = "Seventh_Eighth";
                                    break;
                                case "12th":
                                    tempStr = "Twelfth";
                                    break;
                                case "1st_4th":
                                    tempStr = "First_Fourth";
                                    break;
                                case "10th":
                                    tempStr = "Tenth";
                                    break;
                                case "5th_6th":
                                    tempStr = "Fith_Sixth";
                                    break;
                                case "Outlying_US(Guam_USVI_etc)":
                                    tempStr = "Outlying_US";
                                    break;
                                case "Trinadad&Tobago":
                                    tempStr = "TrinadadTobago";
                                    break;
                                case "?":
                                    tempStr = "unknow";
                                    break;
                                default:
                                    break;
                            }

                            switch (pos)
                            {
                                case 0:
                                    age = double.Parse(tempStr);
                                    break;
                                case 1:
                                    wc = (WorkClass)Enum.Parse(typeof(WorkClass), tempStr);
                                    break;
                                case 2:
                                    fnlwgt = double.Parse(tempStr);
                                    break;
                                case 3:
                                    edu = (Education)Enum.Parse(typeof(Education), tempStr);
                                    break;
                                case 4:
                                    eduNum = double.Parse(tempStr);
                                    break;
                                case 5:
                                    mrt = (MaritalStatus)Enum.Parse(typeof(MaritalStatus), tempStr);
                                    break;
                                case 6:
                                    occ = (Occupation)Enum.Parse(typeof(Occupation), tempStr);
                                    break;
                                case 7:
                                    rls = (Relationship)Enum.Parse(typeof(Relationship), tempStr);
                                    break;
                                case 8:
                                    race = (Race)Enum.Parse(typeof(Race), tempStr);
                                    break;
                                case 9:
                                    sex = (Sex)Enum.Parse(typeof(Sex), tempStr);
                                    break;
                                case 10:
                                    capitalGain = double.Parse(tempStr);
                                    break;
                                case 11:
                                    capitalLoss = double.Parse(tempStr);
                                    break;
                                case 12:
                                    hoursPerWeek = double.Parse(tempStr);
                                    break;
                                case 13:
                                    ntc = (NativeCountry)Enum.Parse(typeof(NativeCountry), tempStr);
                                    break;
                                case 14:
                                    if (tempStr == "<=50K" || tempStr == "<=50K.")
                                        isGreater = false;
                                    else if (tempStr == ">50K" || tempStr == ">50K.")
                                        isGreater = true;
                                    break;
                                default:
                                    break;
                            }
                            // Attribute Next
                            pos++;
                        }
                        #endregion

                        Case ca = new Case(age, wc, fnlwgt, edu, eduNum,
                                mrt, occ, rls, race, sex, capitalGain,
                                capitalLoss, hoursPerWeek, ntc, isGreater);
                        listCase[index] = ca;
                        index++;
                    }
                }
            }
            return listCase;
        }

        /// <summary>
        /// Đọc dữ liệu từ một file, có xử lý progress
        /// </summary>
        /// <param name="path">Đường dẫn tới file</param>
        /// <returns>Mảng các đối tượng</returns>
        public Case[] ReadFile(string path, BackgroundWorker worker, double per)
        {
            Case[] listCase = null;
            using (StreamReader reader = File.OpenText(path))
            {
                string input = null;
                List<string> list = new List<string>();

                while ((input = reader.ReadLine()) != null)
                {
                    list.Add(input);
                }
                if (list.Count != 0)
                {
                    // Dung de tinh phan tram xu ly
                    double numberRow = list.Count / per;
                    double count = 0;

                    listCase = new Case[list.Count];
                    // xử dụng biểu thức chính quy để phân tách thành các sâu con.
                    Regex regex = new Regex(", ");

                    #region Fields
                    double age = -1;
                    WorkClass wc = WorkClass.unknow;
                    double fnlwgt = -1;
                    Education edu = Education.unknow;
                    double eduNum = -1;
                    MaritalStatus mrt = MaritalStatus.unknow;
                    Occupation occ = Occupation.unknow;
                    Relationship rls = Relationship.unknow;
                    Race race = Race.unknow;
                    Sex sex = Sex.unknow;
                    double capitalGain = -1;
                    double capitalLoss = -1;
                    double hoursPerWeek = -1;
                    NativeCountry ntc = NativeCountry.unknow;
                    bool isGreater = true;
                    #endregion

                    int index = 0;
                    foreach (string s in list)
                    {
                        count++;
                        int pos = 0;
                        #region Tách và chuẩn hóa các substring.
                        foreach (string subStr in regex.Split(s))
                        {
                            // Chuẩn hóa sâu, thay các kí tứ '-' thành kí tự '_'
                            Regex r1 = new Regex("-");
                            string tempStr = r1.Replace(subStr, "_");
                            switch (tempStr)
                            {
                                case "11th":
                                    tempStr = "Eleventh";
                                    break;
                                case "9th":
                                    tempStr = "Ninth";
                                    break;
                                case "7th_8th":
                                    tempStr = "Seventh_Eighth";
                                    break;
                                case "12th":
                                    tempStr = "Twelfth";
                                    break;
                                case "1st_4th":
                                    tempStr = "First_Fourth";
                                    break;
                                case "10th":
                                    tempStr = "Tenth";
                                    break;
                                case "5th_6th":
                                    tempStr = "Fith_Sixth";
                                    break;
                                case "Outlying_US(Guam_USVI_etc)":
                                    tempStr = "Outlying_US";
                                    break;
                                case "Trinadad&Tobago":
                                    tempStr = "TrinadadTobago";
                                    break;
                                case "?":
                                    tempStr = "unknow";
                                    break;
                                default:
                                    break;
                            }

                            switch (pos)
                            {
                                case 0:
                                    age = double.Parse(tempStr);
                                    break;
                                case 1:
                                    wc = (WorkClass)Enum.Parse(typeof(WorkClass), tempStr);
                                    break;
                                case 2:
                                    fnlwgt = double.Parse(tempStr);
                                    break;
                                case 3:
                                    edu = (Education)Enum.Parse(typeof(Education), tempStr);
                                    break;
                                case 4:
                                    eduNum = double.Parse(tempStr);
                                    break;
                                case 5:
                                    mrt = (MaritalStatus)Enum.Parse(typeof(MaritalStatus), tempStr);
                                    break;
                                case 6:
                                    occ = (Occupation)Enum.Parse(typeof(Occupation), tempStr);
                                    break;
                                case 7:
                                    rls = (Relationship)Enum.Parse(typeof(Relationship), tempStr);
                                    break;
                                case 8:
                                    race = (Race)Enum.Parse(typeof(Race), tempStr);
                                    break;
                                case 9:
                                    sex = (Sex)Enum.Parse(typeof(Sex), tempStr);
                                    break;
                                case 10:
                                    capitalGain = double.Parse(tempStr);
                                    break;
                                case 11:
                                    capitalLoss = double.Parse(tempStr);
                                    break;
                                case 12:
                                    hoursPerWeek = double.Parse(tempStr);
                                    break;
                                case 13:
                                    ntc = (NativeCountry)Enum.Parse(typeof(NativeCountry), tempStr);
                                    break;
                                case 14:
                                    if (tempStr == "<=50K" || tempStr == "<=50K.")
                                        isGreater = false;
                                    else if (tempStr == ">50K" || tempStr == ">50K.")
                                        isGreater = true;
                                    break;
                                default:
                                    break;
                            }
                            // Attribute Next
                            pos++;
                        }
                        #endregion

                        Case ca = new Case(age, wc, fnlwgt, edu, eduNum,
                                mrt, occ, rls, race, sex, capitalGain,
                                capitalLoss, hoursPerWeek, ntc, isGreater);
                        listCase[index] = ca;
                        index++;
                        // Tinh phan tram cong viec
                        if (worker != null && worker.WorkerReportsProgress)
                        {
                            worker.ReportProgress((int)(count * 100 / numberRow));
                        }
                    }
                }
            }
            return listCase;
        }
        #endregion

        #region Write File
        /// <summary>
        /// Lưu một danh sách dữ liệu ra file
        /// </summary>
        /// <param name="fileName">tên đường dẫn tới file sẽ ghi ra</param>
        /// <param name="datas">List dữ liệu</param>
        public void SaveCase(string path, params Case[] datas)
        {
            using (StreamWriter streamWriter = File.CreateText(path))
            {
                foreach (Case ca in datas)
                {
                    string line = ca.Age.ToString() + ", " + ca.Wcl.ToString() + ", " + ca.Fnlwgt.ToString() + ", " + ca.Edu.ToString() + ", "
                        + ca.EduNum.ToString() + ", " + ca.MariSt.ToString() + ", " + ca.Occ.ToString() + ", "
                        + ca.Reltsh.ToString() + ", " + ca.Ra.ToString() + ", " + ca.SexCase.ToString() + ", "
                        + ca.CapitalGain.ToString() + ", " + ca.CapitalLoss.ToString() + ", " + ca.HoursPerWeek.ToString()
                        + ", " + ca.NtvCountry.ToString() + ", " + ca.IsGreater.ToString();
                    streamWriter.WriteLine(line);
                }
            }
        }
        #endregion

        #region Serialize Tree.
        /// <summary>
        /// Ghi một cây ra file để lưu trữ phục vụ phân loại về sau.
        /// </summary>
        /// <param name="path">Tên đường dẫn</param>
        /// <param name="node">Node gốc</param>
        public void TreeSerialization(string path, Node root)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binaryFormatter.Serialize(fStream, root);
            }
        } 
        #endregion

        #region Deserialize Tree
        /// <summary>
        /// Đọc một cây đã được serialize thành file dữ liệu
        /// </summary>
        /// <param name="path">Đường dẫn tới file dữ liệu</param>
        /// <returns>Node gốc của cây</returns>
        public Node TreeDeserialization(string path)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            Node newRoot = null;
            using (Stream readStream = File.OpenRead(path))
            {
                newRoot = (Node)binaryFormatter.Deserialize(readStream);
            }
            return newRoot;
        }
        #endregion

        #region Create Trainning Data and Validate Data
        /// <summary>
        /// Sinh ngẫu nhiên từ dữ liệu gốc thành hai file dữ liệu validate và dữ
        /// liệu trainning với tỉ lệ nào đó.
        /// </summary>
        /// <param name="originalFile">File dữ liệu gốc</param>
        /// <param name="validateFile">File dữ liệu validate</param>
        /// <param name="newTrainningData">File dữ liệu trainning mới</param>
        /// <param name="percent">Phần trăm dữ liệu dùng để validate</param>
        public void CreateValidateData(string originalFilePath, string validateFilePath, string newTrainningDataPath, double percent)
        {
            List<string> listLine = new List<string>();
            using (StreamReader reader = File.OpenText(originalFilePath))
            {
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    listLine.Add(line);
                }
            }
            double numberLine = listLine.Count;
            int numberValidateLine = (int)(numberLine * percent / 100);
            Random r = new Random();
            // Create Validate Data File
            using (StreamWriter writer = File.CreateText(validateFilePath))
            {
                for (int i = 0; i < numberValidateLine; i++)
                {
                    int index = r.Next(0, listLine.Count - 1);
                    string vline = listLine[index];
                    writer.WriteLine(vline);
                    listLine.RemoveAt(index);
                }
            }
            // Create new trainning data
            using (StreamWriter writer = File.CreateText(newTrainningDataPath))
            {
                foreach (string tLine in listLine)
                {
                    writer.WriteLine(tLine);
                }
            }
        }
        #endregion

        /// <summary>
        /// Ghi các tri thức tìm được ra file để lưu trữ
        /// </summary>
        /// <param name="knowledge">Tri thức cần lưu</param>
        /// <param name="path">Đường dẫn</param>
        public void SaveKnowledge(KnowledgeC45 knowledge, string path)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binaryFormatter.Serialize(fStream, knowledge);
            }
        }
        /// <summary>
        /// Lấy các tri thức tìm được từ file.
        /// </summary>
        /// <param name="path">Đường dẫn</param>
        /// <returns>Tri thức</returns>
        public KnowledgeC45 ReadKnowledge(string path)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            KnowledgeC45 knowledge = null;
            using (Stream readStream = File.OpenRead(path))
            {
                knowledge = (KnowledgeC45)binaryFormatter.Deserialize(readStream);
            }
            return knowledge;
        }
        #endregion

        #region IFile Members

        /// <summary>
        /// Ghi doi tuong naive bayes ra de luu tru
        /// </summary>
        /// <param name="nb">Doi tuong nb</param>
        /// <param name="path">Duong dan</param>
        public void SaveNaiveBayes(KnowledgeNaiveBayes nb, string path)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binaryFormatter.Serialize(fStream, nb);
            }
        }
        /// <summary>
        /// Doc doi tuong naive bayes tu file
        /// </summary>
        /// <param name="path">Duong dan</param>
        /// <returns>Doi tuong naive bayes</returns>
        public KnowledgeNaiveBayes ReadNaiveBayes(string path)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            KnowledgeNaiveBayes nb = null;
            using (Stream readStream = File.OpenRead(path))
            {
                nb = (KnowledgeNaiveBayes)binaryFormatter.Deserialize(readStream);
            }
            return nb;
        }

        #endregion
    }
}
