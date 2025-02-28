using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Data.Interface;

namespace TaskFlow.Data
{
    public static class DatabaseInitializer
    {

        public static void InitializeDatabase(IConnectionFactory connectionFactory)
        {
            using var connection = connectionFactory.CreateConnection();


            var tableExists = connection.ExecuteScalar<bool>("SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='Tasks';");

            if (!tableExists)
            {
                // Cria a tabela "Produtos"
                connection.Execute(@"
                    CREATE TABLE Tasks (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        CreationDate DATETIME NOT NULL
                    )");
            }
        }
    }
}
