using Movies_Project.Models;

namespace Movies_Project.BL
{
	public interface IReviewService
	{
		IEnumerable<Review> GetAllReviews();
		Review GetReviewById(int id);
		Review AddReview(ReviewDto reviewDto);
		bool DeleteReview(int id);
	}
}
