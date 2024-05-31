using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace guslinks.Components.Services
{
	public class CustomAuthenticationStateProvider : AuthenticationStateProvider
	{
		private readonly IJSRuntime _jsRuntime;

		public CustomAuthenticationStateProvider(IJSRuntime jsRuntime)
		{
			_jsRuntime = jsRuntime;
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var user = await _jsRuntime.InvokeAsync<ClaimsPrincipal>("getAuthenticatedUser");
			return new AuthenticationState(user);
		}

		public async Task Login(string username)
		{
			var identity = new ClaimsIdentity(new[]
			{
				new Claim(ClaimTypes.Name, username)
			}, "apiauth_type");

			var user = new ClaimsPrincipal(identity);

			NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
		}

		public async Task Sair()
		{
			await _jsRuntime.InvokeVoidAsync("clearAuthenticatedUser");

			NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
		}
	}
}
