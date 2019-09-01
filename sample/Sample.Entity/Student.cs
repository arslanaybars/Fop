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

        public string Name { get; set; }

        public string Surname { get; set; }

        public string IdentityNumber { get; set; }

        public int Midterm { get; set; }

        public int Final { get; set; }

        public DateTime Birthday { get; set; }

        public char Level { get; set; }

    }
}
