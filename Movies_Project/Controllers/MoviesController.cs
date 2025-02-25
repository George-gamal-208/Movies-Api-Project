using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies_Project.BL;

//using Movies_Project.BL;
using Movies_Project.Models;
//using System.Linq;

namespace Movies_Project.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class MoviesController : ControllerBase
	{
		
		private readonly IMovieService _MovieService;

		
		public MoviesController(IMovieService movieService)
		{
			_MovieService = movieService;
		}
		[HttpGet]
		public ApiResponce GetMovies()
		{
			ApiResponce apiResponce = new ApiResponce();
			apiResponce.Data = _MovieService.GetAllMovies();
			apiResponce.Errors = null;
			apiResponce.StatusCode = "200";
			return apiResponce;
			
		}
		
		
		[HttpGet("{id}")]

		public ApiResponce GetMovieById(int id)
		{
			ApiResponce apiResponce = new ApiResponce();
			apiResponce.Data = _MovieService.GetMovieById(id);
			apiResponce.Errors = null;
			apiResponce.StatusCode = "200";
			return apiResponce;
		}
		[HttpPost]
		public ApiResponce AddMovie([FromBody] MovieDto movieDto)
		{
			try
			{
				_MovieService.AddMovie(movieDto);
				ApiResponce apiResponce = new ApiResponce();
				apiResponce.Data = "done";
				apiResponce.Errors = null;
				apiResponce.StatusCode = "200";
				return apiResponce;
			}
			catch (Exception ex)
			{
				ApiResponce apiResponce = new ApiResponce();
				apiResponce.Data = null;
				apiResponce.Errors = ex.Message;
				apiResponce.StatusCode = "502"; ;
				return apiResponce;
			}
		}

		[HttpPost]
		[Route("Delete/{id}")]
		public ApiResponce DeleteMovie(int id)
		{
			ApiResponce apiResponce = new ApiResponce();
			var movie = _MovieService.DeleteMovie(id);
			if (movie)
			{
				apiResponce.Data = "done";
				apiResponce.Errors = null;
				apiResponce.StatusCode = "200";
				return apiResponce;
			}
			else
			{
				apiResponce.Data = null;
				apiResponce.Errors = "No data found";
				apiResponce.StatusCode = "404";
				return apiResponce;
			}


		}
	}
}






































































/*[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
	private readonly IMovieService _movieService;

	public MoviesController(IMovieService movieService)
	{
		_movieService = movieService;
	}

	[HttpGet]
	public ActionResult<IEnumerable<Movie>> GetMovies()
	{
		return Ok(_movieService.GetAllMovies());
	}

	[HttpGet("{id}")]
	public ActionResult<Movie> GetMovie(int id)
	{
		var movie = _movieService.GetMovieById(id);
		if (movie == null)
		{
			return NotFound();
		}
		return Ok(movie);
	}

	[HttpPost]
	public ActionResult<Movie> AddMovie([FromBody] MovieDto movieDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		try
		{
			var movie = _movieService.AddMovie(movieDto);
			return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpDelete("{id}")]
	public IActionResult DeleteMovie(int id)
	{
		var deleted = _movieService.DeleteMovie(id);
		if (!deleted)
		{
			return NotFound();
		}
		return NoContent();
	}
}*/


