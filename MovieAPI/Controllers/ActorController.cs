using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.DTO.actor;
using MovieAPI.Helpers;
using MovieAPI.Interfaces;
using MovieAPI.Mapper;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ActorController : ControllerBase
    {
        private readonly IActor _actor;

        public ActorController(IActor actor)
        {
            this._actor = actor;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllActors([FromQuery] ActorQueryObject query)
        {
            var actorList = await _actor.GetActorsAync(query);
            return Ok(actorList);
        }

        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetActorByID([FromRoute] int id)
        {
            var foundActor = await _actor.GetActorByIDAsync(id);
            if (foundActor == null)
                return NotFound($"Actor with ID {id} not found");

            var foundActorDto = ActorMapper.ToActorDtoByID(foundActor);
            return Ok(foundActorDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateActor(actorDtoCreate actorDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();
                
            if (!string.IsNullOrEmpty(actorDto.Nationality))
                actorDto.Nationality = char.ToUpper(actorDto.Nationality[0]) + actorDto.Nationality.Substring(1);

            var actor = ActorMapper.ToActorModel(actorDto);
            var createdActor = await _actor.CreateActorAsync(actor);

            if (createdActor == null)
                return BadRequest();

            var createdActorDto = ActorMapper.ToActorDtoByID(createdActor);
            return Ok(createdActorDto);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateActor([FromRoute] int id, [FromBody] actorDtoUpdate actorDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var actor = ActorMapper.ToActorModel(actorDto);
            var updatedActor = await _actor.UpdateActorAsync(id, actor);

            if (updatedActor == null)
                return NotFound($"Actor with ID {id} not found");

            var updatedActorDto = ActorMapper.ToActorDtoByID(updatedActor);
            return Ok(updatedActorDto);
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteActor(int id)
        {
            var deletedActor = await _actor.DeleteActorAsync(id);

            if (deletedActor == null)
                return NotFound($"Actor with ID {id} not found"); ;

            return Ok(ActorMapper.ToActorDto(deletedActor));
        }
    }
}