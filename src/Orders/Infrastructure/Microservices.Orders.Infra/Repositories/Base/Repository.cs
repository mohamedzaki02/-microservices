using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microservices.Orders.Core.Entities.Base;
using Microservices.Orders.Core.Repositories.Base;
using Microservices.Orders.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Orders.Infra.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly OrdersContext _ctx;
        public Repository(OrdersContext ordersContext)
        {
            _ctx = ordersContext ?? throw new ArgumentNullException(nameof(ordersContext));

        }
        public async Task<T> AddAsync(T entity)
        {
            _ctx.Set<T>().Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _ctx.Set<T>().Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _ctx.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _ctx.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = _ctx.Set<T>();
            if (disableTracking) query = query.AsNoTracking();
            if (!string.IsNullOrEmpty(includeString)) query.Include(includeString);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _ctx.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }
    }
}