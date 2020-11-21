using System.Threading.Tasks;
using System.Collections.Generic;

namespace CommandAPI.Access
{
    public interface IDataAccess
    {
        Task<List<T>> LoadData<T, U>(string sql, U parameters, string connectionString);
        Task<int> LoadDataId<T, U>(string sql, U parameters, string connectionString);
        Task<T> LoadDataByParam<T, U>(string sql, U parameters, string connectionString);
        Task SaveData<T>(string sql, T parameters, string connectionString);
    }
}