namespace POSAndOrderSystem.DTOs.LookupsDTO.Request
{
	public class CreateLookupItemDTO
	{
		public int LookupTypeId { get; set; }
		public required string Name { get; set; }
	}
}
