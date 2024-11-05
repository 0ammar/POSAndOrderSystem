using POSAndOrderSystem.DTOs.Request;
using POSAndOrderSystem.Entities;

namespace POSAndOrderSystem.Interfaces
{
	public interface IAdminServices
	{
		// Register, Login, Logout, GetUserById, GetAllUsers, UpdateUser, DeleteUser
		Task<bool> Register(UserDTO userDto);
		Task<UserDTO> Login(LoginDTO loginDto);
		Task Logout();
		Task<bool> UpdateUser(UserDTO userDto);
		Task<bool> DeleteUser(int userId);
		Task<UserDTO> GetUserById(int userId);
		Task<IEnumerable<UserDTO>> GetAllUsers();
	} }

		// Menu Type Operations
		//Task<bool> AddMenuType(MenuType menuType);
		//Task<bool> UpdateMenuType(MenuType menuType);
		//Task<bool> DeleteMenuType(int menuTypeId);
		//Task<IEnumerable<MenuType>> GetMenuTypes();


		// Menu Item Operations
		//Task<bool> AddMenuItem(MenuItem menuItem);
		//Task<bool> UpdateMenuItem(MenuItem menuItem);
		//Task<bool> DeleteMenuItem(int menuItemId);
		//Task<IEnumerable<MenuItem>> GetMenuItems();
