using guslinks.Components.Models;

namespace guslinks.Components.Listas
{
    public class ListaConfiguracoes
    {
        public List<ConfiguracoesModel> listaConfig = new List<ConfiguracoesModel>
        {
            new ConfiguracoesModel { 
                id = 1, 
                idUsuario = 1, 
                planofundo_cor = "", 
                planofundo_img = "hiperespaco.png", 
                imgperfil_borda_cor = "", 
                nome_cor = "", 
                nome_fonte = "", 
                texto_cor = "", 
                texto_fonte = "", 
                link_fundo_cor = "", 
                link_texto_cor = "", 
                link_texto_fonte = "",
                icone_cor = ""
            },
            new ConfiguracoesModel { 
                id = 2, 
                idUsuario = 2, 
                planofundo_cor = "#c3bfbf", 
                planofundo_img = "textura_fibras.png", 
                imgperfil_borda_cor = "",
                nome_cor = "#000", 
                nome_fonte = "", 
                texto_cor = "#000", 
                texto_fonte = "", 
                link_fundo_cor = "", 
                link_texto_cor = "#000", 
                link_texto_fonte = "",
                icone_cor = "#000"
            },
        };
    }
}
