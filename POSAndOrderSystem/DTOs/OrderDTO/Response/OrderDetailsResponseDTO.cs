namespace POSAndOrderSystem.DTOs.OrderDTO.Response
{
	public class OrderDetailsResponseDTO
	{
		public int OrderID { get; set; }
		public DateTime OrderDate { get; set; }
		public float TotalAmount { get; set; }
		public required string PaymentMethod { get; set; }
		public required string PickUpType { get; set; }
		public string? OrderStatus { get; set; }
		public string? PaymentStatus { get; set; }
		public List<OrderItemResponseDTO> OrderItems { get; set; }
	}
}