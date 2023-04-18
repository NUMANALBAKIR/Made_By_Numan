using API.Data;
using API.Repository.IRepository;
using System.Linq.Expressions;

namespace API.Repository;

public abstract class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _db;
    internal DbSet<T> dbSet;

    public Repository(AppDbContext db)
    {
        _db = db;
        dbSet = _db.Set<T>();
        //_db.CartItems.Include(u => u.AppUser).ToList();
    }

    public async Task CreateAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        await SaveAsync();
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            var propertyArr = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var property in propertyArr)
            {
                query = query.Include(property);
            }
        }
        return await query.ToListAsync();
    }


    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;

        query = query.Where(filter);

        if (tracked == false)
        {
            query = query.AsNoTracking();
        }

        if (includeProperties != null)
        {
            var propertyArr = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var property in propertyArr)
            {
                query = query.Include(property);
            }
        }
        return await query.FirstOrDefaultAsync();
    }


    public async Task RemoveAsync(T entity)
    {
        dbSet.Remove(entity);
        await SaveAsync();
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }

}
