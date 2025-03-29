using System.Net.NetworkInformation;
using guslinks.Components.Listas;
using guslinks.Components.Models;
using Microsoft.AspNetCore.Components;
using guslinks.Components.Models;
using guslinks.Components.Listas;
using General.Entidades.Guslinks;
using Infra.Data.Repository.Persistence.Guslinks;
using System.Runtime.CompilerServices;

namespace guslinks.Components.Pages
{
	public class HomeComponenteBase : ComponentBase
	{
		[Parameter]
		public string Url { get; set; }
        public string imgPerfil { get; set; }

        public bool carregarConteudo { get; set; } = false;
        public bool loading { get; set; } = true;

        // Entidades
        public Usuarios usuario { get; set; } = new();
        public Configuracoes config {  get; set; } = new();
        public List<Links> links { get; set; } = new();
        public List<LinksContatos> contatos { get; set; } = new();
        public List<TipoContato> icones { get; set; } = new();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await CarregaSite();
                StateHasChanged();
            }
        }

        public async Task CarregaSite()
        {
            if (Url != null & Url != "favicon.png")
            {
                using (UsuariosRepository user = new())
                {
                    usuario = await user.CustomSearch(c => c.url == Url);
                }

                if(usuario != null)
                {
                    using (ConfiguracoesRepository conf = new())
                    {
                        config = await conf.CustomSearch(c => c.idUsuario == usuario.id);
                    }

                    using (LinksRepository link = new())
                    {
                        links = await link.CustomList(c => c.idUsuario == usuario.id);
                    }

                    using (LinksContatosRepository cont = new())
                    {
                        contatos = await cont.CustomList(c => c.idUsuario == usuario.id);
                    }

                    using (TipoContatoRepository tipoc = new())
                    {
                        icones = await tipoc.FindAllAsync();
                    }

                    if (config.tem_img_perfil)
                    {
                        imgPerfil = usuario.id + ".png";
                    }
                    else
                    {
                        imgPerfil = "sem_img.svg";
                    }
                    carregarConteudo = true;
                }
                else
                {
                    loading = false;
                }
            }
        }
    }
}
