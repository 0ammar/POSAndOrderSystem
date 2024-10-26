using Microsoft.EntityFrameworkCore;
using POSAndOrderSystem.DbContexts;
using POSAndOrderSystem.DTOs.AuthDTO.Request;
using POSAndOrderSystem.Entities;
using POSAndOrderSystem.Helper;
using POSAndOrderSystem.Helpers;
using POSAndOrderSystem.Interfaces;

namespace POSAndOrderSystem.Implemntations
{
	public class AuthServices : IAuthServices
	{
		private readonly POSAndOrderContext _context;
		public AuthServices(POSAndOrderContext context)
		{
			_context = context;
		}
		public async Task<string> Login(LoginDTO input)
		{
			if (input != null)
			{
				if (!string.IsNullOrEmpty(input.Email) && !string.IsNullOrEmpty(input.Password))
				{
					//input.Email = EncryptionHelper.GenerateSHA384String(input.Email);
					//input.Password = EncryptionHelper.GenerateSHA384String(input.Password);
					var authUser = await (from user in _context.Users
										  join li in _context.LookupItems
										  on user.RoleId equals li.ID
										  where user.Email == input.Email
								   && user.Password == input.Password
										  select new
										  {
											  userID = user.ID.ToString(),
											  Role = li.Name.ToString(),
										  }).FirstOrDefaultAsync();
					return authUser != null ? await TokenHelper.GenerateToken(authUser.userID, authUser.Role) : "Authentication failed";
				}
				else
				{
					throw new Exception("Email Or Password Are Required.");

				}
			}
			else
			{
				throw new Exception("Email Or Password Are Required.");
			}
		}

		// To register new userb inside the system
		public async Task<string> RegisterAsync(RegisterDTO request)
		{
			if (!EmailValidationHelper.IsValidEmail(request.Email))
				throw new ArgumentException("Invalid email format.");
			if (!PasswordValidationHelper.IsValidPassword(request.Password))
				throw new ArgumentException("Password does not meet complexity requirements.");
			if (!PhoneValidationHelper.IsValidPhone(request.Phone))
				throw new ArgumentException("Invalid phone number format.");
			var existingUser = await _context.Users.FirstOrDefaultAsync(req => req.Email == request.Email);
			if (existingUser != null)
				throw new InvalidOperationException("User with this email already exists.");
			var newUser = new User
			{
				FirstName = request.FirstName,
				LastName = request.LastName,
				Email = EncryptionHelper.GenerateSHA384String(request.Email),
				Password = EncryptionHelper.GenerateSHA384String(request.Password),
				Phone = request.Phone,
				Address = request.Address,
				RoleId = 3
			};
			await _context.AddAsync(newUser);
			await _context.SaveChangesAsync();
			return $"New User Added Successfully: {newUser.FirstName} {newUser.LastName}";
		}
	}
}
