using MySql.Data.MySqlClient;

namespace PetAgenda.Models {
    public class DataBaseConnection {

        public string? ConnectionString { get; set; }

        public DataBaseConnection(string? connectionString) {
            ConnectionString = connectionString ?? throw new ArgumentNullException(connectionString);
        }

        public MySqlConnection CreateConnection() { 
            return new MySqlConnection(ConnectionString);
        }
    }
}
