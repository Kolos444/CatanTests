using System.Text.Json;

namespace CatanTests;

public static class Programm{
	public static void Main(string[] args) {
		Game game = new Game(5, 5);

		File.WriteAllText(
			"board.json",
			JsonSerializer.Serialize(
				game,
				new JsonSerializerOptions { WriteIndented = true }
			));
	}
}