using RentalService.Domain;

namespace RentalService.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _items = new();
    
    public void Add(User user)
    {
        _items.Add(user);
    }
    
    public User? GetById(Guid id)
    {
        return _items.FirstOrDefault(x => x.Id == id);
    }
    
    public IReadOnlyList<User> GetAll()
    {
        return _items.ToList();
    }
    
}