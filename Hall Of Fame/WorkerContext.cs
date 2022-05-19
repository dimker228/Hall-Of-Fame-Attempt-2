using Hall_Of_Fame.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace Hall_Of_Fame
{
    public class WorkerContext : DbContext
    {

        public DbSet<Person> Persons { get; set; }
        public DbSet<Skills> Skilss { get; set; }

        public WorkerContext(DbContextOptions<WorkerContext> options) : base(options)
        {

        }
    }
}