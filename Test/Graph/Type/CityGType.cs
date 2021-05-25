using System;
using GraphQL.Types;
using Test.Core.DomainServices;
using Test.Core.Entity;

namespace Test.Graph.Type {
    public class CityGType : ObjectGraphType<City> {
        public IServiceProvider Provider { get; set; }
        public CityGType(IServiceProvider provider)
        {
            Field(x => x.Id, type: typeof(IntGraphType));
            Field(x => x.Name, type: typeof(StringGraphType));
            Field(x => x.Population, type: typeof(IntGraphType));
            Field<CountryGType>("country", resolve: context => {
                IGenericRepo<Country> countryRepo = (IGenericRepo<Country>)provider.GetService(typeof(IGenericRepo<Country>));
                return countryRepo.GetById(context.Source.CountryId);
            });   
        }
    }
}