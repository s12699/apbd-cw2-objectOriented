using RentalService.Domain;
using RentalService.Exceptions;
using RentalService.Repositories;

namespace RentalService.Services;

public class EquipmentService
{
    private readonly IEquipmentRepository _equipment;
    
    public EquipmentService(IEquipmentRepository equipment)
    {
        _equipment = equipment;
    }

    public void Add(Equipment equimpent)
    {
        _equipment.Add(equimpent);
    }
    
    public IReadOnlyList<Equipment> GetAll()
    {
        return _equipment.GetAll();
    }

    public IReadOnlyList<Equipment> GetAvailable()
    {
        return _equipment.GetAll().Where(e => e.IsAvailable).ToList();
    }

    public Equipment GetById(Guid id)
    {
        return _equipment.GetById(id) ?? throw new EntityNotFoundException(id);
    }
    
    public void MarkAsUnavailable(Guid id)
    {
        GetById(id).MarkAsUnavailable();
    }
}