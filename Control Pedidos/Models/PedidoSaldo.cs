using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Control_Pedidos.Models
{
    public class PedidoSaldo : INotifyPropertyChanged
    {
        private decimal _montoAsignado;

        public event PropertyChangedEventHandler PropertyChanged;

        public int PedidoId { get; set; }
        public string Folio { get; set; } = string.Empty;
        public DateTime FechaEntrega { get; set; }
        public decimal Total { get; set; }
        public decimal Abonado { get; set; }
        public decimal Saldo => Total - Abonado;

        public decimal MontoAsignado
        {
            get => _montoAsignado;
            set
            {
                if (_montoAsignado != value)
                {
                    _montoAsignado = value;
                    OnPropertyChanged();
                }
            }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
