using Models;

namespace Abstractions.Repositories.Implementations {
    public class ClienteRepository : IClienteRepository {
       
        Task<IEnumerable<Persona>> IClienteRepository.GetAll() {
            throw new NotImplementedException();
        }

        Task<Persona> IClienteRepository.GetById(int id) {
            throw new NotImplementedException();
        }

        Task<bool> IClienteRepository.Isert(Cliente cliente) {
            throw new NotImplementedException();
        }

        Task<bool> IClienteRepository.Update(Cliente cliente) {
            throw new NotImplementedException();
        }

        Task<bool> IClienteRepository.Delete(int id) {
            throw new NotImplementedException();
        }

    }

}
