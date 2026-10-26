using RentalService.Domain;

namespace RentalService.Policies;

public class DailyPenaltyCalculator : IPenaltyCalculator
{
    private readonly decimal _dailyFee;

    public DailyPenaltyCalculator(decimal dailyFee = 10m)
    {
        if (dailyFee < 0)
            throw new ArgumentOutOfRangeException(nameof(dailyFee), "Daily fee cannot be negative");
        
        _dailyFee = dailyFee;
    }

    public decimal Calculate(Rental rental, DateTime returnedAt)
    {
        if (returnedAt <= rental.DueDate)
            return 0m;
        
        var lateDays = (int)Math.Ceiling((returnedAt - rental.DueDate).TotalDays);
        return lateDays * _dailyFee;
    }
}