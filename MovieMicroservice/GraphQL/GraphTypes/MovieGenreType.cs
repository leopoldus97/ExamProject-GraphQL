using GraphQL.Types;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.GraphQL.GraphTypes {
    public class MovieGenreType : ObjectGraphType<MovieGenre> {
        public MovieGenreType()
        {
            Name = nameof(MovieGenre);
            Description = "A genre assigned to a movie";

            Field(x => x.MovieId).Description("Movie Foreign key");
            Field(x => x.GenreId).Description("Genre Foreign key");
            Field(name: "Movie", description: "Movie for this genre", type: typeof(MovieType), resolve: m => m.Source.Movie);
            Field(name: "Genre", description: "Genre of the selected movie", type: typeof(GenreType), resolve: g => g.Source.Genre);
        }
    }
}