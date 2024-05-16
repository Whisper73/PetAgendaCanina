using Dapper;
using Models;
using MySql.Data.MySqlClient;
using PetAgenda.Models;

namespace Abstractions.Repositories
{

    public class MascotaRepository : IMascotaRepository
    {

        private readonly string QueryGetMascotaById = @"
            SELECT 
                m.Id AS IdMascota,
                m.Raza,
                m.Nombre,
                m.IdCategoriaMascota,
                m.FechaNacim,                
                m.NivelTemperamento,
                m.Observaciones,
                m.FechaRegistro

            FROM 
                Mascota m
            WHERE m.EstaBorrado = false
            AND m.Id = @id;";

        private readonly string QueryGetAllMascotas = @"
            SELECT 
                m.Id AS IdMascota,
                m.Raza,
                m.Nombre,
                m.IdCategoriaMascota,
                m.FechaNacim,                
                m.NivelTemperamento,
                m.Observaciones,
                m.FechaRegistro

            FROM 
                Mascota m
            
            WHERE m.EstaBorrado = false";

        private readonly string QueryInsertMascota = @"
            INSERT INTO Mascota (Id, Raza, Nombre, Id_CategoriaMascota, FechaNacim, NivelTemperamento, Observaciones, FechaRegistro)
            VALUES (@Id, @Raza, @Nombre, @Id_CategoriaMascota, @FechaNacim, @NivelTemperamento, @Observaciones, @FechaRegistro); 
            SELECT LAST_INSERT_ID();";

        private readonly string QueryInsertCategoriaMascota = @"
            INSERT INTO Empleado (Id_CategoriaMascota, Id_TipoPelo, Id_TamanoMascota)
            VALUES (@Id_CategoriaMascota, @Id_TipoPelo, @Id_TamanoMascota);";

        private readonly string QueryUpdateMascota = @"
            UPDATE Mascota
            SET Raza = @Raza,
                Nombre = @Nombre,
                Id_CategoriaMascota = @Id_CategoriaMascota,
                FechaNacim = @FechaNacim,
                Observaciones = @Observaciones
            WHERE Id = @Id_Mascota;";

        private readonly string QueryDeleteMascota = @"
            UPDATE Mascota
            SET EstaBorrado = true,
                FechaBorrado = @FechaBorrado
            WHERE Mascota.Id = @Id_Mascota;";


        private readonly DataBaseConnection _dbConnection;

        public MascotaRepository(DataBaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Mascota>?> GetAll()
        {

            IEnumerable<Mascota>? response = null;

            await using (MySqlConnection con = _dbConnection.CreateConnection())
            {

                await con.OpenAsync();

                try
                {
                    response = await con.QueryAsync<Mascota>(QueryGetAllMascotas, new { });
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

        public async Task<Mascota?> GetById(int id)
        {

            Mascota? response = null;

            await using (MySqlConnection con = _dbConnection.CreateConnection())
            {

                await con.OpenAsync();

                try
                {
                    response = await con.QueryFirstOrDefaultAsync<Mascota>(QueryGetMascotaById, new { Id = id });
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

        public async Task<bool> Insert(Mascota mascota)
        {

            if (mascota == null)
            {
                throw new ArgumentNullException(nameof(mascota));
            }

            mascota.FechaRegistro = DateTime.Now.ToString("yyyy-MM-dd");

            try
            {
                await using (MySqlConnection con = _dbConnection.CreateConnection())
                {

                    await con.OpenAsync();

                    using (MySqlTransaction transaction = con.BeginTransaction())
                    {

                        try
                        {
                             await con.ExecuteScalarAsync<int>(QueryInsertMascota,
                                new
                                {
                                    mascota.Raza,
                                    mascota.Nombre,
                                    mascota.Id_CategoriaMascota,
                                    mascota.FechaNacim,
                                    mascota.NivelTemperamento,
                                    mascota.Observaciones,
                                    mascota.FechaRegistro,
                                }, transaction);

                           
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

        public async Task<bool> Update(Mascota mascota)
        {

            int response;

            if (mascota == null)
            {
                throw new ArgumentNullException(nameof(mascota));
            }

            await using (MySqlConnection con = _dbConnection.CreateConnection())
            {

                await con.OpenAsync();

                try
                {

                    response = await con.ExecuteAsync(QueryUpdateMascota,
                        new
                        {
                            mascota.Raza,
                            mascota.Nombre,
                            mascota.Id_CategoriaMascota,
                            mascota.FechaNacim,
                            mascota.NivelTemperamento,
                            mascota.Observaciones,
                            mascota.FechaRegistro,
                        });

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

            string FechaBorrado = DateTime.Now.ToString("yyyy-MM-dd");

            await using (MySqlConnection con = _dbConnection.CreateConnection())
            {

                await con.OpenAsync();

                try
                {
                    response = await con.ExecuteAsync(QueryDeleteMascota, new { FechaBorrado, Id_Persona = id });
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
