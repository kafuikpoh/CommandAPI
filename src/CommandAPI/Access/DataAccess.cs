using Dapper;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CommandAPI.Access
{
    public class DataAccess : IDataAccess
    {
        public async Task<List<T>> LoadData<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                IEnumerable<T> rows = await connection.QueryAsync<T>(sql, parameters);

                return rows.AsList();
            }
        }

        public async Task<T> LoadDataByParam<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var row = await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);

                return row;
            }
        }

        public async Task<int> LoadDataId<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var latestId = await connection.QueryFirstAsync<int>(sql, parameters);

                return latestId;
            }
        }

        public Task SaveData<T>(string sql, T parameters, string connectionString)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                return connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}