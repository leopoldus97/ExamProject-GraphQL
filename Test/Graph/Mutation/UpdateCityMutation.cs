using System;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Hosting;
using Test.Core.ApplicationServices;
using Test.Core.DomainServices;
using Test.Core.Entity;
using Test.Graph.Type;

namespace Test.Graph.Mutation {
    public class UpdateCityMutation : IFieldMutationServiceItem
    {
        public void Activate(ObjectGraphType objectGraph, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            objectGraph.Field<CityGType>("updateCity",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "cityId" },
                new QueryArgument<IntGraphType> { Name = "countryId" },
                new QueryArgument<StringGraphType> { Name = "cityName" },
                new QueryArgument<IntGraphType> { Name = "population" }
            ),
            resolve: context => {
                var cityId = context.GetArgument<int>("cityId");
                var countryId = context.GetArgument<int?>("countryId");
                var cityName = context.GetArgument<string>("cityName");
                var population = context.GetArgument<int?>("population");

                var cityRepo = (IGenericRepo<City>)serviceProvider.GetService(typeof(IGenericRepo<City>));
                var foundCity = cityRepo.GetById(cityId);

                if (countryId != null)
                    foundCity.CountryId = countryId.Value;
                if (!String.IsNullOrEmpty(cityName))
                    foundCity.Name = cityName;
                if (population != null)
                    foundCity.Population = population.Value;

                return cityRepo.Update(foundCity);
            });
        }
    }
}