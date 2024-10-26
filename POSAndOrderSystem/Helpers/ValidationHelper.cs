namespace POSAndOrderSystem.Helpers
{
	public static class ValidationHelper
	{
		public static void ValidateRequiredString(string value, string parameterName)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException($"{parameterName} is required.");
			}
		}
		public static void ValidateID(int Id)
		{
			if (Id <= 0)
				throw new ArgumentException("User id must be greater than zero.", nameof(Id));
		}
	}
}
