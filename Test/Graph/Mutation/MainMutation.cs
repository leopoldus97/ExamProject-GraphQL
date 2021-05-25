using System;
using GraphQL.Types;
using Microsoft.AspNetCore.Hosting;
using Test.Core.ApplicationServices;

namespace Test.Graph.Mutation {
    public class MainMutation : ObjectGraphType {
        public MainMutation(IServiceProvider provider, IWebHostEnvironment env, IFieldService fieldService)
        {
            Name = "MainMutation";
            fieldService.ActivateFields(this, FieldServiceType.Mutation, env, provider);
        }
    }
}