
namespace Models {
    public class Persona {

        public int? Id { get; set; }
        public  string? Documento { get; set; }
        public string?  Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Num_Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }
        public string? FechaRegistro { get; set; }
        public bool EstaBorrado { get; set; }
        public string? FechaBorrado { get; set; }

        public Persona() {

        }

        public Persona(int id, string? documento, string? nombre, string? apellido, string? num_Telefono, 
            string? correo, string? direccion, string? fechaRegistro, bool estaBorrado, string? fechaBorrado) {

            Id = id;
            Documento = documento;
            Nombre = nombre;
            Apellido = apellido;
            Num_Telefono = num_Telefono;
            Correo = correo;
            Direccion = direccion;
            FechaRegistro = fechaRegistro;
            EstaBorrado = estaBorrado;
            FechaBorrado = fechaBorrado;

        }

    }

}
