using Microsoft.EntityFrameworkCore;
using POSAndOrderSystem.DbContexts;
using POSAndOrderSystem.DTOs.Request;
using POSAndOrderSystem.Entities;
using POSAndOrderSystem.Interfaces;

namespace POSAndOrderSystem.Implementations
{
	public class AdminServices : IAdminServices
	{
		private readonly POSAndOrderContext _context;

		// Constructor to inject the DbContext
		public AdminServices(POSAndOrderContext context)
		{
			_context = context;
		}

		// Register a new admin (user)
		public async Task<bool> Register(UserDTO userDto)
		{
			if (_context.Users.Any(u => u.Email == userDto.Email || u.Phone == userDto.Phone))
				return false; // Email or Phone already exists

			var user = new User
			{
				FirstName = userDto.FirstName,
				LastName = userDto.LastName,
				Email = userDto.Email,
				Address = userDto.Address,
				Password = userDto.Password,
				Phone = userDto.Phone,
				RoleId = 1,
				CreationDate = DateTime.UtcNow
			};

			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
			return true;
		}

		// Login admin
		public async Task<UserDTO> Login(LoginDTO loginDto)
		{
			var user = await _context.Users
				.Where(u => u.Email == loginDto.Email && u.Password == loginDto.Password)
				.FirstOrDefaultAsync();

			if (user == null)
				return null;

			return new UserDTO
			{
				ID = user.ID,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				Phone = user.Phone
			};
		}
		// Logout operation
		public Task Logout()
		{
			return Task.CompletedTask;
		}
		// Update user
		public async Task<bool> UpdateUser(UserDTO userDto)
		{
			var user = await _context.Users
				.Where(u => u.ID == userDto.ID && !u.IsActive)
				.FirstOrDefaultAsync();

			if (user == null)
				return false;

			user.FirstName = userDto.FirstName;
			user.LastName = userDto.LastName;
			user.Email = userDto.Email;
			user.Phone = userDto.Phone;
			user.Password = userDto.Password;

			_context.Users.Update(user);
			await _context.SaveChangesAsync();
			return true;
		}
		// Delete user (soft delete)
		public async Task<bool> DeleteUser(int userId)
		{
			var user = await _context.Users
				.Where(u => u.ID == userId && !u.IsActive)
				.FirstOrDefaultAsync();

			if (user == null)
				return false;

			user.IsActive = true;
			_context.Users.Update(user);
			await _context.SaveChangesAsync();
			return true;
		}
		// Get user by ID
		public async Task<UserDTO> GetUserById(int userId)
		{
			var user = await _context.Users
				.Where(u => u.ID == userId && !u.IsActive)
				.FirstOrDefaultAsync();

			if (user == null)
				return null;

			return new UserDTO
			{
				ID = user.ID,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				Phone = user.Phone
			};
		}
		// Get all users (admins and others)
		public async Task<IEnumerable<UserDTO>> GetAllUsers()
		{
			var users = await _context.Users
				.Where(u => !u.IsActive) // Return only active users
				.ToListAsync();
			return users.Select(u => new UserDTO
			{
				ID = u.ID,
				FirstName = u.FirstName,
				LastName = u.LastName,
				Email = u.Email,
				Phone = u.Phone
			}).ToList();
		}
	}
}