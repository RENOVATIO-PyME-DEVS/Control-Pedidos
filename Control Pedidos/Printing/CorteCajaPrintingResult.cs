using System;

namespace Control_Pedidos.Printing
{
    /// <summary>
    /// Resultado de intentar imprimir o generar un PDF del corte de caja.
    /// Permite informar a la capa de presentación si la impresión fue exitosa
    /// y, en caso contrario, cuál fue la ruta del PDF generado automáticamente.
    /// </summary>
    public class CorteCajaPrintingResult
    {
        private CorteCajaPrintingResult(bool printed, bool savedPdf, string pdfPath, bool cancelledByUser, Exception printError, Exception pdfError)
        {
            Printed = printed;
            SavedPdf = savedPdf;
            PdfPath = pdfPath;
            CancelledByUser = cancelledByUser;
            PrintError = printError;
            PdfError = pdfError;
        }

        /// <summary>
        /// Indica si la impresión física se envió correctamente a la impresora.
        /// </summary>
        public bool Printed { get; }

        /// <summary>
        /// Señala si el contenido se guardó como PDF.
        /// </summary>
        public bool SavedPdf { get; }

        /// <summary>
        /// Ruta del archivo PDF generado, en caso de existir.
        /// </summary>
        public string PdfPath { get; }

        /// <summary>
        /// Indica si el usuario canceló el cuadro de diálogo de impresión.
        /// </summary>
        public bool CancelledByUser { get; }

        /// <summary>
        /// Error producido durante el intento de impresión física.
        /// </summary>
        public Exception PrintError { get; }

        /// <summary>
        /// Error producido al generar el PDF de respaldo.
        /// </summary>
        public Exception PdfError { get; }

        public static CorteCajaPrintingResult PrintedSuccessfully()
            => new CorteCajaPrintingResult(true, false, string.Empty, false, null, null);

        public static CorteCajaPrintingResult SavedToPdf(string pdfPath, bool cancelledByUser, Exception printError, Exception pdfError)
            => new CorteCajaPrintingResult(false, true, pdfPath, cancelledByUser, printError, pdfError);
    }
}
