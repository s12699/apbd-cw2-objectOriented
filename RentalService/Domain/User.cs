namespace RentalService.Domain;

public class User
{
    public Guid Id { get; } = Guid.NewGuid();
    public string FirstName { get; }
    public string LastName { get; }
    public UserType Type { get; }

    public string FullName => $"{FirstName} {LastName}";

    public User(string firstName, string lastName, UserType type)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty", nameof(lastName));
        FirstName = firstName;
        LastName = lastName;
        Type = type;
    }
}