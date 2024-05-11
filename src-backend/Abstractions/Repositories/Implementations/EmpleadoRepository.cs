using Dapper;
using Models;
using MySql.Data.MySqlClient;
using PetAgenda.Models;

namespace Abstractions.Repositories.Implementations {

    public class EmpleadoRepository : IEmpleadoRepository {

        private readonly string QueryGetEmpleadoById = @"
            SELECT 
                e.Id AS IdEmpleado,
                e.Id_Persona,
                e.NivelPermisos,
                e.EsAdmin,
                e.Contrasena,
                p.Id,
                p.Documento,
                p.Nombre,
                p.Apellido,
                p.Num_Telefono,
                p.Correo,
                p.Direccion,
                p.FechaRegistro,
                p.EstaBorrado,
                p.FechaBorrado
            FROM 
                Empleado e
            INNER JOIN 
                Persona p ON e.Id_Persona = p.Id          
            WHERE P.EstaBorrado = false
            AND e.Id = @id;";

        private readonly string QueryGetAllEmpleados = @"
            SELECT 
                e.Id AS IdEmpleado,
                e.Id_Persona,
                e.NivelPermisos,
                e.EsAdmin,
                e.Contrasena,
                p.Id,
                p.Documento,
                p.Nombre,
                p.Apellido,
                p.Num_Telefono,
                p.Correo,
                p.Direccion,
                p.FechaRegistro,
                p.EstaBorrado,
                p.FechaBorrado
            FROM 
                Empleado e
            INNER JOIN 
                Persona p ON e.Id_Persona = p.Id          
            WHERE P.EstaBorrado = false;";

        private readonly string QueryInsertPersona = @"
            INSERT INTO Persona (Documento, Nombre, Apellido, Num_Telefono, Correo, Direccion, FechaRegistro)
            VALUES (@Documento, @Nombre, @Apellido, @Num_Telefono, @Correo, @Direccion, @FechaRegistro); 
            SELECT LAST_INSERT_ID();";

        private readonly string QueryInsertEmpleado = @"
            INSERT INTO Empleado (Id_Persona, NivelPermisos, EsAdmin, Contrasena)
            VALUES (@Id_Persona, @NivelPermisos, @EsAdmin, @Contrasena);";

        private readonly string QueryUpdateEmpleado = @"
            UPDATE Persona
            SET Documento = @Documento,
                Nombre = @Nombre,
                Apellido = @Apellido,
                Num_Telefono = @Num_Telefono,
                Correo = @Correo,
                Direccion = @Direccion
            WHERE Id = @Id_Persona;

            UPDATE Empleado
            SET NivelPermisos = @NivelPermisos,
                EsAdmin = @EsAdmin,
                Contrasena = @Contrasena
            WHERE Id = @IdEmpleado;";

        private readonly string QueryDeleteEmpleado = @"
            UPDATE Persona
            SET EstaBorrado = true,
                FechaBorrado = @FechaBorrado
            WHERE Persona.Id = @Id_Persona;";


        private readonly DataBaseConnection _dbConnection;

        public EmpleadoRepository(DataBaseConnection dbConnection) {
            _dbConnection = dbConnection;
        }
    
        public async Task<IEnumerable<Empleado>?> GetAll() {

            IEnumerable<Empleado>? response = null;

            await using (MySqlConnection con = _dbConnection.CreateConnection()) {

                await con.OpenAsync();

                try {
                    response = await con.QueryAsync<Empleado>(QueryGetAllEmpleados, new { });
                }
                catch (Exception) {
                    throw;
                }
                finally {
                    con.Close();
                }

            }
     
            return response;
        }

        public async Task<Empleado?> GetById(int id) {

            Empleado? response = null;

            await using (MySqlConnection con = _dbConnection.CreateConnection()) {

                await con.OpenAsync();

                try {
                    response = await con.QueryFirstOrDefaultAsync<Empleado>(QueryGetEmpleadoById, new { Id = id });
                }
                catch(Exception) {
                    throw;
                }
                finally { 
                    con.Close();
                }

            }

              return response;        
        }

        public async Task<bool> Insert(Empleado empleado) {

            if (empleado == null) {
                throw new ArgumentNullException(nameof(empleado));
            }

            int personaId;

            empleado.FechaRegistro = DateTime.Now.ToString("yyyy-MM-dd");

            try {
                await using (MySqlConnection con = _dbConnection.CreateConnection()) {
                    await con.OpenAsync();

                    using (MySqlTransaction transaction = con.BeginTransaction()) {

                        try {
                            personaId = await con.ExecuteScalarAsync<int>(QueryInsertPersona,
                                new {
                                    empleado.Documento,
                                    empleado.Nombre,
                                    empleado.Apellido,
                                    empleado.Num_Telefono,
                                    empleado.Correo,
                                    empleado.Direccion,
                                    empleado.FechaRegistro,
                                }, transaction);

                            await con.ExecuteAsync(QueryInsertEmpleado, new {
                                Id_Persona = personaId,
                                empleado.NivelPermisos,
                                empleado.EsAdmin,
                                empleado.Contrasena
                            }, transaction);

                            transaction.Commit();
                            return true;
                        }
                        catch (Exception) {
                            transaction.Rollback();
                            return false;
                        }

                    }

                }

            }
            catch (Exception ex) {
                // Manejar la excepción o lanzarla nuevamente si es necesario
                throw new Exception("Error al insertar datos", ex);
            }

        }

        public async Task<bool> Update(Empleado empleado) {

            int response;

            if (empleado == null) {
                throw new ArgumentNullException(nameof(empleado));
            }

            empleado.FechaRegistro = DateTime.Now.ToString("yyyy-MM-dd");

            await using (MySqlConnection con = _dbConnection.CreateConnection()) {

                await con.OpenAsync();

                try {

                    response = await con.ExecuteAsync(QueryUpdateEmpleado,
                        new {
                            empleado.Documento,
                            empleado.Nombre,
                            empleado.Apellido,
                            empleado.Num_Telefono,
                            empleado.Correo,
                            empleado.Direccion,
                            empleado.NivelPermisos,
                            empleado.EsAdmin,
                            empleado.Contrasena,
                            empleado.Id_Persona,
                            empleado.IdEmpleado
                        });

                }
                catch (Exception) {
                    throw;
                }
                finally {
                    con.Close();
                }

            }

            if (response == 0) {
                return false;
            }

            return true;
        }

        public async Task<bool> Delete(int id) {

            int response;

            string FechaBorrado = DateTime.Now.ToString("yyyy-MM-dd");

            await using (MySqlConnection con = _dbConnection.CreateConnection()) {

                await con.OpenAsync();

                try {
                    response = await con.ExecuteAsync(QueryDeleteEmpleado, new { FechaBorrado , Id_Persona = id });
                }
                catch (Exception) {
                    throw;
                }
                finally {
                    con.Close();
                }

            }

            if (response == 0) {
                return false;
            }

            return true;

        }

    }

}
