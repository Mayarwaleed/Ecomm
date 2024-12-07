using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ecomm.Models;
using Ecomm.Data;

namespace Ecomm.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext _context { get; set; }
        private DbSet<T> _dbSet;

        // Constructor to inject the ApplicationDbContext
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // Add a new entity
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);  // Adding the entity to DbSet
            await _context.SaveChangesAsync(); // Saving changes to the database
        }

        // Update an existing entity
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity); // Updating the entity in DbSet
            await _context.SaveChangesAsync(); // Saving changes to the database
        }

        // Delete an entity by Id
        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id); // Find the entity by id
            if (entity != null)
            {
                _dbSet.Remove(entity); // Remove the entity from DbSet
                await _context.SaveChangesAsync(); // Saving changes to the database
            }
        }

        // Get all entities of type T
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync(); // Return all entities as a list
        }

        // Get entity by Id
        public async Task<T> GetByIdAsync(int id, QueryOptions<T> options)
        {
            var entity = await _dbSet.FindAsync(id); // Find the entity by id
            return entity;
        }
    }
}
