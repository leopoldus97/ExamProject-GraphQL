using System;
using GraphQL.Types;
using Microsoft.AspNetCore.Hosting;

namespace Test.Core.ApplicationServices {
    public interface IFieldService {
        void ActivateFields(
            ObjectGraphType objectGraph,
            FieldServiceType fieldType,
            IWebHostEnvironment env,
            IServiceProvider provider
        );

        void RegisterFields();
    }

    public enum FieldServiceType {
        Query,
        Mutation,
        Subscription
    }
}