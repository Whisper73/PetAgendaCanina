using Abstractions.Repositories;

namespace PetAgenda.Abstractions.Repositories {
    public interface IRepository {

        public IEmpleadoRepository Empleados { get; }

        public IClienteRepository Clientes { get; }

        public ICitaRepository Citas { get; }

        public IMascotaRepository Mascotas { get; }

        public IServicioRepository Servicios { get; }

        public IFacturaRepository Facturas { get; }
    }
}
