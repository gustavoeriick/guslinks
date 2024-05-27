using System.Text.RegularExpressions;

namespace guslinks.Components.Uteis
{
	public static class Validacoes
	{
		public static bool ValidarNome(string valor)
		{
			// Expressão regular ajustada para incluir letras maiúsculas e minúsculas (incluindo acentuadas), números e espaços
			Regex regex = new Regex("[^a-zA-Zà-úÀ-Ú0-9 ]");

			// Verifica se o valor contém caracteres inválidos
			return !regex.IsMatch(valor);
		}

		public static bool ValidarUrl(string valor)
		{
			// Expressão regular que valida se há caracteres que não são letras nem números
			Regex regex = new Regex("[^a-zA-Z0-9]");

			// Verifica se o valor contém caracteres inválidos
			return !regex.IsMatch(valor);
		}

		public static bool ValidarCaracteres2(string valor)
		{
			// Expressão regular que valida se há caracteres que não são letras, números ou especiais
			Regex regex = new Regex("[^a-zA-Z0-9@!?#$%&*]");

			// Verifica se o valor contém caracteres inválidos
			return !regex.IsMatch(valor);
		}

		public static bool ValidarEmail(string email)
		{
			// Expressão regular para validar e-mails
			string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

			// Verifica se o e-mail corresponde ao padrão
			return Regex.IsMatch(email, pattern);
		}
	}
}
