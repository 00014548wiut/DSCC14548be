using Microsoft.EntityFrameworkCore;

namespace _14548DSCC.Models
{
    public class ModelContext : DbContext
    {
        public ModelContext(DbContextOptions<ModelContext> options) : base(options) { } 
       
        public DbSet<Product> Product { get; set;}
        public DbSet<Electronic> Electronic { get; set;}
    }
}
