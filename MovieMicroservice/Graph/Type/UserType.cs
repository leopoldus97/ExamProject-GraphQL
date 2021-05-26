using System.Collections.Generic;
using GraphQL.Types;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Graph.Type {
    public class UserType : ObjectGraphType<User> {
        public UserType()
        {
            Name = nameof(User);
            Description = "A User in the collection";

            Field(x => x.Id, type: typeof(IntGraphType)).Description("User ID");
            Field(x => x.Username, type: typeof(StringGraphType)).Description("Username of the user");
            Field(x => x.RegistrationDate, type: typeof(DateGraphType)).Description("Registration Date of the user");
            Field(name: "RatedMovies", description: "A list of movies rated by the user", type: typeof(ListGraphType<MovieRatingType>), resolve: u => u.Source.RatedMovies);
        }
    }
}