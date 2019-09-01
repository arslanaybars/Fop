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
            var studentList = new List<Student>();
            var identityNumber = 100000;
            var removeMonths = 0;
            for (var i = 0; i < 100; i++)
            {
                studentList.Add(new Student
                {
                    Name = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8),
                    Surname = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8),
                    Midterm = new Random().Next(0, 100),
                    Final = new Random().Next(0, 100),
                    Birthday = DateTime.Now.AddMonths(--removeMonths),
                    IdentityNumber = (++identityNumber).ToString(),
                    Level = Convert.ToChar(Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 1))
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
                Level = 'a'
            });

            return studentList.AsQueryable();
        }
    }
}
