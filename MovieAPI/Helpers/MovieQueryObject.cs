using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Helpers
{
    public class MovieQueryObject
    {
        public string Name { get; set; } = string.Empty;
        public int ReleasedYear { get; set; } = 2023;
        public string SortBy { get; set; } = string.Empty;
        public bool IsDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}