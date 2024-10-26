namespace POSAndOrderSystem.DTOs.OrderDTO.Request
{
	public class UpdateOrderItemDTO
	{
		public int ItemId { get; set; }
		public int Quantity { get; set; }
		public string? Notes { get; set; }
	}
}
