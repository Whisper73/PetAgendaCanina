using Models;

namespace Abstractions.Repositories {
    public interface IClienteRepository {

        Task<IEnumerable<Cliente>?> GetAll();

        Task<Cliente?> GetById(int id);

        Task<bool> Insert(Cliente cliente);

        Task<bool> Update(Cliente cliente);

        Task<bool> Delete(int id);

    }

}
