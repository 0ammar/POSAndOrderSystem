using Microsoft.EntityFrameworkCore;
using POSAndOrderSystem.DbContexts;
using POSAndOrderSystem.DTOs.AuthDTO.Request;
using POSAndOrderSystem.Entities;
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
				if (!string.IsNullOrEmpty(input.Name) && !string.IsNullOrEmpty(input.Password))
				{
					var authUser = await (from user in _context.Users
										  join li in _context.LookupItems
										  on user.RoleId equals li.ID
										  where user.Name == input.Name
								   && user.Password == input.Password
										  select new
										  {
											  userID = user.ID.ToString(),
											  Role = li.Name.ToString(),
										  }).FirstOrDefaultAsync();
					return authUser != null ? await TokenHelper.GenerateToken(authUser.userID, authUser.Role) : "Authentication failed";
				}
				else
					throw new Exception("Email Or Password Are Required.");
			}
			else
				throw new Exception("Email Or Password Are Required.");
		}

		// To register new user inside the system
		public async Task<string> RegisterAsync(RegisterDTO request)
		{
			var existingUser = await _context.Users.FirstOrDefaultAsync(req => req.Name == request.Name);
			if (existingUser != null)
				throw new InvalidOperationException("User with this email already exists.");
			var newUser = new User
			{
				RoleId = 3
			};
			await _context.AddAsync(newUser);
			await _context.SaveChangesAsync();
			return $"New User Added Successfully: {newUser.Name}";
		}
	}
}
