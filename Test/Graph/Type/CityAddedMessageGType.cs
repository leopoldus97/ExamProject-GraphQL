using GraphQL.Types;
using Test.Core.Entity;

namespace Test.Graph.Type {
    public class CityAddedMessageGType : ObjectGraphType<CityAddedMessage> {
        public CityAddedMessageGType()
        {
            Field(x => x.Id, type: typeof(IntGraphType));
            Field(x => x.Message, type: typeof(StringGraphType));
            Field(x => x.CountryName, type: typeof(StringGraphType));
            Field(x => x.CityName, type: typeof(StringGraphType));
        }
    }
}