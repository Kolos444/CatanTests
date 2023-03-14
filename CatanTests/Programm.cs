namespace CatanTests;

public static class Programm{
	public static void Main(string[] args) { }
}

public class Road{
	public Road(int id) {
		ID = id;

		Nodes  = new int[2];
		Player = 0;
		Build  = false;
	}
	public int   ID     { get; }
	/// <summary>
	/// 0 = Links
	/// 1 = Rechts
	/// </summary>
	public int[] Nodes  { get; }
	public int   Player { get; }
	public bool  Build  { get; }
}