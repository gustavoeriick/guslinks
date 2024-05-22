namespace guslinks.Components.Models
{
    public class ModelBase
    {
        public int id { get; set; }
        public int cadastradopor { get; set; }
        public DateTime datacadastro { get; set; }
        public int atualizadopor { get; set; }
        public DateTime dataatualizacao { get; set; }
    }
}
