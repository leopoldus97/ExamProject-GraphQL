using System;
using GraphQL.Server;
using GraphQL.Validation.Complexity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MovieMicroservice.Core.ApplicationServices;
using MovieMicroservice.Core.ApplicationServices.Implementations;
using MovieMicroservice.Core.DomainServices;
using MovieMicroservice.Graph.Mutation;
using MovieMicroservice.Graph.Query;
using MovieMicroservice.Graph.Schema;
using MovieMicroservice.Graph.Type;
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
            services.AddMvc().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.MaxDepth = 3;
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddDbContext<MovieContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SQLConnection"))
            );

            services.AddTransient<IMovieRepo, MovieRepo>();
            services.AddScoped<IMovieService, MovieService>();

            services.AddTransient<IUserRepo, UserRepo>();
            services.AddScoped<IUserService, UserService>();

            services.AddTransient<IGenreRepo, GenreRepo>();
            services.AddScoped<IGenreService, GenreService>();

            //services.AddSingleton<ISchema, MovieMicroserviceSchema>(services => new MovieMicroserviceSchema(new SelfActivatingServiceProvider(services)));

            services
                .AddScoped<MovieMutation>()
                .AddScoped<MovieQuery>()
                .AddScoped<UserType>()
                .AddScoped<MovieType>()
                .AddScoped<GenreType>()
                .AddScoped<MovieGenreType>()
                .AddScoped<MovieRatingType>()
                .AddScoped<MovieInputType>()
                .AddScoped<MovieMicroserviceSchema>();

            services.AddGraphQL(options =>
                {
                    var compConf = new ComplexityConfiguration
                    {
                        MaxDepth = 3,
                        MaxComplexity = 5,
                        MaxRecursionCount = 2
                    };
                    options.EnableMetrics = true;
                    options.UnhandledExceptionDelegate = ctx => { Console.WriteLine(ctx.OriginalException); };
                    options.ComplexityConfiguration = compConf;
                })
            .AddSystemTextJson(deserializationSettings => { }, serializationSettings => { })
            .AddDataLoader()
            .AddGraphTypes(typeof(MovieMicroserviceSchema));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieMicroservice", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieMicroservice v1"));
            }

            app.UseGraphQL<MovieMicroserviceSchema>();

            app.UseGraphQLGraphiQL();

            app.UseGraphQLPlayground();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
