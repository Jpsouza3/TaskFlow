using System.Data.SQLite;
using TaskFlow.Data.Interface;

namespace TaskFlow.Data
{
    public class SQLiteConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;

        public SQLiteConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SQLiteConnection CreateConnection()
        {
            var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
