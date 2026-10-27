namespace RentalService.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string equipmentName) : base($"Equipment {equipmentName} doesn't exist")
    {
    }
}