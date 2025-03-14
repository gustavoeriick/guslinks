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
		public List<Usuarios> lstUsuarios { get; set; }

		public string validacaoNome = "";
		public string validacaoUrl = "";
		public string validacaoEmail = "";
		public string validacaoSenha = "";

		public bool Loading = false;
		public bool Erro = false;
		public string MensagemErro = "";

		public bool CadastroFinalizado = false;

		public string UrlFinal { get; set; } = "";

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

		public async Task ValidaUrl(bool temUrl)
		{
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
					if (temUrl)
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

		public async Task ValidaEmail(bool temEmail)
		{
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
					if (temEmail)
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

		public async Task ValidaSenha()
		{
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

			using (UsuariosRepository rep = new())
			{
				Usuarios user = new();

				var valUrl = await rep.CustomSearch(c => c.url == Usuario.url);
				bool temUrl = false;
				var valEmail = await rep.CustomSearch(c => c.email == Usuario.email);
				bool temEmail = false;
				
				if(valUrl != null)
				{
					temUrl = true;
                }

                if (valEmail != null)
                {
                    temEmail = true;
                }

                // Nome
                ValidaNome();

                // URL
                ValidaUrl(temUrl);

                // E-mail
                ValidaEmail(temEmail);

                // Senha
                ValidaSenha();

                await InvokeAsync(StateHasChanged);

                if (!Erro)
                {
					string token_validacao = Utilitarios.GeraToken();

					var data = DateTime.Now;

					Usuario.senha = PasswordHasher.HashPassword(Usuario.senha);
					Usuario.texto = "Esse é um texto padrão! Não esqueça de alterar!!";
                    Usuario.vip = false;
                    Usuario.ativo = true;
                    Usuario.datacadastro = data;
                    Usuario.dataatualizacao = data;
                    Usuario.cadastradopor = 0;
                    Usuario.atualizadopor = 0;
					Usuario.token = token_validacao;
					Usuario.verificado = false;

                    await rep.InsertAsync(Usuario);
                    await InvokeAsync(StateHasChanged);

                    // cadastrar config
                    using (ConfiguracoesRepository conf = new())
                    {
                        Configuracoes config = new Configuracoes();

                        config.id = 0;
                        config.idUsuario = Usuario.id;
                        config.tem_img_perfil = false;
                        config.planofundo_cor = "";
                        config.planofundo_img = "";
                        config.imgperfil_borda_cor = "";
                        config.nome_cor = "";
                        config.nome_fonte = "";
                        config.texto_cor = "";
                        config.texto_fonte = "";
                        config.link_fundo_cor = "";
                        config.link_texto_cor = "";
                        config.link_texto_fonte = "";
                        config.link_borda_cor = "";
                        config.icone_cor = "";
                        config.datacadastro = data;
                        config.dataatualizacao = data;
                        config.cadastradopor = 0;
                        config.atualizadopor = 0;

                        await conf.InsertAsync(config);
                        await InvokeAsync(StateHasChanged);

                    }

                    using (LinksRepository link = new())
                    {
                        Links links = new Links();

                        links.id = 0;
                        links.idUsuario = Usuario.id;
                        links.link = "https://github.com/gustavoeriick";
                        links.texto = "GitHub: gustavoeriick";
                        links.imagem = "";
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
                        contatos.idUsuario = Usuario.id;
                        contatos.link = "gustavoeriick";
                        contatos.idIcone = 1;
                        contatos.ordem = 1;
						contatos.ativo = true;
                        contatos.datacadastro = data;
                        contatos.dataatualizacao = data;
                        contatos.cadastradopor = 0;
                        contatos.atualizadopor = 0;

                        await cont.InsertAsync(contatos);
                        await InvokeAsync(StateHasChanged);
                    }

                    string Assunto = "Gus App - Verifique o seu cadastro!";
					string Link_validacao = $"https://localhost:7125/Verifica/{token_validacao}";
                    //string Link_validacao = $"https://gus.app.br/Verifica/{token_validacao}";
                    string Corpo = $@"Olá!<br>
										<b>Este é um e-mail automático!</b><br>
										<b>Por gentileza, não responder.</b>
										<br><br>
										Seu cadastro na Gus App foi realizado com sucesso!<br>
										Agora vamos verificar o seu cadastro?<br>
										Para verificar o seu cadastro, basta clicar no link a baixo:<br>
										<a href='{Link_validacao}' target='_blank'>{Link_validacao}</a>
										<br><br>
										<b>Caso não consiga clicar no link, copie e cole no seu navegador!</b><br>
										<br><br>
										Atenciosamente,<br>
										Equipe Gus App
					";

                    List<string> emailList = new List<string>();
                    emailList.Add(Usuario.email);

                    List<System.Net.Mail.Attachment> lsAnexos = new();
                    Utilitarios.EnvioEmail(emailList, Corpo, Assunto, true, lsAnexos);

                    UrlFinal = $"http://gus.app.br/{Usuario.url}";

                    Usuario = new();

                    CadastroFinalizado = true;
                }
            }

            

			Loading = false;

			await InvokeAsync(StateHasChanged);
		}
	}
}
