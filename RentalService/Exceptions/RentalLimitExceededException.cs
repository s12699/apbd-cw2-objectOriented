namespace RentalService.Exceptions;

public class RentalLimitExceededException : Exception
{
    public RentalLimitExceededException(string equipmentName) : base ($"Equipment {equipmentName} exceeds rental limit")
    {
    }
}