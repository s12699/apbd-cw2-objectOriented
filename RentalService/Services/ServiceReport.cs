namespace RentalService.Services;

public record ServiceReport(
    int TotalEquipment,
    int AvailableEquipment,
    int RentedEquipment,
    int UnavailableEquipment,
    int ActiveRentals,
    int OverdueRentals,
    decimal TotalPenalties);