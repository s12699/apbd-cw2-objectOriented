using RentalService.Domain;

namespace RentalService.Policies;

public interface IPenaltyCalculator
{
    decimal Calculate(Rental rental, DateTime returnedAt);
}