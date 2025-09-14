using Microsoft.AspNetCore.Mvc;
using Models;
using MyReposiotry;
namespace MyControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentRepository _studentRepository;
        public StudentController(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        [HttpGet("All")]
        public List<StudentModel> GetAllStudents()
        {
            return _studentRepository.GetAllStudents();
        }

        [HttpGet("{id:int}")]
        public StudentModel GetStudentByIndex(int id)
        {
            return _studentRepository.GetStudentByIndex(id);
        }

        [HttpDelete("{id:int}")]
        public bool RemoveStudentById(int id)
        {
            return _studentRepository.RemoveStudentById(id);
        }

        [HttpGet("Sorted")]
        public List<StudentModel> SortStudents()
        {
            return _studentRepository.SortStudents();
        }
    }
}