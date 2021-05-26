using System;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Hosting;
using Test.Core.ApplicationServices;
using Test.Core.DomainServices;
using Test.Core.Entity;
using Test.Graph.Type;

namespace Test.Graph.Mutation {
    public class AddCityMutation : IFieldMutationServiceItem
    {
        public void Activate(ObjectGraphType objectGraph, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            objectGraph.Field<CityGType>("addCity",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "countryId" },
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "cityName" },
                new QueryArgument<IntGraphType> { Name = "population" }
            ),
            resolve: context => {
                var countryId = context.GetArgument<int>("countryId");
                var cityName = context.GetArgument<string>("cityName");
                var population = context.GetArgument<int?>("population");

                var subscriptionServices = (ISubscriptionServices)serviceProvider.GetService(typeof(ISubscriptionServices));
                var cityRepository = (IGenericRepo<City>)serviceProvider.GetService(typeof(IGenericRepo<City>));
                var countryRepository = (IGenericRepo<Country>)serviceProvider.GetService(typeof(IGenericRepo<Country>));

                var foundCountry = countryRepository.GetById(countryId);

                var newCity = new City {
                    Name = cityName,
                    CountryId = countryId,
                    Population = population
                };

                var addedCity = cityRepository.Insert(newCity);
                subscriptionServices.CityAddedService.AddCityAddedMessage(new CityAddedMessage {
                    CityName = addedCity.Name,
                    CountryName = foundCountry.Name,
                    Id = addedCity.Id,
                    Message = "A new city added"
                });
                return addedCity;
            });
        }
    }
}