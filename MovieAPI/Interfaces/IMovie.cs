using MovieAPI.Models;

namespace Interfaces
{
    public interface IMovie
    {
        Task<List<Movie>> GetAllMovieAsync();
        public Task<Movie?> GetMovieByIDAsync(int id);
        public Task<Movie> CreateMovieAsync(Movie movie);
        public Task<Movie?> UpdateMovieAsync(int id, Movie movie);
        public Task<Movie?> DeleteMovieAsync(int id);

    }
}