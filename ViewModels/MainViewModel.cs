using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Documents;

namespace ViewModels
{
    public class MainViewModel : AViewModel
    {
        private ObservableCollection<Log> _logs;
        public ObservableCollection<Log> Logs
        {
            get { return _logs; }
            set { _logs = value; }
        }

        readonly PDFAssistant.PDFAssistant _PDFAssistant = new PDFAssistant.PDFAssistant();

        private CurriculumVitaeViewModel _currentCV;
        public CurriculumVitaeViewModel CurrentCV
        {
            get { return _currentCV; }
            set { SetProperty(ref _currentCV, value); }
        }


        public MainViewModel()
        {
            if (LoadLogs() == false)
            {
                Logs = new ObservableCollection<Log>();
            }

            if (LoadData() == false)
            {
                CurrentCV = GetDefaultCV();
            }
        }


        public void CreateTestPDF(FlowDocument flowDocument, string saveToPath = "doc.pdf")
        {
            _PDFAssistant.GenPDFFromFlowDocument(flowDocument, saveToPath);
        }

        public FlowDocument OpenPDF()
        {
            CurrentCV.UpdateSource();
            return _PDFAssistant.Parse(@"Templates\DefaultCV.lqd", CurrentCV.CurriculumVitae);
        }

        private CurriculumVitaeViewModel GetDefaultCV()
        {
            Person person = new Person("Name", "Surname", "Midle Name", new DateTime(1994, 7, 17), "380934560923", "Kiev", "Ukraine", "soap@soap.su", @"J:\CVAssistant\PDFAssistant\bin\Debug\Examples\First\DefaultUserIMG.png");
            Experience experience = new Experience("Shabat", "Director", new DateTime(2012, 7, 17), new DateTime(2020, 1, 1), new List<string>()
            {
                "Rulling","Bullying","Human reverse ingeneering","Make some sense"
            });
            Experience experience1 = new Experience("Cool company", "HR", new DateTime(2000, 7, 17), new DateTime(2012, 6, 1), new List<string>()
            {
                "Finding human resources","Selling human resources","Human reverse ingeneering","Make some sense"
            });
            CurriculumVitaeViewModel curriculumVitae = new CurriculumVitaeViewModel(new CurriculumVitae(person, new List<string>()
            {
                "Obey", "Workin hard","Be cool"
            }
            , new List<Experience>()
            {
                experience,experience1
            }));

            return curriculumVitae;
        }


        #region Serialization
        readonly BinaryFormatter _bf = new BinaryFormatter();

        private string _dataPath = "LastCV.dat";
        public string DataPath
        {
            get { return _dataPath; }
            set { SetProperty(ref _dataPath, value); }
        }


        private bool LoadData()
        {
            try
            {
                using (FileStream fs = new FileStream(DataPath, FileMode.Open))
                {
                    CurriculumVitae cv = (CurriculumVitae)_bf.Deserialize(fs);
                    CurrentCV = new CurriculumVitaeViewModel(cv);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nSerealization ex :\n{ex.Message}\n");
                Logs.Add(new Log($"Deserealization ex :\n{ex.Message}\n", DateTime.Now));
                return false;
            }

            return true;
        }
        public bool SaveData()
        {
            try
            {
                using (FileStream fs = new FileStream(DataPath, FileMode.Create))
                {
                    _bf.Serialize(fs, CurrentCV.CurriculumVitae);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nSerealization ex :\n{ex.Message}\n");
                Logs.Add(new Log($"Serealization ex :\n{ex.Message}\n", DateTime.Now));
                return false;
            }
            return true;
        }

        private bool LoadLogs()
        {
            try
            {
                using (FileStream fs = new FileStream("logs.dat", FileMode.Open))
                {
                    Logs = (ObservableCollection<Log>)_bf.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nSerealization ex :\n{ex.Message}\n");
                return false;
            }
            return true;
        }
        public bool SaveLogs()
        {
            try
            {
                using (FileStream fs = new FileStream("logs.dat", FileMode.Create))
                {
                    _bf.Serialize(fs, Logs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nSerealization ex :\n{ex.Message}\n");
                return false;
            }

            return true;
        }
        #endregion


    }

}
