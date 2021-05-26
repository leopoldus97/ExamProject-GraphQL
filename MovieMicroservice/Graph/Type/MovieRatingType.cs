using GraphQL.Types;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Graph.Type {
    public class MovieRatingType : ObjectGraphType<MovieRating> {
        public MovieRatingType()
        {
            Name = nameof(MovieRating);
            Description = "A movie rating done by a user";

            Field(x => x.MovieId, type: typeof(IntGraphType)).Description("Movie Foreign key");
            Field(x => x.UserId, type: typeof(IntGraphType)).Description("User Foreign key");
            Field(x => x.Rating, type: typeof(FloatGraphType)).Description("Rating of the movie");
            Field(name: "RatedMovie", description: "Movie that's been rated", type: typeof(MovieType), resolve: m => m.Source.RatedMovie);
            Field(name: "RatedBy", description: "The user who rated the movie", type: typeof(UserType), resolve: u => u.Source.RatedBy);
        }
    }
}