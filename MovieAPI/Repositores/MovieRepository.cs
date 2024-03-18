using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Helpers;
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

            var foundActor = await _context.Actor.FirstOrDefaultAsync(a => a.Id == movie.LeadActorID);
            if (foundActor == null)
                throw new ArgumentException($"Actor with ID {movie.LeadActorID} not found");

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

        public async Task<List<Movie>> GetAllMovieAsync(MovieQueryObject query)
        {
            
            var movies = _context.Movie.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
                movies = movies.Where(a => a.Name.Contains(query.Name));

            if (query.ReleasedYear>1800)
                movies = movies.Where(m => m.ReleasedYear.HasValue && m.ReleasedYear.Value == query.ReleasedYear);

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    movies = query.IsDescending ? movies.OrderByDescending(m => m.Name) : movies.OrderBy(m => m.Name);

                if (query.SortBy.Equals("ReleasedYear", StringComparison.OrdinalIgnoreCase))
                    movies = query.IsDescending ? movies.OrderByDescending(m => m.ReleasedYear) : movies.OrderBy(a => a.ReleasedYear);
            }

            var skipNumber = (query.PageNumber-1) * query.PageSize;
            return await movies.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Movie?> GetMovieByIDAsync(int id)
        {
            var foundMoive = await _context.Movie.Include(m => m.LeadActor).FirstOrDefaultAsync(m => m.Id == id);

            if (foundMoive == null)
                return null;

            return foundMoive;
        }

        public async Task<Movie?> UpdateMovieAsync(int id, Movie movie)
        {
            var foundMoive = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);

            if (foundMoive == null)
                return null;

            var foundActor = await _context.Actor.FirstOrDefaultAsync(a => a.Id == movie.LeadActorID);
            if (foundActor == null)
                throw new ArgumentException($"Actor with ID {movie.LeadActorID} not found");

            foundMoive.Name = movie.Name;
            foundMoive.ReleasedYear = movie.ReleasedYear;
            foundMoive.IsMyFavourite = movie.IsMyFavourite;
            foundMoive.LeadActorID = movie.LeadActorID;

            await _context.SaveChangesAsync();
            return foundMoive;
        }
    }
}