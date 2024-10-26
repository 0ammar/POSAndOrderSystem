using POSAndOrderSystem.DTOs.OrderDTO.Request;
using POSAndOrderSystem.DTOs.OrderDTO.Response;

namespace POSAndOrderSystem.Interfaces
{
	public interface IOrderServices
	{
		Task<OrderResponseDTO> PlaceOrderAsync(OrderDTO orderRequest);
		Task<bool> UpdateOrderStatusAsync(int orderId, int newStatusId);
		Task<bool> UpdatePaymentStatusAsync(int orderId, int newPaymentStatusId);
		Task<bool> UpdateOrderItemsAsync(int orderId, List<UpdateOrderItemDTO> items);
		Task<bool> UpdateCustomerDetailsAsync(int orderId, string address, string notes);
		Task<bool> DeleteOrderByIdAsync(int orderId);
		Task<OrderDetailsResponseDTO> GetOrderByIdAsync(int orderId);
		Task<List<OrderDetailsResponseDTO>> GetAllOrdersAsync();
	}
}
