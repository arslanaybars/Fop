using System.Collections.Generic;
using System.Threading.Tasks;
using Fop;
using Fop.FopExpression;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.Models;
using Sample.Entity;
using Sample.Repository;

namespace Sample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] FopQuery request)
        {
            var fopRequest = FopExpressionBuilder<Student>.Build(request.Filter, request.Order, request.PageNumber, request.PageSize);

            var (filteredStudents, totalCount) = await _studentRepository.RetrieveStudents(fopRequest);

            return Ok(filteredStudents);
        }

        // You can implement paged result strategy depends on your strategy
        // It's simple 
        // you can contribute to improve it
        [HttpGet("PagedResult")]
        public async Task<IActionResult> PagedResult([FromQuery] FopQuery request)
        {
            var fopRequest = FopExpressionBuilder<Student>.Build(request.Filter, request.Order, request.PageNumber, request.PageSize);

            var (filteredStudents, totalCount) = await _studentRepository.RetrieveStudents(fopRequest);

            var response = new PagedResult<List<Student>>(filteredStudents, totalCount, request.PageNumber, request.PageSize);

            return Ok(response);
        }
    }
}