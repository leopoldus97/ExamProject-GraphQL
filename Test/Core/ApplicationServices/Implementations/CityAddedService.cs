using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Test.Core.Entity;

namespace Test.Core.ApplicationServices.Implementations
{
    public class CityAddedService
    {
        private readonly ISubject<CityAddedMessage> _messageStream = new ReplaySubject<CityAddedMessage>(1);
        public CityAddedMessage AddCityAddedMessage(CityAddedMessage message)
        {
            _messageStream.OnNext(message);
            return message;
        }

        public IObservable<CityAddedMessage> GetMessages(string countryName)
        {
            var mess = _messageStream
                .Where(message => message.CountryName == countryName)
                .Select(s => s)
                .AsObservable();

            return mess;
        }
    }
}