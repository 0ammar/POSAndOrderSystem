namespace POSAndOrderSystem.Entities
{
	public class LookupItem : MainEntity
	{
		public string Name { get; set; }
		public required int LookupTypeId { get; set; }
	}
}
