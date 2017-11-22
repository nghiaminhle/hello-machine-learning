using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    public class C45 : IClassification, IC45
    {
        public static double count = 0;
        public static int countF = 0;
        public static int countDeQuy = 0;

        #region Private Fields
        
        private DiscreteIndex _indexChild;
        private Data _dataSet;
        private Node _root;
        private bool _isContructed;
        private FileProcessing _file;
        private double _infoT;
        private double _totalCase;
        private double _totalMissingCase;
        // Bien dung để xác định lần đầu tiến tính threshold cho mỗi attribute
        private bool _firtAgeThreshold;
        private bool _firtEduNumThreshold;
        private bool _firtFnlwgtThreshold;
        private bool _firtCapitalGainThreshold;
        private bool _firtCapitalLossThreshold;
        private bool _firtHourPerWeekThreshold;
        private Case[] _validateCases;
        private double _maxAccurate;

        #endregion

        #region Constructors
        public C45(Data dataSet, DiscreteIndex indexChild)
        {
            this._dataSet = dataSet;
            this._infoT = this._dataSet.Entropy(); // Luong tin trung binh.
            this._indexChild = indexChild;
            this._isContructed = false;
            this._file = new FileProcessing();
            this._totalCase = this._dataSet.NumberCase;
            this._totalMissingCase = this._dataSet.TotalMissingCase();
            
            this._firtAgeThreshold = true;
            this._firtCapitalGainThreshold = true;
            this._firtCapitalLossThreshold = true;
            this._firtEduNumThreshold = true;
            this._firtFnlwgtThreshold = true;
            this._firtHourPerWeekThreshold = true;

            this._validateCases = null;
            this._maxAccurate = 0;
        }
        #endregion

        #region Public Methods

        #region Constructing Tree
        /// <summary>
        /// Xây dựng cây.
        /// </summary>
        public void ConstructTree()
        {
            this._root = new Node(this._indexChild);

            List<Condition> listCon = new List<Condition>();
            List<AttributeCase> listAttr = new List<AttributeCase>();
            #region Add item into list Attributes
            string[] nameArr = Enum.GetNames(typeof(AttributeCase));
            int n = nameArr.Length;

            for (int i = 0; i < n; i++)
            {
                if (nameArr[i] != "unknow")
                {
                    AttributeCase attr = (AttributeCase)Enum.Parse(typeof(AttributeCase), nameArr[i]);
                    listAttr.Add(attr);
                }
            }

            #endregion

            this.makeTree(this._root, listAttr, 8, this._dataSet, null, listCon.ToArray());
            this._isContructed = true;
        } 
        #endregion

        #region Classification
        /// <summary>
        /// Phân loại một trường hợp
        /// </summary>
        /// <param name="ca">Một trường hợp bất kỳ</param>
        /// <returns>true: lon hon 50k$, false neu nho hon hoac bang 50k$</returns>
        public bool Classify(Case ca)
        {
            if (!this._isContructed)
                throw new Exception("You must construct tree first!");
            return true;
        } 
        #endregion

        #region Computing Tree
        /// <summary>
        /// Duyệt và đánh tính toán số nhánh con lớn hơn và nhỏ hơn 50K$ cho mỗi node.
        /// </summary>
        /// <param name="root">Node gốc</param>
        /// <returns>số node của cây</returns>
        public int ComputingTree(Node root)
        {
            int countNode = 0;
            if (!root.IsLeaf)
            {
                foreach (Node childNode in root.ListChild)
                {
                    if (childNode != null)
                    {
                        countNode++;
                        countNode += ComputingTree(childNode);
                        // So nhanh co thu nhap lon hon 50K$ 
                        childNode.Parent.GreaterBranchNum += childNode.GreaterBranchNum;
                        // So nhanh co thu nhap nhon hon 50K$
                        childNode.Parent.LessEqualBranchNum += childNode.LessEqualBranchNum;
                    }
                }
            }
            else
            {
                countNode = 1;
                // Tang so nhanh lon hon 50K$ cua node cha len 1
                if (root.Result)
                {
                    root.Parent.GreaterBranchNum += 1;
                }
                else
                {
                    // Tang so nhanh nho hon hoac bang 50K$ cua node cha len 1
                    root.Parent.LessEqualBranchNum += 1;
                }
            }
            return countNode;
        }

        #endregion

        #region Prunning Tree
        /// <summary>
        /// Tia cay de toi uu do chinh xac
        /// </summary>
        /// <param name="validateData">Mang cac case</param>
        public void OptimizeTree(Case[] validateData)
        {
            this._validateCases = validateData;
            this._maxAccurate = this.accuracy();
            Console.WriteLine("Accurate:{0}", this._maxAccurate);
            this.prunTree(this._root);
            Console.WriteLine("Accurate:{0}", this._maxAccurate);
            // Xoa cac subtree da bi danh dau xoa.
            this.deleteSubTree(this._root);
        } 
        #endregion

        #region Computing Accuracy of tree
        /// <summary>
        /// Tính toán độ chính xác của cây.
        /// </summary>
        /// <param name="testCases">Test Cases</param>
        /// <returns>Phần trăm chính xác</returns>
        public double Accuracy(Case[] testCases)
        {
            int n = testCases.Length;
            double numberCorrectly = 0;
            for (int i = 0; i < n; i++)
            {
                if (testCases[i].IsGreater == this._root.Classify(testCases[i]))
                {
                    numberCorrectly++;
                }
            }
            return (numberCorrectly / (double)n) * 100;
        }

        #endregion

        #endregion

        #region Private Methods

        #region Make Tree
        private void makeTree(Node node, List<AttributeCase> listRemainAttr, int numberRemainningDiscreteAttr, Data subset, double[] thresholdArr, params Condition[] listCondition)
        {
            // Filter du lieu
            Data set = subset.Filter(listCondition);
            double totalNumberOfSubset = set.NumberCase;
            // So truong hop co thu nhap lon hon 50K$
            double numberGreaterCase = set.NumberGreaterCase();
            // So truong hop co thu nhap nho hon 50K$
            double numberLessEquaCase = totalNumberOfSubset - numberGreaterCase;
            double[] bestThresholdArr = null;
            int bestThresPos = 0;

            // Neu tap subset la rong thi dung lai luon
            if (totalNumberOfSubset == 0)
            {
                // Loai bo nhanh con.
                Node parent = node.Parent;
                parent.ListChild[node.Pos] = null;
                node.Parent = null;
                //this._file.SaveListConditions(C45.countNull.ToString(), listCondition);
                return;
            }
            else
            {
                // Kiểm tra xem nếu cả tập subset đều thuộc cùng một nhóm thì dừng lại luôn.
                if (numberGreaterCase == totalNumberOfSubset)
                {
                    node.SetLeaf(true, totalNumberOfSubset, 0, numberGreaterCase, 0);
                    C45.count++;
                    return;
                }
                else if (numberGreaterCase == 0)
                {
                    node.SetLeaf(false, totalNumberOfSubset, 0, 0, numberLessEquaCase);
                    C45.count++;
                    return;
                }
            }
            double maxGainRatio = 0;
            AttributeCase attrChoosen = AttributeCase.unknow;
            double thresholdChoosen = 0;
            bool continous = false;

            bool checkSplit = true;

            foreach (AttributeCase attr in listRemainAttr)
            {
                switch (attr)
                {
                    #region Attribute Age
                    // Gia tri lien tuc
                    case AttributeCase.Age:
                        {
                            // Khoang cac gia tri lien tuc.
                            double[] threshold = null;
                            if (this._firtAgeThreshold)
                            {
                                threshold = this._dataSet.ThresholdArr[AttributeCase.Age];
                            }
                            else if (node.Parent != null && node.Parent.Label == AttributeCase.Age && thresholdArr != null)
                            {
                                // Lay mang threshold tu node tren dua xuong.
                                threshold = thresholdArr;
                            }
                            else
                                threshold = set.GetThreshold(AttributeCase.Age, new AgeComparer());
                            int n = threshold.GetLength(0);
                            // Break neu khong co khoang de chia
                            if (n == 0)
                            {
                                // Neu nhu khong con threshold nao thi dong nghia voi viec split info=0.
                                checkSplit = false;
                                break;
                            }
                            checkSplit = true;
                            double splitInfo = 0;
                            double infox = 0;
                            int posOfThres = 0;
                            // threshold duoc chon
                            double thres = this.chooseThreshold(set, attr, threshold, n, ref infox, ref splitInfo, ref posOfThres);
                            // Tinh gain ratio
                            double gainRatio = (this._infoT - infox) / splitInfo;
                            if (maxGainRatio < gainRatio)
                            {
                                maxGainRatio = gainRatio;
                                attrChoosen = AttributeCase.Age;
                                thresholdChoosen = thres;
                                continous = true;
                                bestThresholdArr = threshold;
                                bestThresPos = posOfThres;
                            }
                            // Giai phong
                            threshold = null;
                        }
                        break;
                    #endregion

                    #region Attribute Work Class
                    // Gia tri roi rac
                    case AttributeCase.WorkClass:
                        {
                            double missingNum = 0;
                            StatictisObject[] so = set.Statistic(AttributeCase.WorkClass, null);
                            // tinh gain ratio
                            double gainRatio = this.computeGainRation(so, missingNum);

                            // chon gia tri gain ratio lon nhat
                            if (maxGainRatio < gainRatio)
                            {
                                maxGainRatio = gainRatio;
                                attrChoosen = AttributeCase.WorkClass;
                                continous = false;
                            }
                        }
                        break;
                    #endregion

                    #region Attribute Fnlwgt
                    // Gia tri lien tuc
                    case AttributeCase.Fnlwgt:
                        {
                            // Khoang cac gia tri lien tuc.
                            double[] threshold = null;
                            if (this._firtFnlwgtThreshold)
                            {
                                threshold = this._dataSet.ThresholdArr[AttributeCase.Fnlwgt];
                                threshold = set.GetThreshold(AttributeCase.Fnlwgt, new FnlwgtComparer());
                            }
                            else if (node.Parent != null && node.Parent.Label == AttributeCase.Fnlwgt && thresholdArr != null)
                            {
                                // Lay mang threshold tu node tren dua xuong.
                                threshold = thresholdArr;
                            }
                            else
                                threshold = set.GetThreshold(AttributeCase.Fnlwgt, new FnlwgtComparer());
                            int n = threshold.GetLength(0);
                            // Break neu khong co khaong de chia
                            if (n == 0)
                            {
                                // Neu nhu khong con threshold nao thi dong nghia voi viec split info=0.
                                checkSplit = false;
                                break;
                            }
                            checkSplit = true;

                            double splitInfo = 0;
                            double infox = 0;
                            int posOfThres = 0;
                            // threshold duoc chon
                            double thres = this.chooseThreshold(set, attr, threshold, n, ref infox, ref splitInfo, ref posOfThres);
                            // Tinh gain ratio
                            double gainRatio = (this._infoT - infox) / splitInfo;
                            // Tinh gain
                            if (maxGainRatio < gainRatio)
                            {
                                maxGainRatio = gainRatio;
                                attrChoosen = AttributeCase.Fnlwgt;
                                thresholdChoosen = thres;
                                continous = true;
                                bestThresholdArr = threshold;
                                bestThresPos = posOfThres;
                            }
                            // Giai phong
                            threshold = null;
                        }
                        break;
                    #endregion

                    #region Attribute Education
                    // Roi rac.
                    case AttributeCase.Education:
                        {
                            double missingNum = 0;
                            StatictisObject[] so = set.Statistic(AttributeCase.Education, null);
                            // tinh gain ratio
                            double gainRatio = this.computeGainRation(so, missingNum);

                            // chon gia tri gain ratio lon nhat
                            if (maxGainRatio < gainRatio)
                            {
                                maxGainRatio = gainRatio;
                                attrChoosen = AttributeCase.Education;
                                continous = false;
                            }
                        }
                        break;
                    #endregion

                    #region Attribute EducationNum
                    // Gia tri lien tuc
                    case AttributeCase.EducationNum:
                        {
                            // Khoang cac gia tri lien tuc.
                            double[] threshold = null;
                            if (this._firtEduNumThreshold)
                            {
                                threshold = this._dataSet.ThresholdArr[AttributeCase.EducationNum];
                            }
                            else if (node.Parent != null && node.Parent.Label == AttributeCase.EducationNum && thresholdArr != null)
                            {
                                // Lay mang threshold tu node tren dua xuong.
                                threshold = thresholdArr;
                            }
                            else
                                threshold = set.GetThreshold(AttributeCase.EducationNum, new EduNumComparer());
                            int n = threshold.GetLength(0);
                            // Break neu khong co khoang de chia
                            if (n == 0)
                            {
                                // Neu nhu khong con threshold nao thi dong nghia voi viec split info=0.
                                checkSplit = false;
                                break;
                            }
                            checkSplit = true;

                            double splitInfo = 0;
                            double infox = 0;
                            int posOfThres = 0;
                            // threshold duoc chon
                            double thres = this.chooseThreshold(set, attr, threshold, n, ref infox, ref splitInfo, ref posOfThres);
                            // Tinh gain ratio
                            double gainRatio = (this._infoT - infox) / splitInfo;

                            if (maxGainRatio < gainRatio)
                            {
                                maxGainRatio = gainRatio;
                                attrChoosen = AttributeCase.EducationNum;
                                thresholdChoosen = thres;
                                continous = true;
                                bestThresholdArr = threshold;
                                bestThresPos = posOfThres;
                            }
                            // giai phong
                            threshold = null;
                        }
                        break;
                    #endregion

                    #region Attribute MaritalStatus
                    // Roi rac.
                    case AttributeCase.MaritalStatus:
                        {
                            double missingNum = 0;
                            StatictisObject[] so = set.Statistic(AttributeCase.MaritalStatus, null);
                            // tinh gain ratio
                            double gainRatio = this.computeGainRation(so, missingNum);
                            // chon gia tri gain ratio lon nhat
                            if (maxGainRatio < gainRatio)
                            {
                                maxGainRatio = gainRatio;
                                attrChoosen = AttributeCase.MaritalStatus;
                                continous = false;
                            }
                        }
                        break;
                    #endregion

                    #region Attribute Occupation
                    // Roi rac
                    case AttributeCase.Occupation:
                        {
                            double missingNum = 0;
                            StatictisObject[] so = set.Statistic(AttributeCase.Occupation, null);
                            // tinh gain ratio
                            double gainRatio = this.computeGainRation(so, missingNum);
                            // chon gia tri gain ratio lon nhat
                            if (maxGainRatio < gainRatio)
                            {
                                maxGainRatio = gainRatio;
                                attrChoosen = AttributeCase.Occupation;
                                continous = false;
                            }
                        }
                        break;
                    #endregion

                    #region Attribute Relationship
                    // Roi rac
                    case AttributeCase.Relationship:
                        {
                            double missingNum = 0;
                            StatictisObject[] so = set.Statistic(AttributeCase.Relationship, null);
                            // tinh gain ratio
                            double gainRatio = this.computeGainRation(so, missingNum);
                            // chon gia tri gain ratio lon nhat
                            if (maxGainRatio < gainRatio)
                            {
                                maxGainRatio = gainRatio;
                                attrChoosen = AttributeCase.Relationship;
                                continous = false;
                            }
                        }
                        break;
                    #endregion

                    #region Attribute Race
                    // Roi rac
                    case AttributeCase.Race:
                        {
                            double missingNum = 0;
                            StatictisObject[] so = set.Statistic(AttributeCase.Race, null);
                            // tinh gain ratio
                            double gainRatio = this.computeGainRation(so, missingNum);
                            // chon gia tri gain ratio lon nhat
                            if (maxGainRatio < gainRatio)
                            {
                                maxGainRatio = gainRatio;
                                attrChoosen = AttributeCase.Race;
                                continous = false;
                            }
                        }
                        break;
                    #endregion

                    #region Attribute Sex
                    // Roi rac
                    case AttributeCase.Sex:
                        {
                            double missingNum = 0;
                            StatictisObject[] so = set.Statistic(AttributeCase.Sex, null);
                            // tinh gain ratio
                            double gainRatio = this.computeGainRation(so, missingNum);
                            // chon gia tri gain ratio lon nhat
                            if (maxGainRatio < gainRatio)
                            {
                                maxGainRatio = gainRatio;
                                attrChoosen = AttributeCase.Sex;
                                continous = false;
                            }
                        }
                        break;
                    #endregion

                    #region Attribute CapitalGain
                    // Lien tuc
                    case AttributeCase.CapitalGain:
                        {
                            // Khoang cac gia tri lien tuc.
                            double[] threshold = null;
                            if (this._firtCapitalGainThreshold)
                            {
                                threshold = this._dataSet.ThresholdArr[AttributeCase.CapitalGain];
                            }
                            else if (node.Parent != null && node.Parent.Label == AttributeCase.CapitalGain && thresholdArr != null)
                            {
                                // Lay mang threshold tu node tren dua xuong.
                                threshold = thresholdArr;
                            }
                            else
                                threshold = set.GetThreshold(AttributeCase.CapitalGain, new CapitalGainComparer());
                            int n = threshold.GetLength(0);
                            // Break neu khong con khaong de chia
                            if (n == 0)
                            {
                                // Neu nhu khong con threshold nao thi dong nghia voi viec split info=0.
                                checkSplit = false;
                                break;
                            }
                            checkSplit = true;
                            double splitInfo = 0;
                            double infox = 0;
                            int posOfThres = 0;
                            // threshold duoc chon
                            double thres = this.chooseThreshold(set, attr, threshold, n, ref infox, ref splitInfo, ref posOfThres);
                            // Tinh gain ratio
                            double gainRatio = (this._infoT - infox) / splitInfo;

                            // Tinh gain
                            if (maxGainRatio < gainRatio)
                            {
                                maxGainRatio = gainRatio;
                                attrChoosen = AttributeCase.CapitalGain;
                                thresholdChoosen = thres;
                                continous = true;
                                bestThresholdArr = threshold;
                                bestThresPos = posOfThres;
                            }
                            // Giai phong
                            threshold = null;
                        }
                        break;
                    #endregion

                    #region Attribute Capital Loss
                    // Lien tuc
                    case AttributeCase.CapitalLoss:
                        {
                            // Khoang cac gia tri lien tuc.
                            double[] threshold = null;
                            if (this._firtCapitalLossThreshold)
                            {
                                threshold = this._dataSet.ThresholdArr[AttributeCase.CapitalLoss];
                            }
                            else if (node.Parent != null && node.Parent.Label == AttributeCase.CapitalLoss && thresholdArr != null)
                            {
                                // Lay mang threshold tu node tren dua xuong.
                                threshold = thresholdArr;
                            }
                            else
                                threshold = set.GetThreshold(AttributeCase.CapitalLoss, new CapitalLossComparer());
                            int n = threshold.GetLength(0);
                            // Break neu khogn con khoang de chia
                            if (n == 0)
                            {
                                // Neu nhu khong con threshold nao thi dong nghia voi viec split info=0.
                                checkSplit = false;
                                break;
                            }
                            checkSplit = true;
                            double splitInfo = 0;
                            double infox = 0;
                            int posOfThres = 0;
                            // threshold duoc chon
                            double thres = this.chooseThreshold(set, attr, threshold, n, ref infox, ref splitInfo, ref posOfThres);
                            // Tinh gain ratio
                            double gainRatio = (this._infoT - infox) / splitInfo;

                            // Tinh gain
                            if (maxGainRatio < gainRatio)
                            {
                                maxGainRatio = gainRatio;
                                attrChoosen = AttributeCase.CapitalLoss;
                                thresholdChoosen = thres;
                                continous = true;
                                bestThresholdArr = threshold;
                                bestThresPos = posOfThres;
                            }
                            // Giai phong
                            threshold = null;
                        }
                        break;
                    #endregion

                    #region Attribute Hours Per Week
                    // Lien tuc
                    case AttributeCase.HoursPerWeek:
                        {
                            // Khoang cac gia tri lien tuc.
                            double[] threshold = null;
                            if (this._firtHourPerWeekThreshold)
                            {
                                threshold = this._dataSet.ThresholdArr[AttributeCase.HoursPerWeek];
                            }
                            else if (node.Parent != null && node.Parent.Label == AttributeCase.HoursPerWeek && thresholdArr != null)
                            {
                                // Lay mang threshold tu node tren dua xuong.
                                threshold = thresholdArr;
                            }
                            else
                                threshold = set.GetThreshold(AttributeCase.HoursPerWeek, new AgeComparer());
                            int n = threshold.GetLength(0);
                            // Break neu khong con khoang de chia
                            if (n == 0)
                            {
                                // Neu nhu khong con threshold nao thi dong nghia voi viec split info=0.
                                checkSplit = false;
                                break;
                            }
                            checkSplit = true;
                            double splitInfo = 0;
                            double infox = 0;
                            int posOfThres = 0;
                            // threshold duoc chon
                            double thres = this.chooseThreshold(set, attr, threshold, n, ref infox, ref splitInfo, ref posOfThres);
                            // Tinh gain ratio
                            double gainRatio = (this._infoT - infox) / splitInfo;
                            // Tinh gain
                            if (maxGainRatio < gainRatio)
                            {
                                maxGainRatio = gainRatio;
                                attrChoosen = AttributeCase.HoursPerWeek;
                                thresholdChoosen = thres;
                                continous = true;
                                bestThresholdArr = threshold;
                                bestThresPos = posOfThres;
                            }
                            // Giai phong
                            threshold = null;
                        }
                        break;
                    #endregion

                    #region Attribute Naitive Country
                    // Roi rac
                    case AttributeCase.NativeCountry:
                        {
                            double missingNum = 0;
                            StatictisObject[] so = set.Statistic(AttributeCase.NativeCountry, null);
                            // tinh gain ratio
                            double gainRatio = this.computeGainRation(so, missingNum);

                            // chon gia tri gain ratio lon nhat
                            if (maxGainRatio < gainRatio)
                            {
                                maxGainRatio = gainRatio;
                                attrChoosen = AttributeCase.NativeCountry;
                                continous = false;
                            }
                        }
                        break;
                    #endregion

                    default:
                        break;
                }
            }
            if (checkSplit == false && numberRemainningDiscreteAttr == 0)
            {
                if (numberGreaterCase > numberLessEquaCase)
                {
                    node.SetLeaf(true, totalNumberOfSubset, numberLessEquaCase, numberGreaterCase, numberLessEquaCase);
                    C45.count++;
                    return;
                }
                else
                {
                    node.SetLeaf(false, totalNumberOfSubset, numberGreaterCase, numberGreaterCase, numberLessEquaCase);
                    C45.count++;
                    return;
                }
            }
            else
            {
                // Nhan la mot thuoc tinh lien tuc
                if (continous)
                {
                    node.MakeLabel(attrChoosen, thresholdChoosen, totalNumberOfSubset, numberGreaterCase, numberLessEquaCase);
                    C45.countF++;
                    C45.count++;

                    switch (attrChoosen)
                    {
                        case AttributeCase.Age:
                            this._firtAgeThreshold = false;
                            break;
                        case AttributeCase.Fnlwgt:
                            this._firtFnlwgtThreshold = false;
                            break;
                        case AttributeCase.EducationNum:
                            this._firtEduNumThreshold = false;
                            break;
                        case AttributeCase.CapitalGain:
                            this._firtCapitalGainThreshold = false;
                            break;
                        case AttributeCase.CapitalLoss:
                            this._firtCapitalLossThreshold = false;
                            break;
                        case AttributeCase.HoursPerWeek:
                            this._firtHourPerWeekThreshold = false;
                            break;
                        default:
                            break;
                    }

                    double[] thresholdLessArr = null;
                    double[] thresholdGreaterArr = null;

                    if (bestThresPos > 0 && bestThresPos < bestThresholdArr.Length - 1)
                    {
                        thresholdLessArr = new double[bestThresPos];
                        int i = 0;
                        for (i = 0; i < bestThresPos; i++)
                        {
                            thresholdLessArr[i] = bestThresholdArr[i];
                        }
                        thresholdGreaterArr = new double[bestThresholdArr.Length - 1 - bestThresPos];
                        int n = bestThresholdArr.Length;
                        int j = 0;
                        for (i = bestThresPos + 1; i < n; i++)
                        {
                            thresholdGreaterArr[j] = bestThresholdArr[i];
                            j++;
                        }
                    }

                    Condition con1 = new Condition(attrChoosen, thresholdChoosen.ToString(), StateCompare.LessEqua);
                    Condition con2 = new Condition(attrChoosen, thresholdChoosen.ToString(), StateCompare.Greater);

                    Node childNode1 = node.ListChild[0];

                    // Goi de quy
                    C45.countDeQuy++;
                    this.makeTree(childNode1, listRemainAttr, numberRemainningDiscreteAttr, set, thresholdLessArr, con1);
                    C45.countDeQuy--;

                    Node childNode2 = node.ListChild[1];
                    // Goi de quy
                    C45.countDeQuy++;
                    this.makeTree(childNode2, listRemainAttr, numberRemainningDiscreteAttr, set, thresholdGreaterArr, con2);
                    C45.countDeQuy--;
                }
                else // thuoc tinh roi rac
                {
                    node.MakeLabel(attrChoosen, null, totalNumberOfSubset, numberGreaterCase, numberLessEquaCase);
                    C45.count++;
                    // Loai bo thuoc tinh sau khi danh nhan
                    listRemainAttr.Remove(attrChoosen);
                    numberRemainningDiscreteAttr--;
                    //set = null;

                    switch (attrChoosen)
                    {
                        #region Attribute Work Class
                        // Gia tri roi rac
                        case AttributeCase.WorkClass:
                            {
                                string[] strArr = Enum.GetNames(typeof(WorkClass));
                                int i = 0;
                                foreach (string str in strArr)
                                {
                                    if (str != "unknow")
                                    {
                                        Condition con = new Condition(AttributeCase.WorkClass, str, StateCompare.Equal);
                                        C45.countDeQuy++;
                                        this.makeTree(node.ListChild[i], listRemainAttr, numberRemainningDiscreteAttr, set, null, con);
                                        C45.countDeQuy--;
                                        i++;
                                    }
                                }
                            }
                            break;
                        #endregion

                        #region Attribute Education
                        // Roi rac.
                        case AttributeCase.Education:
                            {
                                string[] strArr = Enum.GetNames(typeof(Education));
                                int i = 0;
                                foreach (string str in strArr)
                                {
                                    if (str != "unknow")
                                    {
                                        Condition con = new Condition(AttributeCase.Education, str, StateCompare.Equal);
                                        C45.countDeQuy++;
                                        this.makeTree(node.ListChild[i], listRemainAttr, numberRemainningDiscreteAttr, set, null, con);
                                        C45.countDeQuy--;
                                        i++;
                                    }
                                }
                            }
                            break;
                        #endregion

                        #region Attribute MaritalStatus
                        // Roi rac.
                        case AttributeCase.MaritalStatus:
                            {
                                int i = 0;
                                string[] strArr = Enum.GetNames(typeof(MaritalStatus));
                                foreach (string str in strArr)
                                {
                                    if (str != "unknow")
                                    {
                                        Condition con = new Condition(AttributeCase.MaritalStatus, str, StateCompare.Equal);
                                        C45.countDeQuy++;
                                        this.makeTree(node.ListChild[i], listRemainAttr, numberRemainningDiscreteAttr, set, null, con);
                                        C45.countDeQuy--;
                                        i++;
                                    }
                                }
                            }
                            break;
                        #endregion

                        #region Attribute Occupation
                        // Roi rac
                        case AttributeCase.Occupation:
                            {
                                int i = 0;
                                string[] strArr = Enum.GetNames(typeof(Occupation));
                                foreach (string str in strArr)
                                {
                                    if (str != "unknow")
                                    {
                                        Condition con = new Condition(AttributeCase.Occupation, str, StateCompare.Equal);
                                        C45.countDeQuy++;
                                        this.makeTree(node.ListChild[i], listRemainAttr, numberRemainningDiscreteAttr, set, null, con);
                                        C45.countDeQuy--;
                                        i++;
                                    }
                                }
                            }
                            break;
                        #endregion

                        #region Attribute Relationship
                        // Roi rac
                        case AttributeCase.Relationship:
                            {
                                int i = 0;
                                string[] strArr = Enum.GetNames(typeof(Relationship));
                                foreach (string str in strArr)
                                {
                                    if (str != "unknow")
                                    {
                                        Condition con = new Condition(AttributeCase.Relationship, str, StateCompare.Equal);
                                        C45.countDeQuy++;
                                        this.makeTree(node.ListChild[i], listRemainAttr, numberRemainningDiscreteAttr, set, null, con);
                                        C45.countDeQuy--;
                                        i++;
                                    }
                                }
                            }
                            break;
                        #endregion

                        #region Attribute Race
                        // Roi rac
                        case AttributeCase.Race:
                            {
                                int i = 0;
                                string[] strArr = Enum.GetNames(typeof(Race));
                                foreach (string str in Enum.GetNames(typeof(Race)))
                                {
                                    if (str != "unknow")
                                    {
                                        Condition con = new Condition(AttributeCase.Race, str, StateCompare.Equal);
                                        C45.countDeQuy++;
                                        this.makeTree(node.ListChild[i], listRemainAttr, numberRemainningDiscreteAttr, set, null, con);
                                        C45.countDeQuy--;
                                        i++;
                                    }
                                }
                            }
                            break;
                        #endregion

                        #region Attribute Sex
                        // Roi rac
                        case AttributeCase.Sex:
                            {
                                int i = 0;
                                string[] strArr = Enum.GetNames(typeof(Sex));
                                foreach (string str in strArr)
                                {
                                    if (str != "unknow")
                                    {
                                        Condition con = new Condition(AttributeCase.Sex, str, StateCompare.Equal);
                                        C45.countDeQuy++;
                                        this.makeTree(node.ListChild[i], listRemainAttr, numberRemainningDiscreteAttr, set, null, con);
                                        C45.countDeQuy--;
                                        i++;
                                    }
                                }
                            }
                            break;
                        #endregion

                        #region Attribute Naitive Country
                        // Roi rac
                        case AttributeCase.NativeCountry:
                            {
                                int i = 0;
                                string[] strArr = Enum.GetNames(typeof(NativeCountry));
                                foreach (string str in strArr)
                                {
                                    if (str != "unknow")
                                    {
                                        Condition con = new Condition(AttributeCase.NativeCountry, str, StateCompare.Equal);
                                        C45.countDeQuy++;
                                        this.makeTree(node.ListChild[i], listRemainAttr, numberRemainningDiscreteAttr, set, null, con);
                                        C45.countDeQuy--;
                                        i++;
                                    }
                                }
                            }
                            break;
                        #endregion
                        default:
                            break;
                    }
                    // Add lai thuoc tinh da remove
                    listRemainAttr.Add(attrChoosen);
                }
            }
        }
        #endregion

        #region Choose Threshold
        private double chooseThreshold(Data subset, AttributeCase attr, double[] threshold, int n, ref double infox, ref double splitInfo, ref int posThres)
        {
            // Tim mot threshold sao cho co infox nho nhat(ung voi gain lon nhat)
            double minInfox = 1;
            double spl = 0;
            double thr = 0; // threshold se duoc chon
            int pos = 0;

            for (int i = 0; i < n; i++)
            {
                double thres = threshold[i];
                StatictisObject[] so = subset.Statistic(attr, thres);
                // Để đếm các trường hợp có giá trị nhỏ hơn threshold
                double numberLessEqua1 = so[0].NumberLessEqua;
                double numberGreater1 = so[0].NumberGreater;
                double totalLess = so[0].Total;
                // Để đếm các trường hợp có giá trị lớn hơn threshold
                double numberLessEqual2 = so[1].NumberLessEqua;
                double numberGreater2 = so[1].NumberGreater;
                double totalGreater = so[1].Total;

                // Tinh info X
                #region Tinh infoX
                double t1 = 0;
                if (numberLessEqua1 != 0)
                {
                    t1 = -(numberLessEqua1 / totalLess) * Math.Log((numberLessEqua1 / totalLess), 2);
                }
                double t2 = 0;
                if (numberGreater1 != 0)
                {
                    t2 = -(numberGreater1 / totalLess) * Math.Log((numberGreater1 / totalLess), 2);
                }
                double t3 = 0;
                if (numberLessEqual2 != 0)
                {
                    t3 = -(numberLessEqual2 / totalGreater) * Math.Log((numberLessEqual2 / totalGreater), 2);
                }
                double t4 = 0;
                if (numberGreater2 != 0)
                {
                    t4 = -(numberGreater2 / totalGreater) * Math.Log((numberGreater2 / totalGreater), 2);
                }
                double tempInfox = (totalLess / (this._totalCase)) * (t1 + t2)
                           + (totalGreater / (this._totalCase)) * (t3 + t4);
                #endregion

                if (minInfox > tempInfox)
                {
                    minInfox = tempInfox;
                    thr = thres;
                    pos = i;
                    #region Tinh split info
                    double s1 = 0;
                    if (totalLess != 0)
                    {
                        s1 = (-totalLess / this._totalCase) * Math.Log(totalLess / this._totalCase, 2);
                    }
                    double s2 = 0;
                    if (totalGreater != 0)
                    {
                        s2 = -(totalGreater / this._totalCase) * Math.Log(totalGreater / this._totalCase, 2);
                    }
                    spl = s1 + s2;
                    #endregion
                }
            }
            // Tra lai gia tri
            infox = minInfox;
            splitInfo = spl;
            posThres = pos;
            return thr;
        }
        #endregion

        #region Computing Gain Ratio of discreted attributes
        /// <summary>
        /// Tính gain ration của một thuộc tính rời rạc
        /// </summary>
        /// <param name="so">mảng các thống kê ứng với các value của môt thuộc tính rời rạc</param>
        /// <param name="missingNum">số phần tử bị missing ứng với attribute</param>
        /// <returns>Giá trị gain ratio</returns>
        private double computeGainRation(StatictisObject[] so, double missingNum)
        {
            int n = so.Length;
            // Tinh info
            double infox = 0;
            double splitInfox = 0;
            for (int i = 0; i < n; i++)
            {
                if (so[i].Total != 0)
                {
                    double t1 = 0;
                    if (so[i].NumberLessEqua != 0)
                    {
                        t1 = -(so[i].NumberLessEqua / so[i].Total) * Math.Log(so[i].NumberLessEqua / so[i].Total, 2);
                    }
                    double t2 = 0;
                    if (so[i].NumberGreater != 0)
                    {
                        t2 = -(so[i].NumberGreater / so[i].Total) * Math.Log(so[i].NumberGreater / so[i].Total, 2);
                    }
                    infox += (so[i].Total / (this._totalCase - missingNum)) * (t1 + t2);
                    splitInfox += -(so[i].Total / this._totalCase) * Math.Log(so[i].Total / this._totalCase, 2);
                }
            }
            so = null;
            if (missingNum != 0)
                splitInfox += -(missingNum / this._totalCase) * Math.Log(missingNum / this._totalCase, 2);
            // tinh gain ratio
            double gainRatio = (((this._totalCase - missingNum) / this._totalCase) * (this._infoT - infox)) / splitInfox;
            return gainRatio;
        }
        #endregion

        #region Delete Subtree
        private void deleteSubTree(Node root)
        {
            if (!root.IsLeaf)
            {
                if (root.IsPruned)
                {
                    foreach (Node childNode in root.ListChild)
                    {
                        if (childNode != null)
                            childNode.Parent = null;
                    }
                    root.ListChild = null;
                    return;
                }
                else
                {
                    foreach (Node childNode in root.ListChild)
                    {
                        if (childNode != null)
                        {
                            deleteSubTree(childNode);
                        }
                    }
                }
            }
        }
        #endregion

        #region Prunning Tree
        /// <summary>
        /// Tia cay
        /// </summary>
        /// <param name="root">Goc cua cay</param>
        private void prunTree(Node root)
        {
            if (!root.IsLeaf)
            {
                foreach (Node childNode in root.ListChild)
                {
                    if (childNode != null)
                    {
                        prunTree(childNode);
                    }
                }
                // Tia cay.
                root.IsPruned = true;
                root.IsLeaf = true;
                if (root.GreaterBranchNum > root.LessEqualBranchNum)
                {
                    // Gan cho nhan pho bien nhat
                    root.Result = true;
                    root.NumberError = root.LessEquaNumber;
                }
                else
                {
                    root.Result = false;
                    root.NumberError = root.GreaterNumber;
                }
                // Danh gia do chinh xac cua cay sau khi tia
                double accuracy = this.accuracy();
                if (accuracy <= this._maxAccurate)
                {
                    root.IsLeaf = false;
                    root.NumberError = 0;
                    root.IsPruned = false;
                }
                else
                {
                    this._maxAccurate = accuracy;
                    Console.WriteLine("Accurate:{0}", this._maxAccurate);
                }
            }
        }
        #endregion

        #region Private Accuracy
        /// <summary>
        /// Tinh do chinh xac cua cay.
        /// Thiet ke de tang toc do tinh toan.
        /// </summary>
        /// <returns>do chinh xac (%)</returns>
        private double accuracy()
        {
            int n = this._validateCases.Length;
            double numberCorrectly = 0;
            for (int i = 0; i < n; i++)
            {
                if (this._validateCases[i].IsGreater == this._root.Classify(this._validateCases[i]))
                {
                    numberCorrectly++;
                }
            }
            return (numberCorrectly / (double)n) * 100;
        }
        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Xác định xem cây đã được xây dựng xong chưa
        /// </summary>
        public bool IsConstructed
        {
            get { return this._isContructed; }
        }
        /// <summary>
        /// Trả lại node gốc của cây quyết định
        /// </summary>
        public Node Root
        {
            get { return _root; }
            set { _root = value; }
        }
        #endregion
    }
}