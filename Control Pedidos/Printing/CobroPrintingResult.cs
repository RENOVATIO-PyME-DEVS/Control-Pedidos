using System;

namespace Control_Pedidos.Printing
{
    public sealed class CobroPrintingResult
    {
        private CobroPrintingResult(bool printed, bool savedPdf, string pdfPath, bool cancelledByUser, Exception error)
        {
            Printed = printed;
            SavedPdf = savedPdf;
            PdfPath = pdfPath;
            CancelledByUser = cancelledByUser;
            Error = error;
        }

        public bool Printed { get; }

        public bool SavedPdf { get; }

        public string PdfPath { get; }

        public bool CancelledByUser { get; }

        public Exception Error { get; }

        public static CobroPrintingResult PrintedSuccessfully()
        {
            return new CobroPrintingResult(true, false, null, false, null);
        }

        public static CobroPrintingResult SavedToPdf(string pdfPath, bool cancelledByUser, Exception error)
        {
            return new CobroPrintingResult(false, true, pdfPath, cancelledByUser, error);
        }
    }
}
