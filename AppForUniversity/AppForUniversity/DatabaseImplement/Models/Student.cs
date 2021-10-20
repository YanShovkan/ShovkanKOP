using System.ComponentModel.DataAnnotations;

namespace AppForUniversity.DatabaseImplement.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string FIO { get; set; }
        [Required] 
        public string Сharacteristic { get; set; }
        [Required]
        public string Course { get; set; }
        public int? Scholarship { get; set; }
    }
}
