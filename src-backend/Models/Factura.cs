using System;

namespace Models
{
    public class Factura
    {
        public int Id { get; set; }
        public string? FechaEmision { get; set; }
        public int Id_Cliente { get; set; }
        public int Id_Cita { get; set; }
        public int Id_Promocion { get; set; }
        public float Iva { get; set; }
        public float Total { get; set; }
        public int Id_EstadoPago { get; set; }
        public int Id_MetodoPago { get; set; }

        public Factura() {

        }

        public Factura(int id, string fechaemision, int idcliente, int idcita, int idpromocion, float iva, float total, int idestadopago,
            int idmetodopago) {
            Id = id;
            FechaEmision = fechaemision;
            Id_Cliente = idcliente;
            Id_Cita = idcita;
            Id_Promocion = idpromocion;
            Iva = iva;
            Total = total;
            Id_EstadoPago = idestadopago;
            Id_MetodoPago = idmetodopago;
        }
    }
}