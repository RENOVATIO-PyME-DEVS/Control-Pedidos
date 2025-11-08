using System;

namespace Control_Pedidos.Printing
{
    public class CobroPrintingResult
    {
        private CobroPrintingResult(bool printed, bool savedPdf, bool cancelledByUser, string pdfPath, Exception error, Exception pdfError)
        {
            Printed = printed;
            SavedPdf = savedPdf;
            CancelledByUser = cancelledByUser;
            PdfPath = pdfPath;
            Error = error;
            PdfError = pdfError;
        }

        public bool Printed { get; }
        public bool SavedPdf { get; }
        public bool CancelledByUser { get; }
        public string PdfPath { get; }
        public Exception Error { get; }
        public Exception PdfError { get; }

        public static CobroPrintingResult PrintedSuccessfully()
        {
            return new CobroPrintingResult(true, false, false, string.Empty, null, null);
        }

        public static CobroPrintingResult SavedToPdf(string pdfPath, bool cancelledByUser, Exception error, Exception pdfError)
        {
            return new CobroPrintingResult(false, true, cancelledByUser, pdfPath, error, pdfError);
        }
    }
}
