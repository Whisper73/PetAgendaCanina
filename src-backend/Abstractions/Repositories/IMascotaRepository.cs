using Models;

namespace Abstractions.Repositories
{
    public interface IMascotaRepository
    {

        Task<IEnumerable<Mascota>?> GetAll();

        Task<Mascota?> GetById(int id);

        Task<bool> Insert(Mascota mascota);

        Task<bool> Update(Mascota mascota);

        Task<bool> Delete(int id);

    }

}
