using RentalService.Domain;
using RentalService.Repositories;

namespace RentalService.Services;

public class ReportService
{
    private readonly IEquipmentRepository _equipment;
    private readonly IRentalRepository _rentals;
    
    public ReportService(IEquipmentRepository equipment, IRentalRepository rentals)
    {
        _equipment = equipment;
        _rentals = rentals;
    }

    public ServiceReport Generate(DateTime now)
    {
        var equipment = _equipment.GetAll();
        var rentals = _rentals.GetAll();

        return new ServiceReport(
            TotalEquipment: equipment.Count,
            AvailableEquipment: equipment.Count(e => e.Status == EquipmentStatus.Available),
            RentedEquipment: equipment.Count(e => e.Status == EquipmentStatus.Rented),
            UnavailableEquipment: equipment.Count(e => e.Status == EquipmentStatus.Unavailable),
            ActiveRentals: rentals.Count(r => r.IsActive),
            OverdueRentals: rentals.Count(r => r.IsOverdue(now)),
            TotalPenalties: rentals.Sum(r => r.Penalty)
        );
    }
}