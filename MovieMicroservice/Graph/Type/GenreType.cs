using GraphQL.Types;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Graph.Type {
    public class GenreType : ObjectGraphType<Genre> {
        public GenreType()
        {
            Name = nameof(Genre);
            Description = "A genre in the collection";
            
            Field(x => x.Id, type: typeof(IntGraphType)).Description("Genre ID");
            Field(x => x.Name, type: typeof(StringGraphType)).Description("Genre Name");
            Field(name: "Genre", description: "List of movies having this genre", type: typeof(ListGraphType<MovieGenreType>), resolve: g => g.Source.Movies);
        }
    }
}