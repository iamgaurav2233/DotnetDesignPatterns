using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class StudentModel
    {
        [Required]
        public string Name { get; set; }
        public int Id { get; set; }
    }
}