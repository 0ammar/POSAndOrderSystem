namespace POSAndOrderSystem.DTOs.User.Request
{
	public class UpdateUserDTO
	{
		public int ID { get; set; }
		public string? Name { get; set; }
		public string? Password { get; set; }
	}
}
