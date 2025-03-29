using General.Entidades.Guslinks;
using Infra.Data.Repository.Persistence.Guslinks;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace guslinks.Components.Pages
{
    public class VerificaBase : ComponentBase
    {
        [Parameter]
        public string Token { get; set; }
        public string UrlFinal { get; set; } = "";
        public string Msg { get; set; } = "";
        public bool carregarConteudo { get; set; } = false;
        public bool validado { get; set; } = false;
        public bool _validaTokenExecutado { get; set; } = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && !_validaTokenExecutado)
            {
                _validaTokenExecutado = true;
                await ValidaToken();
                StateHasChanged();
            }
        }

        public async Task ValidaToken()
        {
            using (UsuariosRepository rep = new())
            {
                Usuarios usuario = await rep.CustomSearch(c => c.token == Token);

                if (usuario == null)
                {
                    Msg = "Link inválido!";
                }
                else
                {
                    if (usuario.verificado)
                    {
                        Msg = "Seu cadastro já está validado!";
                    }
                    else
                    {
                        UrlFinal = $"https://gus.app.br/{usuario.url}";

                        usuario.verificado = true;
                        usuario.dataatualizacao = DateTime.Now;
                        usuario.atualizadopor = usuario.id;

                        await rep.UpdateAsync(usuario);

                        validado = true;
                        //Msg = "Seu cadastro foi verificado com sucesso!";
                    }
                }
                carregarConteudo = true;
            }
        }
    }
}
