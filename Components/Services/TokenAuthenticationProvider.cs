using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using guslinks.Components.Uteis;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.JSInterop;

namespace guslinks.Components.Services
{
	public class TokenAuthenticationProvider : AuthenticationStateProvider
	{
		private readonly IJSRuntime js;
		private readonly HttpClient http;
		public string token;
		private readonly IDistributedCache cache;

		public TokenAuthenticationProvider(IJSRuntime ijsRuntime, HttpClient httpClient, IDistributedCache distributedCache)
		{
			js = ijsRuntime;
			http = httpClient;
			cache = distributedCache;
		}

		private AuthenticationState NotAuthenticate => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

		public async override Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var id_usuario = await js.GetFromLocalStorage("t");

			if (string.IsNullOrEmpty(id_usuario))
			{
				return NotAuthenticate;
			}
			;

			token = await cache.GetStringAsync(id_usuario);

			if (string.IsNullOrEmpty(token))
			{
				return NotAuthenticate;
			}
			;

			return CreateAuthenticationState(token);
		}

		public AuthenticationState CreateAuthenticationState(string token)
		{
			// colocar o token obtido do localstorage no header do request 
			// na seção Authorization assim poderemos estar autenticando 
			// cada requisição HTTP enviada ao servidor por este cliente
			http.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("bearer", token);
			http.DefaultRequestHeaders.ConnectionClose = true;
			//extrair as claims
			return new AuthenticationState(new ClaimsPrincipal
				(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
		}

		public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
		{
			var claims = new List<Claim>();
			var payload = jwt.Split('.')[1];
			var jsonBytes = ParseBase64WithoutPadding(payload);
			var keyValuePairs = JsonSerializer
				.Deserialize<Dictionary<string, object>>(jsonBytes);

			keyValuePairs.TryGetValue("perfil", out object roles);

			if (roles != null)
			{
				if (roles.ToString().Trim().StartsWith("["))
				{
					var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());
					foreach (var parsedRole in parsedRoles)
					{
						claims.Add(new Claim(ClaimTypes.Role, parsedRole));
					}
				}
				else
				{
					claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
				}
				keyValuePairs.Remove(ClaimTypes.Role);
			}

			claims.AddRange(keyValuePairs.Select(kvp =>
			new Claim(kvp.Key, kvp.Value.ToString())));
			return claims;
		}

		private static byte[] ParseBase64WithoutPadding(string base64)
		{
			switch (base64.Length % 4)
			{
				case 2: base64 += "=="; break;
				case 3: base64 += "="; break;
			}
			return Convert.FromBase64String(base64);
		}
		public async Task Login(string token, string id_usuario)
		{
			try
			{
				var options = new DistributedCacheEntryOptions
				{
					//Remove completamente após o tempo
					AbsoluteExpiration = DateTimeOffset.Now.AddHours(4),
					//Remove caso não seja usado por 30 minutos
					SlidingExpiration = TimeSpan.FromMinutes(30)
				};

				await cache.SetStringAsync(id_usuario, token, options);
				await js.SetInLocalStorage("t", id_usuario);

				var authState = CreateAuthenticationState(token);
				NotifyAuthenticationStateChanged(Task.FromResult(authState));
			}
			catch (Exception)
			{
				throw;
			}

		}
		public async Task Logout(string userName)
		{
			try
			{
				await cache.RemoveAsync(userName);
				await js.RemoveItem("t");
				var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
				var authState = Task.FromResult(new AuthenticationState(anonymousUser));
				http.DefaultRequestHeaders.Authorization = null;
				NotifyAuthenticationStateChanged(authState);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
