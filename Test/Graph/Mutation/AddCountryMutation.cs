using System;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Hosting;
using Test.Core.ApplicationServices;
using Test.Core.DomainServices;
using Test.Core.Entity;
using Test.Graph.Type;

namespace Test.Graph.Mutation
{
    public class AddCountryMutation : IFieldMutationServiceItem
    {
        public void Activate(ObjectGraphType objectGraph, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            objectGraph.Field<CountryGType>("addCountry",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "countryName" }
            ),
            resolve: context =>
            {
                var countryName = context.GetArgument<string>("countryName");
                var countryRepo = (IGenericRepo<Country>)serviceProvider.GetService(typeof(IGenericRepo<Country>));

                var newCountry = new Country {
                    Name = countryName
                };

                return countryRepo.Insert(newCountry);
            });
        }
    }
}