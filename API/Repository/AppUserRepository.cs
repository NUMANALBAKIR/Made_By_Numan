using API.Data;
using API.Models.User;
using API.Repository.IRepository;

namespace API.Repository;

public class AppUserRepository : Repository<AppUser>, IAppUserRepository
{
    private readonly AppDbContext _db;
    public AppUserRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

}
