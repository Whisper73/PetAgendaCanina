using System;

namespace Models
{
    public class Cita
    {
        public int IdCita { get; set; }
        public int Id_Cliente_Mascota { get; set; }
        public string? Fecha { get; set; }
        public int Id_EstadoCita { get; set; }
        public bool EstaEliminada { get; set; }
        public string? FechaEliminada { get; set; }
        public string? Observacion { get; set; }

        public Cita()
        {
            
        }

        public Cita(int idcita, int idclientemascota, string fecha, int idestadocita, bool estaeliminada,
            string fechaeliminada, string observacion)
        {
            IdCita = idcita;
            Id_Cliente_Mascota = idclientemascota;
            Fecha = fecha;
            Id_EstadoCita = idestadocita;
            EstaEliminada = estaeliminada;
            FechaEliminada = fechaeliminada;
            Observacion = observacion;
        }
    }
}