using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Movies_Project.Models
{
	public class Movie
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Title is required")]
		[StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
		public string Title { get; set; }

		[Required(ErrorMessage = "CategoryId is required")]
		public int CategoryId { get; set; }

		[Required(ErrorMessage = "Director name is required")]
		[StringLength(50, ErrorMessage = "Director name cannot exceed 50 characters")]
		public string Director { get; set; }

		[Range(1900, 2100, ErrorMessage = "Release year must be between 1900 and 2100")]
		public int ReleaseYear { get; set; }
		public Category Category { get; set; } = new();
		public List<Review> Reviews { get; set; } = new();

		public List<MovieActor> MovieActors { get; set; } = new();
	}
}
