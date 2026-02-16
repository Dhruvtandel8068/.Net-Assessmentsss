using System.ComponentModel.DataAnnotations;

namespace Assessment7.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Range(1, 100)]
        public int Age { get; set; }

        [Required]
        public string Course { get; set; } = string.Empty;
    }
}
