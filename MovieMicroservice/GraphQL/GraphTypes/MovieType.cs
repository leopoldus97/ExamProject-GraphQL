using GraphQL.Types;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.GraphQL.GraphTypes
{
    public class MovieType : ObjectGraphType<Movie> {
        public MovieType()
        {
            Name = nameof(Movie);
            Description = "A Movie from the collection";
            
            Field(x => x.Id).Description("Movie ID");
            Field(x => x.Title).Description("Movie Title");
            Field(x => x.Duration).Description("Movie Duraton in minutes");
            Field(x => x.ReleaseDate).Description("Movie Release date");
            Field(name: "Genre", description: "The genres selected for this movie", type: typeof(ListGraphType<MovieGenreType>), resolve: mg => mg.Source.Genre);
            Field(name: "Ratings", description: "The ratings for this movie", type: typeof(ListGraphType<MovieRatingType>), resolve: mr => mr.Source.Ratings);
        }
    }
}