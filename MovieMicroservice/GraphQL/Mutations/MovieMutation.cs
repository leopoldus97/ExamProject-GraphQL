using GraphQL;
using GraphQL.Types;
using MovieMicroservice.Core.DomainServices;
using MovieMicroservice.Core.Entity;
using MovieMicroservice.GraphQL.GraphTypes;

namespace MovieMicroservice.GraphQL.Mutations {
    public class MovieMutation : ObjectGraphType<object> {
        public MovieMutation(IMovieRepo repository)
        {
            Name = "MovieMutations";
            Description = "The base mutation for all the entities in our object graph.";

            FieldAsync<MovieType, Movie>(
                "addMovie",
                "Add a movie to the collection",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> {
                        Name = "title",
                        Description = "Title of the movie"
                    },
                    new QueryArgument<NonNullGraphType<MovieInputType>> {
                        Name = "movie",
                        Description = "Movie to be added to the DB"
                    }
                ),
                context => {
                    var title = context.GetArgument<string>("title");
                    var movie = context.GetArgument<Movie>("movie");
                    return repository.Create(movie);
                }
            );
        }
    }
}