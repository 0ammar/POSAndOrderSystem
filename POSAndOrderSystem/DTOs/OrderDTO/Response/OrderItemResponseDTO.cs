namespace POSAndOrderSystem.DTOs.OrderDTO.Response
{
	public class OrderItemResponseDTO
	{
		public int MenuItemID { get; set; }
		public required string MenuItemName { get; set; }
		public float Price { get; set; }
		public float Quantity { get; set; }
	}
}
