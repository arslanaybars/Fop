using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Entity
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string IdentityNumber { get; set; }

        public int Midterm { get; set; }

        public int Final { get; set; }

        public double Average { get; set; }

        public decimal Bonus { get; set; }

        public DateTime Birthday { get; set; }

        public char Level { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
    }
}
