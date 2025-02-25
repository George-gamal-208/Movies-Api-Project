using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Movies_Project.Models;

namespace Movies_Project.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : Controller
	{
		UserManager<ApplicationUser> _userManager;
		SignInManager<ApplicationUser> _signInManager;
		public UsersController(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		public async Task<IActionResult> LoginOut()
		{
			await _signInManager.SignOutAsync();
			return Redirect("~/");
		}

		

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			ApplicationUser user = new ApplicationUser()
			{
				Email = model.Email,
				UserName = model.Username
			};
			try
			{
				var result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					return Ok(new { Message = "User registered successfully!" });
				}
				else
				{
					return BadRequest(result.Errors);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}


		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginModel model)
		{
			var user = await _userManager.FindByNameAsync(model.Username);
			if (user == null)
				return Unauthorized("Invalid credentials.");

			try
			{
				var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
				if (!result.Succeeded)
					return Unauthorized("Invalid credentials.");
				else
					return Ok(new { Message = "User login successfully!" });
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
	}
}
