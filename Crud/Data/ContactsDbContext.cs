using Crud.Models;
using Microsoft.EntityFrameworkCore;

namespace Crud.Data
{
    public class ContactsDbContext:DbContext
    {
        public ContactsDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Contact> contacts { get; set; }
    }
}
