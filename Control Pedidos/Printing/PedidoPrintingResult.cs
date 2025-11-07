using System;

namespace Control_Pedidos.Printing
{
    public sealed class PedidoPrintingResult
    {
        private PedidoPrintingResult(bool printed, bool savedPdf, string pdfPath, bool cancelledByUser, Exception error)
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

        public static PedidoPrintingResult PrintedSuccessfully()
        {
            return new PedidoPrintingResult(true, false, null, false, null);
        }

        public static PedidoPrintingResult SavedToPdf(string pdfPath, bool cancelledByUser, Exception error)
        {
            return new PedidoPrintingResult(false, true, pdfPath, cancelledByUser, error);
        }
    }
}
