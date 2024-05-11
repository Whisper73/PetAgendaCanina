using Models;

namespace Abstractions.Repositories {
    public interface IPersonaRepository {

        Task<IEnumerable<Persona>> GetAll();

        Task<Persona> GetById(int id);

        Task<bool> Isert(Persona persona);

        Task<bool> Update(Persona persona);

        Task<bool> Delete(int id);

    }
}
