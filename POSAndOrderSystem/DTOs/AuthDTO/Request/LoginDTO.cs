namespace POSAndOrderSystem.DTOs.AuthDTO.Request
{
	public class LoginDTO
	{
		public required string Email { get; set; }
		public required string Password { get; set; }
	}
}
