using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Data;
using MovieAPI.DTO.actor;
using MovieAPI.Interfaces;
using MovieAPI.Mapper;
using MovieAPI.Models;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorController : ControllerBase
    {
        private readonly IActor _actor;

        public ActorController(IActor actor)
        {
            this._actor = actor;
        }


        [HttpGet]

        public async Task<IActionResult> GetAllActors()
        {
            var actorList = await _actor.GetActorsAync();
            return Ok(actorList);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetActorByID([FromRoute] int id)
        {
            var foundActor = await _actor.GetActorByIDAsync(id);
            if(foundActor == null)
                return NotFound($"Actor with ID {id} not found");
            
            var foundActorDto = ActorMapper.ToActorDto(foundActor);
            return Ok(foundActorDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActor(actorDtoCreate actorDto){
            var actor = ActorMapper.ToActorModel(actorDto);
            var createdActor = await _actor.CreateActorAsync(actor);

            if(createdActor == null)
                return BadRequest();

            var createdActorDto = ActorMapper.ToActorDto(createdActor); 
            return Ok(createdActorDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActor([FromRoute] int id, [FromBody] actorDtoUpdate actorDto)
        {
            var actor = ActorMapper.ToActorModel(actorDto);
            var updatedActor = await _actor.UpdateActorAsync(id, actor);

            if(updatedActor == null)
                return NotFound($"Actor with ID {id} not found");

            var updatedActorDto = ActorMapper.ToActorDto(updatedActor);
            return Ok(updatedActorDto);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteActor(int id)
        {
            var deletedActor = await _actor.DeleteActorAsync(id);

            if(deletedActor == null)
                return NotFound($"Actor with ID {id} not found");;

            return Ok(ActorMapper.ToActorDto(deletedActor));
        }
    }
}