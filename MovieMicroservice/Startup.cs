using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MovieMicroservice.Core.ApplicationServices;
using MovieMicroservice.Core.ApplicationServices.Implementations;
using MovieMicroservice.Core.DomainServices;
using MovieMicroservice.GraphQL.Mutations;
using MovieMicroservice.GraphQL.Schemas;
using MovieMicroservice.Infrastructure;

namespace MovieMicroservice
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
            services.AddMvc().AddNewtonsoftJson(opt => {
                opt.SerializerSettings.MaxDepth = 3;
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddControllers();

            services.AddDbContext<MovieContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SQLConnection"))
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieMicroservice", Version = "v1" });
            });

            services.AddTransient<IMovieRepo, MovieRepo>();
            services.AddTransient<IMovieService, MovieService>();

            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IGenreRepo, GenreRepo>();
            services.AddTransient<IGenreService, GenreService>();

            //services.AddSingleton<ISchema, MovieMicroserviceSchema>(services => new MovieMicroserviceSchema(new SelfActivatingServiceProvider(services)));

            /* services.AddGraphQL((options, provider) => {
                var graphQLOptions = Configuration.GetSection("GraphQL").Get<GraphQLOptions>();
                options.ComplexityConfiguration = graphQLOptions.ComplexityConfiguration;
                options.EnableMetrics = graphQLOptions.EnableMetrics;

                var logger = provider.GetRequiredService<ILogger<Startup>>();
                options.UnhandledExceptionDelegate = ctx => 
                    logger.LogError("{Error} occured", ctx.OriginalException.Message);
            }).AddGraphTypes().AddDataLoader(); */
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieMicroservice v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseGraphQL<MovieMicroserviceSchema>();

            /* app.UseGraphQLGraphiQL();
            app.UseGraphQLPlayground(); */
        }
    }
}
