namespace MovieMicroservice.Core.Entity
{
    public class MovieRating {
        public int MovieId { get; set; }
        public Movie RatedMovie { get; set; }
        public double Rating { get; set; }
        public int UserId { get; set; }
        public User RatedBy { get; set; }
    }
}