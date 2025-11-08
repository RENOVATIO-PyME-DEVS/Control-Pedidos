using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Control_Pedidos.Models;

namespace Control_Pedidos.Printing
{
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

                    var pdfPath = GuardarComoPdf(cobro);
                    return CobroPrintingResult.SavedToPdf(pdfPath, true, null);
                }
            }
            catch (Exception ex)
            {
                var pdfPath = GuardarComoPdf(cobro);
                return CobroPrintingResult.SavedToPdf(pdfPath, false, ex);
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
            var directorio = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Cobros");
            Directory.CreateDirectory(directorio);

            var clienteNombre = string.Empty;
            if (cobro.Cliente != null)
            {
                clienteNombre = !string.IsNullOrWhiteSpace(cobro.Cliente.NombreComercial)
                    ? cobro.Cliente.NombreComercial
                    : cobro.Cliente.Nombre;
            }

            var baseNombre = !string.IsNullOrWhiteSpace(clienteNombre)
                ? $"Cobro_{clienteNombre.Replace(' ', '_')}_{cobro.Id}".Trim('_')
                : $"Cobro_{cobro.Id}";

            if (string.IsNullOrWhiteSpace(baseNombre))
            {
                baseNombre = $"Cobro_{DateTime.Now:yyyyMMddHHmmss}";
            }

            var invalidChars = Path.GetInvalidFileNameChars();
            var nombreSeguro = new string(baseNombre.Where(c => !invalidChars.Contains(c)).ToArray());
            if (string.IsNullOrWhiteSpace(nombreSeguro))
            {
                nombreSeguro = $"Cobro_{DateTime.Now:yyyyMMddHHmmss}";
            }

            var archivo = $"{nombreSeguro}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            return Path.Combine(directorio, archivo);
        }
    }
}
