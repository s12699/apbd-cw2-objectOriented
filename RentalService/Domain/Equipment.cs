namespace RentalService.Domain;

public abstract class Equipment
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; }
    public EquipmentStatus Status { get; private set; } = EquipmentStatus.Available;

    public bool IsAvailable => Status == EquipmentStatus.Available;

    protected Equipment(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Equipment name cannot be empty.", nameof(name));
        Name = name;
    }

    public void MarkAsRented()
    {
        if (!IsAvailable)
            throw new InvalidOperationException($"'{Name}' is not available for rent.");
        Status = EquipmentStatus.Rented;
    }

    public void MarkAsUnavailable()
    {
        if (Status == EquipmentStatus.Rented)
            throw new InvalidOperationException($"'{Name}' is rented and must be returned first.");
        Status = EquipmentStatus.Unavailable;
    }

    public void MarkAsReturned()
    {
        if (Status != EquipmentStatus.Rented)
            throw new InvalidOperationException($"'{Name}' is not currently rented.");
        Status = EquipmentStatus.Available;
    }

    public abstract string GetDetails();
}