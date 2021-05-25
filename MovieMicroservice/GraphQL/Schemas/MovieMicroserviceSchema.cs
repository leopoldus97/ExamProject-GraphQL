using System;
using Microsoft.Extensions.DependencyInjection;
using GraphQL.Types;
using MovieMicroservice.Core.ApplicationServices.Implementations;
using MovieMicroservice.Infrastructure;
using MovieMicroservice.GraphQL.Mutations;

namespace MovieMicroservice.GraphQL.Schemas
{
    public class MovieMicroserviceSchema : Schema {
        public MovieMicroserviceSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            //Query = new MovieQuery(serviceProvider.GetRequiredService<MovieRepo>());
            Query = serviceProvider.GetRequiredService<MovieQuery>();
            Mutation = serviceProvider.GetRequiredService<MovieMutation>();
        }
    }
}