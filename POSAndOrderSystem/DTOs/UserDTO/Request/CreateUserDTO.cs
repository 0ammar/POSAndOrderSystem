namespace POSAndOrderSystem.DTOs.User.Request
{
	public class CreateUserDTO
	{
		public required string Name { get; set; }
		public required string Password { get; set; }
		public required int RoleId { get; set; }
	}
}
