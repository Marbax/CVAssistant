using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using dotTemplate = DotLiquid.Template;


namespace PDFAssistant
{
    public class PDFAssistant
    {
        public void GenPDFFromFlowDocument(FlowDocument flowDocument, string inFileName = null)
        {
            string fileName = inFileName == null ? DateTime.Now.Ticks.ToString() : inFileName;
            //fileName += ".pdf";
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

        public FlowDocument Parse(string templatePath, CurriculumVitae curriculumVitae)
        {
            using (var stream = new FileStream(templatePath, FileMode.Open))
            {
                using (var reader = new StreamReader(stream))
                {
                    var templateString = reader.ReadToEnd();
                    var template = dotTemplate.Parse(templateString);
                    //var docContext = CreateDocumentContext();
                    var docContext = DotLiquid.Hash.FromAnonymousObject(new { Model = curriculumVitae });
                    var docString = template.Render(docContext);
                    return (FlowDocument)System.Windows.Markup.XamlReader.Parse(docString);
                }
            }
        }

        private DotLiquid.Hash CreateDocumentContext()
        {

            var context = new
            {
                Title = "Hello, Habrahabr!",
                Subtitle = "Experimenting with dotLiquid, FlowDocument and PDFSharp",
                Steps = new List<dynamic>{
                    new { Title = "Document Context", Description = "Create data source for dotLiquid Template"},
                    new { Title = "Rendering", Description = "Load template string and render it into FlowDocument markup with Document Context given"},
                    new { Title = "Parse markup", Description = "Use XAML Parser to prepare FlowDocument instance"},
                    new { Title = "Save to XPS", Description = "Save prepared FlowDocument into XPS format"},
                    new { Title = "Convert XPS to PDF", Description = "Convert XPS to WPF using PDFSharp"},
                    new { Title = "Convert XPS to PDF", Description = "Convert XPS to WPF using PDFSharp"},
                    new { Title = "Convert XPS to PDF", Description = "Convert XPS to WPF using PDFSharp"},
                    new { Title = "Convert XPS to PDF", Description = "Convert XPS to WPF using PDFSharp"},
                }
            };

            return DotLiquid.Hash.FromAnonymousObject(context);
        }

    }
}
