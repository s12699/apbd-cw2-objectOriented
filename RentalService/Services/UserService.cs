using RentalService.Domain;
using RentalService.Exceptions;
using RentalService.Repositories;

namespace RentalService.Services;

public class UserService
{
    public readonly IUserRepository _users;

    public UserService(IUserRepository users)
    {
        _users = users;
    }

    public User Add(string firstName, string lastName, UserType userType)
    {
        var user = new User(firstName, lastName, userType);
        _users.Add(user);
        return user;
    }

    public IReadOnlyList<User> GetAll()
    {
        return _users.GetAll();
    }

    public User GetById(Guid id)
    {
        return _users.GetById(id) ?? throw new EntityNotFoundException(id);
    }
}