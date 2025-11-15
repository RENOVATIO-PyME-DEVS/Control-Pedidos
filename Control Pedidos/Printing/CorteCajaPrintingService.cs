using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Control_Pedidos.Models;

namespace Control_Pedidos.Printing
{
    /// <summary>
    /// Administra la impresión del corte de caja y garantiza que, ante cualquier problema con la impresora,
    /// se genere automáticamente un PDF en la carpeta "Cortes" del directorio de la aplicación.
    /// </summary>
    public class CorteCajaPrintingService
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

        public CorteCajaPrintingResult Print(CorteCajaReporte reporte, IWin32Window owner)
        {
            if (reporte == null)
            {
                throw new ArgumentNullException(nameof(reporte));
            }

            try
            {
                using (var document = new CorteCajaPrintDocument(reporte))
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
                        return CorteCajaPrintingResult.PrintedSuccessfully();
                    }

                    var pdfResult = TryGuardarComoPdf(reporte);
                    return CorteCajaPrintingResult.SavedToPdf(pdfResult.Path, true, null, pdfResult.Error);
                }
            }
            catch (Exception ex)
            {
                var pdfResult = TryGuardarComoPdf(reporte);
                return CorteCajaPrintingResult.SavedToPdf(pdfResult.Path, false, ex, pdfResult.Error);
            }
        }

        public string GuardarComoPdf(CorteCajaReporte reporte)
        {
            if (reporte == null)
            {
                throw new ArgumentNullException(nameof(reporte));
            }

            var impresoraPdf = BuscarImpresoraPdf();
            if (string.IsNullOrWhiteSpace(impresoraPdf))
            {
                throw new InvalidOperationException("No se encontró una impresora PDF instalada en el sistema.");
            }

            var rutaArchivo = ConstruirRutaPdf(reporte);

            using (var document = new CorteCajaPrintDocument(reporte))
            {
                document.PrinterSettings.PrinterName = impresoraPdf;
                document.PrinterSettings.PrintToFile = true;
                document.PrinterSettings.PrintFileName = rutaArchivo;
                document.PrintController = new StandardPrintController();
                document.Print();
            }

            return rutaArchivo;
        }

        private (string Path, Exception Error) TryGuardarComoPdf(CorteCajaReporte reporte)
        {
            try
            {
                var pdfPath = GuardarComoPdf(reporte);
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

        private static string ConstruirRutaPdf(CorteCajaReporte reporte)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var cortesDirectory = Path.Combine(baseDirectory, "Cortes");
            Directory.CreateDirectory(cortesDirectory);

            var fecha = reporte.FechaCorte == default ? DateTime.Now.Date : reporte.FechaCorte.Date;
            var fechaTexto = fecha.ToString("yyyyMMdd");
            var usuarioSeguro = SanitizarNombreArchivo(reporte.UsuarioNombre);
            var archivo = $"CorteCaja_{usuarioSeguro}_{fechaTexto}.pdf";

            return Path.Combine(cortesDirectory, archivo);
        }

        private static string SanitizarNombreArchivo(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return "Usuario";
            }

            var invalidChars = Path.GetInvalidFileNameChars();
            var limpio = new string(texto.Where(c => !invalidChars.Contains(c)).ToArray());
            return string.IsNullOrWhiteSpace(limpio) ? "Usuario" : limpio.Replace(' ', '_');
        }
    }
}
