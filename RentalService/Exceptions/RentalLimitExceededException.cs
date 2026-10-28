using RentalService.Domain;

namespace RentalService.Exceptions;

public class RentalLimitExceededException : Exception
{
    public RentalLimitExceededException(UserType userType, int limit) : base ($"User {userType} is allowed to rent up to {limit} items")
    {
    }
}