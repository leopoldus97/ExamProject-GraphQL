using System;
using GraphQL.Types;
using Microsoft.AspNetCore.Hosting;
using Test.Core.ApplicationServices;

namespace Test.Graph.Query {
    public class MainQuery : ObjectGraphType {
        public MainQuery(IServiceProvider provider, IWebHostEnvironment env, IFieldService fieldService) {
            Name = "MainQuery";
            fieldService.ActivateFields(this, FieldServiceType.Query, env, provider);
        }
    }
}