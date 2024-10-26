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
			var lookupType = await _context.LookupTypes
		.FirstOrDefaultAsync(lt => lt.ID == createLookupItemDto.LookupTypeId);
			if (lookupType != null)
				throw new ArgumentException("Invalid LookupTypeId. LookupType does not exist.");
			var isDuplicate = await _context.LookupItems
				.AnyAsync(li => li.LookupTypeId == createLookupItemDto.LookupTypeId && li.Name == createLookupItemDto.Name && !li.IsActive);
			if (isDuplicate)
				throw new ArgumentException("A LookupItem with this name already exists in the specified LookupType.");
			var newLookupItem = new LookupItem
			{
				LookupTypeId = createLookupItemDto.LookupTypeId,
				Name = createLookupItemDto.Name,
				IsActive = true
			};
			_context.LookupItems.Add(newLookupItem);
			var isSaved = await _context.SaveChangesAsync() > 0;
			return isSaved;
		}

		public async Task<bool> DeleteLookupItemById(int id)
		{
			ValidationHelper.ValidateID(id);
			var lookupItem = await _context.LookupItems.FindAsync(id);
			if (lookupItem == null)
				throw new ArgumentException("LookupItem not found with the provided ID.");
			_context.LookupItems.Remove(lookupItem);
			var isSaved = await _context.SaveChangesAsync() > 0;
			return isSaved;
		}

		// To Delete LookupType From Database
		public async Task<bool> DeleteLookupType(int lookupTypeId)
		{
			var lookupType = await _context.LookupTypes.FindAsync(lookupTypeId);

			if (lookupType == null)
				throw new Exception("This Lookup Id does not exist");
			_context.LookupTypes.Remove(lookupType);
			var changesSaved = await _context.SaveChangesAsync() > 0;
			return changesSaved;
		}

		// To Get All LookupTypes
		public async Task<List<LookupTypeDTO>> GetAllLookupTypes()
		{
			var getAll = from type in _context.LookupTypes
						 select new LookupTypeDTO
						 {
							 ID = type.ID,
							 Name = type.Name,
							 CreationDate = type.CreationDate,
							 IsActive = type.IsActive,
						 };
			return await getAll.ToListAsync();
		}

		// To return specific look up item 
		public async Task<LookupItem> GetLookupItemById(int id)
		{
			ValidationHelper.ValidateID(id);
			var lookupItem = await _context.LookupItems
				.FirstOrDefaultAsync(li => li.ID == id);
			if (lookupItem == null)
				throw new ArgumentException("LookupItem not found with the provided ID.");
			return lookupItem;
		}

		// To return lookup items by lookup type ID
		public async Task<List<LookupItem>> GetLookupItemsByLookupTypeId(int lookupTypeId)
		{
			var lookupType = await _context.LookupTypes
				.FirstOrDefaultAsync(lt => lt.ID == lookupTypeId);
			if (lookupType == null)
				throw new ArgumentException("Invalid LookupTypeId or LookupType does not exist.");
			var lookupItems = await _context.LookupItems
				.Where(li => li.LookupTypeId == lookupTypeId)
				.ToListAsync();
			return lookupItems;
		}

		// Get look up type by their id 
		public async Task<LookupTypeDTO> GetLookupTypeByID(int ID)
		{
			ValidationHelper.ValidateID(ID);
			var res = await _context.LookupTypes.FindAsync(ID);
			if (res == null)
				throw new Exception("Please enter a correct value.");
			return new LookupTypeDTO
			{
				ID = res.ID,
				Name = res.Name,
				CreationDate = res.CreationDate,
				IsActive = res.IsActive
			};
		}

		// To Create new LookupType
		public async Task LookupTypeCreating(CreateLookupTypeDTO createLookupDTO)
		{
			var ID = await _context.LookupTypes.FirstOrDefaultAsync(x => x.ID == createLookupDTO.ID);
			var Name = await _context.LookupTypes.FirstOrDefaultAsync(x => x.Name == createLookupDTO.Name);

			if (ID != null || Name != null)
				throw new Exception($"Lookup Type {createLookupDTO.Name} already exist.");
			var newLookupType = new LookupType
			{
				Name = createLookupDTO.Name
			};
			await _context.LookupTypes.AddAsync(newLookupType);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> UpdateLookupItem(int id, UpdateLookupItemDTO updateLookupItemDto)
		{
			ValidationHelper.ValidateID(id);
			var lookupItem = await _context.LookupItems.FindAsync(id);
			if (lookupItem == null)
				throw new ArgumentException("LookupItem not found with the provided ID.");
			lookupItem.Name = updateLookupItemDto.Name;
			lookupItem.ModificationDate = DateTime.UtcNow;
			var isSaved = await _context.SaveChangesAsync() > 0;
			return isSaved;
		}

		public async Task<bool> UpdateLookupType(UpdateLookupTypeDTO updateLookupTypeDTO)
		{
			var res = await _context.LookupTypes.FindAsync(updateLookupTypeDTO.ID);
			if (res == null)
				throw new Exception($"Lookup Type {updateLookupTypeDTO.ID} does not exist.");
			if (!string.IsNullOrWhiteSpace(updateLookupTypeDTO.Name))
			{
				res.Name = updateLookupTypeDTO.Name;
			}
			res.ModificationDate = DateTime.UtcNow;
			_context.LookupTypes.Update(res);
			return await _context.SaveChangesAsync() > 0;
		}
	}
}
