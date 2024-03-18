using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Helpers;
using MovieAPI.Interfaces;
using MovieAPI.Models;

namespace MovieAPI.Repositores
{
    public class ActorRepository : IActor
    {
        private readonly ApplicationDBContext _context;
        public ActorRepository(ApplicationDBContext context)
        {
            this._context = context;

        }
        public async Task<Actor> CreateActorAsync(Actor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            await _context.Actor.AddAsync(actor);
            await _context.SaveChangesAsync();
            return actor;
        }

        public async Task<Actor?> DeleteActorAsync(int id)
        {
            var foundActor = await _context.Actor.FirstOrDefaultAsync(a => a.Id == id);
            if (foundActor == null)
                return null;

            _context.Actor.Remove(foundActor);
            await _context.SaveChangesAsync();
            return foundActor;
        }

        public async Task<Actor?> GetActorByIDAsync(int id)
        {
            var foundActor = await _context.Actor.FirstOrDefaultAsync(a => a.Id == id);
            if (foundActor == null)
                return null;

            return foundActor;
        }

        public async Task<List<Actor>> GetActorsAync(ActorQueryObject query)
        {

            var actors = _context.Actor.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
                actors = actors.Where(a => a.Name.Contains(query.Name));

            if (!string.IsNullOrWhiteSpace(query.Nationality))
                actors = actors.Where(a => a.Nationality != null && a.Nationality.Contains(query.Nationality));

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    actors = query.IsDescending ? actors.OrderByDescending(a => a.Name) : actors.OrderBy(a => a.Name);

                if (query.SortBy.Equals("Nationality", StringComparison.OrdinalIgnoreCase))
                    actors = query.IsDescending ? actors.OrderByDescending(a => a.Nationality) : actors.OrderBy(a => a.Nationality);


                if (query.SortBy.Equals("Age", StringComparison.OrdinalIgnoreCase))
                    actors = query.IsDescending ? actors.OrderByDescending(a => a.Age) : actors.OrderBy(a => a.Age);

            }

            var skipNumber = (query.PageNumber-1) * query.PageSize;


            return await actors.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Actor?> UpdateActorAsync(int id, Actor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));


            var foundActor = await _context.Actor.FirstOrDefaultAsync(a => a.Id == id);
            if (foundActor == null)
                return null;

            foundActor.Name = actor.Name;
            foundActor.Age = actor.Age;
            foundActor.Gender = actor.Gender;
            foundActor.Nationality = actor.Nationality;

            await _context.SaveChangesAsync();
            return foundActor;
        }


    }
}