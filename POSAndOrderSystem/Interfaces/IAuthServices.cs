using POSAndOrderSystem.DTOs.AuthDTO.Request;

namespace POSAndOrderSystem.Interfaces
{
	public interface IAuthServices
	{
		public Task<string> RegisterAsync(RegisterDTO request);
		public Task<string> Login(LoginDTO request);
	}
}
