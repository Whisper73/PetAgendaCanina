namespace Models
{
    public class Mascota
        {

        public int Id { get; set; }
        public string? Raza { get; set; }
        public string? Nombre { get; set; }
        public int? Id_CategoriaMascota { get; set; }
        public string? FechaNacim { get; set; }
        public int? NivelTemperamento { get; set; }
        public string? Observaciones { get; set; }
        public string? FechaRegistro { get; set; }
        public bool EstaBorrado { get; set; }
        public string? FechaBorrado { get; set; }

        public Mascota(int id, string? raza, string? nombre, int? id_categoriaMascota, string? fechaNacim,
            int? nivelTemperamento, string? observaciones, string? fechaRegistro, bool estaBorrado, string? fechaBorrado)
        {

            Id = id;
            Raza = raza;
            Nombre = nombre;
            Id_CategoriaMascota = id_categoriaMascota;
            FechaNacim = fechaNacim;
            NivelTemperamento = nivelTemperamento;
            Observaciones = observaciones;
            FechaRegistro = fechaRegistro;
            EstaBorrado = estaBorrado;
            FechaBorrado = fechaBorrado;

        }

    }

}
