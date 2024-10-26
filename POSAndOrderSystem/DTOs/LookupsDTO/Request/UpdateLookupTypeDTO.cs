namespace POSAndOrderSystem.DTOs.LookupsDTO.Request
{
	public class UpdateLookupTypeDTO
	{
		public int ID { get; set; }
		public required string Name { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreationDate { get; set; }
	}
}
