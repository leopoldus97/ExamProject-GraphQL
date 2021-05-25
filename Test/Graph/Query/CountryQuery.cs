using System;
using System.Linq;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Hosting;
using Test.Core.ApplicationServices;
using Test.Core.DomainServices;
using Test.Core.Entity;
using Test.Graph.Type;

namespace Test.Graph.Query {
    public class CountryQuery : IFieldQueryServiceItem
    {
        public void Activate(ObjectGraphType objectGraph, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            objectGraph.Field<ListGraphType<CountryGType>>("countries",
            arguments: new QueryArguments(
                new QueryArgument<StringGraphType> { Name = "name" }
            ),
            resolve: context => {
                var countryRepo = (IGenericRepo<Country>)serviceProvider.GetService(typeof(IGenericRepo<Country>));
                var baseQuery = countryRepo.GetAll();
                var name = context.GetArgument<string>("name");
                if (name != default(string))
                    return baseQuery.Where(w => w.Name.Contains(name));
                return baseQuery.ToList();
            });
        }
    }
}