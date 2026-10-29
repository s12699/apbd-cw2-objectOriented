# Equipment Rental (APBD Tutorial 2)

A simple console app in C# for renting university equipment. You can add equipment and users, rent and return things, mark equipment as unavailable and see a short report. Late returns get a penalty.

Author: Hubert Bajon

## How to run

You need the .NET SDK. Then in the project folder (the one with the `.csproj` file):

```bash
dotnet run
```

The program runs a demo scenario and prints everything to the console. There is no menu.

## What it can do

- add a user (student or employee)
- add equipment (laptop, projector or camera)
- show all equipment with its status, or only the available ones
- rent equipment to a user
- return equipment (with a penalty if it is late)
- mark equipment as unavailable
- show active rentals of one user
- show overdue rentals
- show a short report

Rules I used:

- a student can have max 2 active rentals, an employee max 5
- unavailable equipment cannot be rented
- the number of days is given when renting, the due date is rental date + days
- a late return costs 10 for every started day of delay

## Demo scenario

`Program.cs` runs `DemoScenario`, which does this:

1. adds 4 pieces of equipment (2 laptops, a projector, a camera)
2. adds 3 users (2 students, 1 employee)
3. makes a correct rental
4. tries two wrong things: renting unavailable equipment and going over the student limit (both are rejected with a message)
5. returns something on time
6. returns something 4 days late, so the penalty is 4 x 10 = 40
7. prints active rentals, overdue rentals and the final report

The dates in the demo are fixed and passed in as parameters, so I can show a late return without waiting and the result is always the same.

## Project structure

```
Domain/         Equipment, EquipmentStatus, User, UserType, Rental
Asset/          Laptop, Projector, Camera
Policies/       penalty calculator and rental limit policy (interface + implementation)
Exceptions/     my own exceptions
Repositories/   interfaces + in-memory storage
Services/       EquipmentService, UserService, RentalManager, ReportService
UI/             DemoScenario (only prints to the console)
Program.cs      creates the objects and connects them
```

I split it into these folders so every part has its own job: the domain says what things are, the services contain the logic, the repositories store data and the UI only prints.

## Design decisions

**Responsibilities / cohesion**

- Domain classes only keep data and protect their own state. For example `Equipment` does not allow renting something that is not available, and `Rental` cannot be closed twice.
- Repositories only store and find objects. They do not decide anything.
- All rental rules (availability, limit, penalty) are in `RentalManager`, so I do not have to look for them in many places.
- `ReportService` returns the numbers as a `ServiceReport` object and does not print anything. Printing is done by the UI.
- `Program.cs` has no logic. It only creates the objects and runs the demo.

**Coupling**

- Services use interfaces (for example `IEquipmentRepository`, `IPenaltyCalculator`), not concrete classes. The concrete classes are chosen only in `Program.cs`.
- Because of this I could later save data to a JSON file by writing a new repository, without changing the services.
- `Rental` does not calculate its own penalty, it gets the amount from outside. So changing the penalty rule does not change the domain class.
- I pass dates as parameters instead of using `DateTime.Now` inside the classes.

**Rules that can change are in one place**

- Limits (2 and 5) are in `RentalLimitPolicy`.
- The penalty is in `DailyPenaltyCalculator` and the daily fee is a constructor parameter. A different rule would be a new class with the same interface.

**Inheritance**

- `Equipment` is abstract because laptop, projector and camera share some data but have their own fields. Each one describes itself with `GetDetails()`.
- I did not make `Student` and `Employee` classes. They differ only by the rental limit, so one `User` class with a `UserType` is enough. The limit is in the policy.

**Errors**

I made three exceptions: `EquipmentUnavailableException`, `RentalLimitExceededException` and `EntityNotFoundException`. Each one inherits directly from `Exception`. I did not make a common base class because there are only three of them. The downside is that the UI has to list all three in its `catch`.

## What is missing

- data is only in memory, nothing is saved after the program ends
- no menu, only the demo scenario
- equipment has no price, so the penalty is a flat fee per day
- `RentalManager` uses `EquipmentService` and `UserService` directly, not through interfaces, so it is tied to these two classes and I cannot swap them for another implementation without changing it.
- no way to make equipment available again after it was marked unavailable
- no tests

## Git

I worked on separate branches and merged them into `main`:

- `feature/domain`
- `feature/policies`
- `feature/services`
- `feature/ui`
