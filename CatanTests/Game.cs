using System.Text.Json;

namespace CatanTests;

public class Game{
	public Game(int width, int height) {
		Width  = width;
		Height = height;

		Board = new Tile[width * height];
	}

	public Tile[] Board  { get; }
	public int    Width  { get; }
	public int    Height { get; }

	private void Generate() {
		for (int y = 0; y < Height; y++){
			for (int x = 0; x < Width; x++){
				Tile tile = new(y * Width + x + 1);

				if (y % 2 == 0){
					if (y > 0){
						tile.Neighbours[0]                       = Board[(y - 1) * Width + x].ID; //Oben Rechts
						Board[(y - 1) * Width + x].Neighbours[3] = tile.ID;

						if (x > 0){
							tile.Neighbours[5] = Board[(y - 1) * Width + x - 1].ID; //Oben Links
							Board[(y - 1) * Width + x                  - 1].Neighbours[2] = tile.ID;
						}
					}

					if (x > 0){
						tile.Neighbours[4] = Board[y * Width + x - 1].ID; //links
						Board[y * Width + x                  - 1].Neighbours[1] = tile.ID;
					}
				}
				else{
					if (y > 0){
						if (x < Width - 1){
							tile.Neighbours[0] = Board[(y - 1) * Width + x + 1].ID; //Oben Rechts
							Board[(y - 1) * Width + x + 1].Neighbours[3] = tile.ID;
						}

						tile.Neighbours[5]                       = Board[(y - 1) * Width + x].ID; //Oben Links
						Board[(y - 1) * Width + x].Neighbours[2] = tile.ID;
					}


					if (x > 0){
						tile.Neighbours[4] = Board[y * Width + x - 1].ID; //Links
						Board[y * Width + x                  - 1].Neighbours[1] = tile.ID;
					}
				}


				Board[y * Width + x] = tile;
			}
		}

		string output = JsonSerializer.Serialize(Board, new JsonSerializerOptions { WriteIndented = true });
		File.WriteAllText("WriteLines.json", output);
	}
}