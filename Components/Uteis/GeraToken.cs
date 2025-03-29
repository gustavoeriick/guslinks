namespace guslinks.Components.Uteis
{
	public static class GeraToken
	{
		public static string Gerar()
		{
			Random random = new Random();

			var data = DateTime.Now;

			var dia = data.Day;
			var mes = data.Month;
			var ano = data.Year;
			var hora = data.Hour;
			var minuto = data.Minute;
			var segundo = data.Second;

			var num_random = random.Next(10000000, 99999999);

			var token = $"{dia}{mes}{ano}{hora}{minuto}{segundo}{num_random}";

			return token ;
		}
	}
}
