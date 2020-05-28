using Commands;
using Microsoft.Win32;
using Models;
using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Input;
using System.Linq;
using System.ComponentModel;

namespace ViewModels
{
    public class MainViewModel : AViewModel
    {
        readonly PDFAssistant.PDFAssistant _PDFAssistant = new PDFAssistant.PDFAssistant();


        private CurriculumVitaeViewModel _currentCV;
        public CurriculumVitaeViewModel CurrentCV
        {
            get { return _currentCV; }
            set { SetProperty(ref _currentCV, value); }
        }


        public MainViewModel()
        {
            CurrentCV = GetDefaultCV();

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




    }

}
