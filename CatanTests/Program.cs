using System.Text;
using System.Text.Json;

namespace catan;

public class Programm{
	public static void Main(String[] Args) {
		Board board = new Board(10, 10);


		Console.WriteLine(board.ToString());
	}
}

public class Board{
	public int       Width     { get; }
	public int       Height    { get; }
	ResourceField[,] resources { get; set; }
	Road[,]          roads     { get; set; }
	Node[,]          nodes     { get; set; }

	public Board(int width, int height) {
		Width  = width;
		Height = height;

		resources = new ResourceField[width, height];
		roads = new Road[
			5 + (width  - 1) * 2
		  , 3 + (height - 1) * 2
		];
		nodes = new Node[
			3 + (width - 1)
		  , 4 + (height - 1) * 2
		];
	}

	public override string ToString() {
		char[,] ausgabe = new char[7 + (Width - 1) * 4, 7 + (Height - 1) * 4];

		for (int j = 0; j < ausgabe.GetLength(1); j++){
			for (int i = 0; i < ausgabe.GetLength(0); i++){
				ausgabe[i, j] = ' ';
			}
		}

		for (int y = 0; y < resources.GetLength(1); y++){
			for (int x = 0; x < resources.GetLength(0); x++){
				if (y % 2 == 0)
					ausgabe[2 + x * 4, 3 + y * 4] = '*';
				else
					ausgabe[4 + x * 4, 3 + y * 4] = '*';
			}
		}

		for (int y = 0; y < roads.GetLength(1); y++){
			for (int x = 0; x < roads.GetLength(0); x++){

			}
		}

		StringBuilder builder = new();
		for (int j = 0; j < ausgabe.GetLength(1); j++){
			for (int i = 0; i < ausgabe.GetLength(0); i++){
				builder.Append(ausgabe[i, j]);
			}
			builder.AppendLine("");
		}



		return builder.ToString();
	}
}