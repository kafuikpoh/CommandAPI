using System.Collections.Generic;
using System.Threading.Tasks;
using CommandAPI.Models;

namespace CommandAPI.Data
{
    public interface ICommandAPIRepo
    {
        Task<IEnumerable<Command>> GetAllCommands();
        Task<Command> GetCommandById(int id);
        Task<Command> GetCommandByCommand(string cmdLine);
        Task<int> GetLastInsertedId();
        Task CreateCommand(Command cmd);
        Task UpdateCommand(int id, Command cmd);
        Task DeleteCommand(int id);
    }
}