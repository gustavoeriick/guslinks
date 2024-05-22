namespace guslinks.Components.Models
{
    public class RedesModel : ModelBase
    {
        public int id { get; set; }
        public int idUsuario { get; set; }
        public int ordem { get; set; }
        public string link { get; set; }
        public int icone { get; set; }
        public bool ativo { get; set; }
    }
}
