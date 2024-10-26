using POSAndOrderSystem.DTOs.OrderDTO.Request;

namespace POSAndOrderSystem.DTOs.OrderDTO.Response
{
	public class OrderResponseDTO
	{
		public int OrderID { get; set; }
		public DateTime OrderDate { get; set; }
		public float TotalAmount { get; set; }
		public required string PaymentMethod { get; set; }
		public required string PickUpType { get; set; }
		public string? OrderStatus { get; set; }
		public List<OrderItemDto> OrderItems { get; set; }
		public List<OrderItemDto> SelectedItems { get; set; }
		public string? PaymentStatus { get; internal set; }
	}
}
