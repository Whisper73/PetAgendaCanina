namespace Models {
    public class Cliente : Persona {

        public int IdCliente { get; set;}
        public int Id_Persona {get; set;}
        public int Nivel { get; set;}

        public Cliente() {

        }

        public Cliente(int clienteId, int idPersona, int nivel,
                       int personaId, string? documento, string? nombre, string? apellido,
                       string? numTelefono, string? correo, string? direccion, string? fechaRegistro,
                       bool estaBorrado, string? fechaBorrado)
            : base(personaId, documento, nombre, apellido, numTelefono, correo, direccion,
                   fechaRegistro, estaBorrado, fechaBorrado) {

            IdCliente = clienteId;
            Id_Persona = idPersona;
            Nivel = nivel;

        }

    }

}
