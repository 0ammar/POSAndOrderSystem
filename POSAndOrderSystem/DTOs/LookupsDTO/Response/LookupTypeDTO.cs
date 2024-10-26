namespace POSAndOrderSystem.DTOs.LookupsDTO.Response
{
	public class LookupTypeDTO
	{
		public int ID { get; set; }
		public required string Name { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreationDate { get; set; }
	}
}
