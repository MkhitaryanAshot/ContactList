using ContactList.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ContactList.DAL.DataAccess
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
