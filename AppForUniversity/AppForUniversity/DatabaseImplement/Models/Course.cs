using System.ComponentModel.DataAnnotations;

namespace AppForUniversity.DatabaseImplement.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
