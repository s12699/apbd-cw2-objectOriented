using RentalService.Domain;

namespace RentalService.Repositories;

public interface IRentalRepositories
{
    void Add(Rental rental);
    Rental? GetById(Guid id);
    IReadOnlyList<Rental> GetAll();
}