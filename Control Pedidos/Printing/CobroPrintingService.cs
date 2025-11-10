using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Control_Pedidos.Models;

namespace Control_Pedidos.Printing
{
    /*
     * Clase: CobroPrintingService
     * Descripción: Gestiona la impresión de tickets de cobro y la generación automática de PDFs en caso de falla.
     */
    public class CobroPrintingService
    {
        private static readonly IReadOnlyList<string> PdfPrinterCandidates = new[]
        {
            "Microsoft Print to PDF",
            "Microsoft Print To PDF",
            "Adobe PDF",
            "PDFCreator",
            "CutePDF",
            "doPDF"
        };

        public CobroPrintingResult Print(Cobro cobro, IWin32Window owner)
        {
            if (cobro == null)
            {
                throw new ArgumentNullException(nameof(cobro));
            }

            try
            {
                using (var document = new CobroTicketPrintDocument(cobro))
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

                    var pdfResult = TryGuardarComoPdf(cobro);
                    return CobroPrintingResult.SavedToPdf(pdfResult.Path, true, null, pdfResult.Error);
                }
            }
            catch (Exception ex)
            {
                var pdfResult = TryGuardarComoPdf(cobro);
                return CobroPrintingResult.SavedToPdf(pdfResult.Path, false, ex, pdfResult.Error);
            }
        }

        public string GuardarComoPdf(Cobro cobro)
        {
            if (cobro == null)
            {
                throw new ArgumentNullException(nameof(cobro));
            }

            var impresoraPdf = BuscarImpresoraPdf();
            if (string.IsNullOrWhiteSpace(impresoraPdf))
            {
                throw new InvalidOperationException("No se encontró una impresora PDF instalada en el sistema.");
            }

            var rutaArchivo = ConstruirRutaPdf(cobro);

            using (var document = new CobroTicketPrintDocument(cobro))
            {
                document.PrinterSettings.PrinterName = impresoraPdf;
                document.PrinterSettings.PrintFileName = rutaArchivo;
                document.PrinterSettings.PrintToFile = true;
                document.PrintController = new StandardPrintController();
                document.Print();
            }

            return rutaArchivo;
        }

        private (string Path, Exception Error) TryGuardarComoPdf(Cobro cobro)
        {
            try
            {
                var pdfPath = GuardarComoPdf(cobro);
                return (pdfPath, null);
            }
            catch (Exception ex)
            {
                return (string.Empty, ex);
            }
        }

        private string BuscarImpresoraPdf()
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

        private static string ConstruirRutaPdf(Cobro cobro)
        {
            // Se guarda el PDF en el escritorio dentro de la carpeta "Banquetes/Tickets" para que el usuario lo encuentre fácilmente.
            var escritorio = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var directorio = Path.Combine(escritorio, "Banquetes", "Tickets");
            Directory.CreateDirectory(directorio);

            var identificador = cobro?.CobroPedidoId > 0
                ? cobro.CobroPedidoId.ToString()
                : (cobro?.Id > 0 ? cobro.Id.ToString() : "SinId");

            var fecha = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var archivo = $"TicketCobro_{identificador}_{fecha}.pdf";
            var invalidChars = Path.GetInvalidFileNameChars();
            var seguro = new string(archivo.Where(c => !invalidChars.Contains(c)).ToArray());
            if (string.IsNullOrWhiteSpace(seguro))
            {
                seguro = $"TicketCobro_{fecha}.pdf";
            }

            return Path.Combine(directorio, seguro);
        }
    }
}
