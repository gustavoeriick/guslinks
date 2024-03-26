using guslinks.Components.Listas;
using Microsoft.AspNetCore.Components;

namespace guslinks.Components.Pages
{
	public class LoginBase : ComponentBase
	{
		public string Email;
		public string Senha;

		public bool Erro = false;
		public string MensagemErro = "";

		public ListaUsuarios usuarios = new ListaUsuarios();

		public async Task Logar()
		{
			Erro = false;
			MensagemErro = "";

			await InvokeAsync(StateHasChanged);

			// E-mail

			if (string.IsNullOrEmpty(Email))
			{
				Erro = true;
				MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O preenchimento do campo 'Email' é obrigatório");
				await InvokeAsync(StateHasChanged);
			}
			else
			{
				bool retorno = Uteis.Validacoes.ValidarEmail(Email);

				if (!retorno)
				{
					Erro = true;
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("E-mail inválido!");
					await InvokeAsync(StateHasChanged);
				}
			}

			// Senha

			if (string.IsNullOrEmpty(Senha))
			{
				Erro = true;
				MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O preenchimento do campo 'Senha' é obrigatório");
				await InvokeAsync(StateHasChanged);
			}
			else
			{
				if (Senha.Length < 8)
				{
					Erro = true;
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'Senha' não pode conter menos de 8 caracteres");
					await InvokeAsync(StateHasChanged);
				}

				if (Senha.Length > 20)
				{
					Erro = true;
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'Senha' não pode conter mais de 20 caracteres");
					await InvokeAsync(StateHasChanged);
				}

				bool retorno = Uteis.Validacoes.ValidarCaracteres2(Senha);

				if (!retorno)
				{
					Erro = true;
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'Senha' permite letras, números e alguns caracteres especiais (!?#$%&*)");
					await InvokeAsync(StateHasChanged);
				}
			}

			if (!Erro)
			{
				//tudo certo

				bool acessoLiberado = false;

				foreach (var usuario in usuarios.listaUsuarios)
				{
					if (usuario.email == Email && usuario.senha == Senha)
					{
						// Se encontrou o usuário com o e-mail e senha fornecidos
						// aqui você pode fazer o que precisar, como retornar true ou 
						// executar alguma outra ação.
						acessoLiberado = true;
						continue;
					}
				}

				if (!acessoLiberado) 
				{
					Erro = true;
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("Dados inválidos");
				}
				else
				{
					Erro = true;
					MensagemErro = "Login liberado!";
				}
				await InvokeAsync(StateHasChanged);
			}

		}
	}
}
