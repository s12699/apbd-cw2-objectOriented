namespace RentalService.Exceptions;

public class EquipmentUnavailableException : Exception
{
    public EquipmentUnavailableException(string equipmentName) : base($"EQ {equipmentName} is unavailable for rent")
    {
    }
}