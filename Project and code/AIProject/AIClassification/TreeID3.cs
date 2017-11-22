using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    public class TreeID3
    {
      

        #region Attributes
        private ContinousHandler cHandler;
        private Data examples;
        private NodeID3 root;
        #endregion

        #region Properties

        public NodeID3 Tree
        {
            get { return this.root; }
        }
        #endregion

        public TreeID3(Data examples)
        {
            this.examples = examples;
        }

        #region Public Methods
        /// <summary>
        /// Phân loại một trường hợp
        /// Cách xử lý các trường hợp đặc biệt
        /// 1/ Với missing value: gán 1 giá trị đặc biệt là unknown, xử lý bình thường !
        /// 2/ Với giá trị liên tục, chọn giá trị có Information Gain lớn nhất, chia ra làm từ 3->10 giá trị; thử dần và chọn cái nào tốt nhất ! -> chọn 3
        /// 3/ Xét lại cách: Tính thresholds, và đặc biệt: cách xử lý với Thresholds (GreaterEqual và LessEqua) !
        /// </summary>
        /// <param name="ca">Một trường hợp bất kỳ</param>
        /// <returns>true: lon hon 50k$, false neu nho hon hoac bang 50k$</returns>
        public bool Classify(Case ca)
        {
            int rs;
            rs = this.Evaluate(ca);
            if (rs == 1)
            {
                return true;
            }
            else if (rs == 0)
            {
                return false;
            }
            else
            {
                Random r = new Random();
                if (r.NextDouble() >= 0.5)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void ConstructTree()
        {
            cHandler = new ContinousHandler(examples);
            this.root = GetTree();
        }
        #endregion

        #region Private methods        
        private double InformationGain(Data subset, AttributeDetail attr)
        {
            int i = 0;
            double infoGain = 0;
            double[] db;
            List<Data> subsets = new List<Data>();
            #region info gain
            if (!attr.IsContinous)
            {
                for (i = 0; i < attr.TypeValue.Count; i++)
                {
                    Condition c = new Condition(attr.Type, attr.TypeValue[i].ToString(), StateCompare.Equal);
                    subsets.Add(subset.Filter(c));
                }
                for (i = 0; i < attr.TypeValue.Count; i++)
                {
                    if (subsets[i].NumberCase == 0)
                    {
                        infoGain += 0;
                    }
                    else
                    {
                        infoGain += ((double)subsets[i].NumberCase / (double)subset.NumberCase) * Math.Log(((double)subsets[i].NumberCase / (double)subset.NumberCase), 2);
                    }
                }
                infoGain = subset.Entropy() - infoGain;
            }
            else // continous, the easies approach, Thresholds are not re-computed everytime
            {
                switch (attr.Type)
                {
                    case AttributeCase.Age:
                        for (i = 0; i < cHandler.AgeThresholds.Count; i++)
                        {
                            db = cHandler.AgeThresholds[i];
                            Condition c1 = new Condition(attr.Type, db[0].ToString(), StateCompare.GreaterEqual);
                            Condition c2 = new Condition(attr.Type, db[1].ToString(), StateCompare.LessEqua);
                            subsets.Add(subset.Filter(c1,c2));
                        }
                        for (i = 0; i < cHandler.AgeThresholds.Count; i++)
                        {
                            if (subsets[i].NumberCase == 0)
                            {
                                infoGain += 0;
                            }
                            else
                            {
                                infoGain += ((double)subsets[i].NumberCase / (double)subset.NumberCase) * Math.Log(((double)subsets[i].NumberCase / (double)subset.NumberCase), 2);
                            }
                            //Console.WriteLine("inforGain: "+infoGain);
                            //Console.WriteLine("do nothing");
                        }
                        infoGain = subset.Entropy() - infoGain;
                        break;
                    case AttributeCase.Fnlwgt:
                        for (i = 0; i < cHandler.FnlwgtThresholds.Count; i++)
                        {
                            db = cHandler.FnlwgtThresholds[i];
                            Condition c1 = new Condition(attr.Type, db[0].ToString(), StateCompare.GreaterEqual);
                            Condition c2 = new Condition(attr.Type, db[1].ToString(), StateCompare.LessEqua);
                            subsets.Add(subset.Filter(c1,c2));
                        }
                        for (i = 0; i < cHandler.FnlwgtThresholds.Count; i++)
                        {
                            if (subsets[i].NumberCase == 0)
                            {
                                infoGain += 0;
                            }
                            else
                            {
                                infoGain += ((double)subsets[i].NumberCase / (double)subset.NumberCase) * Math.Log(((double)subsets[i].NumberCase / (double)subset.NumberCase), 2);
                            }
                        }
                        infoGain = subset.Entropy() - infoGain;
                        break;
                    case AttributeCase.CapitalGain:
                        for (i = 0; i < cHandler.CapitalGainThresholds.Count; i++)
                        {
                            db = cHandler.CapitalGainThresholds[i];
                            Condition c1 = new Condition(attr.Type, db[0].ToString(), StateCompare.GreaterEqual);
                            Condition c2 = new Condition(attr.Type, db[1].ToString(), StateCompare.LessEqua);
                            subsets.Add(subset.Filter(c1,c2));
                        }
                        for (i = 0; i < cHandler.CapitalGainThresholds.Count; i++)
                        {
                            if (subsets[i].NumberCase == 0)
                            {
                                infoGain += 0;
                            }
                            else
                            {
                                infoGain += ((double)subsets[i].NumberCase / (double)subset.NumberCase) * Math.Log(((double)subsets[i].NumberCase / (double)subset.NumberCase), 2);
                            }
                        }
                        infoGain = subset.Entropy() - infoGain;
                        break;
                    case AttributeCase.CapitalLoss:
                        for (i = 0; i < cHandler.CapitalLossThresholds.Count; i++)
                        {
                            db = cHandler.CapitalGainThresholds[i];
                            Condition c1 = new Condition(attr.Type, db[0].ToString(), StateCompare.GreaterEqual);
                            Condition c2 = new Condition(attr.Type, db[1].ToString(), StateCompare.LessEqua);
                            subsets.Add(subset.Filter(c1,c2));
                        }
                        for (i = 0; i < cHandler.CapitalLossThresholds.Count; i++)
                        {
                            if (subsets[i].NumberCase == 0)
                            {
                                infoGain += 0;
                            }
                            else
                            {
                                infoGain += ((double)subsets[i].NumberCase / (double)subset.NumberCase) * Math.Log(((double)subsets[i].NumberCase / (double)subset.NumberCase), 2);
                            }
                        }
                        infoGain = subset.Entropy() - infoGain;
                        break;
                    case AttributeCase.EducationNum:
                        for (i = 0; i < cHandler.EducationNumThresholds.Count; i++)
                        {
                            db = cHandler.EducationNumThresholds[i];
                            Condition c1 = new Condition(attr.Type, db[0].ToString(), StateCompare.GreaterEqual);
                            Condition c2 = new Condition(attr.Type, db[1].ToString(), StateCompare.LessEqua);
                            subsets.Add(subset.Filter(c1,c2));
                        }
                        for (i = 0; i < cHandler.EducationNumThresholds.Count; i++)
                        {
                            if (subsets[i].NumberCase == 0)
                            {
                                infoGain += 0;
                            }
                            else
                            {
                                infoGain += ((double)subsets[i].NumberCase / (double)subset.NumberCase) * Math.Log(((double)subsets[i].NumberCase / (double)subset.NumberCase), 2);
                            }
                        }
                        infoGain = subset.Entropy() - infoGain;
                        break;
                    case AttributeCase.HoursPerWeek:
                        for (i = 0; i < cHandler.HoursPerWeekThresholds.Count; i++)
                        {
                            db = cHandler.HoursPerWeekThresholds[i];
                            Condition c1 = new Condition(attr.Type, db[0].ToString(), StateCompare.GreaterEqual);
                            Condition c2 = new Condition(attr.Type, db[1].ToString(), StateCompare.LessEqua);
                            subsets.Add(subset.Filter(c1,c2));
                        }
                        for (i = 0; i < cHandler.HoursPerWeekThresholds.Count; i++)
                        {
                            if (subsets[i].NumberCase == 0)
                            {
                                infoGain += 0;
                            }
                            else
                            {
                                infoGain += ((double)subsets[i].NumberCase / (double)subset.NumberCase) * Math.Log(((double)subsets[i].NumberCase / (double)subset.NumberCase), 2);
                            }
                        }
                        infoGain = subset.Entropy() - infoGain;
                        break;
                }
            }
            #endregion
            return infoGain;
        }
        #endregion

        #region util function
        /// <summary>
        /// Create the decision tree
        /// </summary>
        /// <param name="examples"></param>
        /// <param name="list"></param>
        /// <param name="node"></param>
        public void MakeTree(Data sub_examples, List<AttributeDetail> list, NodeID3 node)
        {
            double numberGreater = sub_examples.NumberGreaterCase();
            double infoGain,temp;
            AttributeDetail currentAttr;
            int i;

            if (sub_examples.NumberCase == 0)
            {
                Console.WriteLine("--------------------------------");
                if (node.Parent.PositiveRatio > 0.5)
                {
                    node.SetLeaf(1, 0, 0);
                }
                else
                {
                    node.SetLeaf(0, 0, 0);
                }
                return;
            }
            else
            {
                if (numberGreater == 0)
                {
                    Console.WriteLine("End - all in False");
                    node.SetLeaf(0,sub_examples.NumberCase, 0);
                    return;
                }
                else if(numberGreater == sub_examples.NumberCase)
                {
                    Console.WriteLine("End - all in True");
                    node.SetLeaf(1, sub_examples.NumberCase, 0);
                    return;
                }
            }
            if (list.Count == 0)
            {
                numberGreater = sub_examples.NumberGreaterCase();
                if (numberGreater > sub_examples.NumberCase - sub_examples.NumberGreaterCase())
                {
                    // true
                    Console.WriteLine("End - true");
                    node.SetLeaf(1,numberGreater,sub_examples.NumberCase-numberGreater);
                    return;
                }
                else if (numberGreater < sub_examples.NumberCase - sub_examples.NumberGreaterCase())
                {
                    // false
                    Console.WriteLine("End - false");
                    node.SetLeaf(0,sub_examples.NumberCase-numberGreater,numberGreater);
                    return;
                }
                else if (numberGreater == sub_examples.NumberCase - sub_examples.NumberGreaterCase())
                {
                    Random r = new Random();
                    if (r.NextDouble() > 0.5)
                    {
                        //true
                        Console.WriteLine("End - true");
                        node.SetLeaf(1,numberGreater,sub_examples.NumberCase-numberGreater);
                        return;
                    }
                    else
                    {
                        //false
                        Console.WriteLine("End - false");
                        node.SetLeaf(0,sub_examples.NumberCase-numberGreater,numberGreater);
                        return;
                    }
                }
            }
            infoGain = 0;
            currentAttr = null;
            foreach( AttributeDetail item in list )
            {
                temp = InformationGain(sub_examples, item);
                if (temp > infoGain)
                {
                    infoGain = temp;
                    currentAttr = item;
                }
            }
            // The decision attribute for R <- A ~> no need; actually, this is about the child nodes.
            list.Remove(currentAttr);
            Console.WriteLine("--------------------------------{0}", currentAttr.Type);
            if (currentAttr.IsContinous)
            {
                switch (currentAttr.Type)
                {
                    #region Age
                    case AttributeCase.Age:
                        for (i = 0; i < cHandler.AgeThresholds.Count; i++)
                        {
                            NodeID3 tmp = new NodeID3();
                            Condition c1;
                            Condition c2;
                            double[] db;
                            tmp.Attribute = currentAttr;
                            tmp.Value = i;
                            node.ChildNodes.Add(tmp);
                            tmp.Pos = i;
                            tmp.Parent = node;
                            tmp.PositiveRatio = numberGreater / sub_examples.NumberCase;
                            db = cHandler.AgeThresholds[i];
                            c1 = new Condition(currentAttr.Type, db[0].ToString(), StateCompare.GreaterEqual);
                            c2 = new Condition(currentAttr.Type, db[1].ToString(), StateCompare.LessEqua);
                            MakeTree(sub_examples.Filter(c1,c2), list, tmp);
                        }
                        break;
                    #endregion
                    #region EducationNum
                    case AttributeCase.EducationNum:
                        for (i = 0; i < cHandler.EducationNumThresholds.Count; i++)
                        {
                            NodeID3 tmp = new NodeID3();
                            Condition c1, c2;
                            double[] db;
                            tmp.Attribute = currentAttr;
                            tmp.Value = i;
                            node.ChildNodes.Add(tmp);
                            tmp.Pos = i;
                            tmp.Parent = node;
                            tmp.PositiveRatio = numberGreater / sub_examples.NumberCase;
                            db = cHandler.EducationNumThresholds[i];
                            c1 = new Condition(currentAttr.Type, db[0].ToString(), StateCompare.GreaterEqual);
                            c2 = new Condition(currentAttr.Type, db[1].ToString(), StateCompare.LessEqua);
                            MakeTree(sub_examples.Filter(c1,c2), list, tmp);
                        }
                        break;
                    #endregion
                    #region CapitalGain
                    case AttributeCase.CapitalGain:
                        for (i = 0; i < cHandler.CapitalGainThresholds.Count; i++)
                        {
                            NodeID3 tmp = new NodeID3();
                            Condition c1,c2;
                            double[] db;
                            tmp.Attribute = currentAttr;
                            tmp.Value = i;
                            node.ChildNodes.Add(tmp);
                            tmp.Pos = i;
                            tmp.Parent = node;
                            tmp.PositiveRatio = numberGreater / sub_examples.NumberCase;
                            db = cHandler.CapitalGainThresholds[i];
                            c1 = new Condition(currentAttr.Type, db[0].ToString(), StateCompare.GreaterEqual);
                            c2 = new Condition(currentAttr.Type, db[1].ToString(), StateCompare.LessEqua);
                            MakeTree(sub_examples.Filter(c1,c2), list, tmp);
                        }
                        break;
                    #endregion
                    #region CapitalLoss
                    case AttributeCase.CapitalLoss:
                        for (i = 0; i < cHandler.CapitalLossThresholds.Count; i++)
                        {
                            NodeID3 tmp = new NodeID3();
                            Condition c1, c2;
                            double[] db;
                            tmp.Attribute = currentAttr;
                            tmp.Value = i;
                            node.ChildNodes.Add(tmp);
                            tmp.Parent = node;
                            db = cHandler.CapitalLossThresholds[i];
                            tmp.PositiveRatio = numberGreater / sub_examples.NumberCase;
                            tmp.Pos = i;
                            c1 = new Condition(currentAttr.Type, db[0].ToString(), StateCompare.GreaterEqual);
                            c2 = new Condition(currentAttr.Type, db[1].ToString(), StateCompare.LessEqua);
                            MakeTree(sub_examples.Filter(c1,c2), list, tmp);
                        }
                        break;
                    #endregion
                    #region Fnlwgt
                    case AttributeCase.Fnlwgt:
                        for (i = 0; i < cHandler.FnlwgtThresholds.Count; i++)
                        {
                            NodeID3 tmp = new NodeID3();
                            Condition c1, c2;
                            double[] db;
                            tmp.Attribute = currentAttr;
                            tmp.Value = i;
                            node.ChildNodes.Add(tmp);
                            db = cHandler.FnlwgtThresholds[i];
                            tmp.Pos = i;
                            tmp.Parent = node;
                            tmp.PositiveRatio = numberGreater / sub_examples.NumberCase;
                            c1 = new Condition(currentAttr.Type, db[0].ToString(), StateCompare.GreaterEqual);
                            c2 = new Condition(currentAttr.Type, db[1].ToString(), StateCompare.LessEqua);
                            MakeTree(sub_examples.Filter(c1,c2), list, tmp);
                        }
                        break;
                    #endregion
                    #region HoursPerWeek
                    case AttributeCase.HoursPerWeek:
                        for (i = 0; i < cHandler.HoursPerWeekThresholds.Count; i++)
                        {
                            NodeID3 tmp = new NodeID3();
                            Condition c1, c2;
                            double[] db;
                            tmp.Attribute = currentAttr;
                            tmp.Value = i;
                            node.ChildNodes.Add(tmp);
                            tmp.Pos = i;
                            tmp.Parent = node;
                            tmp.PositiveRatio = numberGreater / sub_examples.NumberCase;
                            db = cHandler.HoursPerWeekThresholds[i];
                            c1 = new Condition(currentAttr.Type, db[0].ToString(), StateCompare.GreaterEqual);
                            c2 = new Condition(currentAttr.Type, db[1].ToString(), StateCompare.LessEqua);
                            MakeTree(sub_examples.Filter(c1,c2), list, tmp);
                        }
                        break;
                    #endregion
                }
            }
            else
            {
                int k = 0;
                foreach (Object value in currentAttr.TypeValue)
                {
                    NodeID3 tmp = new NodeID3();
                    tmp.Attribute = currentAttr;
                    tmp.Value = value;
                    tmp.Pos = k;
                    node.ChildNodes.Add(tmp);
                    tmp.Parent = node;
                    tmp.PositiveRatio = numberGreater / sub_examples.NumberCase;
                    Condition c = new Condition(currentAttr.Type, value.ToString(), StateCompare.Equal);
                    k++;
                    MakeTree(sub_examples.Filter(c), list, tmp);
                }
            }
        }

        private NodeID3 GetTree()
        {
            NodeID3 root = new NodeID3();
            List<AttributeDetail> ls = new List<AttributeDetail>();
            ls.Add(new AttributeDetail(AttributeCase.Age));
            ls.Add(new AttributeDetail(AttributeCase.CapitalGain));
            ls.Add(new AttributeDetail(AttributeCase.CapitalLoss));
            ls.Add(new AttributeDetail(AttributeCase.Education));
            ls.Add(new AttributeDetail(AttributeCase.EducationNum));
            ls.Add(new AttributeDetail(AttributeCase.Fnlwgt));
            ls.Add(new AttributeDetail(AttributeCase.HoursPerWeek));
            ls.Add(new AttributeDetail(AttributeCase.MaritalStatus));
            ls.Add(new AttributeDetail(AttributeCase.NativeCountry));
            ls.Add(new AttributeDetail(AttributeCase.Occupation));
            ls.Add(new AttributeDetail(AttributeCase.Race));
            ls.Add(new AttributeDetail(AttributeCase.Relationship));
            ls.Add(new AttributeDetail(AttributeCase.Sex));
            ls.Add(new AttributeDetail(AttributeCase.WorkClass));
            root.IsLeaf = false;

            MakeTree(this.examples, ls, root);
            return root;
        }

        public int compareNode(NodeID3 node, Case cs)
        {
            AttributeDetail ad;
            double[] db;
            switch (node.Attribute.Type)
            {
                #region Age
                case AttributeCase.Age:
                    {
                        db = cHandler.AgeThresholds[(int)node.Value];
                        if (cs.Age >= db[0] && cs.Age <= db[1])
                        {
                            return 1;
                        }
                    }
                    break;
                #endregion

                #region CapitalGain
                case AttributeCase.CapitalGain:
                    {
                        db = cHandler.CapitalGainThresholds[(int)node.Value];
                        if (cs.CapitalGain >= db[0] && cs.CapitalGain <= db[1])
                        {
                            return 1;
                        }
                    }
                    break;
                #endregion

                #region CapitalLoss
                case AttributeCase.CapitalLoss:                    
                    {
                        db = cHandler.CapitalLossThresholds[(int)node.Value];
                        if (cs.CapitalLoss >= db[0] && cs.CapitalLoss <= db[1])
                        {
                            return 1;
                        }
                    }
                    break;
                #endregion

                #region Education
                case AttributeCase.Education:
                    {
                        if (cs.Edu == Education.unknow)
                        { return -2; }
                        ad = new AttributeDetail(AttributeCase.Education);
                        if (cs.Edu.ToString() == node.Value.ToString())
                        {
                            return 1;
                        }                    
                    }
                    break;
                #endregion

                #region EducationNum
                case AttributeCase.EducationNum:
                    {
                        db = cHandler.EducationNumThresholds[(int)node.Value];
                        if (cs.EduNum >= db[0] && cs.EduNum <= db[1])
                        {
                            return 1;
                        }
                    }
                    break;
                #endregion

                #region Fnlwgt
                case AttributeCase.Fnlwgt:
                    {
                        db = cHandler.FnlwgtThresholds[(int)node.Value];
                        if (cs.Fnlwgt >= db[0] && cs.Fnlwgt <= db[1])
                        {
                            return 1;
                        }
                    }
                    break;
                #endregion

                #region HoursPerWeek
                case AttributeCase.HoursPerWeek:
                    {
                        db = cHandler.HoursPerWeekThresholds[(int)node.Value];
                        if (cs.HoursPerWeek >= db[0] && cs.HoursPerWeek <= db[1])
                        {
                            return 1;
                        }
                    }
                    break;
                #endregion

                #region MaritalStatus
                case AttributeCase.MaritalStatus:
                    {
                        if (cs.MariSt == MaritalStatus.unknow) { return -2; }
                        ad = new AttributeDetail(AttributeCase.MaritalStatus);
                        if (cs.MariSt.ToString() == node.Value.ToString())
                        {
                            return 1;
                        }
                    }
                    break;
                #endregion

                #region NativeCountry
                case AttributeCase.NativeCountry:
                    {
                        if (cs.NtvCountry == NativeCountry.unknow) { return -2; }
                        ad = new AttributeDetail(AttributeCase.NativeCountry);
                        if (cs.NtvCountry.ToString() == node.Value.ToString())
                        {
                            return 1;
                        }
                    }
                    break;
                #endregion

                #region Occ
                case AttributeCase.Occupation:
                    {
                        if (cs.Occ == Occupation.unknow) { return -2; }
                        ad = new AttributeDetail(AttributeCase.Occupation);
                        if (cs.Occ.ToString() == node.Value.ToString())
                        {
                            return 1;
                        }
                    }
                    break;
                #endregion

                #region Race
                case AttributeCase.Race:
                    {
                        if (cs.Ra == Race.unknow) { return -2; }
                        ad = new AttributeDetail(AttributeCase.Race);
                        if (node.Value.ToString() == cs.Ra.ToString())
                        {
                            return 1;
                        }
                    }
                    break;
                #endregion

                #region Relationship
                case AttributeCase.Relationship:
                    {
                        if (cs.Reltsh == Relationship.unknow) { return -2; }
                        ad = new AttributeDetail(AttributeCase.Relationship);
                        if (cs.Reltsh.ToString() == node.Value.ToString())
                        {
                            return 1;
                        }
                    }
                    break;
                #endregion

                #region Sex
                case AttributeCase.Sex:
                    {
                        if (cs.SexCase == Sex.unknow) { return -2; }
                        ad = new AttributeDetail(AttributeCase.Sex);
                        if (cs.SexCase.ToString() == node.Value.ToString())
                        {
                            return 1;
                        }
                    }
                    break;
                #endregion

                #region WorkClass
                case AttributeCase.WorkClass:
                    {
                        if (cs.Wcl == WorkClass.unknow) { return -2; }
                        ad = new AttributeDetail(AttributeCase.WorkClass);
                        if (cs.Wcl.ToString() == node.Value.ToString())
                        {
                            return 1;
                        }
                    }
                    break;
                #endregion
            }
            return -1;
        }

        public int Evaluate(Case cs)
        {
            NodeID3 node = new NodeID3();
            int i,k,j;
            #region check tree branch
            for (i = 0; i < root.ChildNodes.Count; i++)
            {
                node = root.ChildNodes[i];
                k = compareNode(node, cs);
                if (k != -1 && k != -2)
                {
                    while (!node.IsLeaf)
                    {
                        for (j = 0; j < node.ChildNodes.Count; j++)
                        {
                            k = compareNode(node.ChildNodes[j], cs);
                            if(k != -1 && k != -2)
                            {
                                node = node.ChildNodes[j];
                                break;
                            }
                            if (k == -2)
                            {
                                return -2;
                            }
                        }
                    }
                    return node.Result;
                }
                if (k == -2)
                {
                    return -2;
                }
            }
            #endregion
            return node.Result;
        }
        #endregion
    }
}
