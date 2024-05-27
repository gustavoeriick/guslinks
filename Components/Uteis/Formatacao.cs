using System;
using System.Drawing;

namespace guslinks.Components.Uteis
{
	public static class Formatacao
	{
		public static string Msg(string msg)
		{
			string msgpadrao = $"- {msg}.\n";

			return msgpadrao;
		}

		public static string Nome(string nome)
		{
			// Remove os espaços no início e no fim da string
			nome = nome.Trim();

			return nome;
		}

		public static string Url(string url)
		{
			// Remove os espaços no início e no fim da string
			url = url.Trim();

			// Remove todos os espaços dentro da string
			url = url.Replace(" ", "");

			return url;
		}
	}
}
