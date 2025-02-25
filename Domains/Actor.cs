using System.ComponentModel.DataAnnotations;
namespace Movies_Project.Models
{
	public class Actor
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Actor name is required")]
		[StringLength(50, ErrorMessage = "Actor name cannot exceed 50 characters")]
		public string Name { get; set; }

		[Range(1, 100, ErrorMessage = "Age must be between 1 and 100")]
		public int? Age { get; set; }
		public List<MovieActor> MovieActors { get; set; } = new();

	}
}
