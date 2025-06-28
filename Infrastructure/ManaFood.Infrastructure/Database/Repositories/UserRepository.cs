using ManaFood.Domain.Entities;
using ManaFood.Domain.Interfaces;
using ManaFood.Infrastructure.Database.Context;

namespace ManaFood.Infrastructure.Database.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationContext applicationContext) : base(applicationContext) { }
}
