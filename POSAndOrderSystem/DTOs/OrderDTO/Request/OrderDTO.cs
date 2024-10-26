namespace POSAndOrderSystem.DTOs.OrderDTO.Request
{
	public class OrderDTO
	{
		public int UserID { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Address { get; set; }
		public required List<int> MenuTypeIDs { get; set; }
		public required List<int> SelectedMenuItemIDs { get; set; }
		public string? OrderNotes { get; set; }
		public string[]? OrderItemNotes { get; set; }
		public int OrderStatusID { get; internal set; }
		public int PaymentMethodID { get; set; }
		public int PickUpTypeID { get; set; }
		public DateTime? PickupTime { get; set; }
		public int PaymentStatusID { get; internal set; }
		public float TotalAmount { get; internal set; }
		public bool IsPaid { get; internal set; }
		public int MenuTypeID { get; internal set; }
	}
}
