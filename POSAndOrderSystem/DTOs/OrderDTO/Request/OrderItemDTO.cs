namespace POSAndOrderSystem.DTOs.OrderDTO.Request
{
	public class OrderItemDto
	{
		public int MenuItemID { get; set; }
		public required string MenuItemName { get; set; }
		public float Quantity { get; set; }
		public required float Price { get; set; }
		public string[]? OrderNotes { get; set; }
	}
}
