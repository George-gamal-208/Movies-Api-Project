using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Movies_Project.Models
{
	public class Review
	{
		public int Id { get; set; }



		[Required(ErrorMessage = "Reviewer name is required")]
		[StringLength(50, ErrorMessage = "Reviewer name cannot exceed 50 characters")]
		public string ReviewerName { get; set; }

		[Required(ErrorMessage = "Comment is required")]
		[StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters")]
		public string Comment { get; set; }

		[Range(1, 10, ErrorMessage = "Rating must be between 1 and 10")]
		public int Rating { get; set; }

		[Required(ErrorMessage = "MovieId is required")]
		public int MovieId { get; set; }
		[JsonIgnore]
		public Movie Movie { get; set; } = new();
	}
}
