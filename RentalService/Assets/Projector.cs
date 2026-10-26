using RentalService.Domain;

namespace RentalService.Assets;

public class Projector : Equipment
{
    public string Resolution { get; }
    public int Lumens { get; }
    public string Manufacturer { get; }

    public Projector(string name, string resolution, int lumens, string manufacturer) : base(name)
    {
        if (string.IsNullOrWhiteSpace(resolution))
            throw new ArgumentException("Resolution cannot be empty.", nameof(resolution));
        if (lumens <= 0)
            throw new ArgumentOutOfRangeException(nameof(lumens), "Lumens must be greater than 0.");
        if (string.IsNullOrWhiteSpace(manufacturer))
            throw new ArgumentException("Manufacturer cannot be empty.", nameof(manufacturer));
        Manufacturer = manufacturer;
        Resolution = resolution;
        Lumens = lumens;
    }
    
    public override string GetDetails()
    {
        return $"Projector: {Name} | {Manufacturer}, {Lumens} Lumens, Resolution: {Resolution}";
    }
}