using System;
using GraphQL.Types;
using Microsoft.AspNetCore.Hosting;

namespace Test.Core.ApplicationServices
{
    public interface IFieldServiceItem
    {
        void Activate(ObjectGraphType objectGraph, IWebHostEnvironment env, IServiceProvider serviceProvider);

    }
}