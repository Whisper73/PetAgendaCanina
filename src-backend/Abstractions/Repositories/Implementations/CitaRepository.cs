using Dapper;
using Models;
using MySql.Data.MySqlClient;
using PetAgenda.Models;

namespace Abstractions.Repositories
{
    public class CitaRepository : ICitaRepository
    {
        private readonly string QueryGetCitaById = @"
            SELECT *
            FROM Cita
            WHERE Cita.EstaEliminada = false AND Cita.Id = @IdCita;";

        private readonly string QueryGetAllCitas = @"
            SELECT *
            FROM Cita
            WHERE Cita.EstaEliminada = false;";

        private readonly string QueryInsertCita = @"
            INSERT INTO Cita (Id_Cliente_Mascota, Fecha, Id_EstadoCita, Observacion)
            VALUES (@Id_Cliente_Mascota, @Fecha, @Id_EstadoCita, @Observacion);
            SELECT LAST_INSERT_ID();";

        private readonly string QueryUpdateCita = @"
            UPDATE Cita
            SET Id_Cliente_Mascota = @Id_Cliente_Mascota,
                Fecha = @Fecha,
                Id_EstadoCita = @Id_EstadoCita,
                Observacion = @Observacion
            WHERE Id = @IdCita;";

        private readonly string QueryDeleteCita = @"
            UPDATE Cita
            SET EstaEliminada = true,
                FechaEliminada = @FechaEliminada
            WHERE Cita.Id = @IdCita;";


        private readonly DataBaseConnection _dbConnection;

        public CitaRepository(DataBaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Cita>?> GetAll()
        {
            IEnumerable<Cita>? response = null;

            await using (MySqlConnection con = _dbConnection.CreateConnection())
            {
                await con.OpenAsync();

                try
                {
                    response = await con.QueryAsync<Cita>(QueryGetAllCitas, new { });
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

        public async Task<Cita?> GetById(int id)
        {
            Cita? response = null;

            await using (MySqlConnection con = _dbConnection.CreateConnection())
            {
                await con.OpenAsync();

                try
                {
                    response = await con.QueryFirstOrDefaultAsync<Cita>(QueryGetCitaById, new { IdCita = id });
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

        public async Task<bool> Insert(Cita cita)
        {
            if (cita == null)
            {
                throw new ArgumentNullException(nameof(cita));
            }

            int citaId;

            cita.Fecha = DateTime.Now.ToString("yyyy-MM-dd");

            try
            {
                await using (MySqlConnection con = _dbConnection.CreateConnection())
                {
                    await con.OpenAsync();

                    using (MySqlTransaction transaction = con.BeginTransaction())
                    {
                        try
                        {
                            citaId = await con.ExecuteScalarAsync<int>(
                                QueryInsertCita,
                                new
                                {
                                    cita.Id_Cliente_Mascota,
                                    cita.Fecha,
                                    cita.Id_EstadoCita,
                                    cita.Observacion,
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

        public async Task<bool> Update(Cita cita)
        {
            int response;

            if (cita == null)
            {
                throw new ArgumentNullException(nameof(cita));
            }

            await using (MySqlConnection con = _dbConnection.CreateConnection())
            {
                await con.OpenAsync();

                try
                {
                    response = await con.ExecuteAsync(
                        QueryUpdateCita,
                        new
                        {
                            cita.Id_Cliente_Mascota,
                            cita.Fecha,
                            cita.Id_EstadoCita,
                            cita.Observacion
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

            string FechaEliminada = DateTime.Now.ToString("yyyy-MM-dd");

            await using (MySqlConnection con = _dbConnection.CreateConnection())
            {
                await con.OpenAsync();

                try
                {
                    response = await con.ExecuteAsync(QueryDeleteCita, new { FechaEliminada, IdCita = id });
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
