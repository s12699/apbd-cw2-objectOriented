using RentalService.Assets;
using RentalService.Domain;
using RentalService.Exceptions;
using RentalService.Services;

namespace RentalService.UI;

public class DemoScenario
{
    private readonly EquipmentService _equipment;
    private readonly UserService _users;
    private readonly RentalManager _rentals;
    private readonly ReportService _reports;

    public DemoScenario(EquipmentService equipment, UserService users,
        RentalManager rentals, ReportService reports)
    {
        _equipment = equipment;
        _users = users;
        _rentals = rentals;
        _reports = reports;
    }

    public void Run()
    {
        var start = new DateTime(2026, 11, 2, 10, 0, 0);

        Section("1. Adding equipment");
        var laptop1 = new Laptop("Dell Latitude 5440", 16, "Intel Core i7", 14.0);
        var laptop2 = new Laptop("Lenovo ThinkPad T14", 32, "AMD Ryzen 7", 14.0);
        var projector = new Projector("Epson EB-X49", "1024x768", 3600, "Epson");
        var camera = new Camera("Canon EOS 250D", 24.1, "18-55mm kit", true);
        foreach (Equipment item in new Equipment[] { laptop1, laptop2, projector, camera })
            _equipment.Add(item);
        PrintEquipment();

        Section("2. Adding users");
        var anna = _users.Add("Anna", "Kowalska", UserType.Student);
        var jan = _users.Add("Jan", "Nowak", UserType.Student);
        var piotr = _users.Add("Piotr", "Wisniewski", UserType.Employee);
        foreach (var user in _users.GetAll())
            Console.WriteLine($"  {user.FullName} ({user.Type})");

        Section("3. Correct rental");
        var annaLaptop = _rentals.Rent(anna.Id, laptop1.Id, 7, start);
        Console.WriteLine($"  {Describe(annaLaptop)}");

        Section("4. Invalid operations");
        _equipment.MarkAsUnavailable(camera.Id);
        TryRun("Jan rents the camera marked as unavailable",
            () => _rentals.Rent(jan.Id, camera.Id, 3, start));

        _rentals.Rent(anna.Id, laptop2.Id, 7, start);
        Console.WriteLine("  OK: Anna rents a second item (her limit is 2)");
        TryRun("Anna rents a third item",
            () => _rentals.Rent(anna.Id, projector.Id, 3, start));

        Section("5. Return on time");
        var onTime = _rentals.Return(annaLaptop.Id, start.AddDays(5));
        Console.WriteLine($"  {Describe(onTime)} | on time: {onTime.WasReturnedOnTime} | penalty: {onTime.Penalty}");

        Section("6. Late return with penalty");
        var janProjector = _rentals.Rent(jan.Id, projector.Id, 3, start);
        var late = _rentals.Return(janProjector.Id, start.AddDays(7));
        Console.WriteLine($"  {Describe(late)} | on time: {late.WasReturnedOnTime} | penalty: {late.Penalty}");

        Section("7. Overdue rentals and final report");
        _rentals.Rent(piotr.Id, laptop1.Id, 2, start.AddDays(1));
        var now = start.AddDays(10);

        Console.WriteLine("  Active rentals of Anna:");
        foreach (var r in _rentals.GetActiveForUser(anna.Id))
            Console.WriteLine($"    {Describe(r)}");

        Console.WriteLine("  Overdue rentals:");
        foreach (var r in _rentals.GetOverdue(now))
            Console.WriteLine($"    {Describe(r)}");

        PrintReport(_reports.Generate(now));
    }

    private void PrintEquipment()
    {
        foreach (var item in _equipment.GetAll())
            Console.WriteLine($"  [{item.Status}] {item.GetDetails()}");
    }

    private static void PrintReport(ServiceReport report)
    {
        Console.WriteLine("  Report:");
        Console.WriteLine($"    Equipment: {report.TotalEquipment} total, {report.AvailableEquipment} available, " +
                          $"{report.RentedEquipment} rented, {report.UnavailableEquipment} unavailable");
        Console.WriteLine($"    Rentals: {report.ActiveRentals} active, {report.OverdueRentals} overdue");
        Console.WriteLine($"    Total penalties: {report.TotalPenalties}");
    }

    private static string Describe(Rental r)
        => $"{r.User.FullName} -> {r.Equipment.Name}, due {r.DueDate:yyyy-MM-dd}";

    private static void Section(string title)
    {
        Console.WriteLine();
        Console.WriteLine($"== {title} ==");
    }

    private static void TryRun(string description, Action action)
    {
        try
        {
            action();
            Console.WriteLine($"  OK: {description}");
        }
        catch (Exception ex) when (ex is EquipmentUnavailableException
                                      or RentalLimitExceededException
                                      or EntityNotFoundException)
        {
            Console.WriteLine($"  REJECTED: {description} -> {ex.Message}");
        }
    }
}