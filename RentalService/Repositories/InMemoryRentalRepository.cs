using RentalService.Domain;

namespace RentalService.Repositories;

public class InMemoryRentalRepository : IRentalRepository
{
    public readonly List<Rental> _items = new();
    
    public void Add(Rental rental)
    {
        _items.Add(rental);
    }
    
    public Rental? GetById(Guid id)
    {
        return _items.FirstOrDefault(x => x.Id == id);
    }

    public IReadOnlyList<Rental> GetAll()
    {
        return _items.ToList();
    }
}