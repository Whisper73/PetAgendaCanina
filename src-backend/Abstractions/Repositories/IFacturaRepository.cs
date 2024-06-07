using Models;

namespace Abstractions.Repositories
{
    public interface IFacturaRepository
    {
        Task<IEnumerable<Factura>?> GetAll();

        Task<Factura?> GetById(int id);

        Task<bool> Insert(Factura factura);

        Task<bool> Update(Factura factura);

        Task<bool> Delete(int id);

    }

}
