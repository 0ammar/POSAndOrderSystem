using Microsoft.AspNetCore.Mvc;
using POSAndOrderSystem.DTOs.OrderDTO.Request;
using POSAndOrderSystem.DTOs.OrderDTO.Request.POSAndOrderSystem.DTOs.OrderDTO.Request;
using POSAndOrderSystem.Helpers;
using POSAndOrderSystem.Interfaces;
using Serilog;

namespace POSAndOrderSystem.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CustomerController : ControllerBase
	{
		private readonly IOrderServices _orderService;

		public CustomerController(IOrderServices orderService)
		{
			_orderService = orderService;
		}
		/// <summary>
		/// Retrieves an order by its ID.
		/// </summary>
		/// <param name="orderId">The ID of the order.</param>
		/// <returns>An IActionResult representing the order details.</returns>
		[HttpGet]
		[Route("[action]/{orderId}")]
		public async Task<IActionResult> GetOrderById([FromRoute] int orderId)
		{
			try
			{
				Log.Information("Fetching order with ID: {OrderId}", orderId);
				var orderDetails = await _orderService.GetOrderByIdAsync(orderId);
				return Ok(orderDetails);
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Error fetching order with ID: {OrderId}", orderId);
				return NotFound(new { message = "Order not found." });
			}
		}
		/// <summary>
		/// Retrieves all orders.
		/// </summary>
		/// <returns>A list of all orders.</returns>
		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> GetAllOrders([FromHeader] string token)
		{
			try
			{
				if (!await TokenHelper.ValidateToken(token, "Customer"))
				{
					return Unauthorized("You Are Not Autharized For Get Department");
				}
				var orders = await _orderService.GetAllOrdersAsync();
				if (orders == null || !orders.Any())
				{
					Log.Warning("No orders found.");
					return NotFound(new { message = "No orders found." });
				}
				Log.Information("Retrieved all orders successfully.");
				return Ok(orders);
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Error retrieving all orders.");
				return StatusCode(500, new { message = "An error occurred while retrieving orders." });
			}
		}
		// <summary>
		/// Places an order based on the provided order request.
		/// </summary>
		/// <param name="orderRequest">The order request containing order details.</param>
		/// <returns>An IActionResult representing the result of the operation.</returns>
		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> PlaceOrder([FromBody] OrderDTO orderRequest, [FromHeader] string token)
		{
			if (orderRequest == null)
			{
				Log.Warning("Order request was null.");
				return BadRequest(new { message = "Order request cannot be null." });
			}
			try
			{
				if (!await TokenHelper.ValidateToken(token, "Customer"))
				{
					return Unauthorized("You Are Not Autharized For Get Department");
				}
				Log.Information("Received order request from user ID: {UserId}", orderRequest.UserID);
				var orderResponse = await _orderService.PlaceOrderAsync(orderRequest);
				Log.Information("Order placed successfully with ID: {OrderId}", orderResponse.OrderID);
				return Ok(orderResponse);
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Error placing order for user ID: {UserId}", orderRequest.UserID);
				return StatusCode(500, new { message = "An error occurred while processing your request." });
			}
		}
		/// <summary>
		/// Updates the status of an order.
		/// </summary>
		/// <param name="orderId">The ID of the order to update.</param>
		/// <param name="newStatusId">The new status ID to set for the order.</param>
		/// <returns>An IActionResult indicating the success or failure of the update operation.</returns>
		/// <response code="200">Returns a success message if the order status was updated.</response>
		/// <response code="404">Returns a not found message if the order was not found.</response>
		/// <response code="500">Returns a generic error message if an internal server error occurs.</response>
		[HttpPut]
		[Route("[action]")]
		public async Task<IActionResult> UpdateOrderStatus(int orderId, int newStatusId, [FromHeader] string token)
		{
			try
			{
				if (!await TokenHelper.ValidateToken(token, "Customer"))
				{
					return Unauthorized("You Are Not Autharized For Get Department");
				}
				Log.Information("Attempting to update order status. Order ID: {OrderId}, New Status ID: {NewStatusId}", orderId, newStatusId);
				bool isUpdated = await _orderService.UpdateOrderStatusAsync(orderId, newStatusId);

				if (isUpdated)
				{
					Log.Information("Order status updated successfully. Order ID: {OrderId}, New Status ID: {NewStatusId}", orderId, newStatusId);
					return Ok(new { message = "Order status updated successfully." });
				}
				else
				{
					Log.Warning("Order not found. Order ID: {OrderId}", orderId);
					return NotFound(new { message = "Order not found." });
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, "An error occurred while updating the order status. Order ID: {OrderId}", orderId);
				return StatusCode(500, new { message = "An error occurred while processing your request." });
			}
		}
		/// <summary>
		/// Updates the payment status of an order.
		/// </summary>
		/// <param name="request">An UpdatePaymentStatusDTO containing order ID and new payment status ID.</param>
		/// <returns>An IActionResult indicating the success or failure of the update operation.</returns>
		/// <response code="200">Returns success if the payment status was updated.</response>
		/// <response code="404">Returns not found if the order was not found.</response>
		/// <response code="500">Returns a generic error if an internal server error occurs.</response>
		[HttpPut]
		[Route("[action]")]
		public async Task<IActionResult> UpdatePaymentStatus([FromBody] UpdatePaymentStatusDTO request)
		{
			if (request == null)
			{
				Log.Warning("UpdatePaymentStatus request is null.");
				return BadRequest(new { message = "Request cannot be null." });
			}
			try
			{
				Log.Information("Attempting to update payment status. Order ID: {OrderId}, New Payment Status ID: {NewPaymentStatusId}", request.OrderId, request.NewPaymentStatusId);
				bool isUpdated = await _orderService.UpdatePaymentStatusAsync(request.OrderId, request.NewPaymentStatusId);
				if (isUpdated)
				{
					Log.Information("Payment status updated successfully. Order ID: {OrderId}", request.OrderId);
					return Ok(new { message = "Payment status updated successfully." });
				}
				else
				{
					Log.Warning("Order not found. Order ID: {OrderId}", request.OrderId);
					return NotFound(new { message = "Order not found." });
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Error updating payment status for Order ID: {OrderId}", request.OrderId);
				return StatusCode(500, new { message = "An error occurred while processing your request." });
			}
		}
		// <summary>
		/// Updates items in an order, including quantity and notes.
		/// </summary>
		/// <param name="request">An UpdateOrderItemsDTO containing order ID and the updated items list.</param>
		/// <returns>An IActionResult indicating the success or failure of the update operation.</returns>
		/// <response code="200">Returns success if the order items were updated.</response>
		/// <response code="404">Returns not found if the order was not found.</response>
		/// <response code="500">Returns a generic error if an internal server error occurs.</response>
		[HttpPut]
		[Route("[action]")]
		public async Task<IActionResult> UpdateOrderItems([FromBody] UpdateOrderItemsDTO request)
		{
			if (request == null || request.Items == null || !request.Items.Any())
			{
				Log.Warning("UpdateOrderItems request is null or contains no items.");
				return BadRequest(new { message = "Request cannot be null and must contain at least one item." });
			}
			try
			{
				Log.Information("Attempting to update items for Order ID: {OrderId}", request.OrderId);
				bool isUpdated = await _orderService.UpdateOrderItemsAsync(request.OrderId, request.Items);
				if (isUpdated)
				{
					Log.Information("Order items updated successfully for Order ID: {OrderId}", request.OrderId);
					return Ok(new { message = "Order items updated successfully." });
				}
				else
				{
					Log.Warning("Order not found. Order ID: {OrderId}", request.OrderId);
					return NotFound(new { message = "Order not found." });
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Error updating items for Order ID: {OrderId}", request.OrderId);
				return StatusCode(500, new { message = "An error occurred while processing your request." });
			}
		}
		/// <summary>
		/// Updates customer-specific details on an order, such as address and notes.
		/// </summary>
		/// <param name="request">An UpdateCustomerDetailsDTO containing order ID, address, and notes.</param>
		/// <returns>An IActionResult indicating the success or failure of the update operation.</returns>
		/// <response code="200">Returns success if customer details were updated.</response>
		/// <response code="404">Returns not found if the order was not found.</response>
		/// <response code="500">Returns a generic error if an internal server error occurs.</response>
		[HttpPut]
		[Route("[action]")]
		public async Task<IActionResult> UpdateCustomerDetails([FromBody] UpdateCustomerDetailsDTO request)
		{
			if (request == null || string.IsNullOrWhiteSpace(request.Address))
			{
				Log.Warning("UpdateCustomerDetails request is null or address is missing.");
				return BadRequest(new { message = "Request cannot be null and must contain a valid address." });
			}
			try
			{
				Log.Information("Attempting to update customer details for Order ID: {OrderId}", request.OrderId);
				bool isUpdated = await _orderService.UpdateCustomerDetailsAsync(request.OrderId, request.Address, request.Notes);
				if (isUpdated)
				{
					Log.Information("Customer details updated successfully for Order ID: {OrderId}", request.OrderId);
					return Ok(new { message = "Customer details updated successfully." });
				}
				else
				{
					Log.Warning("Order not found. Order ID: {OrderId}", request.OrderId);
					return NotFound(new { message = "Order not found." });
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Error updating customer details for Order ID: {OrderId}", request.OrderId);
				return StatusCode(500, new { message = "An error occurred while processing your request." });
			}
		}
		/// <summary>
		/// Deletes an order by its ID.
		/// </summary>
		/// <param name="orderId">The ID of the order to be deleted.</param>
		/// <returns>An IActionResult indicating the success or failure of the delete operation.</returns>
		/// <response code="200">Returns success if the order was deleted.</response>
		/// <response code="404">Returns not found if the order was not found.</response>
		/// <response code="500">Returns a generic error if an internal server error occurs.</response>
		[HttpDelete]
		[Route("[action]")]
		public async Task<IActionResult> DeleteOrderById(int orderId)
		{
			try
			{
				Log.Information("Attempting to delete Order ID: {OrderId}", orderId);
				bool isDeleted = await _orderService.DeleteOrderByIdAsync(orderId);
				if (isDeleted)
				{
					Log.Information("Order deleted successfully. Order ID: {OrderId}", orderId);
					return Ok(new { message = "Order deleted successfully." });
				}
				else
				{
					Log.Warning("Order not found. Order ID: {OrderId}", orderId);
					return NotFound(new { message = "Order not found." });
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Error deleting Order ID: {OrderId}", orderId);
				return StatusCode(500, new { message = "An error occurred while processing your request." });
			}
		}
	}
}
