using Microsoft.EntityFrameworkCore;

namespace EFCoreHierarchyIdExampleProject
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public HierarchyId? Position { get; set; } // HierarchyId property
        public int? ManagerId { get; set; }
        public Employee? Manager { get; set; }
        public ICollection<Employee>? Subordinates { get; set; }
    }
}
