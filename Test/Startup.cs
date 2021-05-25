using GraphiQl;
using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Test.Core.ApplicationServices;
using Test.Core.ApplicationServices.Implementations;
using Test.Core.DomainServices;
using Test.Graph.Mutation;
using Test.Graph.Query;
using Test.Graph.Schema;
using Test.Graph.Subscription;
using Test.Graph.Type;
using Test.Infrastructure;

namespace Test
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options => {
                options.UseNpgsql(Configuration.GetConnectionString("SQLConnection"));
            });

            /* services
                .AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>))
                .AddScoped<IFieldService, FieldService>()
                .AddScoped<MainMutation>()
                .AddScoped<MainQuery>()
                .AddScoped<MainSubscription>()
                .AddScoped<CityGType>()
                .AddScoped<CountryGType>()
                .AddScoped<CityAddedMessageGType>();

            services.AddSingleton<ISubscriptionServices, SubscriptionServices>();

            services.AddGraphQL().AddGraphTypes().AddWebSockets(); */

            services
                .AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>))
                .AddScoped<IFieldService, FieldService>()
                .AddSingleton<ISubscriptionServices, SubscriptionServices>()
                .AddScoped<MainMutation>()
                .AddScoped<MainSubscription>()
                .AddScoped<MainQuery>()
                .AddScoped<CityGType>()
                .AddScoped<CountryGType>()
                .AddScoped<GraphQLSchema>()
                .AddScoped<CityAddedMessageGType>()
                .AddGraphQL(options => {
                    options.EnableMetrics = true;
                    options.UnhandledExceptionDelegate = ctx => { Console.WriteLine(ctx.OriginalException); };
                })
                .AddSystemTextJson(deserializerSettings => {  }, serializerSettings => {  })
                .AddWebSockets()
                .AddDataLoader()
                .AddGraphTypes(typeof(GraphQLSchema));

            services.AddGraphiQl(x => {
                x.GraphiQlPath = "/graphiql-ui";
                x.GraphQlApiPath = "/graphql";
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope()) {
                    serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>().Database.Migrate();
                }
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseGraphQL<GraphQLSchema>();

            app.UseGraphiQl();

            app.UseGraphQLPlayground();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
