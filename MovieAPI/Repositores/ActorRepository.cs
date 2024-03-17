using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Interfaces;
using MovieAPI.Migrations;
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
            if(foundActor == null)
                return null;

            _context.Actor.Remove(foundActor);
            await _context.SaveChangesAsync();
            return foundActor;
        }

        public async Task<Actor?> GetActorByIDAsync(int id)
        {
            var foundActor = await _context.Actor.FirstOrDefaultAsync(a => a.Id == id);
            if(foundActor == null)
                return null;

            return foundActor;
        }

        public async Task<List<Actor>> GetActorsAync()
        {
            return await _context.Actor.ToListAsync();
        }

        public async Task<Actor?> UpdateActorAsync(int id, Actor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));


            var foundActor = await _context.Actor.FirstOrDefaultAsync(a => a.Id == id);
            if(foundActor == null)
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