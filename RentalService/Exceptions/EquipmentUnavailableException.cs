namespace RentalService.Exceptions;

public class EquipmentUnavailableException : Exception
{
    public EquipmentUnavailableException(string equipmentName) : base($"Equipment {equipmentName} is unavailable for rent")
    {
    }
}