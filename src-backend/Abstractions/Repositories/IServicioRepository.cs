using Models;

namespace Abstractions.Repositories
{
    public interface IServicioRepository
    {
        Task<IEnumerable<Servicio>?> GetAll();

        Task<Servicio?> GetById(int id);

        Task<bool> Insert(Servicio servicio);

        Task<bool> Update(Servicio servicio);

        Task<bool> Delete(int id);
    }
}
