using Microsoft.VisualBasic;

namespace RentalService.Domain;

public class Rental
{
    public Guid Id { get; } = Guid.NewGuid();
    public User User { get; }
    public Equipment Equipment { get; }
    public DateTime RentedAt { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnedAt { get; private set; }
    public decimal Penalty { get; private set; }

    public bool IsActive => ReturnedAt is null;
    public bool WasReturnedOnTime => ReturnedAt is not null && ReturnedAt <= DueDate;

    public Rental(User user, Equipment equipment, DateTime rentedAt, int days)
    {
        if (user is null)
            throw new ArgumentNullException(nameof(user), "User cannot be null.");
        if (equipment is null)
            throw new ArgumentNullException(nameof(equipment), "Equipment cannot be null.");
        if (days <= 0)
            throw new ArgumentOutOfRangeException(nameof(days), "Rental period must be at least 1 day.");
        User = user;
        Equipment = equipment;
        RentedAt = rentedAt;
        DueDate = rentedAt.AddDays(days);
    }
    
    public bool IsOverdue(DateTime now) => IsActive && now > DueDate;

    public void Close(DateTime returnedAt, decimal penalty)
    {
        if (!IsActive)
            throw new InvalidOperationException("This rental has been already closed.");
        ReturnedAt = returnedAt;
        Penalty = penalty;
    }
}