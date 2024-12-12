using POSAndOrderSystem.Enums;

namespace POSAndOrderSystem.Entities
{
	public class Order : MainEntity
	{
		// Order details
		public required float TotalAmount { get; set; }
		public string OrderNotes { get; set; }

		// User details
		public string UserName { get; set; }

		// Foreign Keys and related properties
		public required int UserID { get; set; }
		public required int OrderStatusID { get; set; } = (int)OrderStatus.Pending;
		public required int PaymentMethodID { get; set; } = (int)PaymentMethod.Cash;
		public required int PaymentStatusID { get; set; } = (int)PaymentStatus.Unpaid;
		public required int PickUpTypeID { get; set; } = (int)PickUpType.Delivery;

		// Order Items List
		public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
	}
}
