using GraphQL.Types;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Graph.Type {
    public sealed class MovieInputType : InputObjectGraphType<Movie> {
        public MovieInputType()
        {
            Name = "MovieInput";
            Description = "A movie of the movie collection";

            Field(m => m.ReleaseDate, type: typeof(DateGraphType)).Description("The movie's release date");
            Field(m => m.Duration, type: typeof(IntGraphType)).Description("Duration of the movie in minutes");
        }
    }
}