using System.Linq.Expressions;
using BookLibraryApi.DataBaseContext;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi.Repository;

public class GenericRepository<TEntity> where TEntity : class
{
    private Context _dbContext;
    private DbSet<TEntity> _dbSet;
    public GenericRepository(Context DbContext)
    {
        _dbContext = DbContext;
        _dbSet = DbContext.Set<TEntity>();
    }

    public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> where = null)
    {
        IQueryable<TEntity> qurey = _dbSet;

        if (where != null)
        {
            qurey = qurey.Where(where);
        }

        return qurey.ToList();
    }

    public TEntity GetById(object id)
    {
        return _dbSet.Find(id);
    }

    public async void DeleteById(object id)
    {
        var getResult = GetById(id);

        if (getResult != null)
        {
            _dbSet.Remove(getResult);
        }

        await SaveChanges();
    }

    public virtual async void InsertRow(TEntity obj)
    {
        _dbSet.Add(obj);
        _dbContext.SaveChanges();
        await SaveChanges();
    }
    
    public virtual async void UpdateRow(TEntity obj)
    {
        _dbSet.Update(obj);
        await SaveChanges();
    }

    private async Task<bool> SaveChanges()
    {
        _dbContext.SaveChanges();
        return true;
    } 
}
