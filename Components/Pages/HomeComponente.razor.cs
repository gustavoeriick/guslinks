using System.Net.NetworkInformation;
using guslinks.Components.Listas;
using guslinks.Components.Models;
using Microsoft.AspNetCore.Components;
using guslinks.Components.Models;
using guslinks.Components.Listas;
using General.Entidades.Guslinks;
using Infra.Data.Repository.Persistence.Guslinks;

namespace guslinks.Components.Pages
{
	public class HomeComponenteBase : ComponentBase
	{
		[Parameter]
		public string url { get; set; }
        public string imgPerfil { get; set; }

        public bool carregarConteudo = false;

        // Entidades
        public Usuarios usuario { get; set; }
        public Configuracoes config {  get; set; }
        public List<Links> links { get; set; }
        public List<LinksContatos> contatos { get; set; }
        public List<TipoContato> icones { get; set; }

        protected override async Task OnInitializedAsync()
        {
            usuario = new();
            config = new();
            links = new();
            contatos = new();
            icones = new();

            if (url != null & url != "favicon.png")
            {
                using (UsuariosRepository user = new())
                {
                    usuario = await user.CustomSearch(c => c.url.Equals(url));
                }

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
        }
    }
}
