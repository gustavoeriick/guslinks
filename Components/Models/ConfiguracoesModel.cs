namespace guslinks.Components.Models
{
    public class ConfiguracoesModel : ModelBase
    {
        public int id { get; set; }
        public int idUsuario { get; set; }
        public string planofundo_cor { get; set; }
        public string planofundo_img { get; set; }
        public string imgperfil_borda_cor { get; set; }
        public string nome_cor { get; set; }
        public string nome_fonte { get; set; }
        public string texto_cor { get; set; }
        public string texto_fonte { get; set; }
        public string link_fundo_cor { get; set; }
        public string link_borda_cor { get; set; }
        public string link_texto_cor { get; set; }
        public string link_texto_fonte { get; set; }
        public string icone_cor { get; set; }
    }
}
