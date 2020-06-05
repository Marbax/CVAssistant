using Models;
using System;
using System.IO;
using System.IO.Packaging;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using dotTemplate = DotLiquid.Template;


namespace PDFAssistant
{
    public static class PDFAssistant
    {
        public static void GenPDFFromFlowDocument(FlowDocument flowDocument, string inFileName = null)
        {
            string fileName = inFileName == null ? DateTime.Now.Ticks.ToString() + ".pdf" : inFileName;
            using (var stream = new MemoryStream())
            {
                using (var package = Package.Open(stream, FileMode.Create, FileAccess.ReadWrite))
                {
                    using (var xpsDoc = new XpsDocument(package, CompressionOption.Maximum))
                    {
                        var rsm = new XpsSerializationManager(new XpsPackagingPolicy(xpsDoc), false);
                        var paginator = ((IDocumentPaginatorSource)flowDocument).DocumentPaginator;
                        rsm.SaveAsXaml(paginator);
                        rsm.Commit();
                    }
                }
                stream.Position = 0;

                var pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(stream);
                PdfSharp.Xps.XpsConverter.Convert(pdfXpsDoc, fileName, 0);
            }
        }

        public static FlowDocument Parse(string templatePath, CurriculumVitae curriculumVitae)
        {
            using (var stream = new FileStream(templatePath, FileMode.Open))
            {
                using (var reader = new StreamReader(stream))
                {
                    var templateString = reader.ReadToEnd();
                    var template = dotTemplate.Parse(templateString);
                    var docContext = DotLiquid.Hash.FromAnonymousObject(new { Model = curriculumVitae });
                    var docString = template.Render(docContext);
                    return (FlowDocument)System.Windows.Markup.XamlReader.Parse(docString);
                }
            }
        }

    }
}
