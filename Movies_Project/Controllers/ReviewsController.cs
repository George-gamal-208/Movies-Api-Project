using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies_Project.BL;
using Movies_Project.Models;
using System.Collections.Generic;
using System.Linq;
namespace Movies_Project.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ReviewsController : ControllerBase
	{
		private readonly IReviewService _reviewService;

		public ReviewsController(IReviewService reviewService)
		{
			_reviewService = reviewService;
		}
		[HttpGet]
		public ApiResponce GetReviews()
		{
			ApiResponce apiResponce = new ApiResponce();
			apiResponce.Data = _reviewService.GetAllReviews();
			apiResponce.Errors = null;
			apiResponce.StatusCode = "200";

			return apiResponce;
			
		}

		[HttpGet("{id}")]
		public ApiResponce GetReview(int id)
		{
			ApiResponce apiResponce = new ApiResponce();
			apiResponce.Data = _reviewService.GetReviewById(id);
			apiResponce.Errors = null;
			apiResponce.StatusCode = "200";

			return apiResponce;
		}

		[HttpPost]
		public ApiResponce AddReview([FromBody] ReviewDto reviewDto)
		{
			try
			{
				_reviewService.AddReview(reviewDto);
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
		public ApiResponce DeleteReview(int id)
		{
			ApiResponce apiResponce = new ApiResponce();
			var review= _reviewService.DeleteReview(id);
			if (review)
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

















































/*[Route("api/[controller]")]
[ApiController]
public class ReviewsController : Controller
{
	private readonly MoviesContext _context;

	public ReviewsController(MoviesContext context)
	{
		_context = context;
	}

	// ✅ GET ALL REVIEWS
	[HttpGet]
	public ActionResult<IEnumerable<object>> GetReviews()
	{
		var reviews = _context.Reviews
			.Include(r => r.Movie)
			.Select(r => new
			{
				r.Id,
				r.ReviewerName,
				r.Comment,
				r.Rating,
				Movie = new
				{
					r.Movie.Id,
					r.Movie.Title
				}
			})
			.ToList();

		return Ok(reviews);
	}

	// ✅ GET REVIEW BY ID
	[HttpGet("{id}")]
	public ActionResult<object> GetReview(int id)
	{
		var review = _context.Reviews
			.Include(r => r.Movie)
			.Where(r => r.Id == id)
			.Select(r => new
			{
				r.Id,
				r.ReviewerName,
				r.Comment,
				r.Rating,
				Movie = new
				{
					r.Movie.Id,
					r.Movie.Title
				}
			})
			.FirstOrDefault();

		if (review == null)
		{
			return NotFound("Review not found.");
		}

		return Ok(review);
	}

	// ✅ ADD REVIEW (POST)
	[HttpPost]
	public ActionResult<Review> AddReview([FromBody] ReviewDto reviewDto)
	{
		// ✅ Step 1: Validate `reviewDto` fields (No `Movie` validation)
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		// 🔍 Step 2: Find the Movie
		var movie = _context.Movies.FirstOrDefault(m => m.Id == reviewDto.MovieId);
		if (movie == null)
		{
			return BadRequest("Invalid MovieId. Movie does not exist.");
		}

		// ✅ Step 3: Create a new Review object
		var review = new Review
		{
			ReviewerName = reviewDto.ReviewerName,
			Comment = reviewDto.Comment,
			Rating = reviewDto.Rating,
			MovieId = reviewDto.MovieId,
			Movie = movie // Assign Movie after validation
		};

		_context.Reviews.Add(review);
		_context.SaveChanges();

		return CreatedAtAction(nameof(GetReview), new { id = review.Id }, review);
	}






	// ✅ DELETE REVIEW
	[HttpDelete("{id}")]
	public ActionResult DeleteReview(int id)
	{
		var review = _context.Reviews.Find(id);
		if (review == null)
		{
			return NotFound("Review not found.");
		}

		_context.Reviews.Remove(review);
		_context.SaveChanges();

		return NoContent();
	}
}*/
