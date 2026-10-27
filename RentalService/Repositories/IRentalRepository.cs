using RentalService.Domain;

namespace RentalService.Repositories;

public interface IRentalRepository
{
    void Add(Rental rental);
    Rental? GetById(Guid id);
    IReadOnlyList<Rental> GetAll();
}