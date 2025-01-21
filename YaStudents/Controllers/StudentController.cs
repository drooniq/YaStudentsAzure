using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YaStudents.Data;
using YaStudents.Models;

namespace YaStudents.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly StudentsDBContext dbContext;
        public StudentController(StudentsDBContext _dbContext)
        {
            dbContext = _dbContext;        
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = dbContext.Students.ToList() ?? [];
            return Ok(students);
        }

        [HttpGet("/")]
        public IActionResult TestConnection()
        {
            try
            {
                // Test if the database can be queried
                var canConnect = dbContext.Database.CanConnect();
                if (canConnect)
                {
                    return Ok("Database connection successful!");
                }
                else
                {
                    return StatusCode(500, "Failed to connect to the database.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
