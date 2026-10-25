using RentalService.Domain;

namespace RentalService.Assets;

public class Camera : Equipment
{
    public double Megapixels { get; }
    public string Lens { get; }
    public bool RecordsVideo { get; }

    public Camera(string name, double megapixels, string lens, bool recordsVideo) : base(name)
    {
        if (megapixels <= 0 )
            throw new ArgumentOutOfRangeException(nameof(megapixels), "Megapixels must be greater than 0.");
        if (string.IsNullOrWhiteSpace(lens))
            throw new ArgumentException("Lens cannot be empty.", nameof(lens));
        Megapixels = megapixels;
        Lens = lens;
        RecordsVideo = recordsVideo;
    }
    
    public override string GetDetails()
    {
        return $"Camera: {Name} | {Lens}, {Megapixels} Megapixels, Records Video: {(RecordsVideo ? "yes" : "no")}";
    }
    
}