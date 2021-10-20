using System.ComponentModel;

namespace AppForUniversity.Logic.ViewModels
{
    public class StudentViewModel
    {
        [DisplayName("Курс")]
        public string Course { get; set; }
        [DisplayName("Стипендия")]
        public int? Scholarship { get; set; }
        [DisplayName("Идентификатор")]
        public int Id { get; set; }
        [DisplayName("ФИО")]
        public string FIO { get; set; }
        [DisplayName("Характеристика")]
        public string Сharacteristic { get; set; }
    }
}
