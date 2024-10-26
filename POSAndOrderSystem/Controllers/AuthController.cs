using Microsoft.AspNetCore.Mvc;
using POSAndOrderSystem.DTOs.AuthDTO.Request;
using POSAndOrderSystem.Interfaces;
using Serilog;

namespace POSAndOrderSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthServices _authServices;

		public AuthController(IAuthServices authServices)
		{
			_authServices = authServices;
		}

		/// <summary>
		/// Registers a new user in the system.
		/// </summary>
		/// <returns>A success message with the user's name.</returns>
		/// <response code="200">Returns the success message.</response>
		/// <response code="400">If the input is invalid.</response>
		/// <response code="409">If a user with the same email already exists.</response>
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDTO request)
		{
			Log.Information("Register operation started for email: {Email}", request.Email);
			try
			{
				var result = await _authServices.RegisterAsync(request);
				Log.Information("User registered successfully: {Name}", $"{request.FirstName} {request.LastName}");
				return Ok(result);
			}
			catch (ArgumentException ex)
			{
				Log.Warning("Registration failed due to validation error: {Message}", ex.Message);
				return BadRequest(ex.Message);
			}
			catch (InvalidOperationException ex)
			{
				Log.Warning("Registration failed: {Message}", ex.Message);
				return Conflict(ex.Message);
			}
			catch (Exception ex)
			{
				Log.Error("An unexpected error occurred: {Message}", ex.Message);
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}
		}
		/// <summary>
		/// Handles user login requests.
		/// </summary>
		/// <returns>
		/// 200 OK if login is successful, 401 Unauthorized if login fails, 
		/// or 500 Internal Server Error if an exception occurs.
		/// </returns>
		/// <response code="200">Returns the authentication token or success message.</response>
		/// <response code="401">If email or password is incorrect.</response>
		/// <response code="500">If an unexpected error occurs.</response>
		[HttpPost("login")]
		public async Task<IActionResult> Auth([FromBody] LoginDTO input)
		{
			Log.Information("Login attempt started for email: {Email}", input.Email);
			try
			{
				var response = await _authServices.Login(input);
				if (response.Equals("Authentication failed"))
				{
					Log.Warning("Login failed for email: {Email}", input.Email);
					return Unauthorized("Email or password is not correct.");
				}

				Log.Information("User logged in successfully: {Email}", input.Email);
				return Ok(response); // Assuming response contains the authentication token.
			}
			catch (Exception ex)
			{
				Log.Error("An unexpected error occurred during login: {Message}", ex.Message);
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}
		}
	}
}

