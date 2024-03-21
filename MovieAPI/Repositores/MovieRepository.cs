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

        public async Task<Movie> CreateMovieAsync(Movie movie, List<int> actorIds)
        {
            if (movie == null)
                throw new ArgumentNullException(nameof(movie));


            var foundActor = await _context.Actor.Where(a => actorIds.Contains(a.Id)).ToListAsync();
            if (foundActor.Count != actorIds.Count)
                throw new ArgumentException($"One or More Actor with ID not found");

            movie.MovieActors = foundActor.Select(actor => new MovieActor { Actor = actor }).ToList();
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

            var movies = _context.Movie.Include(m => m.MovieActors)
                .ThenInclude(ma => ma.Actor).AsQueryable();
                
            if (!string.IsNullOrWhiteSpace(query.Name))
                movies = movies.Where(a => a.Name.Contains(query.Name));

            if (query.ReleasedYear > 1800)
                movies = movies.Where(m => m.ReleasedYear.HasValue && m.ReleasedYear.Value == query.ReleasedYear);

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    movies = query.IsDescending ? movies.OrderByDescending(m => m.Name) : movies.OrderBy(m => m.Name);

                if (query.SortBy.Equals("ReleasedYear", StringComparison.OrdinalIgnoreCase))
                    movies = query.IsDescending ? movies.OrderByDescending(m => m.ReleasedYear) : movies.OrderBy(a => a.ReleasedYear);
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await movies.Skip(skipNumber).Take(query.PageSize).ToListAsync();

        }

        public async Task<Movie?> GetMovieByIDAsync(int id)
        {
            var foundMoive = await _context.Movie.Include(m => m.MovieActors).
            ThenInclude(ma => ma.Actor).FirstOrDefaultAsync(m => m.Id == id);

            if (foundMoive == null)
                return null;

            return foundMoive;
        }

        public async Task<Movie?> UpdateMovieAsync(int id, Movie movie, List<int> actorIds)
        {
            var foundMoive = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);

            if (foundMoive == null)
                return null;

            var foundActors = await _context.Actor.Where(a => actorIds.Contains(a.Id)).ToListAsync();
            if (foundActors.Count != actorIds.Count)
                throw new ArgumentException($"One or More Actor with ID not found");

            // Clear existing associations
            foundMoive.MovieActors.Clear();
            // Create new associations based on provided actorIds
            foreach (var actorId in actorIds)
            {
                var actor = foundActors.FirstOrDefault(a => a.Id == actorId);
                if (actor != null)
                    foundMoive.MovieActors.Add(new MovieActor { Actor = actor });
            }

            foundMoive.Name = movie.Name;
            foundMoive.ReleasedYear = movie.ReleasedYear;
            foundMoive.IsMyFavourite = movie.IsMyFavourite;

            await _context.SaveChangesAsync();
            return foundMoive;
        }
    }
}