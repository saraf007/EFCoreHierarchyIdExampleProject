using Microsoft.EntityFrameworkCore;

namespace EFCoreHierarchyIdExampleProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new OrganizationContext())
            {
                if (!context.Employees.Any())
                {
                    var ceo = new Employee
                    {
                        Name = "CEO",
                        Position = HierarchyId.GetRoot(),
                    };

                    var manager = new Employee
                    {
                        Name = "Manager",
                        Position = ceo.Position.GetDescendant(HierarchyId.Parse("/1/"), null),
                        Manager = ceo
                    };

                    var employee1 = new Employee
                    {
                        Name = "Employee 1",
                        Position = manager.Position.GetDescendant(null, null),
                        Manager = manager
                    };

                    var employee2 = new Employee
                    {
                        Name = "Employee 2",
                        Position = manager.Position.GetDescendant(employee1.Position, null),
                        Manager = manager
                    };

                    context.Employees.AddRange(ceo, manager, employee1, employee2);
                    context.SaveChanges();
                }

                var managerPosition = HierarchyId.Parse("/1/1/");
                var subordinates = context.Employees
                    .Where(e => e.Position.IsDescendantOf(managerPosition))
                    .ToList();

                Console.WriteLine($"Manager's subordinates:");
                foreach (var subordinate in subordinates)
                {
                    Console.WriteLine($"{subordinate.Name} - Position: {subordinate.Position}");
                }
            }
        }
    }
}
