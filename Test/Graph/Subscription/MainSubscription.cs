using System;
using GraphQL.Types;
using Microsoft.AspNetCore.Hosting;
using Test.Core.ApplicationServices;

namespace Test.Graph.Subscription {
    public class MainSubscription : ObjectGraphType {
        public MainSubscription(IServiceProvider provider, IWebHostEnvironment env, IFieldService fieldService)
        {
            Name = "MainSubscription";
            fieldService.ActivateFields(this, FieldServiceType.Subscription, env, provider);
        }
    }
}