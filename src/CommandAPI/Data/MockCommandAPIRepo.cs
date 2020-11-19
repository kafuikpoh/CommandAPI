using System.Collections.Generic;
using System.Threading.Tasks;
using CommandAPI.Models;

namespace CommandAPI.Data
{
    public class MockCommandAPIRepo : ICommandAPIRepo
    {
        public Task CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteCommand(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Command>> GetAllCommands()
        {
            throw new System.NotImplementedException();
        }

        public Task<Command> GetCommandById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateCommand(int id, Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}