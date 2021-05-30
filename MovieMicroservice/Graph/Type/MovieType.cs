using GraphQL.Types;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Graph.Type
{
    public class MovieType : ObjectGraphType<Movie> {
        public MovieType()
        {
            Name = nameof(Movie);
            Description = "A Movie from the collection";
            
            Field(x => x.Id, type: typeof(IntGraphType)).Description("Movie ID");
            Field(x => x.Title, type: typeof(StringGraphType)).Description("Movie Title");
            Field(x => x.Duration, type: typeof(IntGraphType)).Description("Movie Duraton in minutes");
            Field(x => x.ReleaseDate, type: typeof(DateGraphType)).Description("Movie Release date");
            Field(name: "Genre", description: "The genres selected for this movie", 
                type: typeof(ListGraphType<MovieGenreType>), resolve: mg => mg.Source.Genre);
            Field(name: "Ratings", description: "The ratings for this movie",
                type: typeof(ListGraphType<MovieRatingType>), resolve: mr => mr.Source.Ratings);
        }
    }
}