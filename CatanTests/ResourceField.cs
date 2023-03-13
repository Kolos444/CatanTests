using System.Text.Json.Serialization;

namespace catan;

class Feld{
	public Resource Resource { get; set; }
	public int      Value    { get; set; }

	public Feld(Resource resource, int value) {
		Resource = resource;
		Value    = value;
	}
}

public enum Resource{
	Wheat,
	Lumber,
	Ore,
	Sheep,
	Brick
}