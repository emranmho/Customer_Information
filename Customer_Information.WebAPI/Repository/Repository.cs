using Customer_Information.WebAPI.Data;
using Customer_Information.WebAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Customer_Information.WebAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CustomerDBContext _context;
        internal DbSet<T> _dbSet;
        public Repository(CustomerDBContext context)
        {
            _context = context;
           // _context.Customers.Include(u=>u.Country)
            this._dbSet = _context.Set<T>();
        }

        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await Save();
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression = null, bool tracked = true, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }

            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }

            }

            return await _dbSet.ToListAsync();
        }

        public async Task Remove(T entity)
        {
            _dbSet.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
