using POSAndOrderSystem.DTOs.LookupsDTO.Request;
using POSAndOrderSystem.DTOs.LookupsDTO.Response;
using POSAndOrderSystem.Entities;

namespace POSAndOrderSystem.Interfaces
{
	public interface ILookupServices
	{
		Task<List<LookupTypeDTO>> GetAllLookupTypes();
		Task<List<LookupItem>> GetLookupItemsByLookupTypeId(int lookupTypeId);
		Task<bool> CreateLookupItem(CreateLookupItemDTO createLookupItemDto);
		Task<bool> UpdateLookupItem(int id, UpdateLookupItemDTO updateLookupItemDto);
		Task<bool> DeleteLookupItemById(int id);
	}
}