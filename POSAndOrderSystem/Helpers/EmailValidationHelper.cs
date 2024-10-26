using System.Text.RegularExpressions;

namespace POSAndOrderSystem.Helpers
{
	public static class EmailValidationHelper
	{
		private static readonly Regex EmailRegex = new Regex(
	   @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
		public static bool IsValidEmail(string email) =>
			EmailRegex.IsMatch(email);
	}
}
