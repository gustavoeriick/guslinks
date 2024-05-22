using guslinks.Components.Models;

namespace guslinks.Components.Listas
{
    public class ListaLinks
    {
        public List<LinksModel> listaLinks = new List<LinksModel>
        {
            new LinksModel { id = 1, idUsuario = 1, link = "#", texto = "LINK", imagem = "", ordem = 1 , ativo = true},
            new LinksModel { id = 2, idUsuario = 1, link = "#", texto = "LINK", imagem = "", ordem = 2 , ativo = true},
            new LinksModel { id = 3, idUsuario = 1, link = "#", texto = "LINK", imagem = "", ordem = 3 , ativo = true},
            new LinksModel { id = 4, idUsuario = 1, link = "#", texto = "LINK", imagem = "", ordem = 4 , ativo = true},

            new LinksModel { id = 5, idUsuario = 2, link = "https://www.trinks.com/juju-sobrancelhas", texto = "AGENDE SEU HORÁRIO", imagem = "", ordem = 1 , ativo = true},
            new LinksModel { id = 6, idUsuario = 2, link = "https://api.whatsapp.com/message/X3BBAJYWM353G1?autoload=1&app_absent=0", texto = "TIRE SUAS DÚVIDAS PELO WHATSAPP", imagem = "", ordem = 2 , ativo = true},
            new LinksModel { id = 7, idUsuario = 2, link = "https://www.google.com.br/search?q=juju+sobrancelhas+avaliacoes&client=safari&hl=pt-br&sxsrf=ALiCzsZL-N_bBnLLd4dVSU-TXC9FeTHChw%3A1661111046043&ei=BosCY46RAoHI1sQPw5a72A4&oq=juju+sobrancelhas+avali&gs_lcp=ChNtb2JpbGUtZ3dzLXdpei1zZXJwEAEYADIECCMQJzIFCCEQoAEyBQghEKABOgoIABAeEKIEELADOggIABCiBBCwAzoECCEQFUoECEEYAVDiDVi6EmCbHWgAcAB4AIAB0AGIAeUHkgEFMC41LjGYAQCgAQHIAQXAAQE&sclient=mobile-gws-wiz-serp#lkt=LocalPoiReviews&lpg=cid:CgIgAQ%3D%3D&trex=m_t:lcl_akp,rc_ludocids:10776577173865376992,rc_q:juju%2520sobrancelhas%2520avaliacoes,ru_gwp:0%252C7,ru_q:juju%2520sobrancelhas%2520avaliacoes,trex_id:PNtK8b", texto = "AVALIAÇÕES 🤍", imagem = "", ordem = 3 , ativo = true},
        };
    }
}
