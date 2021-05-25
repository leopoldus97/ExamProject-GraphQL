using System;
using System.Collections.Generic;

namespace MovieMicroservice.Core.Entity
{
    public class Movie
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; }
        public IEnumerable<MovieGenre> Genre { get; set; }
        public IEnumerable<MovieRating> Ratings { get; set; }
    }
}