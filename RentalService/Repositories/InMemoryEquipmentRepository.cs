using RentalService.Domain;

namespace RentalService.Repositories;

public class InMemoryEquipmentRepository : IEquipmentRepository
{
    private readonly List<Equipment> _items = new();

    public void Add(Equipment equipment)
    {
        _items.Add(equipment);
    }
    
    public Equipment? GetById(Guid id)
    {
        return _items.FirstOrDefault(x => x.Id == id);
    }
    
    public IReadOnlyList<Equipment> GetAll()
    {
        return _items.ToList();
    }
}