namespace CatanTests;

public class Tile{
	/// <summary>
	/// Initalisiert die Klasse mit den nötigen Arrays für benachbarte ID's
	/// </summary>
	/// <param name="id">Eindeutige Nummer</param>
	/// <param name="harbor"></param>
	private Tile(int id, bool harbor) {
		ID     = id;
		Harbor = harbor;

		Neighbours = new int[6];
		Nodes      = new int[6];
		Roads      = new int[6];

		if (harbor){
			Value = null;
		}
	}

	/// <summary>
	/// Initialisiert ein Resourcen Feld
	/// </summary>
	/// <param name="resource">Die Resource die das Feld erträgt</param>
	/// <param name="value">Bei welchem Würfelwurf das Feld erträge gibt</param>
	/// <param name="id">Eindeutige Nummer</param>
	public Tile(int id, RESOURCE resource, int value) : this(id, false) {
		Resource = resource;
		Value    = value;
	}

	/// <summary>
	/// Erzeugt ein Hafen Feld
	/// </summary>
	/// <param name="id">Eindeutige Nummer</param>
	/// <param name="resource">Welche Resource der Hafen tauscht, wenn nicht angegeben 3:1 jeder Resource</param>
	public Tile(int id, RESOURCE resource = RESOURCE.None) : this(id, true) {
		Resource = resource;
	}

	public Tile(int id) {
		ID       = id;

		Neighbours = new int[6];
		Nodes      = Array.Empty<int>();
		Roads      = Array.Empty<int>();
	}

	public int ID { get; }

	public RESOURCE Resource { get; }
	public int?     Value    { get; }
	public bool     Harbor   { get; }


	public int[] Neighbours { get; }
	public int[] Nodes      { get; }
	public int[] Roads      { get; }
}