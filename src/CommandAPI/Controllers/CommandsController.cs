using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CommandAPI.Data;
using CommandAPI.Models;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandAPIRepo _repository;

        public CommandsController(ICommandAPIRepo repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Command>>> GetAllCommands()
        {
            var commandItems = await _repository.GetAllCommands();

            if (commandItems == null)
            {
                return NotFound();
            }

            return Ok(commandItems);
        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public async Task<ActionResult<Command>> GetCommandById(int id)
        {
            var commandItem = await _repository.GetCommandById(id);

            if (commandItem == null)
            {
                return NotFound();
            }

            return Ok(commandItem);
        }

        [HttpPost]
        public async Task<ActionResult<Command>> CreateCommand(Command command)
        {
            var lastId = await _repository.GetLastInsertedId();

            await _repository.CreateCommand(command);

            var justInsertedId = await _repository.GetLastInsertedId();

            if (lastId == justInsertedId)
            {
                return NotFound();
            }

            var newCmd = await _repository.GetCommandById(justInsertedId);

            return CreatedAtRoute(nameof(GetCommandById), new { Id = newCmd.Id }, newCmd);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCommand(int id, Command cmd)
        {
            var cmdFromDb = await _repository.GetCommandById(id);

            if (cmdFromDb == null)
            {
                return NotFound();
            }

            await _repository.UpdateCommand(id, cmd);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCommand(int id)
        {
            var cmdFromDb = await _repository.GetCommandById(id);

            if (cmdFromDb == null)
            {
                return NotFound();
            }

            await _repository.DeleteCommand(id);

            return NoContent();
        }

    }

}