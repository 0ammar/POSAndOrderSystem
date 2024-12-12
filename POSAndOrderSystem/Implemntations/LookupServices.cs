using Microsoft.EntityFrameworkCore;
using POSAndOrderSystem.DbContexts;
using POSAndOrderSystem.DTOs.LookupsDTO.Request;
using POSAndOrderSystem.DTOs.LookupsDTO.Response;
using POSAndOrderSystem.Entities;
using POSAndOrderSystem.Helpers;
using POSAndOrderSystem.Interfaces;

namespace POSAndOrderSystem.Implemntations
{
	public class LookupServices : ILookupServices
	{
		private readonly POSAndOrderContext _context;
		public LookupServices(POSAndOrderContext context)
		{
			_context = context;
		}

		// To create new Lookup Item inside system
		public async Task<bool> CreateLookupItem(CreateLookupItemDTO createLookupItemDto)
		{
			if (createLookupItemDto == null)
				throw new Exception(nameof(createLookupItemDto));
			var lookupTypeExists = await _context.LookupTypes
				.AnyAsync(lt => lt.ID == createLookupItemDto.LookupTypeId);
			if (!lookupTypeExists)
				throw new Exception($"LookupType with ID {createLookupItemDto.LookupTypeId} not found.");
			var newLookupItem = new LookupItem
			{
				LookupTypeId = createLookupItemDto.LookupTypeId,
				Name = createLookupItemDto.Name,
			};
			_context.LookupItems.Add(newLookupItem);
			return await _context.SaveChangesAsync() > 0;
		}

		// Delete a specifiv lookup item
		public async Task<bool> DeleteLookupItemById(int id)
		{
			ValidationHelper.ValidateID(id);
			var lookupItem = await _context.LookupItems.FindAsync(id);
			if (lookupItem == null)
				throw new Exception("LookupItem not found with the provided ID.");
			_context.LookupItems.Remove(lookupItem);
			var isSaved = await _context.SaveChangesAsync() > 0;
			return isSaved;
		}

		// To Get All LookupTypes
		public async Task<List<LookupTypeDTO>> GetAllLookupTypes()
		{
			var getAll = from type in _context.LookupTypes
						 select new LookupTypeDTO
						 {
							 ID = type.ID,
							 Name = type.Name,
						 };
			return await getAll.ToListAsync();
		}

		// To return lookup items by lookup type ID
		public async Task<List<LookupItem>> GetLookupItemsByLookupTypeId(int lookupTypeId)
		{
			var lookupType = await _context.LookupTypes.FirstOrDefaultAsync(lt => lt.ID == lookupTypeId);
			if (lookupType == null)
				throw new Exception("Invalid LookupTypeId or LookupType does not exist.");
			var lookupItems = await (from li in _context.LookupItems
									 where li.LookupTypeId == lookupTypeId
									 select li).ToListAsync();
			return lookupItems;
		}

		// Update a specific lookup item
		public async Task<bool> UpdateLookupItem(int id, UpdateLookupItemDTO updateLookupItemDto)
		{
			if (updateLookupItemDto == null)
				throw new ArgumentNullException(nameof(updateLookupItemDto), "The update data cannot be null.");

			ValidationHelper.ValidateID(id);
			var lookupItem = await _context.LookupItems.FindAsync(id);
			if (lookupItem == null)
				throw new Exception("LookupItem not found with the provided ID.");

			lookupItem.Name = updateLookupItemDto.Name;
			var isSaved = await _context.SaveChangesAsync() > 0;
			return isSaved;
		}
	}
}
