using POSAndOrderSystem.DTOs.User.Request;
using POSAndOrderSystem.DTOs.User.Response;
using POSAndOrderSystem.Entities;

namespace POSAndOrderSystem.Interfaces
{
	public interface IUserServices
	{
		Task CreateUser(User input);
		Task<bool> UpdateUser(UpdateUserDTO input);
		Task<bool> DeleteUser(int input);
		Task<UserDTO> GetUserByID(int input);
		Task<List<UserDTO>> GetAllUsers();
	}
}
