using System;
using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;
using Microsoft.AspNetCore.Hosting;
using Test.Core.ApplicationServices;
using Test.Core.Entity;
using Test.Graph.Type;

namespace Test.Graph.Subscription {
    public class CityAddedSubscription : IFieldSubscriptionServiceItem
    {
        public void Activate(ObjectGraphType objectGraph, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            objectGraph.AddField(new EventStreamFieldType {
                Name = "cityAdded",
                Type = typeof(CityAddedMessageGType),
                Resolver = new FuncFieldResolver<CityAddedMessage>(context => context.Source as CityAddedMessage),
                Arguments = new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "countryName" }
                ),
                Subscriber = new EventStreamResolver<CityAddedMessage>(context => {
                    var subscriptionServices = (ISubscriptionServices)serviceProvider.GetService(typeof(ISubscriptionServices));
                    var countryName = context.GetArgument<string>("countryName");
                    return subscriptionServices.CityAddedService.GetMessages(countryName);
                })
            });
        }
    }
}