using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Control_Pedidos.Models;

namespace Control_Pedidos.Printing
{
    public class PedidoPrintingService
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

        public PedidoPrintingResult Print(Pedido pedido, IWin32Window owner, bool mostrarLeyendaReimpreso)
        {
            if (pedido == null)
            {
                throw new ArgumentNullException(nameof(pedido));
            }

            try
            {
                using (var document = new PedidoPrintDocument(pedido, mostrarLeyendaReimpreso))
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
                        return PedidoPrintingResult.PrintedSuccessfully();
                    }

                    var pdfPath = GuardarComoPdf(pedido, mostrarLeyendaReimpreso);
                    return PedidoPrintingResult.SavedToPdf(pdfPath, true, null);
                }
            }
            catch (Exception ex)
            {
                var pdfPath = GuardarComoPdf(pedido, mostrarLeyendaReimpreso);
                return PedidoPrintingResult.SavedToPdf(pdfPath, false, ex);
            }
        }

        public string GuardarComoPdf(Pedido pedido, bool mostrarLeyendaReimpreso)
        {
            if (pedido == null)
            {
                throw new ArgumentNullException(nameof(pedido));
            }

            var impresoraPdf = BuscarImpresoraPdf();
            if (string.IsNullOrWhiteSpace(impresoraPdf))
            {
                throw new InvalidOperationException("No se encontró una impresora PDF instalada en el sistema.");
            }

            var rutaArchivo = ConstruirRutaPdf(pedido);

            using (var document = new PedidoPrintDocument(pedido, mostrarLeyendaReimpreso))
            {
                document.PrinterSettings.PrinterName = impresoraPdf;
                document.PrinterSettings.PrintFileName = rutaArchivo;
                document.PrinterSettings.PrintToFile = true;
                document.PrintController = new StandardPrintController();
                document.Print();
            }

            return rutaArchivo;
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

        private static string ConstruirRutaPdf(Pedido pedido)
        {
            var directorio = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Pedidos");
            Directory.CreateDirectory(directorio);

            var baseNombre = !string.IsNullOrWhiteSpace(pedido.FolioFormateado)
                ? pedido.FolioFormateado
                : (!string.IsNullOrWhiteSpace(pedido.Folio) ? pedido.Folio : $"Pedido_{pedido.Id}");

            var invalidChars = Path.GetInvalidFileNameChars();
            var nombreSeguro = new string(baseNombre.Where(c => !invalidChars.Contains(c)).ToArray());
            if (string.IsNullOrWhiteSpace(nombreSeguro))
            {
                nombreSeguro = $"Pedido_{pedido.Id}";
            }

            var archivo = $"{nombreSeguro}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            return Path.Combine(directorio, archivo);
        }
    }
}
