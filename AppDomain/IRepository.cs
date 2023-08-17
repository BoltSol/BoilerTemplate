using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AppDomain.Common;

namespace AppDomain
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        int Count();
        int Count(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync();
        long LongCount(Expression<Func<TEntity, bool>> predicate);
        long LongCount();
        Task<long> LongCountAsync();
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);
        void Delete(TEntity entity);
        void Delete(TPrimaryKey id);
        void Delete(Expression<Func<TEntity, bool>> predicate);
        Task DeleteAsync(TPrimaryKey id);
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        Task DeleteAsync(TEntity entity);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(TPrimaryKey id);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);
        TEntity Get(TPrimaryKey id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> GetAllList();
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAllListAsync();
        Task<TEntity> GetAsync(TPrimaryKey id);
        List<TEntity> InsertList(List<TEntity> entities);
        List<TPrimaryKey> InsertListAndGetIds(List<TEntity> entities);
        Task<List<TEntity>> InsertListAsync(List<TEntity> entities);
        Task<List<TPrimaryKey>> InsertListAndGetIdsAsync(List<TEntity> entities);
        TEntity Insert(TEntity entity);
        TPrimaryKey InsertAndGetId(TEntity entity);
        Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);
        TEntity InsertOrUpdate(TEntity entity);
        TPrimaryKey InsertOrUpdateAndGetId(TEntity entity);
        Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity);
        Task<TEntity> InsertOrUpdateAsync(TEntity entity);
        TEntity Load(TPrimaryKey id);
        T Query<T>(Func<IQueryable<TEntity>, T> queryMethod);
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity Update(TEntity entity);
        TEntity Update(TPrimaryKey id, Action<TEntity> updateAction);
        Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> ExitAsync(Expression<Func<TEntity, bool>> predicate);
        bool Exit(Expression<Func<TEntity, bool>> predicate);
    }
    public class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public int Count()
        {
            return _dbSet.Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Count(predicate);
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.CountAsync(predicate);
        }

        public long LongCount()
        {
            return _dbSet.LongCount();
        }

        public long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.LongCount(predicate);
        }

        public async Task<long> LongCountAsync()
        {
            return await _dbSet.LongCountAsync();
        }

        public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.LongCountAsync(predicate);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(TPrimaryKey id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entitiesToDelete = _dbSet.Where(predicate);
            _dbSet.RemoveRange(entitiesToDelete);
        }

        public async Task DeleteAsync(TPrimaryKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entitiesToDelete = await _dbSet.Where(predicate).ToListAsync();
            _dbSet.RemoveRange(entitiesToDelete);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => _dbSet.Remove(entity));
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public TEntity FirstOrDefault(TPrimaryKey id)
        {
            return _dbSet.Find(id);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public TEntity Get(TPrimaryKey id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = GetAll();
            return propertySelectors.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public List<TEntity> GetAllList()
        {
            return _dbSet.ToList();
        }

        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public async Task<List<TEntity>> GetAllListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return  await _dbSet.FindAsync(id);
        }

        public List<TEntity> InsertList(List<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            return entities;
        }

        public List<TPrimaryKey> InsertListAndGetIds(List<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            return entities.Select(e => e.Id).ToList();
        }

        public async Task<List<TEntity>> InsertListAsync(List<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return entities;
        }

        public async Task<List<TPrimaryKey>> InsertListAndGetIdsAsync(List<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return entities.Select(e => e.Id).ToList();
        }

        public TEntity Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public TPrimaryKey InsertAndGetId(TEntity entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return entity.Id;
        }

        public async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public TEntity InsertOrUpdate(TEntity entity)
        {
            if (EqualityComparer<TPrimaryKey>.Default.Equals(entity.Id, default(TPrimaryKey)))
            {
                _dbSet.Add(entity);
            }
            else
            {
                _dbSet.Update(entity);
            }

            return entity;
        }

        public TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
        {
            if (EqualityComparer<TPrimaryKey>.Default.Equals(entity.Id, default(TPrimaryKey)))
            {
                _dbSet.Add(entity);
            }
            else
            {
                _dbSet.Update(entity);
            }

            _dbContext.SaveChanges();
            return entity.Id;
        }

        public async Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            if (EqualityComparer<TPrimaryKey>.Default.Equals(entity.Id, default(TPrimaryKey)))
            {
                await _dbSet.AddAsync(entity);
            }
            else
            {
                _dbSet.Update(entity);
            }

            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            if (EqualityComparer<TPrimaryKey>.Default.Equals(entity.Id, default(TPrimaryKey)))
            {
                await _dbSet.AddAsync(entity);
            }
            else
            {
                _dbSet.Update(entity);
            }

            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public TEntity Load(TPrimaryKey id)
        {
            return _dbSet.Find(id);
        }

        public T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetAll());
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Single(predicate);
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.SingleAsync(predicate);
        }

        public TEntity Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                updateAction(entity);
                _dbContext.SaveChanges();
            }

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                await updateAction(entity);
                await _dbContext.SaveChangesAsync();
            }

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> ExitAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public bool Exit(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }
    }
}
