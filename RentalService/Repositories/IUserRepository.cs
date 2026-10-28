using RentalService.Domain;

namespace RentalService.Repositories;

public interface IUserRepository
{
    void Add(User user);
    User? GetById(Guid id);
    IReadOnlyList<User> GetAll();
}