using POSAndOrderSystem.DTOs.OrderDTO.Request;

public static class OrderAmountHelper
{
	public static float CalculateTotalAmount(IEnumerable<OrderItemDto> orderItems)
	{
		float totalAmount = 0;

		foreach (var item in orderItems)
		{
			totalAmount += item.Price * item.Quantity;
		}
		return totalAmount;
	}
}
