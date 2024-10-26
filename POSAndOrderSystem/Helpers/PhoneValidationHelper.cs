using System.Text.RegularExpressions;

namespace POSAndOrderSystem.Helpers
{
	public static class PhoneValidationHelper
	{
		private static readonly Regex PhoneRegex = new Regex(
			@"^(?:\+962|962|07)\d{8}$|^\+?[1-9]\d{9,14}$",
			RegexOptions.Compiled);

		public static bool IsValidPhone(string phone) =>
			PhoneRegex.IsMatch(phone);
	}
}
