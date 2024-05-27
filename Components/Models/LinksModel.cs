namespace guslinks.Components.Models
{
    public class LinksModel
    {
        public int id { get; set; }
        public int idUsuario { get; set; }
        public int idGrupoLinks { get; set; }
        public string link { get; set; }
        public string texto { get; set; }
        public string imagem { get; set; }
        public int ordem { get; set; }
        public bool ativo { get; set; }
    }
}
