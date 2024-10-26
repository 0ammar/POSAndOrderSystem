using Microsoft.AspNetCore.Mvc;
using POSAndOrderSystem.DTOs.LookupsDTO.Request;
using POSAndOrderSystem.DTOs.MenusDTO.Request;
using POSAndOrderSystem.DTOs.User.Request;
using POSAndOrderSystem.Entities;
using POSAndOrderSystem.Helpers;
using POSAndOrderSystem.Interfaces;
using Serilog;

namespace POSAndOrderSystem.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AdminController : ControllerBase
	{
		private readonly IUserServices _adminServices;
		private readonly ILookupServices _lookupServices;
		private readonly IMenuServices _menuServices;

		public AdminController(IUserServices adminServices, ILookupServices lookupServices, IMenuServices menuServices)
		{
			_adminServices = adminServices;
			_lookupServices = lookupServices;
			_menuServices = menuServices;
		}
		/// <summary>
		/// Use This Endpoint To Get A User By Their ID From the Database  
		/// </summary>
		/// <param name="userId">The ID of the user.</param>
		/// <param name="token"></param>
		/// <returns>A UserDto object or 404 status code if not found.</returns>
		[HttpGet]
		[Route("[action]/{userId}")]
		public async Task<IActionResult> GetUserByID([FromRoute] int userId, [FromHeader] string token)
		{
			Log.Information("Operation of Get User Has Been Started");
			try
			{
				var userDto = await _adminServices.GetUserByID(userId);
				if (!await TokenHelper.ValidateToken(token, "Admin"))
				{
					return Unauthorized("You Are Not Autharized For Get Department");
				}
				if (userDto == null)
				{
					Log.Warning($"User with ID {userId} not found.");
					return NotFound();
				}
				Log.Information($"Getting User, Their Name Is {userDto.FirstName} {userDto.LastName}, was called from DB");
				return Ok(userDto);
			}
			catch (Exception ex)
			{
				Log.Error($"An Error Occurred When Getting User with ID ({userId})");
				Log.Error(ex.ToString());
				return StatusCode(500, $"Error: {ex.Message}");
			}
		}
		/// <summary>
		/// Use This Endpoint To Get A LookupType By Their ID From the Database  
		/// </summary>
		/// <param name="LooukupTypeId">The ID of the LookupType.</param>
		/// <returns>A LooupTypeDTO object or 404 status code if not found.</returns>
		[HttpGet]
		[Route("[action]/{LooukupTypeId}")]
		public async Task<IActionResult> GetLookupTypeByID([FromRoute] int LooukupTypeId, [FromHeader] string token)
		{
			Log.Information("Operation of Get LookupType Has Been Started");
			try
			{
				if (!await TokenHelper.ValidateToken(token, "Admin"))
				{
					return Unauthorized("You Are Not Autharized For Get Department");
				}
				var LookupType = await _lookupServices.GetLookupTypeByID(LooukupTypeId);

				if (LookupType == null)
				{
					Log.Warning($"LookupType with ID {LooukupTypeId} not found.");
					return NotFound();
				}
				Log.Information($"Getting LookupType, Their Name Is {LookupType}, was called from DB");
				return Ok(LookupType);
			}
			catch (Exception ex)
			{
				Log.Error($"An Error Occurred When Getting LookupType with ID ({LooukupTypeId})");
				Log.Error(ex.ToString());
				return StatusCode(500, $"Error: {ex.Message}");
			}
		}
		/// <summary>
		/// Retrieves a LookupItem by its ID.
		/// </summary>
		/// <param name="id">The ID of the LookupItem to retrieve.</param>
		/// <param name="token"></param>
		/// <returns>A LookupItem representing the requested item.</returns>
		/// <response code="200">Returns the requested LookupItem.</response>
		/// <response code="400">If the ID is invalid or not found.</response>
		/// <response code="500">If an unexpected error occurs.</response>
		[HttpGet]
		[Route("GetLookupItemById/{id}")]
		public async Task<IActionResult> GetLookupItemById(int id, [FromHeader] string token)
		{
			Log.Information($"Get LookupItem operation initiated for ID: {id}");

			try
			{
				if (!await TokenHelper.ValidateToken(token, "Admin"))
				{
					return Unauthorized("You Are Not Autharized For Get Department");
				}
				var lookupItem = await _lookupServices.GetLookupItemById(id);
				Log.Information($"LookupItem retrieved successfully for ID: {id}");
				return Ok(lookupItem);
			}
			catch (ArgumentException ex)
			{
				Log.Warning($"Validation error: {ex.Message}");
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				Log.Error($"Unexpected error: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred.");
			}
		}
		/// <summary>
		/// Retrieves a MenuType by its ID from the system.
		/// </summary>
		/// <param name="id">The ID of the MenuType to retrieve.</param>
		/// <param name="token"></param>
		/// <returns>The requested MenuType or 404 if not found.</returns>
		/// <response code="200">Returns the MenuType.</response>
		/// <response code="404">If the MenuType is not found.</response>
		/// <response code="500">If an unexpected error occurs.</response>
		[HttpGet]
		[Route("GetMenuTypeById/{id}")]
		public async Task<IActionResult> GetMenuTypeById(int id, [FromHeader] string token)
		{
			if (!await TokenHelper.ValidateToken(token, "Admin"))
			{
				return Unauthorized("You Are Not Autharized For Get Department");
			}
			Log.Information($"Get MenuType by ID operation initiated for ID: {id}");
			try
			{
				var menuType = await _menuServices.GetMenuTypeById(id);
				if (menuType == null)
				{
					Log.Warning($"MenuType with ID: {id} not found.");
					return NotFound("MenuType not found.");
				}

				Log.Information($"MenuType with ID: {id} retrieved successfully.");
				return Ok(menuType);
			}
			catch (Exception ex)
			{
				Log.Error($"Unexpected error: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred.");
			}
		}
		/// <summary>
		/// Retrieves a MenuItem by its ID from the system.
		/// </summary>
		/// <param name="id">The ID of the MenuItem to retrieve.</param>
		/// <returns>The requested MenuItem or 404 if not found.</returns>
		/// <response code="200">Returns the MenuItem.</response>
		/// <response code="404">If the MenuItem is not found.</response>
		/// <response code="500">If an unexpected error occurs.</response>
		[HttpGet]
		[Route("GetMenuItemById/{id}")]
		public async Task<IActionResult> GetMenuItemById(int id)
		{
			Log.Information($"Get MenuItem by ID operation initiated for ID: {id}");
			try
			{
				var menuItem = await _menuServices.GetMenuItemById(id);
				if (menuItem == null)
				{
					Log.Warning($"MenuItem with ID: {id} not found.");
					return NotFound("MenuItem not found.");
				}

				Log.Information($"MenuItem with ID: {id} retrieved successfully.");
				return Ok(menuItem);
			}
			catch (Exception ex)
			{
				Log.Error($"Unexpected error: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred.");
			}
		}
		/// <summary>
		/// Use This Endpoint To Get All Users From The Database  
		/// </summary>
		/// <returns>A list of users or 500 status code if an error occurs.</returns>
		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> GetAllUsers([FromHeader] string token)
		{
			Log.Information("Operation of Get All User Has Been Started");
			try
			{
				var response = await _adminServices.GetAllUsers();
				Log.Information($"Successfully retrieved {response.Count} users.");
				return Ok(response);
			}
			catch (Exception ex)
			{
				Log.Error($"An Error Occurred When Getting Users: {ex}");
				return StatusCode(500, $"Error: {ex.Message}");
			}
		}
		/// <summary>
		/// Use This Endpoint To Get All LookupTypes From The Database  
		/// </summary>
		/// <returns>A list of users or 500 status code if an error occurs.</returns>
		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> GetAllLookupTypes()
		{
			Log.Information("Operation of Get All Lookuptypes Has Been Started");
			try
			{
				var response = await _lookupServices.GetAllLookupTypes();

				Log.Information($"Successfully retrieved {response.Count} Lookuptypes.");
				return Ok(response);
			}
			catch (Exception ex)
			{
				Log.Error($"An Error Occurred When Getting Lookuptypes.");
				Log.Error(ex.ToString());
				return StatusCode(500, $"Error: {ex.Message}");
			}
		}
		/// <summary>
		/// Retrieves a list of active LookupItems associated with the specified LookupTypeId.
		/// </summary>
		/// <param name="lookupTypeId">The ID of the LookupType for which to retrieve associated LookupItems.</param>
		/// <returns>
		/// A list of active LookupItems if successful (200 OK).
		/// Returns a Bad Request (400) if the LookupTypeId is invalid.
		/// Returns an Internal Server Error (500) if an unexpected error occurs.
		/// </returns>
		[HttpGet]
		[Route("GetLookupItems/{lookupTypeId}")]
		public async Task<IActionResult> GetLookupItemsByLookupTypeId(int lookupTypeId)
		{
			Log.Information($"Get LookupItems for LookupTypeId {lookupTypeId} operation initiated.");
			try
			{
				var lookupItems = await _lookupServices.GetLookupItemsByLookupTypeId(lookupTypeId);

				Log.Information($"LookupItems retrieved successfully for LookupTypeId {lookupTypeId}.");
				return Ok(lookupItems);
			}
			catch (ArgumentException ex)
			{
				Log.Warning($"Validation error: {ex.Message}");
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				Log.Error($"Unexpected error: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred.");
			}
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
		/// <summary>
		/// Use This Endpoint To Create New User in The System 
		/// </summary>
		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> CreateNewUser([FromBody] User input)
		{
			Log.Information("Operation of Create New User Has Been Started");
			try
			{
				await _adminServices.CreateUser(input);
				Log.Information($"New User Thier Name Is {input.FirstName} {input.LastName} was added to db");
				return StatusCode(201, "New User Has Been Created");
			}
			catch (Exception ex)
			{
				Log.Error("An Error Was Occured When Create New User");
				Log.Error(ex.ToString());
				return StatusCode(500, $"Error : {ex.Message}");
			}
		}
		/// <summary>
		/// Use This Endpoint To Create New LookupType in The System 
		/// </summary>
		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> CreateLookupType([FromBody] CreateLookupTypeDTO input)
		{
			Log.Information("Operation of Create New Lookup Type Has Been Started");
			try
			{
				await _lookupServices.LookupTypeCreating(input);
				Log.Information($"New User Thier Name Is {input.Name} And Its ID {input.ID} was added to db");
				return StatusCode(201, "New Lookup Type Has Been Created");
			}
			catch (Exception ex)
			{
				Log.Error("An Error Was Occured When Create New Lookup Type");
				Log.Error(ex.ToString());
				return StatusCode(500, $"Error : {ex.Message}");
			}
		}
		/// <summary>
		/// Creates a new LookupItem under the specified LookupType.
		/// </summary>
		/// <param name="createLookupItemDto">The details for the new LookupItem.</param>
		/// <returns>Status 201 if created successfully, 400 if validation fails, or 500 if an error occurs.</returns>
		[HttpPost]
		[Route("CreateLookupItem")]
		public async Task<IActionResult> CreateLookupItem([FromBody] CreateLookupItemDTO createLookupItemDto)
		{
			Log.Information("Create LookupItem operation initiated.");
			try
			{
				var isCreated = await _lookupServices.CreateLookupItem(createLookupItemDto);

				if (!isCreated)
				{
					Log.Error("Failed to create LookupItem due to database error.");
					return StatusCode(500, "An error occurred while creating the LookupItem.");
				}

				Log.Information("LookupItem created successfully.");
				return StatusCode(201, "LookupItem created successfully.");
			}
			catch (ArgumentException ex)
			{
				Log.Warning($"Validation error: {ex.Message}");
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				Log.Error($"Unexpected error: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred.");
			}
		}
		/// <summary>
		/// Creates a new MenuType.
		/// </summary>
		/// <param name="createMenuTypeDto">The details for the new MenuType.</param>
		/// <returns>Status 201 if created successfully, 400 if validation fails, or 500 if an error occurs.</returns>
		/// <response code="201">Returns the created MenuType.</response>
		/// <response code="400">If validation fails.</response>
		/// <response code="500">If an unexpected error occurs.</response>
		[HttpPost]
		[Route("CreateMenuType")]
		public async Task<IActionResult> CreateMenuType([FromBody] CreateMenuTypeDTO createMenuTypeDto)
		{
			Log.Information("Create MenuType operation initiated.");
			try
			{
				var isCreated = await _menuServices.CreateMenuType(createMenuTypeDto);

				if (!isCreated)
				{
					Log.Error("Failed to create MenuType due to database error.");
					return StatusCode(500, "An error occurred while creating the MenuType.");
				}

				Log.Information("MenuType created successfully.");
				return StatusCode(201, "MenuType created successfully.");
			}
			catch (ArgumentException ex)
			{
				Log.Warning($"Validation error: {ex.Message}");
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				Log.Error($"Unexpected error: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred.");
			}
		}
		/// <summary>
		/// Creates a new MenuItem associated with a MenuType.
		/// </summary>
		/// <param name="createMenuItemDto">The details for the new MenuItem.</param>
		/// <returns>Status 201 if created successfully, 400 if validation fails, or 500 if an error occurs.</returns>
		/// <response code="201">Returns the created MenuItem.</response>
		/// <response code="400">If validation fails.</response>
		/// <response code="500">If an unexpected error occurs.</response>
		[HttpPost]
		[Route("CreateMenuItem")]
		public async Task<IActionResult> CreateMenuItem([FromBody] CreateMenuItemDTO createMenuItemDto)
		{
			Log.Information("Create MenuItem operation initiated.");
			try
			{
				var isCreated = await _menuServices.CreateMenuItem(createMenuItemDto);

				if (!isCreated)
				{
					Log.Error("Failed to create MenuItem due to database error.");
					return StatusCode(500, "An error occurred while creating the MenuItem.");
				}

				Log.Information("MenuItem created successfully.");
				return StatusCode(201, "MenuItem created successfully.");
			}
			catch (ArgumentException ex)
			{
				Log.Warning($"Validation error: {ex.Message}");
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				Log.Error($"Unexpected error: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred.");
			}
		}
		/// <summary>
		/// Updates the specified user's information in the database.
		/// </summary>
		/// <param name="updateUserDto">The details to update for the user.</param>
		/// <returns>Status 200 if successful, 400 if validation fails, or 500 if an error occurs.</returns>
		[HttpPut]
		[Route("[action]")]
		public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUserDto)
		{
			Log.Information($"Updating User with ID {updateUserDto.ID} started.");
			try
			{
				var isUpdated = await _adminServices.UpdateUser(updateUserDto);
				if (!isUpdated) return NotFound($"User with ID {updateUserDto.ID} not found.");

				Log.Information("User updated successfully.");
				return Ok("User updated successfully.");
			}
			catch (ArgumentException ex)
			{
				Log.Warning($"Validation error: {ex.Message}");
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				Log.Error($"Error updating user: {ex.Message}");
				return StatusCode(500, "An error occurred while updating the user.");
			}
		}
		/// <summary>
		/// Updates the specified Lookup Type Name's information in the database.
		/// </summary>
		/// <param name="updateLookupTypeDTO">DTO containing the Lookup Type information to update.</param>
		/// <returns>Status 200 if successful, 400 if validation fails, or 500 if an error occurs.</returns>
		[HttpPut]
		[Route("UpdateLookupTypeName")]
		public async Task<IActionResult> UpdateLookupTypeName([FromBody] UpdateLookupTypeDTO updateLookupTypeDTO)
		{
			Log.Information("Updating Lookup Type Name with ID {ID} started.", updateLookupTypeDTO.ID);
			try
			{
				bool isUpdated = await _lookupServices.UpdateLookupType(updateLookupTypeDTO);

				if (!isUpdated)
				{
					Log.Warning("Lookup Type with ID {ID} not found.", updateLookupTypeDTO.ID);
					return NotFound($"Lookup Type with ID {updateLookupTypeDTO.ID} not found.");
				}

				Log.Information("Lookup Type Name updated successfully.");
				return Ok("Lookup Type Name updated successfully.");
			}
			catch (ArgumentException ex)
			{
				Log.Warning("Validation error: {Message}", ex.Message);
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				Log.Error("Error updating Lookup Type Name: {Message}", ex.Message);
				return StatusCode(500, $"An error occurred while updating the Lookup Type Name: {ex.Message}.");
			}
		}
		/// <summary>
		/// Updates an existing LookupItem.
		/// </summary>
		/// <param name="id">The ID of the LookupItem to update.</param>
		/// <param name="updateLookupItemDto">The new values for the LookupItem.</param>
		/// <returns>Status 200 if updated successfully, 400 if validation fails, or 500 if an error occurs.</returns>
		[HttpPut]
		[Route("UpdateLookupItem/{id}")]
		public async Task<IActionResult> UpdateLookupItem(int id, [FromBody] UpdateLookupItemDTO updateLookupItemDto)
		{
			Log.Information($"Update LookupItem operation initiated for ID: {id}");
			try
			{
				var isUpdated = await _lookupServices.UpdateLookupItem(id, updateLookupItemDto);
				if (!isUpdated)
				{
					Log.Error("Failed to update LookupItem due to database error.");
					return StatusCode(500, "An error occurred while updating the LookupItem.");
				}
				Log.Information($"LookupItem updated successfully for ID: {id}");
				return Ok("LookupItem updated successfully.");
			}
			catch (ArgumentException ex)
			{
				Log.Warning($"Validation error: {ex.Message}");
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				Log.Error($"Unexpected error: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred.");
			}
		}
		/// <summary>
		/// Updates an existing MenuType by its ID.
		/// </summary>
		/// <param name="id">The ID of the MenuType to update.</param>
		/// <param name="updateMenuTypeDto">The details to update the MenuType.</param>
		/// <returns>Status 200 if updated successfully, 404 if not found, or 500 if an error occurs.</returns>
		/// <response code="200">Returns success message.</response>
		/// <response code="404">If the MenuType is not found.</response>
		/// <response code="500">If an unexpected error occurs.</response>
		[HttpPut]
		[Route("UpdateMenuType/{id}")]
		public async Task<IActionResult> UpdateMenuType(int id, [FromBody] UpdateMenuTypeDTO updateMenuTypeDto)
		{
			Log.Information($"Update MenuType operation initiated for ID: {id}");
			try
			{
				var isUpdated = await _menuServices.UpdateMenuType(id, updateMenuTypeDto);

				if (!isUpdated)
				{
					Log.Warning($"Failed to update MenuType with ID: {id}.");
					return NotFound("MenuType not found.");
				}

				Log.Information($"MenuType with ID: {id} updated successfully.");
				return Ok("MenuType updated successfully.");
			}
			catch (Exception ex)
			{
				Log.Error($"Unexpected error: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred.");
			}
		}
		/// <summary>
		/// Updates an existing MenuItem.
		/// </summary>
		/// <param name="id">The ID of the MenuItem to update.</param>
		/// <param name="updateMenuItemDto">The new values for the MenuItem.</param>
		/// <returns>Status 200 if updated successfully, 404 if not found, or 500 if an error occurs.</returns>
		/// <response code="200">Returns success message.</response>
		/// <response code="404">If the MenuItem is not found.</response>
		/// <response code="500">If an unexpected error occurs.</response>
		[HttpPut]
		[Route("UpdateMenuItem/{id}")]
		public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] UpdateMenuItemDTO updateMenuItemDto)
		{
			Log.Information($"Update MenuItem operation initiated for ID: {id}");
			try
			{
				var isUpdated = await _menuServices.UpdateMenuItem(id, updateMenuItemDto);
				if (!isUpdated)
				{
					Log.Warning($"Failed to update MenuItem with ID: {id}.");
					return NotFound("MenuItem not found or could not be updated.");
				}

				Log.Information($"MenuItem with ID: {id} updated successfully.");
				return Ok("MenuItem updated successfully.");
			}
			catch (Exception ex)
			{
				Log.Error($"Unexpected error: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred.");
			}
		}
		/// <summary>
		/// Use This Endpoint To Soft Delete A User By Their ID  
		/// </summary>
		/// <param name="userId">The ID of the user to delete.</param>
		/// <returns>A success message or 404 status code if the user is not found or already deleted.</returns>
		[HttpDelete]
		[Route("[action]/{userId}")]
		public async Task<IActionResult> DeleteUser([FromRoute] int userId)
		{
			Log.Information("Operation of Delete User Has Been Started");
			try
			{
				var isDeleted = await _adminServices.DeleteUser(userId);

				if (!isDeleted)
				{
					Log.Warning($"User with ID {userId} not found or already deleted.");
					return NotFound("User not found or already deleted.");
				}

				Log.Information($"User with ID {userId} has been soft deleted.");
				return Ok("User has been deleted successfully.");
			}
			catch (Exception ex)
			{
				Log.Error($"An Error Occurred When Deleting User with ID {userId}: {ex.Message}");
				return StatusCode(500, $"Error: {ex.Message}");
			}
		}
		/// <summary>
		/// Deletes a LookupType by its ID.
		/// </summary>
		/// <param name="lookupTypeId">The ID of the LookupType to delete.</param>
		/// <returns>Status 200 if successful, 404 if not found, or 500 if an error occurs.</returns>
		[HttpDelete]
		[Route("DeleteLookupType/{lookupTypeId}")]
		public async Task<IActionResult> DeleteLookupType(int lookupTypeId)
		{
			Log.Information($"Starting deletion process for LookupType with ID: {lookupTypeId}");
			try
			{
				var isDeleted = await _lookupServices.DeleteLookupType(lookupTypeId);
				if (!isDeleted)
				{
					Log.Warning($"LookupType with ID {lookupTypeId} not found or already deleted.");
					return NotFound("LookupType not found or already deleted.");
				}

				Log.Information($"LookupType with ID {lookupTypeId} has been soft deleted successfully.");
				return Ok("LookupType has been deleted successfully.");
			}
			catch (Exception ex)
			{
				Log.Error($"An error occurred while deleting LookupType with ID {lookupTypeId}: {ex.Message}");
				return StatusCode(500, "An error occurred while deleting the LookupType.");
			}
		}
		/// <summary>
		/// Deletes a LookupItem by ID (soft delete).
		/// </summary>
		/// <param name="id">The ID of the LookupItem to delete.</param>
		/// <returns>Status 200 if deleted successfully, 400 if validation fails, or 500 if an error occurs.</returns>
		[HttpDelete]
		[Route("DeleteLookupItem/{id}")]
		public async Task<IActionResult> DeleteLookupItemById(int id)
		{
			Log.Information($"Delete LookupItem operation initiated for ID: {id}");
			try
			{
				var isDeleted = await _lookupServices.DeleteLookupItemById(id);

				if (!isDeleted)
				{
					Log.Error("Failed to delete LookupItem due to database error.");
					return StatusCode(500, "An error occurred while deleting the LookupItem.");
				}

				Log.Information($"LookupItem deleted successfully for ID: {id}");
				return Ok("LookupItem deleted successfully.");
			}
			catch (ArgumentException ex)
			{
				Log.Warning($"Validation error: {ex.Message}");
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				Log.Error($"Unexpected error: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred.");
			}
		}
		/// <summary>
		/// Soft deletes a MenuType by its ID.
		/// </summary>
		/// <param name="id">The ID of the MenuType to delete.</param>
		/// <returns>Status 200 if deleted successfully, 404 if not found, or 500 if an error occurs.</returns>
		/// <response code="200">Returns success message.</response>
		/// <response code="404">If the MenuType is not found.</response>
		/// <response code="500">If an unexpected error occurs.</response>
		[HttpDelete]
		[Route("DeleteMenuType/{id}")]
		public async Task<IActionResult> DeleteMenuType(int id)
		{
			Log.Information($"Delete MenuType operation initiated for ID: {id}");
			try
			{
				var isDeleted = await _menuServices.DeleteMenuType(id);

				if (!isDeleted)
				{
					Log.Warning($"Failed to delete MenuType with ID: {id}.");
					return NotFound("MenuType not found.");
				}

				Log.Information($"MenuType with ID: {id} deleted successfully.");
				return Ok("MenuType deleted successfully.");
			}
			catch (Exception ex)
			{
				Log.Error($"Unexpected error: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred.");
			}
		}
		/// <summary>
		/// Deletes a MenuItem by its ID (soft delete).
		/// </summary>
		/// <param name="id">The ID of the MenuItem to delete.</param>
		/// <returns>Status 200 if deleted successfully, 404 if not found, or 500 if an error occurs.</returns>
		/// <response code="200">Returns success message.</response>
		/// <response code="404">If the MenuItem is not found.</response>
		/// <response code="500">If an unexpected error occurs.</response>
		[HttpDelete]
		[Route("DeleteMenuItem/{id}")]
		public async Task<IActionResult> DeleteMenuItem(int id)
		{
			Log.Information($"Delete MenuItem operation initiated for ID: {id}");
			try
			{
				var isDeleted = await _menuServices.DeleteMenuItem(id);
				if (!isDeleted)
				{
					Log.Warning($"Failed to delete MenuItem with ID: {id}.");
					return NotFound("MenuItem not found or could not be deleted.");
				}

				Log.Information($"MenuItem with ID: {id} deleted successfully.");
				return Ok("MenuItem deleted successfully.");
			}
			catch (Exception ex)
			{
				Log.Error($"Unexpected error: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred.");
			}
		}
	}
}
