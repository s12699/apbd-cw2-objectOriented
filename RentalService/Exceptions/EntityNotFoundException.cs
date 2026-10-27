namespace RentalService.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(Guid equipmentName) : base($"Equipment {equipmentName} doesn't exist")
    {
    }
}