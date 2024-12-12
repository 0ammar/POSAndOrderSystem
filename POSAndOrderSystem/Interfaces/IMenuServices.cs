using POSAndOrderSystem.DTOs.MenusDTO.Request;
using POSAndOrderSystem.DTOs.MenusDTO.Response;

namespace POSAndOrderSystem.Interfaces
{
	public interface IMenuServices
	{
		Task<List<MenuTypeDTO>> GetAllMenuTypes();
		Task<bool> CreateMenuType(CreateMenuTypeDTO createMenuTypeDto);
		Task<bool> UpdateMenuType(int id, UpdateMenuTypeDTO updateMenuTypeDto);
		Task<bool> DeleteMenuType(int id);
		Task<List<MenuItemDTO>> GetAllMenuItemsByMenuTypeId(int menuTypeId);
		Task<bool> CreateMenuItem(CreateMenuItemDTO createMenuItemDto);
		Task<bool> UpdateMenuItem(int id, UpdateMenuItemDTO updateMenuItemDto);
		Task<bool> DeleteMenuItem(int id);
	}
}