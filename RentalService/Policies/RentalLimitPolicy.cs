using RentalService.Domain;

namespace RentalService.Policies;

public class RentalLimitPolicy : IRentalLimitPolicy
{

    private const int StudentLimit = 2;
    private const int EmployeeLimit = 5;

    public int GetMaxActiveRentals(UserType userType)
    {
        switch (userType)
        {
            case UserType.Student:
                return StudentLimit;
            case UserType.Employee:
                return EmployeeLimit;
            default:
                throw new ArgumentOutOfRangeException(nameof(userType), userType, "Unknown user type");
        }
    }
}