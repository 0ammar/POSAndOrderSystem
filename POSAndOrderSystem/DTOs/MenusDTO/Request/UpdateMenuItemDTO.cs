namespace POSAndOrderSystem.DTOs.MenusDTO.Request
{
	public class UpdateMenuItemDTO
	{
		public string? MenuItemName { get; set; }
		public float? Price { get; set; }
		public bool? IsActive { get; set; }
	}
}
