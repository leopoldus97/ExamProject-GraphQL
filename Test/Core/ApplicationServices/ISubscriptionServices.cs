using Test.Core.ApplicationServices.Implementations;

namespace Test.Core.ApplicationServices {
    public interface ISubscriptionServices {
        CityAddedService CityAddedService { get; }
    }
}