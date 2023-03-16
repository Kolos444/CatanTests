namespace CatanTests;

public static class Programm{
	public static void Main(string[] args) {
		Game game = new Game(10, 9);

		game.Generate();
	}
}