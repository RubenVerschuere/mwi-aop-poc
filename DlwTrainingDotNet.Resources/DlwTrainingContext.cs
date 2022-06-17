using DlwTrainingDotNet.Resources.Entities;
using Microsoft.EntityFrameworkCore;

namespace DlwTrainingDotNet.Resources
{
    public class DlwTrainingContext : DbContext
    {
        public DlwTrainingContext(DbContextOptions<DlwTrainingContext> options) : base(options)
        {
        }

        public DbSet<EmployeeEntity> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeEntity>().ToTable("Employee");
        }
    }
}