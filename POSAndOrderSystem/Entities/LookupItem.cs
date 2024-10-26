namespace POSAndOrderSystem.Entities
{
	public class LookupItem : MainEntity
	{
		public required string Name { get; set; }
		public int LookupTypeId { get; set; }
		public string? Description { get; set; }
	}
}
