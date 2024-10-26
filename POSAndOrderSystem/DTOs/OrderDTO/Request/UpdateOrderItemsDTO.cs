namespace POSAndOrderSystem.DTOs.OrderDTO.Request
{
	namespace POSAndOrderSystem.DTOs.OrderDTO.Request
	{
		public class UpdateOrderItemsDTO
		{
			public int OrderId { get; set; }
			public List<UpdateOrderItemDTO>? Items { get; set; }
		}
	}

}
