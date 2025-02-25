using Movies_Project.Models;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Movies_Project.BL
{
	public class MovieService : IMovieService
	{
		private readonly MoviesContext _context;

		public MovieService(MoviesContext context)
		{
			_context = context;
		}

		public IEnumerable<object> GetAllMovies()
		{
			var movies = _context.Movies
				.Include(m => m.Category)  
				.Include(m => m.Reviews)   
				.Include(m => m.MovieActors) 
					.ThenInclude(ma => ma.Actor) 
				.Select(m => new
				{
					m.Id,
					m.Title,
					m.Director,
					m.ReleaseYear,
					Reviews = m.Reviews.Select(r => new
					{
						r.Id,
						r.ReviewerName,
						r.Comment,
						r.Rating
					}).ToList(),
					Actors = m.MovieActors.Select(ma => new
					{
						ma.Actor.Id,
						ma.Actor.Name
					}).ToList()
				})
				.ToList();

			return movies;
		}

		

		public object GetMovieById(int id)
		{
			var movie = _context.Movies
				.Include(m => m.Category)
				.Include(m => m.Reviews)
				.Include(m => m.MovieActors)
					.ThenInclude(ma => ma.Actor)
				.Where(m => m.Id == id)
				.Select(m => new
				{
					m.Id,
					m.Title,
					Genre = m.Category.Name,
					m.Director,
					m.ReleaseYear,
					Reviews = m.Reviews.Select(r => new
					{
						r.Id,
						r.ReviewerName,
						r.Comment,
						r.Rating
					}).ToList(),
					Actors = m.MovieActors.Select(ma => new
					{
						ma.Actor.Id,
						ma.Actor.Name
					}).ToList()
				})
				.FirstOrDefault();
			return movie;
		}

		public bool AddMovie(MovieDto movieDto)
		{
			try
			{
				var category = _context.categories.Find(movieDto.CategoryId);
				var movie = new Movie
				{
					Title = movieDto.Title,
					Director = movieDto.Director,
					ReleaseYear = movieDto.ReleaseYear,
					CategoryId = movieDto.CategoryId,
					Category = category,
					MovieActors = new List<MovieActor>()
				};
				foreach (var actorName in movieDto.ActorNames)
				{
					var actor = _context.Actors.FirstOrDefault(a => a.Name == actorName);
					if (actor == null)
					{
						actor = new Actor { Name = actorName };
						_context.Actors.Add(actor);
						_context.SaveChanges();
					}
					movie.MovieActors.Add(new MovieActor { ActorId = actor.Id });
				}
				_context.Movies.Add(movie);
				_context.SaveChanges();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool DeleteMovie(int id)
		{
			var movie = _context.Movies.Find(id);
			if (movie == null)
			{
				return false;
			}

			_context.Movies.Remove(movie);
			_context.SaveChanges();
			return true;
		}
	}
}
