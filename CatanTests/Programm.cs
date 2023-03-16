namespace CatanTests;

public static class Programm{
	public static void Main(string[] args) {
		Game game = new Game(5, 4);

		game.Generate();
	}
}