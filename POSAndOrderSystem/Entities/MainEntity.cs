using Microsoft.EntityFrameworkCore.Update;

namespace POSAndOrderSystem.Entities
{
	public class MainEntity
	{
		public int ID { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime ModificationDate {  get; set; }
	}
}
