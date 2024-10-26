namespace POSAndOrderSystem.DTOs.User.Response
{
	public class UserDTO
	{
		public int ID { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public string? Phone { get; internal set; }
		public int RoleId { get; set; }
	}
}
