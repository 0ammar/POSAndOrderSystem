using Microsoft.EntityFrameworkCore;
using POSAndOrderSystem.DbContexts;
using POSAndOrderSystem.DTOs.User.Request;
using POSAndOrderSystem.DTOs.User.Response;
using POSAndOrderSystem.Entities;
using POSAndOrderSystem.Interfaces;

namespace POSAndOrderSystem.Implementations
{
	public class UserServices : IUserServices
	{
		private readonly POSAndOrderContext _context;

		public UserServices(POSAndOrderContext context)
		{
			_context = context;
		}
		// Create new user inside the system 
		public async Task CreateUser(CreateUserDTO input)
		{
			// Find if this user already exists or not 
			var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Name == input.Name);
			if (existingUser != null)
				throw new Exception("User already exist.");
			var userEntity = new User
			{
				Name = input.Name,
				Password = input.Password,
				RoleId = input.RoleId,
			};
			await _context.Users.AddAsync(userEntity);
			await _context.SaveChangesAsync();
		}

		// To delete existing user
		public async Task<bool> DeleteUser(int userId)
		{
			var user = await _context.Users.FindAsync(userId);
			if (user == null)
				throw new Exception("User doesn't exist or already deleted.");
			_context.Users.Remove(user);
			await _context.SaveChangesAsync();
			return true;
		}

		// This method retrieved all users.
		public async Task<List<UserDTO>> GetAllUsers()
		{
			var response = from user in _context.Users
						   select new UserDTO
						   {
							   ID = user.ID,
							   Name = user.Name,
							   RoleId = user.RoleId
						   };
			return await response.ToListAsync();
		}

		// Update user information like password email name or phone  
		public async Task<bool> UpdateUser(UpdateUserDTO userDto)
		{
			var user = await _context.Users.FindAsync(userDto.ID);
			if (user == null)
				throw new Exception("User doesn't exist.");
			user.Name = userDto.Name!;
			user.Password = userDto.Password!;
			_context.Users.Update(user);
			return await _context.SaveChangesAsync() > 0;
		}
	}
}