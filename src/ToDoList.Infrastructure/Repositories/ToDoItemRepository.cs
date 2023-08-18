using ToDoList.Domain.Entities;
using ToDoList.Domain.Interfaces.Repositories;
using System.Linq.Expressions;
using ToDoList.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Infrastructure.Repositories
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        private DatabaseContext _context;

        public ToDoItemRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<ToDoItem> GetByIDAsync(Guid id)
        {
            var item = await _context.ToDoItem.FindAsync(id);
            return item;
        }

        public async Task<IEnumerable<ToDoItem>> GetAllAsync()
        {
            return await _context.ToDoItem.ToListAsync();
        }

        public async Task<IEnumerable<ToDoItem>> GetByCriteria(Expression<Func<ToDoItem, bool>> criteria)
        {
            var items = await _context.ToDoItem.Where(criteria).ToListAsync();
            return items;
        }

        
        public void Add(ToDoItem item)
        {
            _context.ToDoItem.Add(item);
        }

        public void Update(ToDoItem item)
        {
             _context.ToDoItem.Update(item);
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}