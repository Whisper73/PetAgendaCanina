using Dapper;
using Models;
using MySql.Data.MySqlClient;
using PetAgenda.Models;

namespace Abstractions.Repositories{

    public class ClienteRepository : IClienteRepository {

        private readonly string QueryGetClienteById = @"
            SELECT 
                c.Id AS IdCliente,
                c.Id_Persona,
                c.Nivel,
                p.Id,
                p.Documento,
                p.Nombre,
                p.Apellido,
                p.Num_Telefono,
                p.Correo,
                p.Direccion,
                p.FechaRegistro
            FROM 
                Cliente c
            INNER JOIN 
                Persona p ON c.Id_Persona = p.Id          
            WHERE P.EstaBorrado = false
            AND c.Id = @id;";

        private readonly string QueryGetAllClientes = @"
            SELECT 
                c.Id AS IdCliente,
                c.Id_Persona,
                c.Nivel,
                p.Id,
                p.Documento,
                p.Nombre,
                p.Apellido,
                p.Num_Telefono,
                p.Correo,
                p.Direccion,
                p.FechaRegistro
            FROM 
                Cliente c
            INNER JOIN 
                Persona p ON c.Id_Persona = p.Id          
            WHERE P.EstaBorrado = false;";

        private readonly string QueryInsertPersona = @"
            INSERT INTO Persona (Documento, Nombre, Apellido, Num_Telefono, Correo, Direccion, FechaRegistro)
            VALUES (@Documento, @Nombre, @Apellido, @Num_Telefono, @Correo, @Direccion, @FechaRegistro); 
            SELECT LAST_INSERT_ID();";

        private readonly string QueryInsertCliente = @"
            INSERT INTO Cliente (Id_Persona, Nivel)
            VALUES (@Id_Persona, @Nivel);";

        private readonly string QueryUpdateCliente = @"
            UPDATE Persona
            SET Documento = @Documento,
                Nombre = @Nombre,
                Apellido = @Apellido,
                Num_Telefono = @Num_Telefono,
                Correo = @Correo,
                Direccion = @Direccion
            WHERE Id = @Id_Persona;

            UPDATE Cliente
            SET Nivel = @Nivel
            WHERE Id = @IdCliente;";

        private readonly string QueryDeleteCliente = @"
            UPDATE Persona
            SET EstaBorrado = true,
                FechaBorrado = @FechaBorrado
            WHERE Persona.Id = @Id_Persona;";


        private readonly DataBaseConnection _dbConnection;

        public ClienteRepository(DataBaseConnection dbConnection) {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Cliente>?> GetAll() {

            IEnumerable<Cliente>? response = null;

            await using (MySqlConnection con = _dbConnection.CreateConnection()) {

                await con.OpenAsync();

                try {
                    response = await con.QueryAsync<Cliente>(QueryGetAllClientes, new { });
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

        public async Task<Cliente?> GetById(int id) {

            Cliente? response = null;

            await using (MySqlConnection con = _dbConnection.CreateConnection()) {

                await con.OpenAsync();

                try {
                    response = await con.QueryFirstOrDefaultAsync<Cliente>(QueryGetClienteById, new { Id = id });
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

        public async Task<bool> Insert(Cliente cliente) {

            if (cliente == null) {
                throw new ArgumentNullException(nameof(cliente));
            }

            int personaId;

            cliente.FechaRegistro = DateTime.Now.ToString("yyyy-MM-dd");

            try {
                await using (MySqlConnection con = _dbConnection.CreateConnection()) {
                    await con.OpenAsync();

                    using (MySqlTransaction transaction = con.BeginTransaction()) {

                        try {
                            personaId = await con.ExecuteScalarAsync<int>(QueryInsertPersona,
                                new {
                                    cliente.Documento,
                                    cliente.Nombre,
                                    cliente.Apellido,
                                    cliente.Num_Telefono,
                                    cliente.Correo,
                                    cliente.Direccion,
                                    cliente.FechaRegistro,
                                }, transaction);

                            await con.ExecuteAsync(QueryInsertCliente, new {
                                Id_Persona = personaId,
                                cliente.Nivel,                             
                            }, transaction);

                            transaction.Commit();
                            return true;
                        }
                        catch (Exception) {
                            transaction.Rollback();
                            return false;
                        }
                        finally {
                            await con.CloseAsync();
                        }

                    }

                }

            }
            catch (Exception ex) {
                throw new Exception("Error al insertar datos", ex);
            }

        }

        public async Task<bool> Update(Cliente cliente) {

            int response;

            if (cliente == null) {
                throw new ArgumentNullException(nameof(cliente));
            }

            await using (MySqlConnection con = _dbConnection.CreateConnection()) {

                await con.OpenAsync();

                try {

                    response = await con.ExecuteAsync(QueryUpdateCliente,
                        new {
                            cliente.Documento,
                            cliente.Nombre,
                            cliente.Apellido,
                            cliente.Num_Telefono,
                            cliente.Correo,
                            cliente.Nivel,
                            cliente.Direccion,
                            cliente.Id_Persona,
                            cliente.IdCliente
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
                    response = await con.ExecuteAsync(QueryDeleteCliente, new { FechaBorrado, Id_Persona = id });
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
