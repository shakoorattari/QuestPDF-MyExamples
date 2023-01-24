using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

namespace QuestPDF.ExampleInvoice
{
    class Program
    {
        /// <summary>
        /// For documentation and implementation details, please visit:
        /// https://www.questpdf.com/documentation/getting-started.html
        /// </summary>
        static void Main(string[] args)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20).FontFamily("Arial"));

                    page.Header()
                        .AlignRight()
                        .Text("بسم الله الرحمن الرحيم")
                        .DirectionFromRightToLeft()
                        .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .ContentFromRightToLeft()
                        .Column(x =>
                        {
                            x.Spacing(20);

                            x.Item().AlignRight().Text(t =>
                            {
                                t.Span("البرمجة بلغة").DirectionFromRightToLeft();
                                t.Span(" C# ");
                                t.Span("ضمن اطار عمل").DirectionFromRightToLeft();
                                t.Span(" .Net Core ");
                                t.Span("ممتع").DirectionFromRightToLeft();
                            });
                            x.Item().Image(Placeholders.Image(200, 100));
                        });

                    page.Footer()
                        .AlignCenter()
                        .ContentFromRightToLeft()
                        .Text(x =>
                        {
                            x.Span("صفحة ");
                            x.CurrentPageNumber();
                        });
                });
            });

            var pdfFile = document.GeneratePdf();
            File.WriteAllBytes("temp.pdf", pdfFile);

            //var model = InvoiceDocumentDataSource.GetInvoiceDetails();
            //var document = new InvoiceDocument(model);





            // Generate PDF file and show it in the default viewer
            //GenerateDocumentAndShow(document);

            // Or open the QuestPDF Previewer and experiment with the document's design
            // in real-time without recompilation after each code change
            //document.ShowInPreviewer();
        }

        static void GenerateDocumentAndShow(InvoiceDocument document)
        {
            const string filePath = "invoice.pdf";

            document.GeneratePdf(filePath);

            var process = new Process
            {
                StartInfo = new ProcessStartInfo(filePath)
                {
                    UseShellExecute = true
                }
            };

            process.Start();
        }
    }
}