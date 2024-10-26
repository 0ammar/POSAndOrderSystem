using Microsoft.EntityFrameworkCore;
using POSAndOrderSystem.DbContexts;
using POSAndOrderSystem.DTOs.User.Request;
using POSAndOrderSystem.DTOs.User.Response;
using POSAndOrderSystem.Entities;
using POSAndOrderSystem.Helper;
using POSAndOrderSystem.Helpers;
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
		public async Task CreateUser(User input)
		{
			if (!EmailValidationHelper.IsValidEmail(input.Email))
				throw new ArgumentException("Invalid email format.");
			if (!PhoneValidationHelper.IsValidPhone(input.Phone))
				throw new ArgumentException("Invalid phone number format.");
			if (!PasswordValidationHelper.IsValidPassword(input.Password))
				throw new ArgumentException("Password must be at least 8 characters, including letters, numbers, and special characters.");
			var existingUser = await _context.Users
				.FirstOrDefaultAsync(u => u.Email == input.Email || u.Phone == input.Phone);
			if (existingUser != null)
				throw new Exception("User with this email or phone already exists.");
			var email = input.Email = EncryptionHelper.GenerateSHA384String(input.Email);
			var password = input.Password = EncryptionHelper.GenerateSHA384String(input.Password);
			var newUser = new User
			{
				FirstName = input.FirstName,
				LastName = input.LastName,
				Email = input.Email,
				Password = input.Password,
				Address = input.Address,
				RoleId = 3,
			};
			await _context.Users.AddAsync(newUser);
			await _context.SaveChangesAsync();
		}

		// To delete existing user
		public async Task<bool> DeleteUser(int userId)
		{
			var user = await _context.Users.FindAsync(userId);
			if (user == null || user.IsActive == true)
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
							   FirstName = user.FirstName,
							   LastName = user.LastName,
							   Phone = user.Phone,
							   RoleId = user.RoleId,
						   };
			return await response.ToListAsync();
		}

		// Get user by thier ID from the Database
		public async Task<UserDTO> GetUserByID(int userId)
		{
			ValidationHelper.ValidateID(userId);
			var user = await _context.Users.FindAsync(userId);
			if (user == null)
				throw new Exception("Please enter a valid ID.");
			return new UserDTO
			{
				ID = userId,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Phone = user.Phone
			};
		}

		// Update user information like password email name or phone  
		public async Task<bool> UpdateUser(UpdateUserDTO userDto)
		{
			var user = await _context.Users.FindAsync(userDto.ID);
			if (user == null)
				throw new Exception("User doesn't exist.");
			if (!string.IsNullOrWhiteSpace(userDto.FirstName))
			{
				user.FirstName = userDto.FirstName;
			}
			if (!string.IsNullOrWhiteSpace(userDto.LastName))
			{
				user.LastName = userDto.LastName;
			}
			if (!string.IsNullOrWhiteSpace(userDto.Email))
			{
				if (!EmailValidationHelper.IsValidEmail(userDto.Email))
					throw new ArgumentException("Invalid email format.");

				user.Email = userDto.Email;
			}
			if (!string.IsNullOrWhiteSpace(userDto.Phone))
			{
				if (!PhoneValidationHelper.IsValidPhone(userDto.Phone))
					throw new ArgumentException("Invalid phone format.");

				user.Phone = userDto.Phone;
			}
			if (!string.IsNullOrWhiteSpace(userDto.Password))
			{
				if (!PasswordValidationHelper.IsValidPassword(userDto.Password))
					throw new ArgumentException("Password does not meet security requirements.");

				user.Password = PasswordValidationHelper.PasswordHashing(userDto.Password);
			}
			if (!string.IsNullOrWhiteSpace(userDto.UserImage))
			{
				user.UserImage = userDto.UserImage;
			}
			if (!string.IsNullOrWhiteSpace(userDto.Address))
			{
				user.Address = userDto.Address;
			}
			user.ModificationDate = DateTime.UtcNow;
			_context.Users.Update(user);
			return await _context.SaveChangesAsync() > 0;
		}
	}
}