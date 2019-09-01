using System.Collections.Generic;
using System.Threading.Tasks;
using Fop;
using Sample.Entity;

namespace Sample.Repository
{
    public interface IStudentRepository
    {
        Task<(List<Student>, int)> RetrieveStudents(IFopRequest request);
    }
}
