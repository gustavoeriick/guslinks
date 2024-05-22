using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Components;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using guslinks.Components.Uteis;
using Infra.Data.Repository.Persistence.Guslinks;
using guslinks.Components.Models;
using Microsoft.IdentityModel.Tokens;
using General.Entidades.Guslinks;
using guslinks.Components.Listas;
using System.Globalization;

namespace guslinks.Components.Pages
{
	public class CadastroBase : ComponentBase
	{
		public Usuarios Usuario { get; set; }

		//public string Nome = "";
		//public string URL = "";
		//public string Email = "";
		//public string Senha = "";

		public string validacaoNome = "";
		public string validacaoUrl = "";
		public string validacaoEmail = "";
		public string validacaoSenha = "";

		public bool Erro = false;
		public string MensagemErro = "";

		public bool CadastroFinalizado = false;

		protected override async Task OnInitializedAsync()
		{
			Usuario = new();

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

			if (string.IsNullOrEmpty(Usuario.nome))
			{
				Erro = true;
				validacaoNome = "is-invalid";
				MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O preenchimento do campo 'Nome' é obrigatório");
				await InvokeAsync(StateHasChanged);
			}
			else
			{
				if (Usuario.nome.Length < 4)
				{
					Erro = true;
					validacaoNome = "is-invalid";
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'Nome' não pode conter menos de 4 caracteres");
					await InvokeAsync(StateHasChanged);
				}

				if (Usuario.nome.Length > 100)
				{
					Erro = true;
                    validacaoNome = "is-invalid";
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'Nome' não pode conter mais de 100 caracteres");
					await InvokeAsync(StateHasChanged);
				}

				bool retorno = Uteis.Validacoes.ValidarNome(Usuario.nome);

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

			if (string.IsNullOrEmpty(Usuario.url))
			{
				Erro = true;
				validacaoUrl = "is-invalid";
				MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O preenchimento do campo 'URL' é obrigatório");
				await InvokeAsync(StateHasChanged);
			}
			else
			{
				if (Usuario.url.Length < 6)
				{
					Erro = true;
                    validacaoUrl = "is-invalid";
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'URL' não pode conter menos de 6 caracteres");
					await InvokeAsync(StateHasChanged);
				}

				if (Usuario.url.Length > 50)
				{
					Erro = true;
                    validacaoUrl = "is-invalid";
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'URL' não pode conter mais de 50 caracteres");
					await InvokeAsync(StateHasChanged);
				}

				bool retorno = Uteis.Validacoes.ValidarCaracteres1(Usuario.url);

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

			if (string.IsNullOrEmpty(Usuario.email))
			{
				Erro = true;
				validacaoEmail = "is-invalid";
				MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O preenchimento do campo 'Email' é obrigatório");
				await InvokeAsync(StateHasChanged);
			}
			else
			{
				bool retorno = Uteis.Validacoes.ValidarEmail(Usuario.email);

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

			if (string.IsNullOrEmpty(Usuario.senha))
			{
				Erro = true;
				validacaoSenha = "is-invalid";
				MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O preenchimento do campo 'Senha' é obrigatório");
				await InvokeAsync(StateHasChanged);
			}
			else
			{
				if (Usuario.senha.Length < 8)
				{
					Erro = true;
                    validacaoSenha = "is-invalid";
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'Senha' não pode conter menos de 8 caracteres");
					await InvokeAsync(StateHasChanged);
				}

				if (Usuario.senha.Length > 20)
				{
					Erro = true;
                    validacaoSenha = "is-invalid";
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'Senha' não pode conter mais de 20 caracteres");
					await InvokeAsync(StateHasChanged);
				}

				bool retorno = Uteis.Validacoes.ValidarCaracteres2(Usuario.senha);

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

				using (UsuariosRepository rep = new())
				{
					var retorno = await rep.FindAllAsync();
					var retornoUrl = await rep.CustomList(c => c.url.Equals(Usuario.url));
					var retornoEmail = await rep.CustomList(c => c.email.Equals(Usuario.email));

					if (retornoUrl.Count > 0)
					{
						Erro = true;
						validacaoUrl = "is-invalid";
						MensagemErro = MensagemErro + Uteis.Formatacao.Msg("Url já cadastrada!");
						await InvokeAsync(StateHasChanged);
					}

					if (retornoEmail.Count > 0)
					{
						Erro = true;
						validacaoUrl = "is-invalid";
						MensagemErro = MensagemErro + Uteis.Formatacao.Msg("E-mail já cadastrado!");
						await InvokeAsync(StateHasChanged);
					}

					if (!Erro)
					{
						var data = DateTime.Now;
						var dataConvertida = data.ToString("yyyy-MM-dd HH:mm:ss");
						var dataFormatada = DateTime.ParseExact(dataConvertida, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

						//Usuario.nome = Nome;
						//Usuario.url = URL;
						//Usuario.email = Email;
						//Usuario.senha = Senha;
						Usuario.texto = "Esse é um texto padrão! Não esqueça de alterar!!";
						Usuario.vip = false;
						Usuario.ativo = true;
						Usuario.datacadastro = dataFormatada;
						Usuario.dataatualizacao = dataFormatada;
						Usuario.cadastradopor = 0;
						Usuario.atualizadopor = 0;

						await rep.InsertAsync(Usuario);
						await InvokeAsync(StateHasChanged);

						Usuario = new();

						CadastroFinalizado = true;
					}
				}
			}

			await InvokeAsync(StateHasChanged);
		}
	}
}
