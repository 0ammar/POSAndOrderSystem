using POSAndOrderSystem.Entities;

namespace POSAndOrderSystem.Helpers
{
	public static class PaymentStatusHelper
	{
		public static void HandlePaymentStatus(Order order, bool isPaid)
		{
			order.PaymentStatusID = isPaid ? 12 : 13;
			if (isPaid)
			{
				order.PickupTime = DateTime.UtcNow;
				order.OrderStatusID = 7;
			}
			else
			{
				order.PickupTime = null;
				order.OrderStatusID = 5;
			}
		}
	}
}
