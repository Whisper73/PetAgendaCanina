using Dapper;
using Models;
using MySql.Data.MySqlClient;
using PetAgenda.Models;

namespace Abstractions.Repositories
{
    public class ServicioRepository : IServicioRepository
    {
        private readonly string QueryGetServicioById = @"
            SELECT *
            FROM Servicio
            WHERE Servicio.Id = @IdServicio;";

        private readonly string QueryGetAllServicios = @"
            SELECT *
            FROM Servicio;";

        private readonly string QueryInsertServicio = @"
            INSERT INTO Servicio (Id, InicioVigencia, FinVigencia, Descripcion, ValorServicioBase)
            VALUES (@Id, @InicioVigencia, @FinVigencia, @Descripcion,  @ValorServicioBase);
            SELECT LAST_INSERT_ID();";

        private readonly string QueryUpdateServicio = @"
            UPDATE Servicio
            SET InicioVigencia = @InicioVigencia,
                FinVigencia = @FinVigencia,
                Descripcion = @Descripcion,
                ValorServicioBase = @ValorServicioBase
            WHERE Id = @Id;";

        private readonly string QueryDeleteServicio = @"
            UPDATE Servicio
            SET FinVigencia = @FinVigencia
            WHERE Id = @Id;";


        private readonly DataBaseConnection _dbConnection;

        public ServicioRepository(DataBaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Servicio>?> GetAll()
        {
            IEnumerable<Servicio>? response = null;

            await using (MySqlConnection con = _dbConnection.CreateConnection())
            {
                await con.OpenAsync();

                try
                {
                    response = await con.QueryAsync<Servicio>(QueryGetAllServicios, new { });
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

        public async Task<Servicio?> GetById(int id)
        {
            Servicio? response = null;

            await using (MySqlConnection con = _dbConnection.CreateConnection())
            {
                await con.OpenAsync();

                try
                {
                    response = await con.QueryFirstOrDefaultAsync<Servicio>(QueryGetServicioById, new { Id = id });
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

        public async Task<bool> Insert(Servicio servicio)
        {
            if (servicio == null)
            {
                throw new ArgumentNullException(nameof(servicio));
            }

            int Id;

            string InicioVigencia = DateTime.Now.ToString("yyyy-MM-dd");

            try
            {
                await using (MySqlConnection con = _dbConnection.CreateConnection())
                {
                    await con.OpenAsync();

                    using (MySqlTransaction transaction = con.BeginTransaction())
                    {
                        try
                        {
                            Id = await con.ExecuteScalarAsync<int>(
                                QueryInsertServicio,
                                new
                                {
                                    servicio.Id,
                                    servicio.InicioVigencia,
                                    servicio.FinVigencia,
                                    servicio.Descripcion,
                                    servicio.ValorServicioBase,
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

        public async Task<bool> Update(Servicio servicio)
        {
            int response;

            if (servicio == null)
            {
                throw new ArgumentNullException(nameof(servicio));
            }

            await using (MySqlConnection con = _dbConnection.CreateConnection())
            {
                await con.OpenAsync();

                try
                {
                    response = await con.ExecuteAsync(
                        QueryUpdateServicio,
                        new
                        {
                            servicio.Id,
                            servicio.InicioVigencia,
                            servicio.FinVigencia,
                            servicio.Descripcion,
                            servicio.ValorServicioBase,
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

            string FinVigencia = DateTime.Now.ToString("yyyy-MM-dd");

            await using (MySqlConnection con = _dbConnection.CreateConnection())
            {
                await con.OpenAsync();

                try
                {
                    response = await con.ExecuteAsync(QueryDeleteServicio, new { FinVigencia, Id = id });
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
