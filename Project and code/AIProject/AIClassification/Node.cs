using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    [Serializable]
    public class Node:IEnumerable<Node>
    {
        #region Private Fields
        // Nhan cu mot node trong
        private AttributeCase _label;
        // Node cha
        private Node _parent;
        // Mang cac con cua mot node
        private Node[] _listChild;
        // Doi tuong chua index cua cac gia tri cua thuoc tinh roi rac.
        private DiscreteIndex _indexOfChild;
        // Khoang duoc chon ung voi truong la duoc danh dau ung voi thuoc tinh lien tuc
        private double _threshold;
        // True neu la la, ngoac lai la false
        private bool _isLeaf;
        // So luong tap subset tai thoi diem node duoc chon
        private double _numberCase;
        // So loi tai vi tri la
        private double _numberError;
        // So truong hop khi node duoc danh nhan co thu nhap lon hon 50K$
        private double _greaterNumber;
        // So truong hop khi node duoc danh nhan co thu nhap nho hon hoac bang 50K$
        private double _lessEquaNumber;
        // Ket qua node duoc danh dau ung voi viec lon hon hay nho 50k$
        private bool _result;
        // Vi tri no con trong mang cac node con cua node cha
        private int _pos;
        // Xac dinh xem node da duoc prune hay chua.
        private bool _isPruned;
        // Số nhánh con của node mà có kết luận lớn hơn 50k$
        private int _greaterBranchNum;
        // Số nhánh con của node mà từ đó có kết luận nhỏ hơn hoặc bằng 50K4
        private int _lessEqualBranchNum;
        
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructors.
        /// </summary>
        public Node(DiscreteIndex indexOfChild)
        {
            this._parent = null;
            this._listChild = null;
            this._threshold = 0;
            this._isLeaf = false;
            this._numberCase = 0;
            this._numberError = 0;
            this._result = false;
            this._indexOfChild = indexOfChild;
            this._label = AttributeCase.unknow;
            this._pos = 0;
            this._greaterNumber = 0;
            this._lessEquaNumber = 0;
            this._isPruned = false;
            this._greaterBranchNum = 0;
            this._lessEqualBranchNum = 0;
        }
        
        //public Node(AttributeCase label)
        #endregion

        #region Public Methods

        #region Make Label for node
        /// <summary>
        /// Đánh nhãn thuộc tính cho mỗi node trong (không phải là node lá).
        /// </summary>
        /// <param name="attr">Tên thuộc tính</param>
        /// <param name="threshold">Khoảng giá trị nếu đấy là thuộc tính liên tục</param>
        public void MakeLabel(AttributeCase attr, double? threshold, double numberCase, double greaterNum, double lessEqualNum)
        {
            this._numberCase = numberCase;
            this._greaterNumber = greaterNum;
            this._lessEquaNumber = lessEqualNum;
            // Gia tri lien tuc
            if (threshold != null)
            {
                this._threshold = (int)threshold;
                this._label = attr;

                this._listChild = new Node[2];

                this._listChild[0] = new Node(this._indexOfChild);
                this._listChild[0].Parent = this;
                this._listChild[0]._pos = 0;

                this._listChild[1] = new Node(this._indexOfChild);
                this._listChild[1].Parent = this;
                this._listChild[1]._pos = 1;
            }
            else
            {
                this._label = attr;
                switch (attr)
                {
                    case AttributeCase.Education:
                        {
                            this._listChild = new Node[_indexOfChild.EducationValues];
                            int n = this._listChild.Count();
                            for (int i = 0; i < n; i++)
                            {
                                this._listChild[i] = new Node(this._indexOfChild);
                                this._listChild[i].Parent = this;
                                this._listChild[i]._pos = i;
                            }
                        }
                        break;
                    case AttributeCase.MaritalStatus:
                        {
                            this._listChild = new Node[this._indexOfChild.MaritalStatusValues];
                            int n = this._listChild.Count();
                            for (int i = 0; i < n; i++)
                            {
                                this._listChild[i] = new Node(this._indexOfChild);
                                this._listChild[i].Parent = this;
                                this._listChild[i]._pos = i;
                            }
                        }
                        break;
                    case AttributeCase.NativeCountry:
                        {
                            this._listChild = new Node[this._indexOfChild.NaitiveCountryValues];
                            int n = this._listChild.Count();
                            for (int i = 0; i < n; i++)
                            {
                                this._listChild[i] = new Node(this._indexOfChild);
                                this._listChild[i].Parent = this;
                                this._listChild[i]._pos = i;
                            }
                        }
                        break;
                    case AttributeCase.Occupation:
                        {
                            this._listChild = new Node[this._indexOfChild.OccupationValues];
                            int n = this._listChild.Count();
                            for (int i = 0; i < n; i++)
                            {
                                this._listChild[i] = new Node(this._indexOfChild);
                                this._listChild[i].Parent = this;
                                this._listChild[i]._pos = i;
                            }
                        }
                        break;
                    case AttributeCase.Race:
                        {
                            this._listChild = new Node[this._indexOfChild.RaceValues];
                            int n = this._listChild.Count();
                            for (int i = 0; i < n; i++)
                            {
                                this._listChild[i] = new Node(this._indexOfChild);
                                this._listChild[i].Parent = this;
                                this._listChild[i]._pos = i;
                            }
                        }
                        break;
                    case AttributeCase.Relationship:
                        {
                            this._listChild = new Node[this._indexOfChild.RelationshipValues];
                            int n = this._listChild.Count();
                            for (int i = 0; i < n; i++)
                            {
                                this._listChild[i] = new Node(this._indexOfChild);
                                this._listChild[i].Parent = this;
                                this._listChild[i]._pos = i;
                            }
                        }
                        break;
                    case AttributeCase.Sex:
                        {
                            this._listChild = new Node[this._indexOfChild.SexValues];
                            int n = this._listChild.Count();
                            for (int i = 0; i < n; i++)
                            {
                                this._listChild[i] = new Node(this._indexOfChild);
                                this._listChild[i].Parent = this;
                                this._listChild[i]._pos = i;
                            }
                        }
                        break;
                    case AttributeCase.WorkClass:
                        {
                            this._listChild = new Node[this._indexOfChild.WorkClassValues];
                            int n = this._listChild.Count();
                            for (int i = 0; i < n; i++)
                            {
                                this._listChild[i] = new Node(this._indexOfChild);
                                this._listChild[i].Parent = this;
                                this._listChild[i]._pos = i;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        
        #endregion

        #region Set Leaf State for node
        /// <summary>
        /// Xét trạng thái của một node là lá. 
        /// </summary>
        /// <param name="result">Kết quả là lớn hơn hay nhỏ 50$. True- Lớn hơn 50K$; Fasle-Nhỏ hơn hoặc bằng 50K$</param>
        public void SetLeaf(bool result, double numberCase, double numberError, double greaterNum, double lessEqualNum)
        {
            this._isLeaf = true;
            this._result = result;
            this._numberCase = numberCase;
            this._numberError = numberError;
            this._greaterNumber = greaterNum;
            this._lessEquaNumber = lessEqualNum;
            this._label = AttributeCase.unknow;
        } 
        #endregion

        #region Classify
        /// <summary>
        /// Xác định một trường hợp đưa vào thuộc nhóm nào
        /// </summary>
        /// <param name="ca">Một trường hợp bất kỳ</param>
        /// <returns>Nhóm phân loại</returns>
        public bool Classify(Case ca)
        {
            Node node = this;
            Node parent = null;
            while (!node.IsLeaf)
            {
                parent = node;
                switch (node.Label)
                {
                    #region Age
                    case AttributeCase.Age:
                        {
                            if (ca.Age >= 0)
                            {
                                if (ca.Age <= node.Threshold)
                                {
                                    node = node.ListChild[0];
                                }
                                else
                                    node = node.ListChild[1];
                            }
                        }
                        break; 
                    #endregion

                    #region Capital Gain
                    case AttributeCase.CapitalGain:
                        {
                            if (ca.CapitalGain >= 0)
                            {
                                if (ca.CapitalGain <= node.Threshold)
                                {
                                    node = node.ListChild[0];
                                }
                                else
                                    node = node.ListChild[1];
                            }
                        }
                        break; 
                    #endregion

                    #region Capital Loss
                    case AttributeCase.CapitalLoss:
                        {
                            if (ca.CapitalLoss >= 0)
                            {
                                if (ca.CapitalLoss <= node.Threshold)
                                {
                                    node = node.ListChild[0];
                                }
                                else
                                    node = node.ListChild[1];
                            }
                        }
                        break; 
                    #endregion

                    #region Education
                    case AttributeCase.Education:
                        {
                            if (ca.Edu != Education.unknow)
                            {
                                int index = this._indexOfChild.IndexValues(AttributeCase.Education, ca.Edu);
                                node = node.ListChild[index];
                            }
                        }
                        break; 
                    #endregion

                    #region Education Num
                    case AttributeCase.EducationNum:
                        {
                            if (ca.EduNum >= 0)
                            {
                                if (ca.EduNum <= node.Threshold)
                                {
                                    node = node.ListChild[0];
                                }
                                else
                                    node = node.ListChild[1];
                            }
                        }
                        break; 
                    #endregion

                    #region Fnlwgt
                    case AttributeCase.Fnlwgt:
                        {
                            if (ca.Fnlwgt >= 0)
                            {
                                if (ca.Fnlwgt <= node.Threshold)
                                {
                                    node = node.ListChild[0];
                                }
                                else
                                    node = node.ListChild[1];
                            }
                        }
                        break; 
                    #endregion

                    #region Hours Per Week
                    case AttributeCase.HoursPerWeek:
                        {
                            if (ca.HoursPerWeek >= 0)
                            {
                                if (ca.HoursPerWeek <= node.Threshold)
                                {
                                    node = node.ListChild[0];
                                }
                                else
                                    node = node.ListChild[1];
                            }
                        }
                        break; 
                    #endregion

                    #region Marital Status
                    case AttributeCase.MaritalStatus:
                        {
                            if (ca.MariSt != MaritalStatus.unknow)
                            {
                                int index = this._indexOfChild.IndexValues(AttributeCase.MaritalStatus, ca.MariSt);
                                node = node.ListChild[index];
                            }
                        }
                        break; 
                    #endregion

                    #region Native Country
                    case AttributeCase.NativeCountry:
                        {
                            if (ca.NtvCountry != NativeCountry.unknow)
                            {
                                int index = this._indexOfChild.IndexValues(AttributeCase.NativeCountry, ca.NtvCountry);
                                node = node.ListChild[index];
                            }
                        }
                        break; 
                    #endregion

                    #region Occupation
                    case AttributeCase.Occupation:
                        {
                            if (ca.Occ != Occupation.unknow)
                            {
                                int index = this._indexOfChild.IndexValues(AttributeCase.Occupation, ca.Occ);
                                node = node.ListChild[index];
                            }
                        }
                        break; 
                    #endregion

                    #region Race
                    case AttributeCase.Race:
                        {
                            if (ca.Ra != Race.unknow)
                            {
                                int index = this._indexOfChild.IndexValues(AttributeCase.Race, ca.Ra);
                                node = node.ListChild[index];
                            }
                        }
                        break; 
                    #endregion

                    #region Relationship
                    case AttributeCase.Relationship:
                        {
                            if (ca.Reltsh != Relationship.unknow)
                            {
                                int index = this._indexOfChild.IndexValues(AttributeCase.Relationship, ca.Reltsh);
                                node = node.ListChild[index];
                            }
                        }
                        break; 
                    #endregion

                    #region Sex
                    case AttributeCase.Sex:
                        {
                            if (ca.SexCase != Sex.unknow)
                            {
                                int index = this._indexOfChild.IndexValues(AttributeCase.Sex, ca.SexCase);
                                node = node.ListChild[index];
                            }
                        }
                        break; 
                    #endregion

                    #region Work Class
                    case AttributeCase.WorkClass:
                        {
                            if (ca.Wcl != WorkClass.unknow)
                            {
                                int index = this._indexOfChild.IndexValues(AttributeCase.WorkClass, ca.Wcl);
                                node = node.ListChild[index];
                            }
                        }
                        break; 
                    #endregion
                    default:
                        break;
                }
                if (node==null)
                {
                    if (parent.GreaterBranchNum > parent.LessEqualBranchNum)
                        return true;
                    else
                        return false;
                }
            }
            return node.Result;
        } 
        #endregion

        #region Get child node

        /// <summary>
        /// Lấy một node con ứng với nhãn là một thuộc tính nào đó
        /// </summary>
        /// <param name="attr">Thuộc tính nhãn của một node</param>
        /// <param name="value">Giá trị ứng với thuộc tính của node</param>
        /// <returns>Một node con phù hợp</returns>
        public Node GetChildNode(AttributeCase attr, object value)
        {
            Node node = null;
            if (attr != _label)
            {
                throw new Exception("Attr is not the label of the node");
            }
            else
            {
                switch (this._label)
                {
                    case AttributeCase.Age:
                        {
                            int val = (int)value;
                            if (val <= this._threshold)
                            {
                                node = this._listChild[0];
                            }
                            else
                                node = this._listChild[1];
                        }
                        break;
                    case AttributeCase.CapitalGain:
                        {
                            int val = (int)value;
                            if (val <= this._threshold)
                            {
                                node = this._listChild[0];
                            }
                            else
                                node = this._listChild[1];
                        }
                        break;
                    case AttributeCase.CapitalLoss:
                        {
                            int val = (int)value;
                            if (val <= this._threshold)
                            {
                                node = this._listChild[0];
                            }
                            else
                                node = this._listChild[1];
                        }
                        break;
                    case AttributeCase.Education:
                        {
                            Education val = (Education)value;
                            int index = this._indexOfChild.IndexValues(AttributeCase.Education, val);
                            node = this._listChild[index];
                        }
                        break;
                    case AttributeCase.EducationNum:
                        {
                            int val = (int)value;
                            if (val <= this._threshold)
                            {
                                node = this._listChild[0];
                            }
                            else
                                node = this._listChild[1];
                        }
                        break;
                    case AttributeCase.Fnlwgt:
                        {
                            int val = (int)value;
                            if (val <= this._threshold)
                            {
                                node = this._listChild[0];
                            }
                            else
                                node = this._listChild[1];
                        }
                        break;
                    case AttributeCase.HoursPerWeek:
                        {
                            int val = (int)value;
                            if (val <= this._threshold)
                            {
                                node = this._listChild[0];
                            }
                            else
                                node = this._listChild[1];
                        }
                        break;
                    case AttributeCase.MaritalStatus:
                        {
                            MaritalStatus mrt = (MaritalStatus)value;
                            int index = this._indexOfChild.IndexValues(AttributeCase.MaritalStatus, mrt);
                            node = this._listChild[index];
                        }
                        break;
                    case AttributeCase.NativeCountry:
                        {
                            NativeCountry ntc = (NativeCountry)value;
                            int index = this._indexOfChild.IndexValues(AttributeCase.NativeCountry, ntc);
                            node = this._listChild[index];
                        }
                        break;
                    case AttributeCase.Occupation:
                        {
                            Occupation occ = (Occupation)value;
                            int index = this._indexOfChild.IndexValues(AttributeCase.Occupation, occ);
                            node = this._listChild[index];
                        }
                        break;
                    case AttributeCase.Race:
                        {
                            Race race = (Race)value;
                            int index = this._indexOfChild.IndexValues(AttributeCase.Race, race);
                            node = this._listChild[index];
                        }
                        break;
                    case AttributeCase.Relationship:
                        {
                            Relationship res = (Relationship)value;
                            int index = this._indexOfChild.IndexValues(AttributeCase.Relationship, res);
                            node = this._listChild[index];
                        }
                        break;
                    case AttributeCase.Sex:
                        {
                            Sex sex = (Sex)value;
                            int index = this._indexOfChild.IndexValues(AttributeCase.Sex, sex);
                            node = this._listChild[index];
                        }
                        break;
                    case AttributeCase.WorkClass:
                        {
                            WorkClass wcl = (WorkClass)value;
                            int index = this._indexOfChild.IndexValues(AttributeCase.WorkClass, wcl);
                            node = this._listChild[index];
                        }
                        break;
                    default:
                        break;
                }
            }
            return node;
        }
        
        #endregion

        #endregion

        #region Private Methods
        // Write private methods here
        #endregion

        #region Properties
        /// <summary>
        /// Nhãn thuộc tính của một node.
        /// </summary>
        public AttributeCase Label
        {
            get { return this._label; }
            set { this._label = value; }
        }
        /// <summary>
        /// Xác định trạng thái của một node có phải là mức lá hay không?
        /// </summary>
        public bool IsLeaf
        {
            get { return this._isLeaf; }
            set { this._isLeaf = value; }
        }
        /// <summary>
        /// Số trường hợp khi xét ở mức lá
        /// </summary>
        public double NumberCase
        {
            get { return this._numberCase; }
            set { this._numberCase = value; }
        }
        /// <summary>
        /// Số trường hợp bị lỗi khi xét tới mức lá.
        /// </summary>
        public double NumberError
        {
            get { return this._numberError; }
            set { this._numberError = value; }
        }
        /// <summary>
        /// Số trường hợp tới khi node được xét đánh nhãn có thu nhập lớn hơn 50K$
        /// </summary>
        public double GreaterNumber
        {
            get { return _greaterNumber; }
            set { _greaterNumber = value; }
        }
        /// <summary>
        /// Số trường hợp tới khi node được xét đánh nhãn có thu nhập nhỏ hoặc bằng 50K$
        /// </summary>
        public double LessEquaNumber
        {
            get { return _lessEquaNumber; }
            set { _lessEquaNumber = value; }
        }
        /// <summary>
        /// Trạng thái của một node lá: true- lớn hơn 50K$ và false-nhỏ hơn hoặc bằng 50k$.
        /// </summary>
        public bool Result
        {
            get
            {
                if (!this._isLeaf)
                    throw new Exception("It is not leaf");
                return this._result;
            }
            set { this._result = value; }
        }
        /// <summary>
        /// Number of child nodes
        /// </summary>
        public int NumberChilds
        {
            get 
            {
                if (this._listChild == null)
                    return 0;
                return this._listChild.Count(); 
            }
        }
        /// <summary>
        /// Node of parent;
        /// </summary>
        public Node Parent
        {
            get { return this._parent; }
            set { this._parent = value; }
        }

        public Node[] ListChild
        {
            get { return this._listChild; }
            set { this._listChild = value; }
        }
        /// <summary>
        /// Vi tri cua mot node con trong danh sach cac node con cua node cha
        /// </summary>
        public int Pos
        {
            get { return this._pos; }
            set { this._pos = value; }
        }
        /// <summary>
        /// Threshold
        /// </summary>
        public double Threshold
        {
            get { return _threshold; }
            set { _threshold = value; }
        }
        /// <summary>
        /// Xac dinh node da bi prune hay chua?
        /// </summary>
        public bool IsPruned
        {
            get { return _isPruned; }
            set { _isPruned = value; }
        }
        /// <summary>
        /// Số nhánh con có kết quả kết luận là lớn hơn 50K$
        /// </summary>
        public int GreaterBranchNum
        {
            get { return _greaterBranchNum; }
            set { _greaterBranchNum = value; }
        }
        /// <summary>
        /// số nhánh con có kết luận là nhỏn hơn hoặc bằng 50K$
        /// </summary>
        public int LessEqualBranchNum
        {
            get { return _lessEqualBranchNum; }
            set { _lessEqualBranchNum = value; }
        }
        #endregion

        #region IEnumerable<Node> Members

        public IEnumerator<Node> GetEnumerator()
        {
            return new NodeIENumerator(this);
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}