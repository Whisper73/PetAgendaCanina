
using Abstractions.Repositories;

namespace PetAgenda.Abstractions.Repositories {
    public interface IRepository {

        public IEmpleadoRepository Empleados { get; }

        //public IClienteRepository Clientes { get; }

    }
}
