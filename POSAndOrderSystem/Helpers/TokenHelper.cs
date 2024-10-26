using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace POSAndOrderSystem.Helpers
{
	public class TokenHelper
	{
		private static readonly string SecretKey = "LongSecrectStringForModulekodestartppopopopsdfjnshbvhueFGDKJSFBYJDSAGVYKDSGKFUYDGASYGFskc vhHJVCBYHVSKDGHASVBCL";

		public async static Task<string> GenerateToken(string userID, string role)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(SecretKey);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim("UserID", userID),
					new Claim(ClaimTypes.Role, role)
				}),
				Expires = DateTime.UtcNow.AddHours(2),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

		public async static Task<bool> ValidateToken(string tokenString, string role)
		{
			String toke = "Bearer " + tokenString;
			var jwtEncodedString = toke.Substring(7);
			var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
			var roleString = (token.Claims.First(c => c.Type == "role").Value.ToString());
			if (token.ValidTo > DateTime.UtcNow && roleString.Equals(role, StringComparison.OrdinalIgnoreCase))
				return true;
			return false;
		}
	}
}
