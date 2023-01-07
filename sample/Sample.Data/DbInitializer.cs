using Sample.Entity;

namespace Sample.Data;

public class DbInitializer
{
    const string chars = "ABCDEF";

    public static async Task SeedAsync(AppDbContext context)
    {
        if (!context.Departments.Any())
        {
            var departments = new List<Department>
            {
                new Department
                {
                    Name = "Software Engineering"
                },
                new Department
                {
                    Name = "Architecture"
                },
                new Department
                {
                    Name = "Zoology"
                }
            };

            context.Departments.AddRange(departments);
            await context.SaveChangesAsync();
        }

        if (!context.Students.Any())
        {
            var students = new List<Student>();
            var identityNumber = 100000;
            var removeMonths = 0;
            for (var i = 0; i < 100; i++)
            {
                students.Add(new Student
                {
                    Name = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8),
                    Surname = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8),
                    Midterm = new Random().Next(0, 100),
                    Final= new Random().Next(0, 100),
                    Birthday = DateTime.Now.AddMonths(--removeMonths),
                    IdentityNumber = (++identityNumber).ToString(),
                    Level = chars.ToCharArray()[new Random().Next(0, 6)],
                    DepartmentId = new Random().Next(1, 4)
                });
            }

            context.Students.AddRange(students);
            await context.SaveChangesAsync();
        }
    }
}