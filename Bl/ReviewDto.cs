using System.ComponentModel.DataAnnotations;

namespace Movies_Project.Models
{
	public class ReviewDto
	{
		[Required(ErrorMessage = "Reviewer name is required")]
		public string ReviewerName { get; set; }

		[Required(ErrorMessage = "Comment is required")]
		public string Comment { get; set; }

		[Range(1, 10, ErrorMessage = "Rating must be between 1 and 10")]
		public int Rating { get; set; }

		[Required(ErrorMessage = "MovieId is required")]
		public int MovieId { get; set; }
	}
}
