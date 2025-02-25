using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Movies_Project.Models;
namespace Movies_Project.Models
{
	public class MoviesContext:IdentityDbContext<ApplicationUser>
	{
		public MoviesContext(DbContextOptions<MoviesContext> options) : base(options) { }

		public DbSet<Movie> Movies { get; set; }
		public DbSet<Actor> Actors { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<MovieActor> MovieActors { get; set; }
		public DbSet<Category> categories { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<MovieActor>(entity =>
			{
				entity.HasKey(ma => new { ma.MovieId, ma.ActorId });

				entity.HasOne(ma => ma.Movie)
				.WithMany(m => m.MovieActors)
				.HasForeignKey(ma => ma.MovieId);

				entity.HasOne(ma => ma.Actor)
				.WithMany(a => a.MovieActors)
				.HasForeignKey(ma => ma.ActorId);
			});
			
			modelBuilder.Entity<Movie>(entity => {
				entity.HasOne(m => m.Category)
				.WithMany(c => c.Movies)
				.HasForeignKey(m => m.CategoryId);
			});

			modelBuilder.Entity<Review>(entity =>
			{
				entity.HasOne(r => r.Movie)
				.WithMany(m => m.Reviews)
				.HasForeignKey(r => r.MovieId);
			});
		}
	}
}
