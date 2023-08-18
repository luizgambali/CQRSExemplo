using Microsoft.EntityFrameworkCore;
using ToDoList.Infrastructure.Context;

namespace ToDoList.Api.Util
{
    public class DatabaseUtil
    {
        public static void Create()
        {
            using(DbContext context = new DatabaseContext())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}