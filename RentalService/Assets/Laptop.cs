using RentalService.Domain;

namespace RentalService.Assets;

public class Laptop : Equipment
{
    
    public int RamGB { get; }
    public string Processor { get; }
    public double ScreenSizeInches { get; }

    public Laptop(string name, int ramGB, string processor, double screenSizeInches) : base(name)
    {
        if(ramGB <= 0)
            throw new ArgumentOutOfRangeException(nameof(ramGB), "RAM must be greater than 0.");
        if (string.IsNullOrEmpty(processor))
            throw new ArgumentException("Processor cannot be empty.", nameof(processor));
        if (screenSizeInches <= 0)
            throw new ArgumentOutOfRangeException(nameof(screenSizeInches), "Screen size must be greater than 0.");
        RamGB = ramGB;
        Processor = processor;  
        ScreenSizeInches = screenSizeInches;
    }

    public override string GetDetails() => $"Laptop: {Name} | {Processor}, {RamGB} GB RAM, {ScreenSizeInches}\"";
}