using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class UserRepository(DatabaseContext databaseContext)
{
    public IEnumerable<User> GetAll()
    {
        return databaseContext.Users
            .Include(user => user.Statement)
            .ThenInclude(statement => statement.Vacations)
            .AsEnumerable();
    }
}
