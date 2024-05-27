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
        public int usuarioAtual { get; set; }
        public string imgPerfil { get; set; }
        public bool carregarConteudo = false;

        // Entidades
        public Usuarios usuario { get; set; }
        public Configuracoes config {  get; set; }
        public List<Links> links { get; set; }
        public List<LinksContatos> contatos { get; set; }
        public List<TipoContato> icones { get; set; }

        // Models
        //public UsuarioModel usuario { get; set; }
		public List<LinksModel> linksUsuario { get; set; }
		public List<RedesModel> redesUsuario { get; set; }
		public ConfiguracoesModel configUsuario { get; set; }


		//public ListaUsuarios usuarios { get; set; }
		//public ListaLinks links { get; set; }
		//public ListaRedes redes { get; set; }
		//public ListaIcones icones { get; set; }
		//public ListaConfiguracoes config { get; set; }

        protected override async Task OnInitializedAsync()
        {
            usuario = new();
            config = new();
            links = new();
            contatos = new();
            icones = new();

            //linksUsuario = new();
            //redesUsuario = new();
            //configUsuario = new();

            if (url != null & url != "favicon.png")
            {
                carregarConteudo = true;

                using (UsuariosRepository user = new())
                {
                    usuario = await user.CustomSearch(c => c.url.Equals(url));
                }

                using (ConfiguracoesRepository conf = new())
                {
                    config = await conf.CustomSearch(c => c.idUsuario.Equals(usuario.id));
                }

                using (LinksRepository link = new())
                {
                    links = await link.CustomList(c => c.idUsuario.Equals(usuario.id));
                }

                using (LinksContatosRepository cont = new())
                {
                    contatos = await cont.CustomList(c => c.idUsuario.Equals(usuario.id));
                }

                using (TipoContatoRepository tipoc = new())
                {
                    icones = await tipoc.FindAllAsync();
                }

                //usuarioAtual = usuarios.listaUsuarios.FirstOrDefault(u => u.url == url).id;
                //linksUsuario = links.listaLinks.Where(l => l.idUsuario == usuarioAtual).ToList();
                //redesUsuario = redes.listaRedes.Where(r => r.idUsuario == usuarioAtual).ToList();
                //configUsuario = config.listaConfig.Where(x => x.idUsuario == usuarioAtual).First();

                imgPerfil = usuarioAtual + ".png";
            }
        }
    }
}
