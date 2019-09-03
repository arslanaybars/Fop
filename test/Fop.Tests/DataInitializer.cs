using System;
using System.Collections.Generic;
using System.Linq;
using Sample.Entity;

namespace Fop.Tests
{
    public class DataInitializer
    {
        public static IQueryable<Student> GenerateStudentList()
        {
            var departments = new List<Department>
            {
                new Department
                {
                    Id = 1,
                    Name = "Software Engineering"
                },
                new Department
                {
                    Id = 2,
                    Name = "Architecture"
                },
                new Department
                {
                    Id = 3,
                    Name = "Zoology"
                }
            };

            var studentList = new List<Student>();
            var identityNumber = 100000;
            var removeMonths = 0;
            for (var i = 0; i < 100; i++)
            {
                int departmentId = new Random().Next(1, 3);
                studentList.Add(new Student
                {
                    Name = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8),
                    Surname = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8),
                    Midterm = new Random().Next(0, 100),
                    Final = new Random().Next(0, 100),
                    Birthday = DateTime.Now.AddMonths(--removeMonths),
                    IdentityNumber = (++identityNumber).ToString(),
                    Level = Convert.ToChar(Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 1)),
                    Department = departments.FirstOrDefault(x => x.Id == departmentId)
                });
            }

            studentList.Add(new Student
            {
                Name = "Aybars",
                Surname = "Arslan",
                Midterm = 100,
                Final = 90,
                Birthday = new DateTime(1995, 04, 06),
                IdentityNumber = (++identityNumber).ToString(),
                Level = 'a',
                DepartmentId = 1
            });

            return studentList.AsQueryable();
        }
    }
}
