using System;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Control_Pedidos.Printing
{
    public class CobroPrintingService
    {
        private static readonly string[] PdfPrinterCandidates =
        {
            "Microsoft Print to PDF",
            "Microsoft Print To PDF",
            "Adobe PDF",
            "PDFCreator",
            "CutePDF",
            "doPDF"
        };

        public CobroPrintingResult Print(CobroTicketInfo ticketInfo, IWin32Window owner)
        {
            if (ticketInfo == null)
            {
                throw new ArgumentNullException(nameof(ticketInfo));
            }

            try
            {
                using (var document = new CobroPrintDocument(ticketInfo))
                using (var dialog = new PrintDialog
                {
                    AllowSomePages = false,
                    AllowSelection = false,
                    UseEXDialog = true,
                    Document = document
                })
                {
                    var dialogResult = owner == null ? dialog.ShowDialog() : dialog.ShowDialog(owner);

                    if (dialogResult == DialogResult.OK)
                    {
                        document.PrinterSettings = dialog.PrinterSettings;
                        document.PrintController = new StandardPrintController();
                        document.Print();
                        return CobroPrintingResult.PrintedSuccessfully();
                    }

                    var pdfPath = GuardarComoPdf(ticketInfo);
                    return CobroPrintingResult.SavedToPdf(pdfPath, true, null);
                }
            }
            catch (Exception ex)
            {
                var pdfPath = GuardarComoPdf(ticketInfo);
                return CobroPrintingResult.SavedToPdf(pdfPath, false, ex);
            }
        }

        public string GuardarComoPdf(CobroTicketInfo ticketInfo)
        {
            if (ticketInfo == null)
            {
                throw new ArgumentNullException(nameof(ticketInfo));
            }

            var impresoraPdf = BuscarImpresoraPdf();
            if (string.IsNullOrWhiteSpace(impresoraPdf))
            {
                throw new InvalidOperationException("No se encontró una impresora PDF instalada en el sistema.");
            }

            var ruta = ConstruirRutaPdf(ticketInfo);

            using (var document = new CobroPrintDocument(ticketInfo))
            {
                document.PrinterSettings.PrinterName = impresoraPdf;
                document.PrinterSettings.PrintFileName = ruta;
                document.PrinterSettings.PrintToFile = true;
                document.PrintController = new StandardPrintController();
                document.Print();
            }

            return ruta;
        }

        private static string BuscarImpresoraPdf()
        {
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                if (PdfPrinterCandidates.Any(candidate => printer.IndexOf(candidate, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    return printer;
                }
            }

            return null;
        }

        private static string ConstruirRutaPdf(CobroTicketInfo ticketInfo)
        {
            var directorio = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Cobros");
            Directory.CreateDirectory(directorio);

            var baseNombre = ticketInfo.Cobro?.Id > 0 ? $"Cobro_{ticketInfo.Cobro.Id}" : "Cobro";
            var archivo = $"{baseNombre}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            return Path.Combine(directorio, archivo);
        }
    }
}
