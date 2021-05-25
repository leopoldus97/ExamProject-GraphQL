using GraphQL.Types;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.GraphQL.GraphTypes {
    public sealed class MovieInputType : InputObjectGraphType<Movie> {
        public MovieInputType()
        {
            Name = "MovieInput";
            Description = "A movie of the movie collection";

            Field(m => m.ReleaseDate).Description("The movie's release date");
            Field(m => m.Duration).Description("Duration of the movie in minutes");
        }
    }
}