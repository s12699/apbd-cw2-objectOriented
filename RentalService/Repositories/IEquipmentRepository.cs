using RentalService.Domain;

namespace RentalService.Repositories;

public interface IEquipmentRepository
{
    void Add(Equipment equipment);
    Equipment? GetById(Guid id);
    IReadOnlyList<Equipment> GetAll();
}