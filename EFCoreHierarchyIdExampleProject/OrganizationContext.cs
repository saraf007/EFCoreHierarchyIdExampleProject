using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreHierarchyIdExampleProject
{
    public class OrganizationContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("YOUR CONNECTION STRING");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }
    }

    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Position)
                .HasColumnType("hierarchyid") // Specify the SQL Server HierarchyId type
                .HasConversion(
                    v => v.ToString(), // Convert HierarchyId to string for storage
                    v => HierarchyId.Parse(v)); // Parse string back to HierarchyId
        }
    }
}
