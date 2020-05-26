using Commands;
using Microsoft.Win32;
using Models;
using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Input;

namespace ViewModels
{
    public class MainViewModel : AViewModel
    {
        private readonly RelayCommand _selectPhotoCommand;
        public ICommand SelectPhoto => _selectPhotoCommand;


        private CurriculumVitae _currentCV;
        public CurriculumVitae CurrentCV
        {
            get { return _currentCV; }
            set { SetProperty(ref _currentCV, value); }
        }

        private string _selectedSkill;
        public string SelectedSkill
        {
            get { return _selectedSkill; }
            set { SetProperty(ref _selectedSkill, value); }
        }

        readonly PDFAssistant.PDFAssistant _PDFAssistant = new PDFAssistant.PDFAssistant();

        public MainViewModel()
        {
            CurrentCV = GetDefaultCV();
            _selectPhotoCommand = new RelayCommand(OpenAndLoadPhoto);
        }

        public void CreateTestPDF(FlowDocument flowDocument, string saveToPath = "doc.pdf")
        {
            _PDFAssistant.GenPDFFromFlowDocument(flowDocument, saveToPath);
        }

        public FlowDocument OpenPDF()
        {
            return _PDFAssistant.Parse(@"Templates\DefaultCV.lqd", CurrentCV);
        }

        private CurriculumVitae GetDefaultCV()
        {
            Person person = new Person("Vasiliy", "Pupkin", "Chaikin", new DateTime(1994, 7, 17), "380934560923", "Kiev", "Ukraine", "soap@soap.su", @"J:\CVAssistant\PDFAssistant\bin\Debug\Examples\First\DefaultUserIMG.png");
            Experience experience = new Experience("Shabat", "Director", new DateTime(2012, 7, 17), new DateTime(2020, 1, 1), new List<string>()
            {
                "Rulling","Bullying","Human reverse ingeneering","Make some sense"
            });
            Experience experience1 = new Experience("Cool company", "HR", new DateTime(2000, 7, 17), new DateTime(2012, 6, 1), new List<string>()
            {
                "Finding human resources","Selling human resources","Human reverse ingeneering","Make some sense"
            });
            CurriculumVitae curriculumVitae = new CurriculumVitae(person, new List<string>()
            {
                "Obey", "Workin hard","Be cool"
            }
            , new List<Experience>()
            {
                experience,experience1
            });

            return curriculumVitae;
        }

        private void OpenAndLoadPhoto(object args = null)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                _currentCV.Person.Photo = openFileDialog.FileName;
            }
        }
    }

}
