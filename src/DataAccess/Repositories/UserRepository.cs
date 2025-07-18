using Domain;

namespace DataAccess.Repositories;

public class UserRepository(DatabaseContext databaseContext)
{
    public IEnumerable<User> GetAll()
    {
        return databaseContext.Users.AsEnumerable();
    }
}
