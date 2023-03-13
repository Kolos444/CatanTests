using System.Text.Json.Serialization;

namespace catan;

public class Connection{
	public bool Road   { get; set; }
	public int  Player { get; set; }

}

public class ResourceField{
	public Resource Resource { get; set; }
	public int      Value    { get; set; }

	public ResourceField(Resource resource, int value) {
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