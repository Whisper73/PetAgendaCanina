namespace Models {
    public class Empleado : Persona {

        public int? IdEmpleado {get; set;}
        public int? Id_Persona {get; set;}
        public int? NivelPermisos { get; set; }
        public bool EsAdmin  { get; set; }
        public string? Contrasena { get; set; }

        public Empleado() {

        }
        public Empleado(int id, int idPersona, int nivelPermisos, bool esAdmin, string? contrasena,
                      int personaId, string? documento, string? nombre, string? apellido,
                      string? numTelefono, string? correo, string? direccion, string? fechaRegistro,
                      bool estaBorrado, string? fechaBorrado)
          : base(personaId, documento, nombre, apellido, numTelefono, correo, direccion,
                 fechaRegistro, estaBorrado, fechaBorrado) {

            IdEmpleado = id;
            Id_Persona = idPersona;
            NivelPermisos = nivelPermisos;
            EsAdmin = esAdmin;
            Contrasena = contrasena;
        }

    }

}
