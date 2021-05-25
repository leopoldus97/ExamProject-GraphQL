using System;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Hosting;
using Test.Core.ApplicationServices;
using Test.Core.DomainServices;
using Test.Core.Entity;

namespace Test.Graph.Mutation {
    public class DeleteCityMutation : IFieldMutationServiceItem
    {
        public void Activate(ObjectGraphType objectGraph, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            objectGraph.Field<StringGraphType>("deleteCity",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "cityId" }
            ),
            resolve: context => {
                var cityId = context.GetArgument<int>("cityId");
                var cityRepo = (IGenericRepo<City>)serviceProvider.GetService(typeof(IGenericRepo<City>));
                cityRepo.Delete(cityId);
                return $"cityId: {cityId} deleted";
            });
        }
    }
}