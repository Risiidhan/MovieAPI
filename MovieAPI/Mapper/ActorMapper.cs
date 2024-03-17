using MovieAPI.DTO.actor;
using MovieAPI.Models;

namespace MovieAPI.Mapper
{
    public static class ActorMapper
    {
        public static actorDto ToActorDto(Actor actor) => new actorDto
        {
            Id = actor.Id,
            Name = actor.Name,
            Age = actor.Age,
            Gender = actor.Gender,
            Nationality = actor.Nationality
        };

        public static Actor ToActorModel(actorDtoCreate actorDto) => new Actor
        {
            Name = actorDto.Name,
            Age = actorDto.Age,
            Gender = actorDto.Gender,
            Nationality = actorDto.Nationality
        };

       public static Actor ToActorModel(actorDtoUpdate actorDto) => new Actor
        {
            Name = actorDto.Name,
            Age = actorDto.Age,
            Gender = actorDto.Gender,
            Nationality = actorDto.Nationality
        };
    }
}