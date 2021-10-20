using System;
using System.Collections.Generic;
using AppForUniversity.DatabaseImplement.Storages;
using AppForUniversity.Logic.BindingModels;
using AppForUniversity.Logic.ViewModels;

namespace AppForUniversity.Logic.BusinessLogic
{
    public class CourseLogic
    {
        private readonly CourseStorage courseStorage;
        public CourseLogic(CourseStorage courseStorage)
        {
            this.courseStorage = courseStorage;
        }

        public List<CourseViewModel> Read(CourseBindingModel model)
        {
            if (model == null)
            {
                return courseStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<CourseViewModel> { courseStorage.GetElement(model) };
            }
            return null;
        }

        public void Create(CourseBindingModel model)
        {
            courseStorage.Insert(model);
        }

        public void Update(CourseBindingModel model)
        {
            var element = courseStorage.GetElement(new CourseBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            courseStorage.Update(model);
        }

        public void Delete(CourseBindingModel model)
        {
            var element = courseStorage.GetElement(new CourseBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            courseStorage.Delete(model);
        }
    }
}
