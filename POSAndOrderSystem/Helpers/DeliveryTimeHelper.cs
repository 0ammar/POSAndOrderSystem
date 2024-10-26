using POSAndOrderSystem.DTOs.OrderDTO.Request;

namespace POSAndOrderSystem.Helpers
{
	public static class DeliveryTimeHelper
	{
		private const int DefaultDeliveryTime = 30;
		private const int SpecialDeliveryTime = 40;

		public static DateTime GetEstimatedDeliveryTime(List<OrderItemDto> orderItems)
		{
			foreach (var item in orderItems)
			{
				if (item.MenuItemName.Equals("Proasted Chicken", StringComparison.OrdinalIgnoreCase) ||
					item.MenuItemName.Equals("Chicken On Charcoal", StringComparison.OrdinalIgnoreCase))
				{
					return DateTime.UtcNow.AddMinutes(SpecialDeliveryTime);
				}
			}
			return DateTime.UtcNow.AddMinutes(DefaultDeliveryTime);
		}
	}
}
