﻿using guslinks.Components.Models;

namespace guslinks.Components.Listas
{
    public class ListaRedes
    {
        public List<RedesModel> listaRedes = new List<RedesModel>
        {
            new RedesModel { id = 1, idUsuario = 1, link = "https://www.instagram.com/gustavoeriick/", icone = 1, ordem = 1 , ativo = true},
            new RedesModel { id = 2, idUsuario = 1, link = "https://github.com/gustavoeriick", icone = 3, ordem = 2 , ativo = true},

            new RedesModel { id = 3, idUsuario = 2, link = "https://www.instagram.com/jujusobrancelhas/", icone = 1, ordem = 1 , ativo = true},
            new RedesModel { id = 4, idUsuario = 2, link = "https://www.facebook.com/jujusobrancelhass", icone = 4, ordem = 2 , ativo = true},
        };
    }
}
