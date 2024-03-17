using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            ActorsList = movie.ActorsList,
            Actors = movie.Actors.ToList()
        };

        public static Movie ToMovieModel(movieDtoCreate movieCreateDto) => new Movie
        {
            Name = movieCreateDto.Name,
            ReleasedYear = movieCreateDto.ReleasedYear,
            IsMyFavourite = movieCreateDto.IsMyFavourite,
            ActorsList = movieCreateDto.ActorsList
        };

        public static Movie ToMovieModel(movieDtoUpdate movieUpdateDto) => new Movie
        {
            Name = movieUpdateDto.Name,
            ReleasedYear = movieUpdateDto.ReleasedYear,
            IsMyFavourite = movieUpdateDto.IsMyFavourite,
            Actors = movieUpdateDto.Actors
        };
    }
}