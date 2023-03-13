namespace catan;

public class Node{
	public BUILDING? Building { get; set; }
	public int?      Player   { get; set; }

	public Node() {
		Building = null;
		Player   = null;
	}
}

public enum BUILDING{
	Settlement,
	City
}