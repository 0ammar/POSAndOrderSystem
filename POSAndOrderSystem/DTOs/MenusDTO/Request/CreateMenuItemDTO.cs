namespace POSAndOrderSystem.DTOs.MenusDTO.Request
{
	public class CreateMenuItemDTO
	{
		public required string MenuItemName { get; set; }
		public float Price { get; set; }
		public int MenuTypeID { get; set; }
	}
}
