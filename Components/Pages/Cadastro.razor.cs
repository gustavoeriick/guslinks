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
using System.Runtime.CompilerServices;

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

		public bool Loading = false;
		public bool Erro = false;
		public string MensagemErro = "";

		public bool CadastroFinalizado = false;

		protected override async Task OnInitializedAsync()
		{
			Usuario = new();

			await base.OnInitializedAsync();
			await InvokeAsync(StateHasChanged);
		}

		public async Task ValidaNome()
		{
			validacaoNome = "";

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

				string nomeFormatado = Uteis.Formatacao.Nome(Usuario.nome);

				Usuario.nome = nomeFormatado;

				await InvokeAsync(StateHasChanged);

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
		}

		public async Task ValidaUrl()
		{
			Erro = false;
			MensagemErro = "";
			validacaoUrl = "";

			await InvokeAsync(StateHasChanged);

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

				string urlFormatada = Uteis.Formatacao.Url(Usuario.url);

				Usuario.url = urlFormatada;

				await InvokeAsync(StateHasChanged);

				bool retorno = Uteis.Validacoes.ValidarUrl(Usuario.url);

				if (!retorno)
				{
					Erro = true;
					validacaoUrl = "is-invalid";
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'URL' permite somente letras e números");
					await InvokeAsync(StateHasChanged);
				}

				if (string.IsNullOrEmpty(validacaoUrl))
				{
					using (UsuariosRepository rep = new())
					{
                        Usuarios ValidaUrl = new();

                        ValidaUrl = await rep.CustomSearch(c => c.url.Equals(Usuario.url));

						if (ValidaUrl.url != null)
						{
                            Erro = true;
                            validacaoUrl = "is-invalid";
                            MensagemErro = MensagemErro + Uteis.Formatacao.Msg("Url já cadastrada");
                            await InvokeAsync(StateHasChanged);
                        }
                        else
                        {
                            validacaoUrl = "is-valid";
                            await InvokeAsync(StateHasChanged);
                        }

                    }
				}
			}
		}

		public async Task ValidaEmail()
		{
			Erro = false;
			MensagemErro = "";
			validacaoEmail = "";

			await InvokeAsync(StateHasChanged);

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
					using (UsuariosRepository rep = new())
					{
						Usuarios validaEmail = new();

                        validaEmail = await rep.CustomSearch(c => c.email.Equals(Usuario.email));

						if (validaEmail.email != null)
						{
                            Erro = true;
                            validacaoEmail = "is-invalid";
                            MensagemErro = MensagemErro + Uteis.Formatacao.Msg("E-mail já cadastrado");
                            await InvokeAsync(StateHasChanged);
                        }
                        else
                        {
                            validacaoEmail = "is-valid";
                            await InvokeAsync(StateHasChanged);
                        }
                    }
				}
			}
		}

		public async Task ValidaSenha()
		{
			Erro = false;
			MensagemErro = "";
			validacaoSenha = "";

			await InvokeAsync(StateHasChanged);

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
					MensagemErro = MensagemErro + Uteis.Formatacao.Msg("O campo 'Senha' permite letras, números e alguns caracteres especiais (@!?#$%&*)");
					await InvokeAsync(StateHasChanged);
				}

				if (string.IsNullOrEmpty(validacaoSenha))
				{
					validacaoSenha = "is-valid";
					await InvokeAsync(StateHasChanged);
				}
			}
		}

		public async Task Criar()
		{
			Erro = false;
			MensagemErro = "";
			Loading = true;

			await Task.Delay(2000);
			await InvokeAsync(StateHasChanged);

			// Nome
			ValidaNome();

			// URL
			ValidaUrl();

			// E-mail
			ValidaEmail();

			// Senha
			ValidaSenha();

			await InvokeAsync(StateHasChanged);

			if (!Erro)
			{
				//tudo certo

				using (UsuariosRepository rep = new())
				{
					var data = DateTime.Now;

					Usuario.texto = "Esse é um texto padrão! Não esqueça de alterar!!";
					Usuario.vip = false;
					Usuario.ativo = true;
					Usuario.datacadastro = data;
					Usuario.dataatualizacao = data;
					Usuario.cadastradopor = 0;
					Usuario.atualizadopor = 0;

					await rep.InsertAsync(Usuario);
					await InvokeAsync(StateHasChanged);

					var usuario_cadastrado = await rep.CustomSearch(c => c.url.Equals(Usuario.id));

					using (LinksRepository link = new())
					{
						Links links = new Links();

						links.id = 0;
						links.idUsuario = usuario_cadastrado.id;
						links.link = "https://github.com/gustavoeriick";
						links.texto = "GitHub: gustavoeriick";
						links.ordem = 1;
						links.ativo = true;
						links.datacadastro = data;
						links.dataatualizacao = data;
						links.cadastradopor = 0;
						links.atualizadopor = 0;

                        await link.InsertAsync(links);
                        await InvokeAsync(StateHasChanged);
                    }

					using (LinksContatosRepository cont = new())
					{
						LinksContatos contatos = new LinksContatos();

                        contatos.id = 0;
                        contatos.idUsuario = usuario_cadastrado.id;
                        contatos.link = "gustavoeriick";
                        contatos.idIcone = 1;
                        contatos.ordem = 1;
                        contatos.datacadastro = data;
                        contatos.dataatualizacao = data;
                        contatos.cadastradopor = 0;
                        contatos.atualizadopor = 0;

                        await cont.InsertAsync(contatos);
                        await InvokeAsync(StateHasChanged);
                    }

                    Usuario = new();

					CadastroFinalizado = true;
				}
			}

			Loading = false;

			await InvokeAsync(StateHasChanged);
		}
	}
}
