using Dapper;
using Models;
using MySql.Data.MySqlClient;
using PetAgenda.Models;

namespace Abstractions.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly string QueryGetFacturaById = @"
            SELECT *
            FROM Factura
            WHERE Factura.Id = @Id;";

        private readonly string QueryGetAllFacturas = @"
            SELECT *
            FROM Factura;";

        private readonly string QueryInsertFactura = @"
            INSERT INTO Factura (FechaEmision, Id_Cliente, Id_Cita, Id_Promocion, Iva, Total, Id_EstadoPago, Id_MetodoPago)
            VALUES (@FechaEmision, @Id_Cliente, @Id_Cita, @Id_Promocion, @Iva, @Total, @Id_EstadoPago, @Id_MetodoPago);
            SELECT LAST_INSERT_ID();";

        private readonly string QueryUpdateFactura = @"
            UPDATE Factura
            SET FechaEmision = @FechaEmision,
                Id_Cliente = @Id_Cliente,
                Id_Cita = @Id_Cita,
                Id_Promocion = @Id_Promocion,
                Iva = @Iva,
                Total = @Total,
                Id_EstadoPago = @Id_EstadoPago,
                Id_MetodoPago = @Id_MetodoPago
            WHERE Id = @Id;";

        private readonly string QueryDeleteFactura = @"";


        private readonly DataBaseConnection _dbConnection;

        public FacturaRepository(DataBaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Factura>?> GetAll()
        {
            IEnumerable<Factura>? response = null;

            await using (MySqlConnection con = _dbConnection.CreateConnection())
            {
                await con.OpenAsync();

                try
                {
                    response = await con.QueryAsync<Factura>(QueryGetAllFacturas, new { });
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }

            return response;
        }

        public async Task<Factura?> GetById(int id)
        {
            Factura? response = null;

            await using (MySqlConnection con = _dbConnection.CreateConnection())
            {
                await con.OpenAsync();

                try
                {
                    response = await con.QueryFirstOrDefaultAsync<Factura>(QueryGetFacturaById, new { Id = id });
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }

            return response;
        }

        public async Task<bool> Insert(Factura factura)
        {
            if (factura == null)
            {
                throw new ArgumentNullException(nameof(factura));
            }

            int facturaId;

            factura.FechaEmision = DateTime.Now.ToString("yyyy-MM-dd");

            try
            {
                await using (MySqlConnection con = _dbConnection.CreateConnection())
                {
                    await con.OpenAsync();

                    using (MySqlTransaction transaction = con.BeginTransaction())
                    {
                        try
                        {
                            facturaId = await con.ExecuteScalarAsync<int>(
                                QueryInsertFactura,
                                new
                                {
                                    factura.FechaEmision,
                                    factura.Id_Cliente,
                                    factura.Id_Cita,
                                    factura.Id_Promocion,
                                    factura.Iva,
                                    factura.Total,
                                    factura.Id_EstadoPago,
                                    factura.Id_MetodoPago
                                },
                                transaction
                            );

                            transaction.Commit();
                            return true;
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            return false;
                        }
                        finally
                        {
                            await con.CloseAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar datos", ex);
            }
        }

        public async Task<bool> Update(Factura factura)
        {
            int response;

            if (factura == null)
            {
                throw new ArgumentNullException(nameof(factura));
            }

            await using (MySqlConnection con = _dbConnection.CreateConnection())
            {
                await con.OpenAsync();

                try
                {
                    response = await con.ExecuteAsync(
                        QueryUpdateFactura,
                        new
                        {
                            factura.FechaEmision,
                            factura.Id_Cliente,
                            factura.Id_Cita,
                            factura.Id_Promocion,
                            factura.Iva,
                            factura.Total,
                            factura.Id_EstadoPago,
                            factura.Id_MetodoPago
                        }
                    );
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }

            if (response == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            int response;

            await using (MySqlConnection con = _dbConnection.CreateConnection())
            {
                await con.OpenAsync();

                try
                {
                    response = await con.ExecuteAsync(QueryDeleteFactura, new { Id = id });
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }

            if (response == 0)
            {
                return false;
            }

            return true;

        }

    }

}
