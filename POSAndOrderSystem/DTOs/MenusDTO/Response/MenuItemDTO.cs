namespace POSAndOrderSystem.DTOs.MenusDTO.Response
{
	public class MenuItemDTO
	{
		public int ID { get; set; }
		public required string MenuItemName { get; set; }
		public float Price { get; set; }
		public DateTime CreationDate { get; set; }
	}
}
