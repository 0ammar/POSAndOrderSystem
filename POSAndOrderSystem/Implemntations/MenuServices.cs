using Microsoft.EntityFrameworkCore;
using POSAndOrderSystem.DbContexts;
using POSAndOrderSystem.DTOs.MenusDTO.Request;
using POSAndOrderSystem.DTOs.MenusDTO.Response;
using POSAndOrderSystem.Entities;
using POSAndOrderSystem.Helpers;
using POSAndOrderSystem.Interfaces;

namespace POSAndOrderSystem.Implemntations
{
	public class MenuServices : IMenuServices
	{
		private readonly POSAndOrderContext _context;
		public MenuServices(POSAndOrderContext context)
		{
			_context = context;
		}

		// Create new menu item inside specific menu type 
		public async Task<bool> CreateMenuItem(CreateMenuItemDTO createMenuItemDto)
		{
			ValidationHelper.ValidateRequiredString(createMenuItemDto.MenuItemName, nameof(createMenuItemDto.MenuItemName));
			var menuTypeExists = await _context.MenuTypes.AnyAsync(mt => mt.ID == createMenuItemDto.MenuTypeID && mt.IsActive);
			if (!menuTypeExists)
				throw new Exception($"MenuType with ID {createMenuItemDto.MenuTypeID} does not exist or is inactive.");
			var newMenuItem = new MenuItem
			{
				MenuItemName = createMenuItemDto.MenuItemName,
				Price = createMenuItemDto.Price,
				MenuTypeID = createMenuItemDto.MenuTypeID,
				ModificationDate = DateTime.UtcNow
			};
			_context.MenuItems.Add(newMenuItem);
			var isSaved = await _context.SaveChangesAsync() > 0;
			return isSaved;
		}

		// Create New Menu Type Inside The System
		public async Task<bool> CreateMenuType(CreateMenuTypeDTO createMenuTypeDto)
		{
			ValidationHelper.ValidateRequiredString(createMenuTypeDto.MenuTypeName, nameof(createMenuTypeDto.MenuTypeName));
			var newMenuType = new MenuType
			{
				MenuTypeName = createMenuTypeDto.MenuTypeName,
				Image = createMenuTypeDto.Image,
			};
			_context.MenuTypes.Add(newMenuType);
			var isSaved = await _context.SaveChangesAsync() > 0;
			return isSaved;
		}

		// Soft deletion  for a specific menu item 
		public async Task<bool> DeleteMenuItem(int id)
		{
			var menuItem = await _context.MenuItems.FindAsync(id);
			if (menuItem == null || !menuItem.IsActive)
				throw new Exception($"MenuItem with ID {id} does not exist or is already inactive.");
			_context.MenuItems.Remove(menuItem);
			menuItem.ModificationDate = DateTime.UtcNow;
			var isUpdated = await _context.SaveChangesAsync() > 0;
			return isUpdated;
		}

		// Soft deletion for specific menu type 
		public async Task<bool> DeleteMenuType(int id)
		{
			var menuType = await _context.MenuTypes.FindAsync(id);
			if (menuType == null)
				throw new Exception($"MenuType with ID {id} does not exist.");
			_context.MenuTypes.Remove(menuType);
			menuType.ModificationDate = DateTime.UtcNow;
			var isDeleted = await _context.SaveChangesAsync() > 0;
			return isDeleted;
		}

		// This method gets all menu items by their menu type ID 
		public async Task<List<MenuItemDTO>> GetAllMenuItemsByMenuTypeId(int menuTypeId)
		{
			var menuTypeExists = await _context.MenuTypes.AnyAsync(mt => mt.ID == menuTypeId && mt.IsActive);
			if (!menuTypeExists)
				throw new Exception($"MenuType with ID {menuTypeId} does not exist or is inactive.");
			var menuItems = from item in _context.MenuItems
							where item.MenuTypeID == menuTypeId && item.IsActive
							select new MenuItemDTO
							{
								ID = item.ID,
								MenuItemName = item.MenuItemName,
								Price = item.Price,
								IsActive = item.IsActive,
								CreationDate = item.CreationDate
							};
			return await menuItems.ToListAsync();
		}

		// Retrieves all MenuTypes from the database and returns them as a list of MenuTypeDTO.
		public async Task<List<MenuTypeDTO>> GetAllMenuTypes()
		{
			if (_context == null)
				throw new InvalidOperationException("Database context is not initialized.");
			var menuTypes = from type in _context.MenuTypes
							select new MenuTypeDTO
							{
								ID = type.ID,
								MenuTypeName = type.MenuTypeName,
								IsActive = type.IsActive,
								CreationDate = type.CreationDate,
							};
			return await menuTypes.ToListAsync();
		}

		// Retrieve an specific menu item by their id 
		public async Task<MenuItemDTO?> GetMenuItemById(int id)
		{
			var menuItem = await _context.MenuItems.FindAsync(id);
			if (menuItem == null || !menuItem.IsActive)
				throw new Exception($"MenuItem with ID {id} does not exist or is inactive.");

			return new MenuItemDTO
			{
				ID = menuItem.ID,
				MenuItemName = menuItem.MenuItemName,
				Price = menuItem.Price,
				IsActive = menuItem.IsActive,
				CreationDate = menuItem.CreationDate
			};
		}

		// Retrieves a MenuType by its ID and returns it as a MenuTypeDTO.
		public async Task<MenuTypeDTO?> GetMenuTypeById(int id)
		{
			var menuType = await _context.MenuTypes.FindAsync(id);
			if (menuType == null)
				throw new Exception($"The menu time the ID is {id} does not existe ");
			return new MenuTypeDTO
			{
				ID = menuType.ID,
				MenuTypeName = menuType.MenuTypeName,
				IsActive = menuType.IsActive,
				CreationDate = menuType.CreationDate,
				Image = menuType.Image
			};
		}

		// Update menu item their price and deleted or update their name
		public async Task<bool> UpdateMenuItem(int id, UpdateMenuItemDTO updateMenuItemDto)
		{
			var menuItem = await _context.MenuItems.FindAsync(id);
			if (menuItem == null || !menuItem.IsActive)
				throw new Exception($"MenuItem with ID {id} does not exist or is inactive.");
			if (!string.IsNullOrWhiteSpace(updateMenuItemDto.MenuItemName))
				menuItem.MenuItemName = updateMenuItemDto.MenuItemName;
			if (updateMenuItemDto.Price.HasValue)
				menuItem.Price = updateMenuItemDto.Price.Value;
			menuItem.ModificationDate = DateTime.UtcNow;
			var isUpdated = await _context.SaveChangesAsync() > 0;
			return isUpdated;
		}

		// This service to update menu type values 
		public async Task<bool> UpdateMenuType(int id, UpdateMenuTypeDTO updateMenuTypeDto)
		{
			var menuType = await _context.MenuTypes.FindAsync(id);
			if (menuType == null)
				throw new Exception($"MenuType with ID {id} does not exist.");
			if (updateMenuTypeDto.MenuTypeName != null)
			{
				ValidationHelper.ValidateRequiredString(updateMenuTypeDto.MenuTypeName, nameof(updateMenuTypeDto.MenuTypeName));
				menuType.MenuTypeName = updateMenuTypeDto.MenuTypeName;
			}
			if (updateMenuTypeDto.Image != null)
				menuType.Image = updateMenuTypeDto.Image;
			menuType.ModificationDate = DateTime.UtcNow;
			var isUpdated = await _context.SaveChangesAsync() > 0;
			return isUpdated;
		}
	}
}