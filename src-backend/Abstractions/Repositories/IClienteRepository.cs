using Models;

namespace Abstractions.Repositories {
    public interface IClienteRepository {

        Task<IEnumerable<Persona>> GetAll();

        Task<Persona> GetById(int id);

        Task<bool> Isert(Cliente cliente);

        Task<bool> Update(Cliente cliente);

        Task<bool> Delete(int id);

    }

}
