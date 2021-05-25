using System.Collections.Generic;

namespace MovieMicroservice.Core.Entity
{
    public class Genre
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<MovieGenre> Movies { get; set; }
    }
}