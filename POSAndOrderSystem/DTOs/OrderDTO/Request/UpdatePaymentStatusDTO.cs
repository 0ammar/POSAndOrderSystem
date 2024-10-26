namespace POSAndOrderSystem.DTOs.OrderDTO.Request
{
	public class UpdatePaymentStatusDTO
	{
		public int OrderId { get; set; }
		public int NewPaymentStatusId { get; set; }
	}
}
