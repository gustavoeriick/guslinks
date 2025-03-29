using System;
using System.Security.Cryptography;
using General.Entidades.Guslinks;
using guslinks.Components.Services;
using guslinks.Components.Uteis;
using Infra.Data.Repository.Persistence.Guslinks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace guslinks.Components.Pages
{
	public class LoginBase : ComponentBase
	{
		public Usuarios usuario { get; set; }

		[Inject] public TokenAuthenticationProvider authStateProvider { get; set; }
		[Inject] public NavigationManager navigation { get; set; }

		public bool Loading { get; set; } = false;
        public bool Erro { get; set; } = false;
		public string MensagemErro { get; set; } = "";

		protected override async Task OnInitializedAsync()
		{
			usuario = new();

			//await base.OnInitializedAsync();
			//await InvokeAsync(StateHasChanged);
		}

		public async Task Logar()
		{
			Loading = true;
            Erro = false;
			MensagemErro = "";

			// E-mail

			if (string.IsNullOrEmpty(usuario.email))
			{
				Erro = true;
				MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O preenchimento do campo 'Email' é obrigatório");
			}
			else
			{
				bool retorno = Uteis.Validacoes.ValidarEmail(usuario.email);

				if (!retorno)
				{
					Erro = true;
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("E-mail inválido!");
				}
			}

			// Senha

			if (string.IsNullOrEmpty(usuario.senha))
			{
				Erro = true;
				MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O preenchimento do campo 'Senha' é obrigatório");
			}
			else
			{
				if (usuario.senha.Length < 8)
				{
					Erro = true;
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'Senha' não pode conter menos de 8 caracteres");
				}

				if (usuario.senha.Length > 20)
				{
					Erro = true;
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'Senha' não pode conter mais de 20 caracteres");
				}

				bool retorno = Uteis.Validacoes.ValidarCaracteres2(usuario.senha);

				if (!retorno)
				{
					Erro = true;
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'Senha' permite letras, números e alguns caracteres especiais (!?#$%&*)");
				}
			}

			if (!Erro)
			{
				//tudo certo

				using (UsuariosRepository us = new())
				{
					var validaLogin = await us.CustomSearch(c => c.email == usuario.email);

					if (validaLogin != null)
					{
						bool isSenhaValida = PasswordHasher.VerifyPassword(validaLogin.senha, usuario.senha);

						if (!isSenhaValida)
						{
							Erro = true;
							MensagemErro = MensagemErro + Uteis.Formatacao.Msg("Senha inválida");
						}
						else
						{

							var token = GeraToken.Gerar();

							authStateProvider.token = token;
							await authStateProvider.Login(token, usuario.email).ContinueWith((taskwithmessage) =>
							{
								navigation.NavigateTo("/Painel");
							});
						}
					}
					else
					{
						Erro = true;
						MensagemErro = MensagemErro + Uteis.Formatacao.Msg("E-mail não cadastrado");
					}
				}
			}

			Loading = false;

        }
	}
}
