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
    public class CityQuery : IFieldQueryServiceItem
    {
        public void Activate(ObjectGraphType objectGraph, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            objectGraph.Field<ListGraphType<CityGType>>("cities",
            arguments: new QueryArguments(
                new QueryArgument<StringGraphType> { Name = "name" }
            ),
            resolve: context => {
                var cityRepo = (IGenericRepo<City>)serviceProvider.GetService(typeof(IGenericRepo<City>));
                var baseQuery = cityRepo.GetAll();
                var name = context.GetArgument<string>("name");
                if (name != default(string))
                    return baseQuery.Where(w => w.Name.Contains(name));
                return baseQuery.ToList();
            });
        }
    }
}