using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Models;

namespace MovieAPI.Repositores
{
    public class MovieRepository : IMovie
    {
        private readonly ApplicationDBContext _context;

        public MovieRepository(ApplicationDBContext context)
        {
            this._context = context;

        }

        public async Task<Movie> CreateMovieAsync(Movie movie)
        {
            if (movie == null)
                throw new ArgumentNullException(nameof(movie));

            foreach (var actorID in movie.ActorsList)
            {
                var foundActor = await _context.Actor.FirstOrDefaultAsync(a=> a.Id == actorID);
                if(foundActor == null)
                    throw new ArgumentException($"Actor with ID {actorID} not found");
            }

            await _context.Movie.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie?> DeleteMovieAsync(int id)
        {
            var foundMoive = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);

            if (foundMoive == null)
                return null;

            _context.Movie.Remove(foundMoive);
            await _context.SaveChangesAsync();
            return foundMoive;
        }

        public async Task<List<Movie>> GetAllMovieAsync()
        {
            return await _context.Movie.Include(m => m.Actors).ToListAsync();
            
        }

        public async Task<Movie?> GetMovieByIDAsync(int id)
        {
            var foundMoive = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);

            if (foundMoive == null)
                return null;

            return foundMoive;
        }

        public async Task<Movie?> UpdateMovieAsync(int id, Movie movie)
        {
            var foundMoive = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);

            if (foundMoive == null)
                return null;

            foreach (var actorID in movie.ActorsList)
            {
                var foundActor = await _context.Actor.FirstOrDefaultAsync(a=> a.Id == actorID);
                if(foundActor == null)
                    throw new ArgumentException($"Actor with ID {actorID} not found");
            }

            foundMoive.Name = movie.Name;
            foundMoive.ReleasedYear = movie.ReleasedYear;
            foundMoive.IsMyFavourite = movie.IsMyFavourite;
            foundMoive.ActorsList = movie.ActorsList;

            await _context.SaveChangesAsync();
            return foundMoive;
        }
    }
}