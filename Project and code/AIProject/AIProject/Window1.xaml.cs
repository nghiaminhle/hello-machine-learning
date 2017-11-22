using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AIClassification;
using Microsoft.Win32;
using System.Windows.Threading;
using System.ComponentModel;

namespace AIProject
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        #region Private Fields
        
        private string _dataPath;
        private string _knowledgePath;
        private Data _data;
        private Case[] _testCases;
        private Case[] _validateCases;
        private IFile _file;
        
        private C45 _c45Classifier;
        private bool _c45State = false;
        
        private NaiveBayes _naiveBayes;
        private bool _nbState = false;
        private ProbalityTable _proTable;

        private BackgroundWorker _testWorker;
        private BackgroundWorker _trainingWorker;
        private bool _dataProcessing = false;
        private bool _testData = false;
        private bool _validateData = false;
        private bool _testProcessing = false;
        
        private KnowledgeC45 _c45Knowledge;
        private KnowledgeNaiveBayes _nbKnowlege;
        
        private double _accurate;
        private Algorithm _algorithm;
        private string _saveKnowledgePath;

        #endregion
        
        public Window1()
        {
            InitializeComponent();
            
            this._file = new FileProcessing();
            this._data = new Data(this._file);

            this._c45Knowledge = null;
            this._accurate = 1;
            this._algorithm = Algorithm.C45;

            #region Test worker
            this._testWorker = new BackgroundWorker();
            this._testWorker.WorkerReportsProgress = true;
            this._testWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            this._testWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
            this._testWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged); 
            #endregion

            #region Training Worker
            this._trainingWorker = new BackgroundWorker();
            this._trainingWorker.DoWork += new DoWorkEventHandler(trainingWorker_DoWork);
            this._trainingWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(trainingWorker_RunWorkerCompleted);
            #endregion
        }

        #region Read Data Button Click
        private void btReadData_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.DefaultExt = ".txt";
            openFile.AddExtension = true;
            openFile.InitialDirectory = @"E:\";
            openFile.Title = "Data";
            openFile.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";

            if (openFile.ShowDialog().Value)
            {
                this._dataPath = openFile.FileName;
                this.txtDataPath.Text = this._dataPath;

                if ((bool)this.rdoTrainningData.IsChecked)
                {
                    this.btReadData.IsEnabled = false;
                    this._dataProcessing = true;
                    this._testWorker.RunWorkerAsync(this._data);
                }
                else if ((bool)this.rdoTestData.IsChecked)
                {
                    this.btReadData.IsEnabled = false;
                    this._testData = true;
                    this._testWorker.RunWorkerAsync(this._file);
                }
                else if ((bool)this.rdoValidate.IsChecked)
                {
                    this.btReadData.IsEnabled = false;
                    this._validateData = true;
                    this._testWorker.RunWorkerAsync(this._file);
                }
            }
        }
        
        #endregion

        #region Choos Knowledg Button Click
        private void btChooseKnowlege_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.DefaultExt = ".txt";
            openFile.AddExtension = true;
            openFile.InitialDirectory = @"E:\";
            openFile.Title = "Knowlege";
            openFile.Filter = "Knowledge(.dat)|*.dat|All Files (*.*)|*.*";

            if (openFile.ShowDialog().Value)
            {
                this._knowledgePath = openFile.FileName;
                this.txtKnowledge.Text = this._dataPath;
            }
        } 
        #endregion

        #region Test Button Click
        private void btTest_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataPath == null)
            {
                MessageBox.Show("Phải chọn bộ dữ liệu để test");
            }
            else if (this._knowledgePath == null)
            {
                MessageBox.Show("Phải chọn cơ sở tri thức để test");
            }
            else
            {
                this.btReadData.IsEnabled = false;
                this.btChooseKnowlege.IsEnabled = false;
                this.prgTestPercent.Value = 0;
                this.txtAccurate.Text = "";

                if ((bool)this.rdbC45.IsChecked)
                {
                    this.rdbBayes.IsEnabled = false;
                    this.rdbID3.IsEnabled = false;
                    this.btTest.IsEnabled = false;
                    this._testProcessing = true;
                    try
                    {
                        this._c45Knowledge = (KnowledgeC45)this._file.ReadKnowledge(this._knowledgePath);
                        this._testWorker.RunWorkerAsync(this._c45Knowledge); 
                    }
                    catch
                    {
                        MessageBox.Show("Kiểm tra loại thuật toán phân loại");
                    }
                }
                else if ((bool)this.rdbID3.IsChecked)
                {
                    this.rdbC45.IsEnabled = false;
                    this.rdbBayes.IsEnabled = false;
                    this.btTest.IsEnabled = false;
                    
                }
                else if ((bool)this.rdbBayes.IsChecked)
                {
                    this.rdbC45.IsEnabled = false;
                    this.rdbID3.IsEnabled = false;
                    this.btTest.IsEnabled = false;

                    this._testProcessing = true;
                    try
                    {
                        this._nbKnowlege = (KnowledgeNaiveBayes)this._file.ReadNaiveBayes(this._knowledgePath);
                        this._testWorker.RunWorkerAsync(this._nbKnowlege);
                    }
                    catch 
                    {
                        MessageBox.Show("Kiểm tra loại thuật toán phân loại");
                    }
                }
            }
        }
        #endregion

        #region Read and Test Data Worker
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (this._testProcessing)
                this.prgTestPercent.Value = e.ProgressPercentage;
            if (this._dataProcessing || this._testData || this._validateData)
                this.prgData.Value = e.ProgressPercentage;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this._dataProcessing)
            {
                double n = this._data.NumberCase;
                double g = this._data.NumberGreaterCase();
                this.txtTotalCase.Text = n.ToString();
                this.txtGreater.Text = g.ToString();
                this.txtLessEqual.Text = (n - g).ToString();
                this.txtMissBefore.Text = this._data.TotalMissingCase().ToString();
                this.txtMissAfter.Text = "0";
                this.btReadData.IsEnabled = true;
                this._dataProcessing = false;
            }
            else if (this._testData)
            {
                double n = this._testCases.Length;
                double g = 0;
                double m = 0;
                foreach (Case ca in this._testCases)
                {
                    if (ca.IsGreater)
                        g++;
                    if (ca.IsMissingValue)
                        m++;
                }
                this.txtGreater.Text = g.ToString();
                this.txtTotalCase.Text = n.ToString();
                this.txtMissBefore.Text = m.ToString();
                this.txtLessEqual.Text = (n - g).ToString();
                this.btReadData.IsEnabled = true;
                this._testData = false;
            }
            else if (this._validateData)
            {
                double n = this._validateCases.Length;
                double g = 0;
                double m = 0;
                foreach (Case ca in this._validateCases)
                {
                    if (ca.IsGreater)
                        g++;
                    if (ca.IsMissingValue)
                        m++;
                }
                this.txtGreater.Text = g.ToString();
                this.txtTotalCase.Text = n.ToString();
                this.txtMissBefore.Text = m.ToString();
                this.txtLessEqual.Text = (n - g).ToString();
                this.btReadData.IsEnabled = true;
                this._validateData = false;
            }
            else if (this._testProcessing)
            {
                this.btReadData.IsEnabled = true;
                this.btChooseKnowlege.IsEnabled = true;
                this.btTest.IsEnabled = true;

                if ((bool)this.rdbC45.IsChecked)
                {
                    this.rdbBayes.IsEnabled = true;
                    this.rdbID3.IsEnabled = true;
                }
                else if ((bool)this.rdbID3.IsChecked)
                {
                    this.rdbC45.IsEnabled = true;
                    this.rdbBayes.IsEnabled = true;
                }
                else if ((bool)this.rdbBayes.IsChecked)
                {
                    this.rdbC45.IsEnabled = true;
                    this.rdbID3.IsEnabled = true;
                }
                this._testProcessing = false;
                this.txtAccurate.Text = this._accurate.ToString() + "%";
                this.txtMissAfter.Text = "0";
            }
        }
        
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument is Data)
            {
                Data data = (Data)e.Argument;
                this._dataProcessing = true;
                data.LoadData(this._dataPath, this._testWorker);
                data.CleanData();
            }
            else if (e.Argument is IFile && this._testData)
            {
                IFile file = (IFile)e.Argument;
                this._testCases = file.ReadFile(this._dataPath, this._testWorker, 1);
            }
            else if (e.Argument is IFile && this._validateData)
            {
                IFile file = (IFile)e.Argument;
                this._validateCases = file.ReadFile(this._dataPath, this._testWorker, 1);
            }
            else if (e.Argument is KnowledgeC45)
            {
                KnowledgeC45 k = (KnowledgeC45)e.Argument;
                this._accurate = k.TestAccuracy(this._testWorker, this._testCases);
            }
            else if (e.Argument is KnowledgeNaiveBayes)
            {
                KnowledgeNaiveBayes kld = (KnowledgeNaiveBayes)e.Argument;
                this._accurate = kld.TestAccuracy(this._testWorker, this._testCases);
            }
        }
        #endregion

        #region Training Worker
        private void trainingWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this._c45State)
            {
                this.txtState.Text = "Successfully!";
                this.rdbID3Classifier.IsEnabled = true;
                this.rdbBayesClassifier.IsEnabled = true;
                this.rdoTrainningData.IsEnabled = true;
                this.btSaveKnowlege.IsEnabled = true;
                this.btReadData.IsEnabled = true;
                this._c45State = false;
                this.btTraining.IsEnabled = true;

                // Luu tru tri thuc tim duoc
                KnowledgeC45 kl = new KnowledgeC45(this._c45Classifier.Root, this._data.MostCommonLabelGreatr, 
                    this._data.MostCommonLabelLessEqua, this._data.NumberCase, this._validateCases.Length);
                this._file.SaveKnowledge(kl, this._saveKnowledgePath);
            }
            else if (this._nbState)
            {
                this.txtState.Text = "Successfully!";
                this.rdbID3Classifier.IsEnabled = true;
                this.rdbBayesClassifier.IsEnabled = true;
                this.rdbC45Classifier.IsEnabled = true;
                this.rdoTrainningData.IsEnabled = true;
                this.btSaveKnowlege.IsEnabled = true;
                this.btReadData.IsEnabled = true;
                this._nbState = false;
                this.btTraining.IsEnabled = true;

                
                // Luu tru tri thuc tim duoc
                ReducedProbalityTable rdProTab = new ReducedProbalityTable(this._proTable.getProTab(), this._proTable.getThresholdsArr());
                this._naiveBayes = new NaiveBayes(rdProTab, this._data.NumberCase, this._data.NumberGreaterCase());
                KnowledgeNaiveBayes klb = new KnowledgeNaiveBayes(this._naiveBayes, this._data.MostCommonLabelGreatr, 
                    this._data.MostCommonLabelLessEqua, this._data.NumberCase);
                this._file.SaveNaiveBayes(klb, this._saveKnowledgePath);
            }
        }

        private void trainingWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument is C45)
            {
                C45 c45 = (C45)e.Argument;
                c45.ConstructTree();
                c45.ComputingTree(c45.Root);
                this._data.CleanData(this._validateCases);
                c45.OptimizeTree(this._validateCases);
                this._c45State = true;
            }
            else if (e.Argument is ProbalityTable)
            {
                ProbalityTable proTab = (ProbalityTable)e.Argument;
                proTab.ComputeProbabilityTab();
                this._nbState = true;
            }
        }
        #endregion

        #region Radio C4.5 Click
        private void rdbC45_Click(object sender, RoutedEventArgs e)
        {
            this._algorithm = Algorithm.C45;
        }
        
        #endregion

        #region Radio ID3 Click
        private void rdbID3_Click(object sender, RoutedEventArgs e)
        {
            this._algorithm = Algorithm.ID3;
        } 
        #endregion

        #region Radio Naive Bayes Click
        private void rdbBayes_Click(object sender, RoutedEventArgs e)
        {
            this._algorithm = Algorithm.NaiveBayes;
        } 
        #endregion

        #region Radio Choose Test Data Click
        private void rdoTestData_Click(object sender, RoutedEventArgs e)
        {
            this._testData = true;
        } 
        #endregion

        #region Radio Training Data Click
        private void rdoTrainningData_Click(object sender, RoutedEventArgs e)
        {
            this._dataProcessing = true;
        }
        
        #endregion

        #region Radio Button Validate Click
        private void rdoValidate_Click(object sender, RoutedEventArgs e)
        {
            this._validateData = true;
        }
        #endregion

        #region Radio Bayes Classifier Click
        private void rdbBayesClassifier_Click(object sender, RoutedEventArgs e)
        {
            this._nbState = true;
        }
        
        #endregion

        #region Radio C45 Classifier Click
        private void rdbC45Classifier_Click(object sender, RoutedEventArgs e)
        {
            this._c45State = true;
        } 
        #endregion

        #region Radio ID3 Classifier Click
        private void rdbID3Classifier_Click(object sender, RoutedEventArgs e)
        {

        } 
        #endregion

        #region Button Save Knowledge Click
        private void btSaveKnowlege_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialogBox = new SaveFileDialog();
            saveDialogBox.DefaultExt = ".dat";
            saveDialogBox.AddExtension = true;
            saveDialogBox.InitialDirectory = @"E:\";
            saveDialogBox.Title = "Data";
            saveDialogBox.Filter = "Text Files (.dat)|*.dat|All Files (*.*)|*.*";

            if (saveDialogBox.ShowDialog().Value)
            {
                this.txtSaveKnowledge.Text = saveDialogBox.FileName;
                this._saveKnowledgePath = saveDialogBox.FileName;
            }
        } 
        #endregion

        #region Button Training Click
        private void btTraining_Click(object sender, RoutedEventArgs e)
        {
            if (this._data.NumberCase == 0)
            {
                MessageBox.Show("Phải chọn dữ liệu để training!");
                return;
            }
            if (this._saveKnowledgePath == null)
            {
                MessageBox.Show("Phải chọn nơi lưu trữ tri thức sau khi training!");
                return;
            }
            
            if ((bool)rdbC45Classifier.IsChecked)
            {
                if (this._validateCases == null)
                {
                    MessageBox.Show("Phải chọn dữ liệu để validate");
                    return;
                }
                this.rdbID3Classifier.IsEnabled = false;
                this.rdbBayesClassifier.IsEnabled = false;
                this.rdoTrainningData.IsEnabled = false;
                this.btSaveKnowlege.IsEnabled = false;
                this.btReadData.IsEnabled = false;
                this.btTraining.IsEnabled = false;
                
                this.txtState.Text = "Waiting...";

                this._c45State = true;
                this._c45Classifier = new C45(this._data, new DiscreteIndex());
                this._trainingWorker.RunWorkerAsync(this._c45Classifier);
            }
            else if ((bool)rdbID3Classifier.IsChecked)
            {
                rdbC45Classifier.IsEnabled = false;
                rdbBayesClassifier.IsEnabled = false;
                this.rdoTrainningData.IsEnabled = false;
                this.btSaveKnowlege.IsEnabled = false;

                this.txtState.Text = "Waiting...";
                
                
            }
            else if ((bool)rdbBayesClassifier.IsChecked)
            {
                rdbC45Classifier.IsEnabled = false;
                rdbID3Classifier.IsEnabled = false;
                this.rdoTrainningData.IsEnabled = false;
                this.btSaveKnowlege.IsEnabled = false;
                this.btReadData.IsEnabled = false;
                this.btTraining.IsEnabled = false;

                this.txtState.Text = "Waiting...";

                this._nbState = true;
                this._proTable = new ProbalityTable(this._data);
                this._trainingWorker.RunWorkerAsync(this._proTable);
            }
        } 
        #endregion

        #region Creat Validate and Training Data.
        
        private string _sourceOriginal;
        private string _newValidatePath;
        private string _newTrainingPath;

        private void btSource_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.DefaultExt = ".txt";
            openFile.AddExtension = true;
            openFile.InitialDirectory = @"E:\";
            openFile.Title = "Data";
            openFile.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";

            if (openFile.ShowDialog().HasValue)
            {
                this._sourceOriginal = openFile.FileName;
                this.txtSource.Text = this._sourceOriginal;
            }
        }

        private void btValidate_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialogBox = new SaveFileDialog();
            saveDialogBox.DefaultExt = ".dat";
            saveDialogBox.AddExtension = true;
            saveDialogBox.InitialDirectory = @"E:\";
            saveDialogBox.Title = "Data";
            saveDialogBox.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";

            if (saveDialogBox.ShowDialog().Value)
            {
                this._newValidatePath = saveDialogBox.FileName;
                this.txtValidate.Text = this._newValidatePath;
            }
        }

        private void btTrainingOpen_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialogBox = new SaveFileDialog();
            saveDialogBox.DefaultExt = ".dat";
            saveDialogBox.AddExtension = true;
            saveDialogBox.InitialDirectory = @"E:\";
            saveDialogBox.Title = "Data";
            saveDialogBox.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";

            if (saveDialogBox.ShowDialog().Value)
            {
                this._newTrainingPath = saveDialogBox.FileName;
                this.txtTrainingPath.Text = this._newValidatePath;
            }
        }

        private void btCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double percent = Convert.ToDouble(this.txtPercentValidate.Text);
                if (percent < 0 || percent > 100)
                {
                    throw new Exception("Sai du lieu!");
                }
                else
                {
                    this._file.CreateValidateData(this._sourceOriginal, this._newValidatePath, this._newTrainingPath, percent);
                    MessageBox.Show("Successfully!");
                }
            }
            catch
            {
                MessageBox.Show("Nhap sai du lieu percent");
            }
        } 
        #endregion
       
    }
}
