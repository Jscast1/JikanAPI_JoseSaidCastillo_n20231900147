using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using JikanAPI_JoseSaidCastillo_n20231900147.Models;

class Program
{
	static async Task Main()
	{
		Console.WriteLine("🌀 Buscador de Anime - Jujutsu Kaisen");

		Console.Write("Ingrese nombre del anime: ");
		string nombre = Console.ReadLine();

		await MostrarAnime(nombre);
	}

	static async Task MostrarAnime(string nombre)
	{
		using HttpClient client = new HttpClient();
		string url = $"https://api.jikan.moe/v4/anime?q={nombre.Replace(" ", "%20")}";

		try
		{
			HttpResponseMessage response = await client.GetAsync(url);

			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine("❌ Error al consultar la API.");
				return;
			}

			string json = await response.Content.ReadAsStringAsync();

			AnimeResponse data = JsonSerializer.Deserialize<AnimeResponse>(json);

			if (data.data.Length > 0)
			{
				var anime = data.data[0];

				Console.WriteLine($"\n🎬 Título: {anime.title}");
				Console.WriteLine($"📅 Año: {anime.year}");
				Console.WriteLine($"⭐ Score: {anime.score}");
				Console.WriteLine($"📺 Episodios: {anime.episodes}");
				Console.WriteLine($"\n📖 Sinopsis:\n{anime.synopsis}");
			}
			else
			{
				Console.WriteLine("⚠ No se encontraron resultados.");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"⚠ Error: {ex.Message}");
		}
	}
}