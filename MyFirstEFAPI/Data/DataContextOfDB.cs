using Microsoft.EntityFrameworkCore;

namespace MyFirstEFAPI.Data
{
    //the supercontroller will inject this context here and then we are able to access the supperhero from the database
    //but to be able to this first i need a constractor here 
    public class DataContextOfDB : DbContext 
    {
        public DataContextOfDB(DbContextOptions<DataContextOfDB> options) : base(options) { }

        public DbSet<SuperAPI> SuperHeroes { get; set; }

    }
}
