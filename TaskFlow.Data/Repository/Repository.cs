using System.Data.SQLite;
using Dapper;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TaskFlow.Data.Interface;
using TaskFlow.Business.Interface;

namespace TaskFlow.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IConnectionFactory _connectionFactory;
        private readonly string _tableName;

        public Repository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _tableName = GetTableName();
        }

        public async Task Add(TEntity obj)
        {
            using var connection = _connectionFactory.CreateConnection();
            var properties = GetWritableProperties(excludeId: true);
            var columns = string.Join(", ", properties.Select(p => p.Name));
            var parameters = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            var sql = $"INSERT INTO {_tableName} ({columns}) VALUES ({parameters})";
            await connection.ExecuteAsync(sql, obj);
        }

        public async Task<TEntity?> GetById(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<TEntity>(
                $"SELECT * FROM {_tableName} WHERE Id = @id",
                new { id }
            );
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<TEntity>($"SELECT * FROM {_tableName}");
        }

        public async Task Update(TEntity obj)
        {
            using var connection = _connectionFactory.CreateConnection();
            var properties = GetWritableProperties(excludeId: false);
            var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));

            var sql = $"UPDATE {_tableName} SET {setClause} WHERE Id = @Id";
            await connection.ExecuteAsync(sql, obj);
        }

        public async Task Remove(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(
                $"DELETE FROM {_tableName} WHERE Id = @id",
                new { id }
            );
        }
        private string GetTableName()
        {
            var type = typeof(TEntity);
            var tableAttr = type.GetCustomAttribute<TableAttribute>();
            return tableAttr?.Name ?? type.Name + "s";
        }

        private IEnumerable<PropertyInfo> GetWritableProperties(bool excludeId)
        {
            var properties = typeof(TEntity).GetProperties()
                .Where(p => p.CanWrite && !IsKey(p));

            if (excludeId)
                properties = properties.Where(p => !IsKey(p));

            return properties;
        }

        private bool IsKey(PropertyInfo prop)
        {
            return prop.GetCustomAttribute<KeyAttribute>() != null
                   || prop.Name.Equals("Id", StringComparison.OrdinalIgnoreCase);
        }
    }
}