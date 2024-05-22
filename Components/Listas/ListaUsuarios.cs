using guslinks.Components.Models;

namespace guslinks.Components.Listas
{
    public class ListaUsuarios
    {
        public List<UsuarioModel> listaUsuarios = new List<UsuarioModel>
        {
            new UsuarioModel { id = 1, nome = "Gustavo Ferreira", texto = "Analista de Sistemas / Dev Full Stack", url = "gus", email = "gustavo.eriick@gmail.com", senha = "gus123456", vip = true, ativo = true },
            new UsuarioModel { id = 2, nome = "Juju Sobrancelhas", texto = "✨ leveza e personalidade ao seu olhar", url = "juju", email = "juju@example.com", senha = "juju123", vip = true, ativo = true },
        };
    }
}
