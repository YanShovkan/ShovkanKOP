using System;
using System.Collections.Generic;
using System.Linq;
using AppForUniversity.Logic.BindingModels;
using AppForUniversity.Logic.ViewModels;
using AppForUniversity.DatabaseImplement.Models;

namespace AppForUniversity.DatabaseImplement.Storages
{
    public class StudentStorage
    {
        public List<StudentViewModel> GetFullList()
        {
            using (var context = new Database())
            {
                return context.Students
                .Select(CreateModel)
               .ToList();
            }
        }
        public List<StudentViewModel> GetFilteredList()
        {
            using (var context = new Database())
            {
                return context.Students
                .Where(rec => rec.Scholarship.HasValue)
               .Select(CreateModel)
                .ToList();
            }
        }
        public StudentViewModel GetElement(StudentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Database())
            {
                var student = context.Students
                .FirstOrDefault(rec => rec.Id == model.Id);
                return student != null ?
                CreateModel(student) : null;
            }
        }

        public void Insert(StudentBindingModel model)
        {
            using (var context = new Database())
            {
                context.Students.Add(CreateModel(model, new Student()));
                context.SaveChanges();
            }
        }

        public void Update(StudentBindingModel model)
        {
            using (var context = new Database())
            {
                var student = context.Students
                    .FirstOrDefault(rec => rec.Id == model.Id);
                if (student == null)
                {
                    throw new Exception("Студент не найден");
                }
                CreateModel(model, student);
                context.SaveChanges();
            }
        }

        public void Delete(StudentBindingModel model)
        {
            using (var context = new Database())
            {
                Student student = context.Students
                    .FirstOrDefault(rec => rec.Id == model.Id);
                if (student != null)
                {
                    context.Students.Remove(student);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Студент не найден");
                }
            }
        }

        private StudentViewModel CreateModel(Student student)
        {
            return new StudentViewModel
            {
                Id = student.Id,
                FIO = student.FIO,
                Course = student.Course,
                Scholarship = student.Scholarship,
                Сharacteristic = student.Сharacteristic
            };
        }

        private Student CreateModel(StudentBindingModel model, Student student)
        {
            student.Course = model.Course;
            student.FIO = model.FIO;
            student.Scholarship = model.Scholarship.GetValueOrDefault();
            student.Сharacteristic = model.Сharacteristic;
            return student;
        }
    }
}
