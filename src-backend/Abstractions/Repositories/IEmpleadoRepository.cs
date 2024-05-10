using Models;

namespace Abstractions.Repositories {
    public interface IEmpleadoRepository {

        Task<IEnumerable<Empleado>?> GetAll();

        Task<Empleado?> GetById(int id);

        Task<bool> Insert(Empleado empleado);

        Task<bool> Update(Empleado empleado);

        Task<bool> Delete(int id);

    }

}
