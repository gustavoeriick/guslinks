using guslinks.Components.Models;

namespace guslinks.Components.Listas
{
    public class ListaUsuarios
    {
        public List<Usuario> listaUsuarios = new List<Usuario>
        {
            new Usuario { id = 1, nome = "Gustavo Ferreira", texto = "Analista de Sistemas / Dev Full Stack", link = "gus", email = "gus@example.com", senha = "gus123", plus = true, ativo = true },
            new Usuario { id = 2, nome = "Juju Sobrancelhas", texto = "✨ leveza e personalidade ao seu olhar", link = "juju", email = "juju@example.com", senha = "juju123", plus = true, ativo = true },
        };
    }
}
