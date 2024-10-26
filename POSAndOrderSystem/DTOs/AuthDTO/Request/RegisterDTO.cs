namespace POSAndOrderSystem.DTOs.AuthDTO.Request
{
	public class RegisterDTO
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Email { get; set; }
		public required string Password { get; set; }
		public required string Phone { get; set; }
		public required string RoleID { get; set; }
		public required string Address { get; set; }
	}
}
