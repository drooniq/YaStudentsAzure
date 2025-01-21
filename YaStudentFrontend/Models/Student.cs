using System.ComponentModel.DataAnnotations;

namespace YaStudentFrontend.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [MaxLength(50)]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        [MaxLength(50)]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email must be between 5 and 100 characters")]
        public required string Email { get; set; }
    }
}
