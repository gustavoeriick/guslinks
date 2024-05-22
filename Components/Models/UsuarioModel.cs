namespace guslinks.Components.Models
{
    public class UsuarioModel
    {
		public int id { get; set; }
		public int cadastradopor { get; set; }
		public DateTime datacadastro { get; set; }
		public int atualizadopor { get; set; }
		public DateTime dataatualizacao { get; set; }
		public string nome { get; set; }
        public string texto { get; set; }
        public string url { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public bool vip { get; set; }
        public bool ativo { get; set; }
    }
}
