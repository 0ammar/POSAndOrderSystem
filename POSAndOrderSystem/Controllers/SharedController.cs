using Microsoft.AspNetCore.Mvc;
using POSAndOrderSystem.Interfaces;
using Serilog;

namespace POSAndOrderSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SharedController : ControllerBase
	{
		private readonly IMenuServices _menuServices;

		public SharedController(IMenuServices menuServices)
		{
			_menuServices = menuServices;
		}

		/// <summary>
		/// Retrieves all MenuTypes from the system.
		/// </summary>
		/// <returns>A list of MenuTypes.</returns>
		/// <response code="200">Returns a list of MenuTypes.</response>
		/// <response code="500">If an unexpected error occurs.</response>
		[HttpGet]
		[Route("GetAllMenuTypes")]
		public async Task<IActionResult> GetAllMenuTypes()
		{
			Log.Information("Get all MenuTypes operation initiated.");
			try
			{
				var menuTypes = await _menuServices.GetAllMenuTypes();
				Log.Information("MenuTypes retrieved successfully.");
				return Ok(menuTypes);
			}
			catch (Exception ex)
			{
				Log.Error($"Unexpected error: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred.");
			}
		}
		/// <summary>
		/// Retrieves all MenuItems associated with a specific MenuType.
		/// </summary>
		/// <param name="menuTypeId">The ID of the MenuType to retrieve MenuItems for.</param>
		/// <returns>A list of MenuItems associated with the MenuType.</returns>
		/// <response code="200">Returns a list of MenuItems.</response>
		/// <response code="404">If the MenuType is not found.</response>
		/// <response code="500">If an unexpected error occurs.</response>
		[HttpGet]
		[Route("GetAllMenuItemsByMenuType/{menuTypeId}")]
		public async Task<IActionResult> GetAllMenuItemsByMenuType(int menuTypeId)
		{
			Log.Information($"Get all MenuItems for MenuType operation initiated for MenuType ID: {menuTypeId}");
			try
			{
				var menuItems = await _menuServices.GetAllMenuItemsByMenuTypeId(menuTypeId);
				Log.Information($"MenuItems for MenuType ID: {menuTypeId} retrieved successfully.");
				return Ok(menuItems);
			}
			catch (Exception ex)
			{
				Log.Error($"Unexpected error: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred.");
			}
		}
	}
}
