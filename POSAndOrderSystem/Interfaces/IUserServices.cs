using POSAndOrderSystem.DTOs.User.Request;
using POSAndOrderSystem.DTOs.User.Response;

namespace POSAndOrderSystem.Interfaces
{
	public interface IUserServices
	{
		Task<List<UserDTO>> GetAllUsers();
		Task CreateUser(CreateUserDTO input);
		Task<bool> UpdateUser(UpdateUserDTO input);
		Task<bool> DeleteUser(int input);
	}
}