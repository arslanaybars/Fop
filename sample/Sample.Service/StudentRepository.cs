using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fop;
using Microsoft.EntityFrameworkCore;
using Sample.Data;
using Sample.Entity;

namespace Sample.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(List<Student>, int)> RetrieveStudents(IFopRequest request)
        {
            var (filteredStudents, totalCount) = _context.Students.Include(x => x.Department).ApplyFop(request);
            return (await filteredStudents.ToListAsync(), totalCount);
        }
    }
}
