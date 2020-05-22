using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using PDFAssistant;

namespace ViewModels
{
    public class MainViewModel
    {
        PDFAssistant.PDFAssistant _PDFAssistant = new PDFAssistant.PDFAssistant();

        public void CreateTestPDF(FlowDocument flowDocument, string path = "doc.pdf")
        {
            _PDFAssistant.GenPDFFromFlowDocument(flowDocument, path);
        }

        public FlowDocument OpenPDF()
        {
            return _PDFAssistant.Parse(@"Templates\report1.lqd");
        }
    }
}
