namespace POSAndOrderSystem.Entities
{
	public class User : MainEntity
	{
		public string Name { get; set; }
		public string Password { get; set; }
		public required int RoleId { get; set; }
	}
}