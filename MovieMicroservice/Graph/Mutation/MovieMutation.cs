using System;
using GraphQL;
using GraphQL.Types;
using MovieMicroservice.Core.DomainServices;
using Microsoft.Extensions.DependencyInjection;
using MovieMicroservice.Core.Entity;
using MovieMicroservice.Graph.Type;

namespace MovieMicroservice.Graph.Mutation {
    public class MovieMutation : ObjectGraphType<object> {
        public MovieMutation(IServiceProvider sp)
        {
            Name = "MovieMutations";
            Description = "The base mutation for all the entities in our object graph.";

            FieldAsync<MovieType, Movie>(
                "addMovie",
                "Add a movie to the DB",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> {
                        Name = "title",
                        Description = "Title of the movie"
                    },
                    new QueryArgument<DateGraphType> {
                        Name = "releaseDate",
                        Description = "Release date of the movie",
                        DefaultValue = default(DateTime)
                    },
                    new QueryArgument<NonNullGraphType<IntGraphType>> {
                        Name = "duration",
                        Description = "Movie duration in minutes"
                    }
                ),
                context => {
                    var repository = (IMovieRepo)sp.GetRequiredService(typeof(IMovieRepo));
                    var title = context.GetArgument<string>("title");
                    var duration = context.GetArgument<int>("duration");
                    var releaseDate = context.GetArgument<DateTime>("releaseDate");

                    Movie movie = new Movie {
                        Title = title,
                        Duration = duration,
                        ReleaseDate = releaseDate
                    };

                    return repository.Create(movie);
                }
            );

            FieldAsync<UserType, User>(
                "addUser",
                "Add a user to the DB",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> {
                        Name = "username",
                        Description = "Username for the user"
                    },
                    new QueryArgument<NonNullGraphType<DateGraphType>> {
                        Name = "registrationDate",
                        Description = "Registration date of the user"
                    }
                ),
                context => {
                    var repository = (IUserRepo)sp.GetRequiredService(typeof(IUserRepo));
                    var username = context.GetArgument<string>("username");
                    var registrationDate = context.GetArgument<DateTime>("registrationDate");

                    User user = new User {
                        Username = username,
                        RegistrationDate = registrationDate
                    };

                    return repository.Create(user);
                }
            );

            FieldAsync<GenreType, Genre>(
                "addGenre",
                "Add a genre to the DB",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> {
                        Name = "name",
                        Description = "Name of the genre"
                    }
                ),
                context => {
                    var repository = (IGenreRepo)sp.GetRequiredService(typeof(IGenreRepo));
                    var name = context.GetArgument<string>("name");

                    Genre genre = new Genre {
                        Name = name
                    };

                    return repository.Create(genre);
                }
            );

            FieldAsync<MovieType, Movie>(
                "updateMovie",
                "Update the selected movie",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> {
                        Name = "id",
                        Description = "Id of the movie"
                    },
                    new QueryArgument<NonNullGraphType<StringGraphType>> {
                        Name = "title",
                        Description = "Title of the movie"
                    },
                    new QueryArgument<DateGraphType> {
                        Name = "releaseDate",
                        Description = "Release date of the movie",
                        DefaultValue = default(DateTime)
                    },
                    new QueryArgument<NonNullGraphType<IntGraphType>> {
                        Name = "duration",
                        Description = "Movie duration in minutes"
                    }
                ),
                context => {
                    var repository = (IMovieRepo)sp.GetRequiredService(typeof(IMovieRepo));
                    var id = context.GetArgument<int>("id");
                    var title = context.GetArgument<string>("title");
                    var duration = context.GetArgument<int>("duration");
                    var releaseDate = context.GetArgument<DateTime>("releaseDate");

                    Movie movie = new Movie {
                        Id = id,
                        Title = title,
                        Duration = duration,
                        ReleaseDate = releaseDate
                    };

                    return repository.Update(id, movie);
                }
            );

            FieldAsync<UserType, User>(
                "updateUser",
                "Update the selected user",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> {
                        Name = "id",
                        Description = "Id of the user"
                    },
                    new QueryArgument<NonNullGraphType<StringGraphType>> {
                        Name = "username",
                        Description = "Username for the user"
                    },
                    new QueryArgument<NonNullGraphType<DateGraphType>> {
                        Name = "registrationDate",
                        Description = "Registration date of the user"
                    }
                ),
                context => {
                    var repository = (IUserRepo)sp.GetRequiredService(typeof(IUserRepo));
                    var id = context.GetArgument<int>("id");
                    var username = context.GetArgument<string>("username");
                    var registrationDate = context.GetArgument<DateTime>("registrationDate");

                    User user = new User {
                        Id = id,
                        Username = username,
                        RegistrationDate = registrationDate
                    };

                    return repository.Update(id, user);
                }
            );

            FieldAsync<GenreType, Genre>(
                "updateGenre",
                "Update the selected genre",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> {
                        Name = "id",
                        Description = "Id of the genre"
                    },
                    new QueryArgument<NonNullGraphType<StringGraphType>> {
                        Name = "name",
                        Description = "Name of the genre"
                    }
                ),
                context => {
                    var repository = (IGenreRepo)sp.GetRequiredService(typeof(IGenreRepo));
                    var id = context.GetArgument<int>("id");
                    var name = context.GetArgument<string>("name");

                    Genre genre = new Genre {
                        Id = id,
                        Name = name
                    };

                    return repository.Update(id, genre);
                }
            );

            FieldAsync<MovieType, Movie>(
                "deleteMovie",
                "Delete the selected movie",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> {
                        Name = "id",
                        Description = "Id of the movie"
                    }
                ),
                context => {
                    var repository = (IMovieRepo)sp.GetRequiredService(typeof(IMovieRepo));
                    var id = context.GetArgument<int>("id");

                    return repository.Delete(id);
                }
            );

            FieldAsync<UserType, User>(
                "deleteUser",
                "Delete the selected user",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> {
                        Name = "id",
                        Description = "Id of the user"
                    }
                ),
                context => {
                    var repository = (IUserRepo)sp.GetRequiredService(typeof(IUserRepo));
                    var id = context.GetArgument<int>("id");

                    return repository.Delete(id);
                }
            );

            FieldAsync<GenreType, Genre>(
                "deleteGenre",
                "Delete the selected genre",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> {
                        Name = "id",
                        Description = "Id of the genre"
                    }
                ),
                context => {
                    var repository = (IGenreRepo)sp.GetRequiredService(typeof(IGenreRepo));
                    var id = context.GetArgument<int>("id");

                    return repository.Delete(id);
                }
            );
        }
    }
}