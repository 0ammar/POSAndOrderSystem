namespace POSAndOrderSystem.Entities
{
	public class Order : MainEntity
	{
		// Foreign Keys and related properties
		public int UserID { get; set; }
		public int OrderStatusID { get; set; } // Default: pending
		public int PaymentMethodID { get; set; } // Default: cash
		public int PaymentStatusID { get; set; } // Default: unpaid
		public int PickUpTypeID { get; set; } // Default: delivery

		// Order details
		public DateTime OrderDate { get; set; } = DateTime.UtcNow; // Auto-generated when order is created
		public DateTime? PickupTime { get; set; } // When paymentStatus value be paid
		public DateTime EstimatedDeliveryTime { get; set; } // If the order contains any of those menu types Chicken On Charcoal or Prorated Chicken
		public DateTime CancellationAllowedUntil { get; set; } // The customer has the ability to cancle order within 5 minutes from now

		// Calculated properties
		public float TotalAmount { get; set; } // From Calculating all menu items prices 
		public string? OrderNotes { get; set; }

		// User details
		public string UserFirstName { get; set; } = string.Empty; // Required user detail , Get those information from user class
		public string UserLastName { get; set; } = string.Empty; // Required user detail  , Get those information from user class
		public string UserAddress { get; set; } = string.Empty; // Required user detail	  , Get those information from user class
		public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
	}
}
