
using MovieAPI.DTO.movie;
using MovieAPI.Models;

namespace MovieAPI.Mapper
{
    public static class MovieMapper
    {
        public static movieDto ToMovieDto(Movie movie) => new movieDto
        {
            Id = movie.Id,
            Name = movie.Name,
            ReleasedYear = movie.ReleasedYear,
            IsMyFavourite = movie.IsMyFavourite,
            Actors = movie.MovieActors.Select(ma => ActorMapper.ToActorDto(ma.Actor)).ToList()
        };

        public static movieDtoByID ToMovieDtoByID(Movie movie) => new movieDtoByID
        {
            Id = movie.Id,
            Name = movie.Name,
            ReleasedYear = movie.ReleasedYear,
            IsMyFavourite = movie.IsMyFavourite,
            Actors = movie.MovieActors.Select(ma => ActorMapper.ToActorDtoByID(ma.Actor)).ToList()
        };

        public static Movie ToMovieModel(movieDtoCreate movieCreateDto) => new Movie
        {
            Name = movieCreateDto.Name,
            ReleasedYear = movieCreateDto.ReleasedYear,
            IsMyFavourite = movieCreateDto.IsMyFavourite,
            // Since the movie creation DTO now contains a list of actor IDs,
            // you need to map these IDs to MovieActor entities.
            MovieActors = movieCreateDto.ActorIds.Select(actorId => new MovieActor { ActorID = actorId }).ToList()
        };

        public static Movie ToMovieModel(movieDtoUpdate movieUpdateDto) => new Movie
        {
            Name = movieUpdateDto.Name,
            ReleasedYear = movieUpdateDto.ReleasedYear,
            IsMyFavourite = movieUpdateDto.IsMyFavourite,
            MovieActors = movieUpdateDto.ActorIds.Select(actorId => new MovieActor { ActorID = actorId }).ToList()
        };
    }
}