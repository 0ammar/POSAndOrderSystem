using POSAndOrderSystem.DTOs.LookupsDTO.Request;
using POSAndOrderSystem.DTOs.LookupsDTO.Response;
using POSAndOrderSystem.Entities;

namespace POSAndOrderSystem.Interfaces
{
	public interface ILookupServices
	{
		Task LookupTypeCreating(CreateLookupTypeDTO createLookupDTO);
		Task<bool> UpdateLookupType(UpdateLookupTypeDTO createLookupDTO);
		Task<bool> DeleteLookupType(int lookupTypeId);
		Task<LookupTypeDTO> GetLookupTypeByID(int ID);
		Task<List<LookupTypeDTO>> GetAllLookupTypes();
		Task<bool> CreateLookupItem(CreateLookupItemDTO createLookupItemDto);
		Task<bool> UpdateLookupItem(int id, UpdateLookupItemDTO updateLookupItemDto);
		Task<bool> DeleteLookupItemById(int id);
		Task<LookupItem> GetLookupItemById(int id);
		Task<List<LookupItem>> GetLookupItemsByLookupTypeId(int lookupTypeId);
	}
}