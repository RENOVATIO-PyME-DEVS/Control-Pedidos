using Control_Pedidos.Models;
using System;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Control_Pedidos.Printing
{
    /*
     * Clase: DevolucionPrintingService
     * Descripción: Gestiona la impresión del comprobante de devolución mostrando un diálogo de impresión
     *              y permitiendo emitir una copia adicional para el cliente cuando se requiera.
     */
    public class DevolucionPrintingService
    {
        /*
         * Método: Print
         * Descripción: Ejecuta la impresión del ticket original y, bajo petición del usuario,
         *              imprime una copia adicional con la leyenda correspondiente.
         */
        public void Print(DevolucionTicketData data, IWin32Window owner)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            try
            {
                using (var document = new DevolucionTicketPrintDocument(data, false))
                using (var dialog = new PrintDialog
                {
                    AllowSelection = false,
                    AllowSomePages = false,
                    UseEXDialog = true,
                    Document = document
                })
                {
                    // Se muestra el cuadro de diálogo para que el usuario elija la impresora del ticket.
                    var dialogResult = owner == null ? dialog.ShowDialog() : dialog.ShowDialog(owner);
                    if (dialogResult != DialogResult.OK)
                    {
                        return;
                    }

                    // Impresión del comprobante original.
                    document.PrinterSettings = dialog.PrinterSettings;
                    document.PrintController = new StandardPrintController();
                    document.Print();

                    // Se consulta si se necesita una copia adicional para el cliente.
                    var copiaResult = MessageBox.Show(owner, "¿Desea imprimir copia para el cliente?", "Imprimir copia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (copiaResult == DialogResult.Yes)
                    {
                        using (var copyDocument = new DevolucionTicketPrintDocument(data, true))
                        {
                            copyDocument.PrinterSettings = dialog.PrinterSettings;
                            copyDocument.PrintController = new StandardPrintController();
                            copyDocument.Print();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(owner, $"No fue posible imprimir el comprobante: {ex.Message}", "Error de impresión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
