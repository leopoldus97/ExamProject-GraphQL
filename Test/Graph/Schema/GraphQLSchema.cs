using System;
using Microsoft.Extensions.DependencyInjection;
using Test.Core.ApplicationServices;
using Test.Graph.Mutation;
using Test.Graph.Query;
using Test.Graph.Subscription;

namespace Test.Graph.Schema {
    public class GraphQLSchema : GraphQL.Types.Schema {
        public GraphQLSchema(IServiceProvider provider) : base(provider)
        {
            var fieldService = provider.GetRequiredService<IFieldService>();
            fieldService.RegisterFields();
            Mutation = provider.GetRequiredService<MainMutation>();
            Query = provider.GetRequiredService<MainQuery>();
            Subscription = provider.GetRequiredService<MainSubscription>();
        }
    }
}