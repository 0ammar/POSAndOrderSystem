using Microsoft.AspNetCore.Mvc;
using POSAndOrderSystem.DTOs.LookupsDTO.Request;
using POSAndOrderSystem.DTOs.MenusDTO.Request;
using POSAndOrderSystem.DTOs.User.Request;
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
		/// Use This Endpoint To Get All Users From The Database  
		/// </summary>
		/// <returns>A list of users or 500 status code if an error occurs.</returns>
		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> GetAllUsers([FromHeader] string token)
		{
			Log.Information("Operation of Get All User Has Been Started");
			if (!await TokenHelper.ValidateToken(token, "Admin"))
				return Unauthorized("You haven't an access for this operation");
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
		public async Task<IActionResult> GetAllLookupTypes([FromHeader] string token)
		{
			Log.Information("Operation of Get All Lookuptypes Has Been Started");
			if (!await TokenHelper.ValidateToken(token, "Admin"))
				return Unauthorized("You haven't an access for this operation");
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
		public async Task<IActionResult> GetLookupItemsByLookupTypeId(int lookupTypeId, [FromHeader] string token)
		{
			Log.Information($"Get LookupItems for LookupTypeId {lookupTypeId} operation initiated.");
			if (!await TokenHelper.ValidateToken(token, "Admin"))
				return Unauthorized("You haven't an access for this operation");
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
		/// Use this endpoint to create a new user in the system.
		/// </summary>
		/// <param name="input">The user details to create the new user.</param>
		/// <returns>Returns a status indicating whether the creation was successful or failed.</returns>
		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> CreateNewUser([FromBody] CreateUserDTO input, [FromHeader] string token)
		{
			Log.Information("Operation to create a new user has started.");
			if (!await TokenHelper.ValidateToken(token, "Admin"))
				return Unauthorized("You haven't an access for this operation");
			try
			{
				await _adminServices.CreateUser(input);
				Log.Information("New user {FirstName} {LastName} created successfully.", input.Name);
				// Respond with 201 (Created) after successful creation
				return StatusCode(201, new { message = "New user has been created successfully." });
			}
			catch (Exception ex)
			{
				Log.Error(ex, "An error occurred while creating a new user.");
				// Respond with 500 (Internal Server Error) in case of failure
				return StatusCode(500, new { error = $"Error: {ex.Message}" });
			}
		}
		/// <summary>
		/// Creates a new LookupItem under the specified LookupType.
		/// </summary>
		/// <param name="createLookupItemDto">The details for the new LookupItem.</param>
		/// <returns>Status 201 if created successfully, 400 if validation fails, or 500 if an error occurs.</returns>
		[HttpPost]
		[Route("CreateLookupItem")]
		public async Task<IActionResult> CreateLookupItem([FromBody] CreateLookupItemDTO createLookupItemDto, [FromHeader] string token)
		{
			Log.Information("Create LookupItem operation initiated.");
			if (!await TokenHelper.ValidateToken(token, "Admin"))
				return Unauthorized("You haven't an access for this operation");
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
				return StatusCode(500, ex.InnerException!.Message);
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
		public async Task<IActionResult> CreateMenuType([FromBody] CreateMenuTypeDTO createMenuTypeDto, [FromHeader] string token)
		{
			Log.Information("Create MenuType operation initiated.");
			if (!await TokenHelper.ValidateToken(token, "Admin"))
				return Unauthorized("You haven't an access for this operation");
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
		public async Task<IActionResult> CreateMenuItem([FromBody] CreateMenuItemDTO createMenuItemDto, [FromHeader] string token)
		{
			Log.Information("Create MenuItem operation initiated.");
			if (!await TokenHelper.ValidateToken(token, "Admin"))
				return Unauthorized("You haven't an access for this operation");
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
		public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUserDto, [FromHeader] string token)
		{
			Log.Information($"Updating User with ID {updateUserDto.ID} started.");
			if (!await TokenHelper.ValidateToken(token, "Admin"))
				return Unauthorized("You haven't an access for this operation");
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
		/// Updates an existing LookupItem.
		/// </summary>
		/// <param name="id">The ID of the LookupItem to update.</param>
		/// <param name="updateLookupItemDto">The new values for the LookupItem.</param>
		/// <returns>Status 200 if updated successfully, 400 if validation fails, or 500 if an error occurs.</returns>
		[HttpPut]
		[Route("UpdateLookupItem/{id}")]
		public async Task<IActionResult> UpdateLookupItem(int id, [FromBody] UpdateLookupItemDTO updateLookupItemDto, [FromHeader] string token)
		{
			Log.Information($"Update LookupItem operation initiated for ID: {id}");
			if (!await TokenHelper.ValidateToken(token, "Admin"))
				return Unauthorized("You haven't an access for this operation");
			if (updateLookupItemDto == null)
				return BadRequest("The request body cannot be null.");

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
				return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
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
		public async Task<IActionResult> UpdateMenuType(int id, [FromBody] UpdateMenuTypeDTO updateMenuTypeDto, [FromHeader] string token)
		{
			Log.Information($"Update MenuType operation initiated for ID: {id}");
			if (!await TokenHelper.ValidateToken(token, "Admin"))
				return Unauthorized("You haven't an access for this operation");
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
		public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] UpdateMenuItemDTO updateMenuItemDto, [FromHeader] string token)
		{
			Log.Information($"Update MenuItem operation initiated for ID: {id}");
			if (!await TokenHelper.ValidateToken(token, "Admin"))
				return Unauthorized("You haven't an access for this operation");
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
		public async Task<IActionResult> DeleteUser([FromRoute] int userId, [FromHeader] string token)
		{
			Log.Information("Operation of Delete User Has Been Started");
			if (!await TokenHelper.ValidateToken(token, "Admin"))
				return Unauthorized("You haven't an access for this operation");
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
		/// Deletes a LookupItem by ID (soft delete).
		/// </summary>
		/// <param name="id">The ID of the LookupItem to delete.</param>
		/// <returns>Status 200 if deleted successfully, 400 if validation fails, or 500 if an error occurs.</returns>
		[HttpDelete]
		[Route("DeleteLookupItem/{id}")]
		public async Task<IActionResult> DeleteLookupItemById(int id, [FromHeader] string token)
		{
			Log.Information($"Delete LookupItem operation initiated for ID: {id}");
			if (!await TokenHelper.ValidateToken(token, "Admin"))
				return Unauthorized("You haven't an access for this operation");
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
		public async Task<IActionResult> DeleteMenuType(int id, [FromHeader] string token)
		{
			Log.Information($"Delete MenuType operation initiated for ID: {id}");
			if (!await TokenHelper.ValidateToken(token, "Admin"))
				return Unauthorized("You haven't an access for this operation");
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
		public async Task<IActionResult> DeleteMenuItem(int id, [FromHeader] string token)
		{
			Log.Information($"Delete MenuItem operation initiated for ID: {id}");
			if (!await TokenHelper.ValidateToken(token, "Admin"))
				return Unauthorized("You haven't an access for this operation");
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
