using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalAssistance.Model;

namespace PersonalAssistance.Data
{
    public class ItemsDbContext:DbContext
    {
        public ItemsDbContext(DbContextOptions<ItemsDbContext> options):base(options)
        {
            
        }
        public DbSet<Item> Items { get; set; }
    }
}
