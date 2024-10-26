using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POSAndOrderSystem.DTOs.Request;
using POSAndOrderSystem.Interfaces;

namespace POSAndOrderSystem.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AdminController : ControllerBase
	{
	
		// Register a new user (Admin or any other role)
		[HttpPost("[action]")]
		public async Task<IActionResult> Register([FromQuery] int i)
		{
			//if (!ModelState.IsValid)
			//	return BadRequest("Invalid input");

			//var result = await _adminServices.Register(userDto);
			//if (!result) return BadRequest("User already exists or registration failed.");

			return Ok("User registered successfully.");
		}

		// Login user
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
		{
			if (!ModelState.IsValid)
				return BadRequest("Invalid input");

			var user = await _adminServices.Login(loginDto);
			if (user == null) return Unauthorized("Invalid email or password.");

			// Normally, you would return a JWT token here after login success
			return Ok(user);
		}

		// Logout (if you're managing sessions)
		[HttpPost("logout")]
		public async Task<IActionResult> Logout()
		{
			await _adminServices.Logout();
			return Ok("Logged out successfully.");
		}

		// Update user details
		[HttpPut("update")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> UpdateUser([FromBody] UserDTO userDto)
		{
			if (!ModelState.IsValid)
				return BadRequest("Invalid input");

			var result = await _adminServices.UpdateUser(userDto);
			if (!result) return NotFound("User not found or update failed.");

			return Ok("User updated successfully.");
		}

		// Delete a user
		[HttpDelete("delete/{userId}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteUser(int userId)
		{
			var result = await _adminServices.DeleteUser(userId);
			if (!result) return NotFound("User not found or deletion failed.");

			return Ok("User deleted successfully.");
		}

		// Get user by ID
		[HttpGet("get-user/{userId}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetUserById(int userId)
		{
			var user = await _adminServices.GetUserById(userId);
			if (user == null) return NotFound("User not found.");

			return Ok(user);
		}

		// Get all users
		[HttpGet("all-users")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetAllUsers()
		{
			var users = await _adminServices.GetAllUsers();
			if (!users.Any()) return NotFound("No users found.");

			return Ok(users);
		}
	}
}
