namespace POSAndOrderSystem.DTOs.OrderDTO.Request
{
	public class UpdateCustomerDetailsDTO
	{
		public int OrderId { get; set; }
		public string? Address { get; set; }
		public string? Notes { get; set; }
	}
}
