
using Microsoft.AspNetCore.Mvc;
using Interfaces;
using MovieAPI.Mapper;
using MovieAPI.DTO.movie;
using MovieAPI.Helpers;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovie _movie;
        public MovieController(IMovie movie)
        {
            this._movie = movie;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllMovie([FromQuery] MovieQueryObject query)
        {
            var movies = await _movie.GetAllMovieAsync(query);
            var moiveDto = movies.Select(movie => MovieMapper.ToMovieDto(movie));
            return Ok(moiveDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMovieByID([FromRoute] int id)
        {
            var foundMovie = await _movie.GetMovieByIDAsync(id);
            if(foundMovie == null)
                return NotFound($"Movie with ID {id} not found");
            
            var foundMovieDto = MovieMapper.ToMovieDtoByID(foundMovie);
            return Ok(foundMovieDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] movieDtoCreate movieDto)
        {
            var movie = MovieMapper.ToMovieModel(movieDto);
            var createdMovie = await _movie.CreateMovieAsync(movie);
            return Ok(MovieMapper.ToMovieDto(createdMovie));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMovie([FromRoute] int id, [FromBody] movieDtoUpdate movieDto)
        {
            var Movie = MovieMapper.ToMovieModel(movieDto);
            var updatedMovie = await _movie.UpdateMovieAsync(id, Movie);

            if(updatedMovie == null)
                return NotFound($"Movie with ID {id} not found");

            var updatedMovieDto = MovieMapper.ToMovieDto(updatedMovie);
            return Ok(updatedMovieDto);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var deletedMovie = await _movie.DeleteMovieAsync(id);

            if(deletedMovie == null)
                return NotFound($"Movie with ID {id} not found");;

            return Ok(MovieMapper.ToMovieDto(deletedMovie));
        }
    
    }
}