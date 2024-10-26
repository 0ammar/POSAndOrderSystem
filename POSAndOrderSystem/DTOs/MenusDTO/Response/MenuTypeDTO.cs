namespace POSAndOrderSystem.DTOs.MenusDTO.Response
{
	public class MenuTypeDTO
	{
		public int ID { get; set; }
		public required string MenuTypeName { get; set; }
		public string? Image { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreationDate { get; set; }
	}
}
