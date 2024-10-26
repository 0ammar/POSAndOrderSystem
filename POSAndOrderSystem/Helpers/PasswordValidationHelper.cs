public static class PasswordValidationHelper
{
	public static bool IsValidPassword(string password)
	{
		return password.Length >= 8 &&
			   password.Any(char.IsUpper) &&
			   password.Any(char.IsLower) &&
			   password.Any(char.IsDigit) &&
			   password.Any(ch => "!@#$%^&*()_+[]{}|;':\",.<>?/`~".Contains(ch));
	}
	public static string PasswordHashing(string password)
	{
		return BCrypt.Net.BCrypt.HashPassword(password);
	}
}
