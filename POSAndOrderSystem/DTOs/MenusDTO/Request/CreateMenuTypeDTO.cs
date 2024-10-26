namespace POSAndOrderSystem.DTOs.MenusDTO.Request
{
	public class CreateMenuTypeDTO
	{
		public required string MenuTypeName { get; set; }
		public string? Image { get; set; }
	}
}
