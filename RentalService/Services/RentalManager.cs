using RentalService.Domain;
using RentalService.Exceptions;
using RentalService.Policies;
using RentalService.Repositories;

namespace RentalService.Services;

public class RentalManager
{
    private readonly IRentalRepository _rentals;
    private readonly EquipmentService _equipment;
    private readonly UserService _users;
    private readonly IRentalLimitPolicy _limitPolicy;
    private readonly IPenaltyCalculator _penaltyCalculator;
    
    public RentalManager(
        IRentalRepository rentals,
        EquipmentService equipment,
        UserService users,
        IRentalLimitPolicy limitPolicy,
        IPenaltyCalculator penaltyCalculator)
    {
        _rentals = rentals;
        _equipment = equipment;
        _users = users;
        _limitPolicy = limitPolicy;
        _penaltyCalculator = penaltyCalculator;
    }

    public Rental Rent(Guid userId, Guid equipmentId, int days, DateTime rentedAt)
    {
        var user = _users.GetById(userId);
        var equipment = _equipment.GetById(equipmentId);

        if (!equipment.IsAvailable)
            throw new EquipmentUnavailableException(equipment.Name);

        var limit = _limitPolicy.GetMaxActiveRentals(user.Type);
        var activeCount = _rentals.GetAll().Count(r => r.IsActive && r.User.Id == userId);

        if (activeCount >= limit)
            throw new RentalLimitExceededException(user.Type, limit);
        
        var rental = new Rental(user, equipment, rentedAt, days);
        equipment.MarkAsRented();
        _rentals.Add(rental);
        return rental;
    }

    public Rental Returtn(Guid rentalId, DateTime returnedAt)
    {
        var rental = _rentals.GetById(rentalId) ?? throw new EntityNotFoundException(rentalId);

        var penalty = _penaltyCalculator.Calculate(rental, returnedAt);
        rental.Close(returnedAt, penalty);
        rental.Equipment.MarkAsReturned();
        return rental;
    }

    public IReadOnlyList<Rental> GetActiveForUser(Guid userId)
    {
        var user = _users.GetById(userId);
        return _rentals.GetAll().Where(r => r.IsActive && r.User.Id == userId).ToList();
    }

    public IReadOnlyList<Rental> GetOverdue(DateTime now)
    {
        return _rentals.GetAll().Where(r => r.IsOverdue(now)).ToList();
    }
}
