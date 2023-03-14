namespace CatanTests;

public class Node{

	public Node(int id) {
		ID = id;

		Tiles    = new int[3];
		Roads    = new int[3];
		Building = BUILDING.None;
	}

	public BUILDING Building { get; }
	public int      ID       { get; }

	public int[] Tiles { get; }
	public int[] Roads { get; }

}