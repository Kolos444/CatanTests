using System.Text.Json;

namespace CatanTests;

public class Game{
	public Game(int width, int height) {
		Width  = width;
		Height = height;

		Tiles = new Tile[width * height];

		Nodes = new Node[Width * 2 * Height + Height * 2 + Width * 2];

		Roads = new Road[(3 * Width + 2) * Height + 2 * Width - 1];

		int test = (3 * Width + 2) * Height + 2 * Width - 1;
	}

	public Tile[] Tiles  { get; }
	public int    Width  { get; }
	public int    Height { get; }
	public Node[] Nodes  { get; }
	public Road[] Roads  { get; }

	public void Generate() {
		int nodeID = 0;
		int roadID = 0;
		for (int y = 0; y < Height; y++){
			for (int x = 0; x < Width; x++){
				Tile tile = new(y * Width + x + 1);


				#region Tile generierung

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

				#endregion


				#region Node generierung

				//Wenn es ein Gerade Reihe ist
				if (y % 2 == 0){
					//Wenn es das erste in der Reihe ist
					if (x == 0){
						Node nodeSouthWest = new Node(nodeID++);
						tile.Nodes[4] = nodeSouthWest.ID;
						Node nodeNorthWest = new Node(nodeID++);
						tile.Nodes[5] = nodeNorthWest.ID;

						nodeSouthWest.Tiles[0] = tile.ID;
						nodeNorthWest.Tiles[1] = tile.ID;

						Nodes[nodeSouthWest.ID] = nodeSouthWest;
						Nodes[nodeNorthWest.ID] = nodeNorthWest;
					}

					Node nodeNorth = new Node(nodeID++);
					tile.Nodes[0] = nodeNorth.ID;
					Node nodeNorthEast = new Node(nodeID++);
					tile.Nodes[1] = nodeNorthEast.ID;

					nodeNorth.Tiles[1]     = tile.ID;
					nodeNorthEast.Tiles[2] = tile.ID;



					if (x > 0){
						Nodes[Tiles[tile.ID - 2].Nodes[1]].Tiles[1] = tile.ID;
						tile.Nodes[5]                               = Tiles[tile.ID - 2].Nodes[1];
					}

					if (y > 0){
						nodeNorthEast.Tiles[0] = Tiles[tile.ID - Width - 1].ID; //Oben

						if (x < Width - 1){
							nodeNorth.Tiles[0] = Tiles[tile.ID - Width - 1].ID; //Oben Rechts
						}

						if (x > 0){
							nodeNorth.Tiles[2] = Tiles[tile.ID - Width - 2].ID; //Oben links
						}

						Tiles[tile.ID - Width - 1].Nodes[3] = nodeNorthEast.ID;
						Tiles[tile.ID - Width - 1].Nodes[4] = nodeNorth.ID;

						Tiles[tile.ID - Width - 2].Nodes[2] = nodeNorth.ID;
					}

					Nodes[nodeNorth.ID]     = nodeNorth;
					Nodes[nodeNorthEast.ID] = nodeNorthEast;

					//Wenn es die letzte Reihe ist
					if (y == Height - 1){
						Node nodeSouth = new Node(nodeID++);
						tile.Nodes[3] = nodeSouth.ID;
						Node nodeSouthEast = new Node(nodeID++);
						tile.Nodes[2] = nodeSouthEast.ID;

						nodeSouth.Tiles[0]     = tile.ID;
						nodeSouthEast.Tiles[2] = tile.ID;

						//Wenn es nicht das letzte in der Reihe ist
						if (x > 0)
							tile.Nodes[4] = Tiles[tile.ID-2].Nodes[2];

						Nodes[nodeSouth.ID]     = nodeSouth;
						Nodes[nodeSouthEast.ID] = nodeSouthEast;
					}
				}
				else{
					Node nodeNorthWest = new Node(nodeID++);
					tile.Nodes[5] = nodeNorthWest.ID;
					Node nodeNorth = new Node(nodeID++);
					tile.Nodes[0] = nodeNorth.ID;


					nodeNorth.Tiles[1]     = tile.ID; //Unten
					nodeNorthWest.Tiles[1] = tile.ID; //Unten Links

					tile.Nodes[5] = nodeNorthWest.ID;


					Tiles[tile.ID - Width - 1].Nodes[2]  = nodeNorth.ID;
					Tiles[tile.ID - Width - 1].Nodes[3] = nodeNorthWest.ID;
					if (x < Width - 1)
						Tiles[tile.ID - Width].Nodes[4] = nodeNorth.ID;

					if (x > 0){
						nodeNorthWest.Tiles[2]      = tile.ID - 1; //Unten links
						Tiles[tile.ID - 2].Nodes[1] = nodeNorthWest.ID;
					}

					if (x < Width - 1){
						nodeNorth.Tiles[0] = Tiles[tile.ID - Width].ID; //Oben rechts
					}


					nodeNorth.Tiles[2]     = Tiles[tile.ID - Width - 1].ID; //Oben links
					nodeNorthWest.Tiles[0] = Tiles[tile.ID - Width - 1].ID; //Oben

					//Wenn es das Letzte in der Reihe ist
					if (x == Width - 1){
						Node nodeSouthEast = new Node(nodeID++);
						tile.Nodes[1] = nodeSouthEast.ID;
						Node nodeNorthEast = new Node(nodeID++);
						tile.Nodes[2] = nodeNorthEast.ID;

						nodeSouthEast.Tiles[2] = tile.ID;
						nodeNorthEast.Tiles[2] = tile.ID;

						Nodes[nodeSouthEast.ID] = nodeSouthEast;
						Nodes[nodeNorthEast.ID] = nodeNorthEast;
					}

					Nodes[nodeNorth.ID]     = nodeNorth;
					Nodes[nodeNorthWest.ID] = nodeNorthWest;


					//Wenn es die letzte Reihe ist
					if (y == Height - 1){
						Node nodeSouthWest = new Node(nodeID++);
						tile.Nodes[4] = nodeSouthWest.ID;
						Node nodeSouth = new Node(nodeID++);
						tile.Nodes[3] = nodeSouth.ID;

						nodeSouthWest.Tiles[0] = tile.ID;
						nodeSouth.Tiles[0]     = tile.ID;

						//Wenn es nicht das letzte in der Reihe ist
						if (x > 0){
							nodeSouthWest.Tiles[2]      = tile.ID - 1;
							Tiles[tile.ID - 2].Nodes[2] = nodeSouthWest.ID;
						}

						Nodes[nodeSouthWest.ID] = nodeSouthWest;
						Nodes[nodeSouth.ID]     = nodeSouth;
					}
				}

				#endregion


				Tiles[y * Width + x] = tile;
			}
		}

		for (int y = 0; y < Height; y++){
			for (int x = 0; x < Width; x++){
				Tile tile = Tiles[y * Width + x];

				#region Straßen generierung



				#endregion
			}
		}

		string output = JsonSerializer.Serialize(Tiles, new JsonSerializerOptions { WriteIndented = true });
		File.WriteAllText("Tiles.json", output);

		output = JsonSerializer.Serialize(Nodes, new JsonSerializerOptions { WriteIndented = true });
		File.WriteAllText("Nodes.json", output);

		output = JsonSerializer.Serialize(Roads, new JsonSerializerOptions { WriteIndented = true });
		File.WriteAllText("Roads.json", output);

		// int roadErrors = -2; //-2 Da eine NodeID 0 ist und sie kann 2 Roads haben
		// for (int i = 0; i < Roads.Length; i++){
		// 	if (Roads[i].Nodes[0] == 0)
		// 		roadErrors++;
		// 	if (Roads[i].Nodes[1] == 0)
		// 		roadErrors++;
		// }
	}
}