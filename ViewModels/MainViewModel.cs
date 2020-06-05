using Commands;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Controls;
using System.Windows.Input;

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

        private CurriculumVitaeViewModel _currentCV;
        public CurriculumVitaeViewModel CurrentCV
        {
            get { return _currentCV; }
            set { SetProperty(ref _currentCV, value); }
        }

        private string _templatePath = @".\Templates\DefaultCV.lqd";
        public string CurrentTemplatePath
        {
            get { return _templatePath; }
            set { SetProperty(ref _templatePath, value); }
        }

        private string _documentSavePath = "MyCV.pdf";
        public string CurrentDocumentSavePath
        {
            get { return _documentSavePath; }
            set { SetProperty(ref _documentSavePath, value); }
        }

        #region Commands
        private readonly RelayCommand _createNewCV;
        public ICommand CreateNewCV => _createNewCV;

        private readonly RelayCommand _getCvPreview;
        public ICommand GetCvPreview => _getCvPreview;

        private readonly RelayCommand _saveToPDF;
        public ICommand SaveToPDF => _saveToPDF;

        private readonly RelayCommand _viewLoaded;
        public ICommand ViewLoaded => _viewLoaded;

        private readonly RelayCommand _viewClosing;
        public ICommand ViewClosing => _viewClosing;
        #endregion

        public MainViewModel()
        {
            #region Commands Init
            _createNewCV = new RelayCommand(CreateNewCVMethod);
            _getCvPreview = new RelayCommand(GenerateDocument);
            _saveToPDF = new RelayCommand(SaveDocumentToPDF, CanSaveDocumentToPDF);
            _viewLoaded = new RelayCommand(TryLoadSerializaideddData);
            _viewClosing = new RelayCommand(TrySerializeData);
            #endregion
        }

        private bool CanSaveDocumentToPDF(object arg)
        {
            var doc = (arg as FlowDocumentReader).Document;
            return doc != null && doc.ContentStart != doc.ContentEnd;
        }

        private void SaveDocumentToPDF(object obj)
        {
            PDFAssistant.PDFAssistant.GenPDFFromFlowDocument((obj as FlowDocumentReader).Document, CurrentDocumentSavePath);
        }

        private void GenerateDocument(object obj)
        {
            CurrentCV.UpdateSource();
            (obj as FlowDocumentReader).Document = PDFAssistant.PDFAssistant.Parse(CurrentTemplatePath, CurrentCV.CurriculumVitae);
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

        private void CreateNewCVMethod(object obj = null)
        {
            CurrentCV = new CurriculumVitaeViewModel(new CurriculumVitae(
                new Person("", "", "", default, "", "", @"./Templates/DefaultUserIMG.png"),
                new string[] { "" },
                new Experience[] { new Experience("", "", default, default, new string[] { "" }) }));
            Console.WriteLine("\n\nCreated\n\n");
        }

        #region Serialization
        private readonly BinaryFormatter _bf = new BinaryFormatter();

        private string _dataPath = "LastCV.dat";
        public string DataPath
        {
            get { return _dataPath; }
            set { SetProperty(ref _dataPath, value); }
        }

        private string _logsPath = "logs.dat";
        public string LogsPath
        {
            get { return _logsPath; }
            set { SetProperty(ref _logsPath, value); }
        }


        private void TryLoadSerializaideddData(object obj = null)
        {
            LoadLogs();

            if (LoadData() == false)
                CurrentCV = GetDefaultCV();
        }
        private void TrySerializeData(object obj = null)
        {
            CurrentCV.UpdateSource();
            SaveData();
            SaveLogs();
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
                Logs.Add(new Log($"Serealization ex :\n{ex.Message}\n", DateTime.Now));
                return false;
            }
            return true;
        }

        private bool LoadLogs()
        {
            try
            {
                using (FileStream fs = new FileStream(LogsPath, FileMode.Open))
                {
                    Logs = (ObservableCollection<Log>)_bf.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                Logs = new ObservableCollection<Log>();
                Logs.Add(new Log($"Logs Serialization ex:\n{ex.Message}\n", DateTime.Now));
                return false;
            }
            return true;
        }
        public bool SaveLogs()
        {
            try
            {
                using (FileStream fs = new FileStream(LogsPath, FileMode.Create))
                {
                    _bf.Serialize(fs, Logs);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        #endregion

    }

}
