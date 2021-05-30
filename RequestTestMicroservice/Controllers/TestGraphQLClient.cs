using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using RequestTestMicroservice.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestTestMicroservice.Controllers
{
    public class TestGraphQLClient
    {
        private readonly GraphQLHttpClient _client;
        public TestGraphQLClient(GraphQLHttpClient client)
        {
            _client = client;
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            var query = new GraphQLRequest
            {
                Query = @"
                        query movies {
                            movies {
                                id
                                title
                                duration
                        "
            };
            var response = await _client.SendQueryAsync<List<Movie>>(query);
            if (response.Errors != null && response.Errors.Any())
                throw new ApplicationException(response.Errors[0].Message);
            var movies = response.Data;
            return movies;
        }
    }
}
