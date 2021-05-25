using GraphQL.Types;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.GraphQL.GraphTypes {
    public class GenreType : ObjectGraphType<Genre> {
        public GenreType()
        {
            Name = nameof(Genre);
            Description = "A genre in the collection";
            
            Field(x => x.Id).Description("Genre ID");
            Field(x => x.Name).Description("Genre Name");
            Field(name: "Movies with this genre", description: "List of movies having this genre", type: typeof(ListGraphType<MovieGenreType>), resolve: g => g.Source.Movies);
        }
    }
}