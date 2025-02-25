using System.ComponentModel.DataAnnotations;

namespace Movies_Project.Models
{
	public class MovieDto
	{
		[Required(ErrorMessage = "Title is required")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Director name is required")]
		public string Director { get; set; }

		[Range(1900, 2100, ErrorMessage = "Release year must be between 1900 and 2100")]
		public int ReleaseYear { get; set; }

		[Required(ErrorMessage = "CategoryId is required")]
		public int CategoryId { get; set; }
		public List<string> ActorNames { get; set; }
	}
}
