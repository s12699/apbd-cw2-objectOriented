using RentalService.Domain;

namespace RentalService.Policies;

public interface IRentalLimitPolicy
{
    int GetMaxActiveRentals(UserType userType);
}