using Models;

namespace Abstractions.Repositories
{
    public interface ICitaRepository
    {
        Task<IEnumerable<Cita>?> GetAll();

        Task<Cita?> GetById(int id);

        Task<bool> Insert(Cita cita);

        Task<bool> Update(Cita cita);

        Task<bool> Delete(int id);

    }

}
