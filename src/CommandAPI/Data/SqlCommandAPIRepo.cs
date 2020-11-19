

using System.Collections.Generic;
using System.Threading.Tasks;
using CommandAPI.Access;
using CommandAPI.Models;
using Microsoft.Extensions.Configuration;

namespace CommandAPI.Data
{
    public class SqlCommandAPIRepo : ICommandAPIRepo
    {

        private readonly IConfiguration _config;
        private IDataAccess _data;

        public SqlCommandAPIRepo(IConfiguration config, IDataAccess data)
        {
            _config = config;
            _data = data;
        }

        public async Task CreateCommand(Command cmd)
        {
            string sql = "insert into commands (HowTo, CommandLine, Platform) values (@HowTo, @CommandLine, @Platform);";
            await _data.SaveData(sql, new { HowTo = cmd.HowTo, CommandLine = cmd.CommandLine, Platform = cmd.Platform }, _config.GetConnectionString("default"));
        }

        public async Task DeleteCommand(int id)
        {
            string sql = "delete from commands where Id = @id";
            await _data.SaveData(sql, new { Id = id }, _config.GetConnectionString("default"));
        }

        public async Task<IEnumerable<Command>> GetAllCommands()
        {
            string sql = "select * from commands";
            var commands = await _data.LoadData<Command, dynamic>(sql, new { }, _config.GetConnectionString("default"));

            return commands;
        }

        public async Task<Command> GetCommandByCommand(string cmdLine)
        {
            string sql = "";
            var command = await _data.LoadDataByParam<Command, dynamic>(sql, new { CommandLine = cmdLine }, _config.GetConnectionString("default"));

            return command;
        }

        public async Task<Command> GetCommandById(int id)
        {
            string sql = "select from commands where Id = @id";
            var command = await _data.LoadDataByParam<Command, dynamic>(sql, new { Id = id }, _config.GetConnectionString("default"));

            return command;
        }

        public async Task UpdateCommand(int id, Command cmd)
        {
            string sql = "update commands set HowTo = @HowTo, CommandLine = @CommandLine, Platform = @Platform where Id = @id";
            await _data.SaveData(sql, new { Id = id, HowTo = cmd.HowTo, CommandLine = cmd.CommandLine, Platform = cmd.Platform }, _config.GetConnectionString("default"));
        }
    }
}