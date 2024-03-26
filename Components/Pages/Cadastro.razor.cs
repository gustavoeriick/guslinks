using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Components;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using guslinks.Components.Uteis;

namespace guslinks.Components.Pages
{
	public class CadastroBase : ComponentBase
	{
		public string Nome = "";
		public string URL = "";
		public string Email = "";
		public string Senha = "";

		public string validacaoNome = "";
		public string validacaoUrl = "";
		public string validacaoEmail = "";
		public string validacaoSenha = "";

		public bool Erro = false;
		public string MensagemErro = "";

		public bool CadastroFinalizado = false;

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			await InvokeAsync(StateHasChanged);
		}

		public async Task Criar()
		{
			Erro = false;
			MensagemErro = "";

			validacaoNome = "";
			validacaoUrl = "";
			validacaoEmail = "";
			validacaoSenha = "";

			await InvokeAsync(StateHasChanged);

			// Nome

			if (string.IsNullOrEmpty(Nome))
			{
				Erro = true;
				validacaoNome = "is-invalid";
				MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O preenchimento do campo 'Nome' é obrigatório");
				await InvokeAsync(StateHasChanged);
			}
			else
			{
				if (Nome.Length < 4)
				{
					Erro = true;
					validacaoNome = "is-invalid";
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'Nome' não pode conter menos de 4 caracteres");
					await InvokeAsync(StateHasChanged);
				}

				if (Nome.Length > 100)
				{
					Erro = true;
                    validacaoNome = "is-invalid";
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'Nome' não pode conter mais de 100 caracteres");
					await InvokeAsync(StateHasChanged);
				}

				bool retorno = Uteis.Validacoes.ValidarNome(Nome);

				if (!retorno)
				{
					Erro = true;
                    validacaoNome = "is-invalid";
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'Nome' permite somente letras e números");
					await InvokeAsync(StateHasChanged);
				}

				if (string.IsNullOrEmpty(validacaoNome))
				{
                    validacaoNome = "is-valid";
                    await InvokeAsync(StateHasChanged);
                }
            }

			// URL

			if (string.IsNullOrEmpty(URL))
			{
				Erro = true;
				validacaoUrl = "is-invalid";
				MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O preenchimento do campo 'URL' é obrigatório");
				await InvokeAsync(StateHasChanged);
			}
			else
			{
				if (URL.Length < 6)
				{
					Erro = true;
                    validacaoUrl = "is-invalid";
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'URL' não pode conter menos de 6 caracteres");
					await InvokeAsync(StateHasChanged);
				}

				if (URL.Length > 50)
				{
					Erro = true;
                    validacaoUrl = "is-invalid";
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'URL' não pode conter mais de 50 caracteres");
					await InvokeAsync(StateHasChanged);
				}

				bool retorno = Uteis.Validacoes.ValidarCaracteres1(URL);

				if (!retorno)
				{
					Erro = true;
                    validacaoUrl = "is-invalid";
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'URL' permite somente letras e números");
					await InvokeAsync(StateHasChanged);
				}

                if (string.IsNullOrEmpty(validacaoUrl))
                {
                    validacaoUrl = "is-valid";
                    await InvokeAsync(StateHasChanged);
                }
            }

			// E-mail

			if (string.IsNullOrEmpty(Email))
			{
				Erro = true;
				validacaoEmail = "is-invalid";
				MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O preenchimento do campo 'Email' é obrigatório");
				await InvokeAsync(StateHasChanged);
			}
			else
			{
				bool retorno = Uteis.Validacoes.ValidarEmail(Email);

				if (!retorno)
				{
					Erro = true;
                    validacaoEmail = "is-invalid";
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("E-mail inválido!");
					await InvokeAsync(StateHasChanged);
				}

                if (string.IsNullOrEmpty(validacaoEmail))
                {
                    validacaoEmail = "is-valid";
                    await InvokeAsync(StateHasChanged);
                }
            }

			// Senha

			if (string.IsNullOrEmpty(Senha))
			{
				Erro = true;
				validacaoSenha = "is-invalid";
				MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O preenchimento do campo 'Senha' é obrigatório");
				await InvokeAsync(StateHasChanged);
			}
			else
			{
				if (Senha.Length < 8)
				{
					Erro = true;
                    validacaoSenha = "is-invalid";
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'Senha' não pode conter menos de 8 caracteres");
					await InvokeAsync(StateHasChanged);
				}

				if (Senha.Length > 20)
				{
					Erro = true;
                    validacaoSenha = "is-invalid";
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'Senha' não pode conter mais de 20 caracteres");
					await InvokeAsync(StateHasChanged);
				}

				bool retorno = Uteis.Validacoes.ValidarCaracteres2(Senha);

				if (!retorno)
				{
					Erro = true;
                    validacaoSenha = "is-invalid";
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'Senha' permite letras, números e alguns caracteres especiais (!?#$%&*)");
					await InvokeAsync(StateHasChanged);
				}

                if (string.IsNullOrEmpty(validacaoSenha))
                {
                    validacaoSenha = "is-valid";
                    await InvokeAsync(StateHasChanged);
                }
            }

			if (!Erro)
			{
				//tudo certo
				CadastroFinalizado = true;
			}

			await InvokeAsync(StateHasChanged);
		}
	}
}
