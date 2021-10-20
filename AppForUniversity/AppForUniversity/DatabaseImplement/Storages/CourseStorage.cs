using System;
using System.Collections.Generic;
using System.Linq;
using AppForUniversity.Logic.BindingModels;
using AppForUniversity.Logic.ViewModels;
using AppForUniversity.DatabaseImplement.Models;

namespace AppForUniversity.DatabaseImplement.Storages
{
    public class CourseStorage
    {
        public List<CourseViewModel> GetFullList()
        {
            using (var context = new Database())
            {
                return context.Courses
                .Select(CreateModel)
               .ToList();
            }
        }

        public CourseViewModel GetElement(CourseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Database())
            {
                var course = context.Courses
                .FirstOrDefault(rec => rec.Id == model.Id);
                return course != null ?
                CreateModel(course) : null;
            }
        }

        public void Insert(CourseBindingModel model)
        {
            using (var context = new Database())
            {
                context.Courses.Add(CreateModel(model, new Course()));
                context.SaveChanges();
            }
        }

        public void Update(CourseBindingModel model)
        {
            using (var context = new Database())
            {
                Course course = context.Courses
                    .FirstOrDefault(rec => rec.Id == model.Id);
                if (course == null)
                {
                    throw new Exception("Курс не найден");
                }
                CreateModel(model, course);
                context.SaveChanges();
            }
        }

        public void Delete(CourseBindingModel model)
        {
            using (var context = new Database())
            {
                Course course = context.Courses
                    .FirstOrDefault(rec => rec.Id == model.Id);
                if (course != null)
                {
                    context.Courses.Remove(course);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Курс не найден");
                }
            }
        }

        private CourseViewModel CreateModel(Course course)
        {
            return new CourseViewModel
            {
                Id = course.Id,
                Name = course.Name
            };
        }

        private Course CreateModel(CourseBindingModel model, Course course)
        {
            course.Name = model.Name;
            return course;
        }
    }
}
