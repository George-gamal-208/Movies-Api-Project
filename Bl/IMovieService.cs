//using Microsoft.AspNetCore.Mvc;
using Movies_Project.Models;

namespace Movies_Project.BL
{
	public interface IMovieService
	{
		IEnumerable<object> GetAllMovies();
		object GetMovieById(int id);
		bool AddMovie(MovieDto movieDto);
		bool DeleteMovie(int id);
	}
}
