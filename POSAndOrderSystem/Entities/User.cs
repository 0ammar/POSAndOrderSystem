﻿namespace POSAndOrderSystem.Entities
{
	public class User : MainEntity
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Email { get; set; }
		public required string Password { get; set; }
		public string? Phone { get; set; }
		public string? UserImage { get; set; }
		public required string Address { get; set; }
		public required int RoleId { get; set; }
	}
}