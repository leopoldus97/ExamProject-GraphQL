using System;
using System.Linq;
using GraphQL.Types;
using Test.Core.DomainServices;
using Test.Core.Entity;

namespace Test.Graph.Type {
    public class CountryGType : ObjectGraphType<Country> {
        public IServiceProvider Provider { get; set; }
        public CountryGType(IServiceProvider provider)
        {
            Field(x => x.Id, type: typeof(IntGraphType));
            Field(x => x.Name, type: typeof(StringGraphType));
            Field<ListGraphType<CityGType>>("cities", resolve: context => {
                IGenericRepo<City> cityRepo = (IGenericRepo<City>)provider.GetService(typeof(IGenericRepo<City>));
                return cityRepo.GetAll().Where(w => w.Country.Id == context.Source.Id);
            });
        }
    }
}