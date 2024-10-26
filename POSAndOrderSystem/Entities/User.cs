namespace POSAndOrderSystem.Entities
{
	public class User : MainEntity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Password { get; set; }
		public string UserImage { get; set; }
		public string Role { get; set; }
	}
}