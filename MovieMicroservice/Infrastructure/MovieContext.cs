using Microsoft.EntityFrameworkCore;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Infrastructure
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieRating>(entity =>
            {
                entity.ToTable("MovieRatings");

                entity.HasKey(o => new { o.MovieId, o.UserId });

                entity.HasOne(o => o.RatedMovie)
                    .WithMany(o => o.Ratings)
                    .HasForeignKey(o => o.MovieId);

                entity.HasOne(o => o.RatedBy)
                    .WithMany(o => o.RatedMovies)
                    .HasForeignKey(o => o.UserId);
            });

            modelBuilder.Entity<MovieGenre>(entity => {
                entity.ToTable("MovieGenres");

                entity.HasKey(o => new { o.MovieId, o.GenreId });

                entity.HasOne(o => o.Movie)
                    .WithMany(o => o.Genre)
                    .HasForeignKey(o => o.MovieId);

                entity.HasOne(o => o.Genre)
                    .WithMany(o => o.Movies)
                    .HasForeignKey(o => o.GenreId);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genres");

                entity.Property(o => o.Name)
                .HasColumnName("Genre");

                entity.HasMany(o => o.Movies)
                    .WithOne(o => o.Genre)
                    .HasForeignKey(o => o.GenreId);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movies");

                entity.HasMany(o => o.Ratings)
                    .WithOne(o => o.RatedMovie)
                    .HasForeignKey(o => o.MovieId);

                entity.HasMany(o => o.Genre)
                    .WithOne(o => o.Movie)
                    .HasForeignKey(o => o.MovieId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasMany(o => o.RatedMovies)
                    .WithOne(o => o.RatedBy)
                    .HasForeignKey(o => o.UserId);
            });
        }
    }
}