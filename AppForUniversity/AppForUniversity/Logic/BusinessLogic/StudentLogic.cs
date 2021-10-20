using System;
using System.Collections.Generic;
using AppForUniversity.DatabaseImplement.Storages;
using AppForUniversity.Logic.BindingModels;
using AppForUniversity.Logic.ViewModels;

namespace AppForUniversity.Logic.BusinessLogic
{
    public class StudentLogic
    {
        private readonly StudentStorage studentStorage;
        public StudentLogic(StudentStorage studentStorage)
        {
            this.studentStorage = studentStorage;
        }

        public List<StudentViewModel> Read(StudentBindingModel model)
        {
            if (model == null)
            {
                return studentStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<StudentViewModel> { studentStorage.GetElement(model) };
            }
            return studentStorage.GetFilteredList();
        }

        public void Create(StudentBindingModel model)
        {
            studentStorage.Insert(model);
        }

        public void Update(StudentBindingModel model)
        {
            var element = studentStorage.GetElement(new StudentBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            studentStorage.Update(model);
        }

        public void Delete(StudentBindingModel model)

        {
            var element = studentStorage.GetElement(new StudentBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            studentStorage.Delete(model);
        }
    }
}
