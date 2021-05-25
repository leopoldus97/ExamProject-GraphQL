using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL;
using GraphQL.Types;
using MovieMicroservice.GraphQL.GraphTypes;
using MovieMicroservice.Infrastructure;

namespace MovieMicroservice.Core.ApplicationServices.Implementations
{
    public class MovieQuery : ObjectGraphType
    {
        public MovieQuery(MovieRepo repo)
        {
            Name = "MovieQuerier";
            Description = "The base query for all the entities in our object graph.";

            Field<ListGraphType<MovieType>>("movies",
            arguments: new QueryArguments(new List<QueryArgument> {
                new QueryArgument<IdGraphType> {
                    Name = "id"
                },
                new QueryArgument<StringGraphType> {
                    Name = "title"
                },
                new QueryArgument<DateGraphType> {
                    Name = "releaseDate"
                },
                new QueryArgument<IdGraphType> {
                    Name = "duration"
                },
                new QueryArgument<ListGraphType<MovieGenreType>> {
                    Name = "genre"
                },
                new QueryArgument<ListGraphType<MovieRatingType>> {
                    Name = "ratings"
                }
            }),
            resolve: context =>
            {
                var query = repo.ReadAll();

                var movieId = context.GetArgument<int?>("id");
                if (movieId.HasValue)
                    return query.Where(m => m.Id == movieId.Value);
                var releasedBefore = context.GetArgument<DateTime?>("releasedBefore");
                if (releasedBefore.HasValue)
                    return query.Where(m => m.ReleaseDate.CompareTo(releasedBefore.Value) < 0);
                var releasedAfter = context.GetArgument<DateTime?>("releasedAfter");
                if (releasedAfter.HasValue)
                    return query.Where(m => m.ReleaseDate.CompareTo(releasedAfter.Value) > 0);

                return query.ToList();
            });
        }
    }
}