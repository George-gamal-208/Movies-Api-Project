using Movies_Project.Models;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Movies_Project.BL
{
	public class ReviewService : IReviewService
	{
		private readonly MoviesContext _context;

		public ReviewService(MoviesContext context)
		{
			_context = context;
		}

		public IEnumerable<Review> GetAllReviews()
		{
			return _context.Reviews.Include(r => r.Movie).ToList();
		}

		public Review GetReviewById(int id)
		{
			return _context.Reviews.Include(r => r.Movie).FirstOrDefault(r => r.Id == id);
		}

		public Review AddReview(ReviewDto reviewDto)
		{
			var movie = _context.Movies.FirstOrDefault(m => m.Id == reviewDto.MovieId);
			if (movie == null)
			{
				throw new Exception("Invalid MovieId. Movie does not exist.");
			}

			var review = new Review
			{
				ReviewerName = reviewDto.ReviewerName,
				Comment = reviewDto.Comment,
				Rating = reviewDto.Rating,
				MovieId = reviewDto.MovieId,
				Movie = movie
			};

			_context.Reviews.Add(review);
			_context.SaveChanges();
			return review;
		}

		public bool DeleteReview(int id)
		{
			var review = _context.Reviews.Find(id);
			if (review == null)
			{
				return false;
			}

			_context.Reviews.Remove(review);
			_context.SaveChanges();
			return true;
		}
	}

}
