namespace POSAndOrderSystem.DTOs.AuthDTO.Request
{
	public class RegisterDTO
	{
		public required string Name { get; set; }
		public required string Password { get; set; }
		public required string RoleID { get; set; }
	}
}
