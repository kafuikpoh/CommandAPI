using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CommandAPI.Data;
using CommandAPI.Models;
using AutoMapper;
using CommandAPI.DTOs;
using System;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandAPIRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandAPIRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetAllCommands()
        {
            var commandItems = await _repository.GetAllCommands();

            if (commandItems == null)
            {
                return NotFound();
            }

            var cmdRead = _mapper.Map<IEnumerable<CommandReadDto>>(commandItems);

            return Ok(cmdRead);
        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public async Task<ActionResult<CommandReadDto>> GetCommandById(int id)
        {
            var commandItem = await _repository.GetCommandById(id);

            if (commandItem == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDto>(commandItem));
        }

        [HttpPost]
        public async Task<ActionResult<CommandReadDto>> CreateCommand(CommandCreateDto commandCreate)
        {

            var lastId = await _repository.GetLastInsertedId();

            var commandModel = _mapper.Map<Command>(commandCreate);
            await _repository.CreateCommand(commandModel);

            var justInsertedId = await _repository.GetLastInsertedId();

            if (lastId == justInsertedId)
            {
                return NotFound();

            }

            var newCmd = await _repository.GetCommandById(justInsertedId);
            var cmdReadDto = _mapper.Map<CommandReadDto>(newCmd);

            return CreatedAtRoute(nameof(GetCommandById), new { Id = cmdReadDto.Id }, cmdReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCommand(int id, CommandUpdateDto cmdUpdate)
        {

            var cmdFromDb = await _repository.GetCommandById(id);

            if (cmdFromDb == null)
            {
                return NotFound();
            }

            await _repository.UpdateCommand(id, _mapper.Map<Command>(cmdUpdate));

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