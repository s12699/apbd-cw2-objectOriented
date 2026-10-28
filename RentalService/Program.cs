// See https://aka.ms/new-console-template for more information

using RentalService.Policies;
using RentalService.Repositories;
using RentalService.Services;
using RentalService.UI;

namespace RentalService;

public class Program
{
    public static void Main(string[] args)
    {
        var equipmentRepository = new InMemoryEquipmentRepository();
        var userRepository = new InMemoryUserRepository();
        var rentalRepository = new InMemoryRentalRepository();
        
        var equipmentService = new EquipmentService(equipmentRepository);
        var userService = new UserService(userRepository);
        var rentalManager = new RentalManager(
            rentalRepository,
            equipmentService,
            userService,
            new RentalLimitPolicy(),
            new DailyPenaltyCalculator(10m));

        var reportSerice = new ReportService(equipmentRepository, rentalRepository);
        
        new DemoScenario(equipmentService, userService, rentalManager, reportSerice);
    }
}