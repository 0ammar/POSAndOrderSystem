namespace POSAndOrderSystem.DTOs.User.Response
{
	public class UserDTO
	{
		public int ID { get; set; }
		public required string Name { get; set; }
		public int RoleId { get; set; }
	}
}
