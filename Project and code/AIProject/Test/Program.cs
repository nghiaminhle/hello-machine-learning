using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIClassification;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string trainingPath;
            string testPast;

            Console.Write("Duong dan file du lieu training:");
            trainingPath = Console.ReadLine();
            
            Console.Write("Duong dan file du lieu test:");
            testPast = Console.ReadLine();

            #region Load data
            FileProcessing file = new FileProcessing();
            Console.WriteLine("Loading Data");
            Data data = new Data(file);
            data.LoadData(trainingPath);
            Console.WriteLine("Loading data finished!");
            data.CleanData();
            Console.WriteLine("Cleaning data finished!");
            #endregion

            ContinousHandler.NUM_OF_RANGE = 5;
            //ContinousHandler cn = new ContinousHandler(data);
            TreeID3 tr = new TreeID3(data);
            Console.WriteLine("Constructing!");
            tr.ConstructTree();
            Console.WriteLine("Constructing Finish!");
            
            // loading test data.
            Case[] testCases = file.ReadFile(testPast);
            data.CleanData(testCases);

            Console.WriteLine("Press enter key to classify!");
            Console.ReadLine();

            TestTreeID3(tr, testCases);
            
            Console.ReadLine();
        }
        
        private static void TestTreeID3(TreeID3 tr, Case[] cases)
        {
            int t = 0, uk = 0;
            for (int i = 0; i < cases.Length; i++)
            {
                switch (tr.Evaluate(cases[i]))
                {
                    case 0:
                        if (cases[i].IsGreater == false)
                        {
                            t++;
                        }
                        break;
                    case 1:
                        if (cases[i].IsGreater == true)
                        {
                            t++;
                        }
                        break;
                    case -2:
                        uk++; // or something else, because this is a missing value case
                        break;
                }
                //Console.Write(" ----- " + cases[i].IsGreater+"\n");
            }
            Console.WriteLine("Invalid: " + (cases.Length - uk - t));
            Console.WriteLine("Valid: " + t);
            Console.WriteLine("Missing value: " + uk);
            Console.WriteLine("Overall: " + (cases.Length - uk));
            Console.WriteLine("Dataset: " + (cases.Length));
            Console.WriteLine("Correctness: " + ((double)t / (double)(cases.Length - uk)) * 100 + " %");

        }

        private static int CountNode(Node root)
        {
            int nodeNum = 0;
            if (!root.IsLeaf)
            {
                foreach (Node childNode in root.ListChild)
                {
                    if (childNode != null)
                    {
                        nodeNum++;
                        nodeNum += CountNode(childNode);
                    }
                }
            }
            else
            {
                nodeNum = 1;
            }
            return nodeNum;
        }
        private static void Test(Node root, Data data, IFile file)
        {
            Console.WriteLine("Loading Test File");
            string testPath = @"E:\ProjectManagement\Projec from 1_2010 to 5_2010\AI Project 1_2010\Data\Test.txt";
            Case[] testCases = file.ReadFile(testPath);
            data.CleanData(testCases);
            Console.WriteLine("Press any key to classify");
            Console.ReadLine();

            int n = testCases.Length;
            double numberTest = 0;
            double numberCorrectly = 0;
            double missNum = 0;
            for (int i = 0; i < n; i++)
            {
                if (!testCases[i].IsMissingValue)
                {
                    numberTest++;
                    if (testCases[i].IsGreater == Classify(root, testCases[i]))
                    {
                        numberCorrectly++;
                    }
                    Console.WriteLine("{0}-{1}", testCases[i].IsGreater, root.Classify(testCases[i]));
                }
                else
                {
                    missNum++;
                }
            }

            Console.WriteLine("Total:{0},Number Test:{1}-Number Correctly:{2}-Percent:{3}", n, numberTest, numberCorrectly, (numberCorrectly / numberTest) * 100);
            Console.WriteLine("Missing Num:{0}", missNum);
        }
        static int count = 0;
        public static bool Classify(Node root, Case ca)
        {
            Node node = root;
            Node parent = null;
            DiscreteIndex indexOfChild = new DiscreteIndex();
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
                                int index = indexOfChild.IndexValues(AttributeCase.Education, ca.Edu);
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
                                int index = indexOfChild.IndexValues(AttributeCase.MaritalStatus, ca.MariSt);
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
                                int index = indexOfChild.IndexValues(AttributeCase.NativeCountry, ca.NtvCountry);
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
                                int index = indexOfChild.IndexValues(AttributeCase.Occupation, ca.Occ);
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
                                int index = indexOfChild.IndexValues(AttributeCase.Race, ca.Ra);
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
                                int index = indexOfChild.IndexValues(AttributeCase.Relationship, ca.Reltsh);
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
                                int index = indexOfChild.IndexValues(AttributeCase.Sex, ca.SexCase);
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
                                int index = indexOfChild.IndexValues(AttributeCase.WorkClass, ca.Wcl);
                                node = node.ListChild[index];
                            }
                        }
                        break;
                    #endregion
                    default:
                        break;
                }
                if (node == null)
                {
                    count++;
                    if (parent.GreaterBranchNum > parent.LessEqualBranchNum)
                        return true;
                    else
                        return false;
                }
            }
            
            return node.Result;
        }
    }
}
