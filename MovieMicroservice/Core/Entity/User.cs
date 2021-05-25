using System;
using System.Collections.Generic;

namespace MovieMicroservice.Core.Entity
{
    public class User {
        public int? Id { get; set; }
        public string Username { get; set; }
        public DateTime RegistrationDate { get; set; }
        public IEnumerable<MovieRating> RatedMovies { get; set; }
    }
}