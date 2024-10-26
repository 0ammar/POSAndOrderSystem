using Microsoft.EntityFrameworkCore;
using POSAndOrderSystem.DbContexts;
using POSAndOrderSystem.DTOs.OrderDTO.Request;
using POSAndOrderSystem.DTOs.OrderDTO.Response;
using POSAndOrderSystem.Entities;
using POSAndOrderSystem.Helpers; // Ensure to include the helper classes
using POSAndOrderSystem.Interfaces;

namespace POSAndOrderSystem.Services
{
	public class OrderService : IOrderServices
	{
		private readonly POSAndOrderContext _context;
		public OrderService(POSAndOrderContext context)
		{
			_context = context;
		}
		// Fetch A specific order by their ID 
		public async Task<OrderDetailsResponseDTO> GetOrderByIdAsync(int orderId)
		{
			var orderDetails = await (from o in _context.Orders
									  where o.ID == orderId
									  select new OrderDetailsResponseDTO
									  {
										  OrderID = o.ID,
										  OrderDate = o.OrderDate,
										  TotalAmount = o.TotalAmount,
										  PaymentMethod = (from li in _context.LookupItems
														   where li.ID == o.PaymentMethodID
														   select li.Description).FirstOrDefault() ?? "Unknown Payment Method",
										  PickUpType = (from li in _context.LookupItems
														where li.ID == o.PickUpTypeID
														select li.Description).FirstOrDefault() ?? "Unknown Pick-Up Type",
										  OrderStatus = (from li in _context.LookupItems
														 where li.ID == o.OrderStatusID
														 select li.Description).FirstOrDefault(),
										  PaymentStatus = (from li in _context.LookupItems
														   where li.ID == o.PaymentStatusID
														   select li.Description).FirstOrDefault(),
										  OrderItems = (from oi in o.OrderItems
														select new OrderItemResponseDTO
														{
															MenuItemID = oi.MenuItemID,
															MenuItemName = oi.MenuItemName,
															Price = oi.Price,
															Quantity = oi.Quantity
														}).ToList()
									  }).FirstOrDefaultAsync();
			if (orderDetails == null)
				throw new Exception("Order not found.");
			return orderDetails;
		}

		// Place new order in the system 
		public async Task<OrderResponseDTO> PlaceOrderAsync(OrderDTO orderRequest)
		{
			var userQuery = _context.Users
				.Where(u => u.ID == orderRequest.UserID)
			.Select(u => new { u.ID, u.FirstName, u.LastName, u.Address });
			var user = await userQuery.FirstOrDefaultAsync();
			if (user == null)
				throw new Exception("User not found.");
			var selectedMenuTypeQuery = _context.MenuTypes
				.Where(mt => orderRequest.MenuTypeIDs.Contains(mt.ID));
			var selectedMenuTypes = await selectedMenuTypeQuery.ToListAsync();
			if (!selectedMenuTypes.Any())
				throw new Exception("Invalid or unavailable menu types.");
			var selectedMenuItemsQuery = _context.MenuItems
				.Where(mi => orderRequest.SelectedMenuItemIDs.Contains(mi.ID) &&
							 orderRequest.MenuTypeIDs.Contains(mi.MenuTypeID))
				.Select(mi => new { mi.ID, mi.MenuItemName, mi.Price });
			var selectedMenuItems = await selectedMenuItemsQuery.ToListAsync();
			if (!selectedMenuItems.Any())
				throw new Exception("No valid items selected in the specified menu type.");
			List<OrderItemDto> orderItems = new List<OrderItemDto>();
			for (int i = 0; i < selectedMenuItems.Count; i++)
			{
				var item = selectedMenuItems[i];
				var orderItem = new OrderItemDto
				{
					MenuItemID = item.ID,
					MenuItemName = item.MenuItemName,
					Quantity = 1,
					Price = item.Price,
					OrderNotes = orderRequest.OrderItemNotes
				};
				orderItems.Add(orderItem);
			}
			var totalAmount = OrderAmountHelper.CalculateTotalAmount(orderItems);
			orderRequest.TotalAmount = totalAmount;
			var paymentMethodId = orderRequest.PaymentMethodID != 0 ? orderRequest.PaymentMethodID : 9;
			var pickupTypeId = orderRequest.PickUpTypeID != 0 ? orderRequest.PickUpTypeID : 15;
			var paymentMethodQuery = _context.LookupItems
				.Where(li => li.ID == paymentMethodId)
				.Select(li => li.Description);
			var pickupTypeQuery = _context.LookupItems
				.Where(li => li.ID == pickupTypeId)
				.Select(li => li.Description);
			var orderStatusQuery = _context.LookupItems
				.Where(li => li.ID == 5)
				.Select(li => li.Description);
			var paymentStatusId = orderRequest.PaymentStatusID != 0 ? orderRequest.PaymentStatusID : 13;
			var paymentStatusQuery = _context.LookupItems
				.Where(li => li.ID == paymentStatusId)
				.Select(li => li.Description);
			var paymentMethod = await paymentMethodQuery.FirstOrDefaultAsync();
			var pickupType = await pickupTypeQuery.FirstOrDefaultAsync();
			var orderStatus = await orderStatusQuery.FirstOrDefaultAsync();
			var paymentStatus = await paymentStatusQuery.FirstOrDefaultAsync();
			var order = new Order
			{
				UserID = user.ID,
				UserFirstName = user.FirstName,
				UserLastName = user.LastName,
				UserAddress = user.Address,
				OrderNotes = orderRequest.OrderNotes,
				PaymentMethodID = paymentMethodId,
				PickUpTypeID = pickupTypeId,
				OrderStatusID = 5, // Default status
				PaymentStatusID = paymentStatusId,
				TotalAmount = totalAmount,
				OrderItems = orderItems.Select(item => new OrderItem
				{
					MenuItemID = item.MenuItemID,
					MenuItemName = item.MenuItemName,
					Quantity = item.Quantity,
					OrderItemNotes = item.OrderNotes?.ToString()
				}).ToList()
			};
			order.PickupTime = DeliveryTimeHelper.GetEstimatedDeliveryTime(orderItems);
			PaymentStatusHelper.HandlePaymentStatus(order, orderRequest.IsPaid == false);
			await _context.Orders.AddAsync(order);
			await _context.SaveChangesAsync();
			return new OrderResponseDTO
			{
				OrderID = order.ID,
				OrderDate = order.OrderDate,
				TotalAmount = order.TotalAmount,
				PaymentMethod = paymentMethod,
				PickUpType = pickupType,
				OrderStatus = orderStatus,
				PaymentStatus = paymentStatus,
				SelectedItems = orderItems
			};
		}
		// fetch all  orders from database 
		public async Task<List<OrderDetailsResponseDTO>> GetAllOrdersAsync()
		{
			var orders = await (from o in _context.Orders
								select new OrderDetailsResponseDTO
								{
									OrderID = o.ID,
									OrderDate = o.OrderDate,
									TotalAmount = o.TotalAmount,
									PaymentMethod = (from li in _context.LookupItems
													 where li.ID == o.PaymentMethodID
													 select li.Description).FirstOrDefault() ?? "Unknown Payment Method",
									PickUpType = (from li in _context.LookupItems
												  where li.ID == o.PickUpTypeID
												  select li.Description).FirstOrDefault() ?? "Unknown Pick-Up Type",
									OrderStatus = (from li in _context.LookupItems
												   where li.ID == o.OrderStatusID
												   select li.Description).FirstOrDefault() ?? "Unknown Order Status",
									PaymentStatus = (from li in _context.LookupItems
													 where li.ID == o.PaymentStatusID
													 select li.Description).FirstOrDefault() ?? "Unknown Payment Status",
									OrderItems = (from oi in o.OrderItems
												  select new OrderItemResponseDTO
												  {
													  MenuItemID = oi.MenuItemID,
													  MenuItemName = oi.MenuItemName,
													  Price = oi.Price,
													  Quantity = oi.Quantity
												  }).ToList()
								}).ToListAsync();
			return orders;
		}
		// Update Specific Order Status 
		public async Task<bool> UpdateOrderStatusAsync(int orderId, int newStatusId)
		{
			var order = await (from o in _context.Orders
							   where o.ID == orderId
							   select o).FirstOrDefaultAsync();
			if (order == null)
				return false;
			order.OrderStatusID = newStatusId;
			_context.Orders.Update(order);
			await _context.SaveChangesAsync();
			return true;
		}

		// Update Specific Order Payment Status
		public async Task<bool> UpdatePaymentStatusAsync(int orderId, int newPaymentStatusId)
		{
			var order = await (from o in _context.Orders
							   where o.ID == orderId
							   select o).FirstOrDefaultAsync();
			if (order == null)
				return false;
			order.PaymentStatusID = newPaymentStatusId;
			await _context.SaveChangesAsync();
			return true;
		}
		// Update Order Items for specifc order
		public async Task<bool> UpdateOrderItemsAsync(int orderId, List<UpdateOrderItemDTO> items)
		{
			var order = await _context.Orders
				.Include(o => o.OrderItems)
				.FirstOrDefaultAsync(o => o.ID == orderId);
			if (order == null)
				return false;
			foreach (var item in items)
			{
				var orderItem = order.OrderItems.FirstOrDefault(oi => oi.MenuItemID == item.ItemId);

				if (orderItem != null)
				{
					orderItem.Quantity = item.Quantity;
					orderItem.Notes = item.Notes!;
				}
			}
			await _context.SaveChangesAsync();
			return true;
		}
		// Update the user information for a specific order 
		public async Task<bool> UpdateCustomerDetailsAsync(int orderId, string address, string notes)
		{
			var order = await _context.Orders.FindAsync(orderId);
			if (order == null)
				return false;
			order.UserAddress = address;
			await _context.SaveChangesAsync();
			return true;
		}

		// Delete Order By Their ID
		public async Task<bool> DeleteOrderByIdAsync(int orderId)
		{
			var order = await _context.Orders.FindAsync(orderId);
			if (order == null)
				return false;
			_context.Orders.Remove(order);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}

