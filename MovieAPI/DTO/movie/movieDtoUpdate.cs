using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieAPI.Models;

namespace MovieAPI.DTO.movie
{
    public class movieDtoUpdate
    {
        public required string Name { get; set; }
        public int? ReleasedYear { get; set; }
        public bool? IsMyFavourite { get; set; }
         public int LeadActorID { get; set; }

    }
}