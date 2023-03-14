using System.Text.Json;

namespace CatanTests;

public class Game{
	public Game(int width, int height) {
		Width  = width;
		Height = height;

		Tiles = new Tile[width * height];
		Nodes = new Node[Width * 2 * Height + Height * 2 ];

		int test = Width * 2 * Height + Height * 2 ;
	}

	public Tile[] Tiles  { get; }
	public int    Width  { get; }
	public int    Height { get; }
	public Node[] Nodes  { get; }

	public void Generate() {
		int nodeID = 0;
		for (int y = 0; y < Height; y++){
			for (int x = 0; x < Width; x++){
				Tile tile = new(y * Width + x + 1);

				if (y % 2 == 0){
					if (y > 0){
						tile.Neighbours[0]                       = Tiles[(y - 1) * Width + x].ID; //Oben Rechts
						Tiles[(y - 1) * Width + x].Neighbours[3] = tile.ID;

						if (x > 0){
							tile.Neighbours[5] = Tiles[(y - 1) * Width + x - 1].ID; //Oben Links
							Tiles[(y - 1) * Width + x                  - 1].Neighbours[2] = tile.ID;
						}
					}

					if (x > 0){
						tile.Neighbours[4] = Tiles[y * Width + x - 1].ID; //links
						Tiles[y * Width + x                  - 1].Neighbours[1] = tile.ID;
					}
				}
				else{
					if (y > 0){
						if (x < Width - 1){
							tile.Neighbours[0] = Tiles[(y - 1) * Width + x + 1].ID; //Oben Rechts
							Tiles[(y - 1) * Width + x + 1].Neighbours[3] = tile.ID;
						}

						tile.Neighbours[5]                       = Tiles[(y - 1) * Width + x].ID; //Oben Links
						Tiles[(y - 1) * Width + x].Neighbours[2] = tile.ID;
					}


					if (x > 0){
						tile.Neighbours[4] = Tiles[y * Width + x - 1].ID; //Links
						Tiles[y * Width + x                  - 1].Neighbours[1] = tile.ID;
					}
				}


				//Node generierung

				//Wenn es ein Gerade Reihe ist
				if (y % 2 == 0){
					Node nodeNorth = new Node(nodeID++);
					tile.Nodes[0] = nodeNorth.ID;
					Node nodeNorthEast = new Node(nodeID++);
					tile.Nodes[1] = nodeNorthEast.ID;

					Nodes[nodeNorth.ID]     = nodeNorth;
					Nodes[nodeNorthEast.ID] = nodeNorthEast;

					//Wenn es das erste in der Reihe ist
					if (x == 0){
						Node nodeSouthWest = new Node(nodeID++);
						tile.Nodes[4] = nodeSouthWest.ID;
						Node nodeNorthWest = new Node(nodeID++);
						tile.Nodes[5] = nodeNorthWest.ID;

						Nodes[nodeSouthWest.ID] = nodeSouthWest;
						Nodes[nodeNorthWest.ID] = nodeNorthWest;
					}
				}
				else{
					Node nodeNorth = new Node(nodeID++);
					tile.Nodes[0] = nodeNorth.ID;
					Node nodeNorthWest = new Node(nodeID++);
					tile.Nodes[5] = nodeNorthWest.ID;

					Nodes[nodeNorth.ID]     = nodeNorth;
					Nodes[nodeNorthWest.ID] = nodeNorthWest;

					//Wenn es das Letzte in der Reihe ist
					if (x == Width - 1){
						Node nodeSouthEast = new Node(nodeID++);
						tile.Nodes[1] = nodeSouthEast.ID;
						Node nodeNorthEast = new Node(nodeID++);
						tile.Nodes[2] = nodeNorthEast.ID;

						Nodes[nodeSouthEast.ID] = nodeSouthEast;
						Nodes[nodeNorthEast.ID] = nodeNorthEast;
					}
				}


				Tiles[y * Width + x] = tile;
			}
		}

		string output = JsonSerializer.Serialize(Tiles, new JsonSerializerOptions { WriteIndented = true });
		File.WriteAllText("Tiles.json", output);

		output = JsonSerializer.Serialize(Nodes, new JsonSerializerOptions { WriteIndented = true });
		File.WriteAllText("Nodes.json", output);
	}
}