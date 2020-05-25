using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Models;
using PDFAssistant;
using Prism.Mvvm;

namespace ViewModels
{
    public class MainViewModel //: BindableBase
    {
        PDFAssistant.PDFAssistant _PDFAssistant = new PDFAssistant.PDFAssistant();

        public void CreateTestPDF(FlowDocument flowDocument, string saveToPath = "doc.pdf")
        {
            _PDFAssistant.GenPDFFromFlowDocument(flowDocument, saveToPath);
        }

        public FlowDocument OpenPDF()
        {
            return _PDFAssistant.Parse(@"Templates\DefaultCV.lqd", GetDefaultCV());
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
    }
}
