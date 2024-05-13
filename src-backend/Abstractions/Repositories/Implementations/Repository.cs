using Abstractions.Repositories;
using PetAgenda.Models;

namespace PetAgenda.Abstractions.Repositories.Implementations
{
    public class Repository : IRepository
    {
        public IEmpleadoRepository Empleados { get; }

        public IClienteRepository Clientes { get; }

        public ICitaRepository Citas { get; }

        public Repository(DataBaseConnection dbConnection)
        {
            if (dbConnection == null) {
                throw new NullReferenceException(nameof(dbConnection));
            }

            Empleados = new EmpleadoRepository(dbConnection);
            Clientes = new ClienteRepository(dbConnection);
            Citas = new CitaRepository(dbConnection);
        }
    }
}
