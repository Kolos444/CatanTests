﻿namespace CatanTests;

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
		ID = id;

		Neighbours = new int[6];
		Nodes      = new int[6];
		Roads      = new int[6];
	}

	public int ID { get; }

	public RESOURCE Resource { get; }
	public int?     Value    { get; }
	public bool     Harbor   { get; }

	/// <summary>
	/// 0-5
	/// <para> Oben Rechts </para>
	/// Rechts
	/// <para> Unten Rechts </para>
	/// Unten Links
	/// <para> Links </para>
	/// Oben Links
	/// </summary>
	public int[] Neighbours { get; }

	/// <summary>
	/// 0 - 5 Oben bis Oben Links
	/// </summary>
	public int[] Nodes { get; }

	/// <summary>
	/// 0-5
	/// <para> Oben Rechts </para>
	/// Rechts
	/// <para> Unten Rechts </para>
	/// Unten Links
	/// <para> Links </para>
	/// Oben Links
	/// </summary>
	public int[] Roads { get; }
}