namespace Test.Core.ApplicationServices.Implementations {
    public class SubscriptionServices : ISubscriptionServices
    {
        public SubscriptionServices()
        {
            this.CityAddedService = new CityAddedService();
        }
        public CityAddedService CityAddedService { get; set; }
    }
}