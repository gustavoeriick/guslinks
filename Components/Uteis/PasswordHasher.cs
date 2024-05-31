using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace guslinks.Components.Uteis
{
	public class PasswordHasher
	{
		public static string HashPassword(string password)
		{
			// Gera um salt seguro de 128 bits
			byte[] salt = new byte[16];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(salt);
			}

			// Configura o Argon2id
			var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
			{
				Salt = salt,
				DegreeOfParallelism = 8, // Número de threads a serem usadas
				MemorySize = 1024 * 64,  // Quantidade de memória a ser usada em KB
				Iterations = 4           // Número de iterações
			};

			// Gera o hash
			byte[] hash = argon2.GetBytes(16);

			// Retorna o hash e o salt juntos
			return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
		}

		public static bool VerifyPassword(string hashedPasswordWithSalt, string providedPassword)
		{
			// Divide a string hash e o salt
			var parts = hashedPasswordWithSalt.Split(':');
			if (parts.Length != 2)
			{
				throw new FormatException("Formato inválido de senha hash.");
			}

			var salt = Convert.FromBase64String(parts[0]);
			var storedHash = Convert.FromBase64String(parts[1]);

			// Configura o Argon2id com o salt extraído
			var argon2 = new Argon2id(Encoding.UTF8.GetBytes(providedPassword))
			{
				Salt = salt,
				DegreeOfParallelism = 8,
				MemorySize = 1024 * 64,
				Iterations = 4
			};

			// Gera o hash da senha fornecida
			byte[] providedHash = argon2.GetBytes(16);

			// Compara o hash derivado com o hash armazenado
			return storedHash.SequenceEqual(providedHash);
		}
	}
}
