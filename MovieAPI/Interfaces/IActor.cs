using MovieAPI.Helpers;
using MovieAPI.Models;

namespace MovieAPI.Interfaces
{
    public interface IActor
    {
        public Task<List<Actor>> GetActorsAync(ActorQueryObject query);
        public Task<Actor?> GetActorByIDAsync(int id);
        public Task<Actor> CreateActorAsync(Actor actor);
        public Task<Actor?> UpdateActorAsync(int id, Actor actor);
        public Task<Actor?> DeleteActorAsync(int id);


    }
}