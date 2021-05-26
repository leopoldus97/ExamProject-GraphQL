using System;
using Microsoft.Extensions.DependencyInjection;
using MovieMicroservice.Graph.Mutation;
using MovieMicroservice.Graph.Query;

namespace MovieMicroservice.Graph.Schema
{
    public class MovieMicroserviceSchema : GraphQL.Types.Schema {
        public MovieMicroserviceSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            //Query = new MovieQuery(serviceProvider.GetRequiredService<MovieRepo>());
            Query = serviceProvider.GetRequiredService<MovieQuery>();
            Mutation = serviceProvider.GetRequiredService<MovieMutation>();
        }
    }
}