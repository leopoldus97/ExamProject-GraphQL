using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using GraphQL;
using GraphQL.Types;
using MovieMicroservice.Core.DomainServices;
using MovieMicroservice.Graph.Type;

namespace MovieMicroservice.Graph.Query
{
    public class MovieQuery : ObjectGraphType
    {
        public MovieQuery(IServiceProvider sp)
        {
            Name = "MovieQueries";
            Description = "The base query for all the entities in our object graph.";

            Field<ListGraphType<MovieType>>("movies",
            arguments: new QueryArguments(
                new QueryArgument<IntGraphType>
                {
                    Name = "id"
                },
                new QueryArgument<StringGraphType>
                {
                    Name = "title"
                },
                new QueryArgument<DateGraphType>
                {
                    Name = "releasedBefore"
                },
                new QueryArgument<DateGraphType>
                {
                    Name = "releasedAfter"
                },
                new QueryArgument<IntGraphType>
                {
                    Name = "duration"
                }
            ),
            resolve: context =>
            {
                var movieRepo = (IMovieRepo)sp.GetRequiredService(typeof(IMovieRepo));
                var query = movieRepo.ReadAll();

                var movieId = context.GetArgument<int?>("id");
                if (movieId.HasValue)
                    return query.Where(m => m.Id == movieId.Value);
                var movieTitle = context.GetArgument<string>("title");
                if (!string.IsNullOrEmpty(movieTitle))
                    return query.Where(m => m.Title.Contains(movieTitle));
                var releasedBefore = context.GetArgument<DateTime?>("releasedBefore");
                if (releasedBefore.HasValue)
                    return query.Where(m => m.ReleaseDate.CompareTo(releasedBefore.Value) < 0);
                var releasedAfter = context.GetArgument<DateTime?>("releasedAfter");
                if (releasedAfter.HasValue)
                    return query.Where(m => m.ReleaseDate.CompareTo(releasedAfter.Value) > 0);
                var movieDuration = context.GetArgument<int?>("duration");
                if (movieDuration.HasValue)
                    return query.Where(m => m.Duration <= movieDuration);

                return query.ToList();
            });

            Field<ListGraphType<GenreType>>("genres",
            arguments: new QueryArguments(
                new QueryArgument<IntGraphType>
                {
                    Name = "id"
                },
                new QueryArgument<StringGraphType>
                {
                    Name = "name"
                }
            ),
            resolve: context =>
            {
                var genreRepo = (IGenreRepo)sp.GetRequiredService(typeof(IGenreRepo));
                var query = genreRepo.ReadAll();

                var genreId = context.GetArgument<int?>("id");
                if (genreId.HasValue)
                    return query.Where(g => g.Id == genreId);
                var genreName = context.GetArgument<string>("name");
                if (!string.IsNullOrEmpty(genreName))
                    return query.Where(g => g.Name.Contains(genreName));
                
                return query.ToList();
            });

            Field<ListGraphType<UserType>>("users",
            arguments: new QueryArguments(
                new QueryArgument<IntGraphType> {
                    Name = "id"
                },
                new QueryArgument<StringGraphType> {
                    Name = "username"
                },
                new QueryArgument<DateGraphType> {
                    Name = "registeredBefore"
                },
                new QueryArgument<DateGraphType> {
                    Name = "registeredAfter"
                }
            ),
            resolve: context => {
                var userRepo = (IUserRepo)sp.GetRequiredService(typeof(IUserRepo));
                var query = userRepo.ReadAll();

                var userId = context.GetArgument<int?>("id");
                if (userId.HasValue)
                    return query.Where(u => u.Id == userId);
                var username = context.GetArgument<string>("username");
                if (!string.IsNullOrEmpty(username))
                    return query.Where(u => u.Username.Contains(username));
                var registeredBefore = context.GetArgument<DateTime?>("registeredBefore");
                if (registeredBefore.HasValue)
                    return query.Where(u => u.RegistrationDate.CompareTo(registeredBefore) < 0);
                var registeredAfter = context.GetArgument<DateTime?>("registeredAfter");
                if (registeredAfter.HasValue)
                    return query.Where(u => u.RegistrationDate.CompareTo(registeredAfter) > 0);

                return query.ToList();
            });
        }
    }
}