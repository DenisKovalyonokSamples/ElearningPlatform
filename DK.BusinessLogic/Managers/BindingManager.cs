using DK.BusinessLogic.Models;
using DK.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DK.BusinessLogic.Managers
{
    public static class BindingManager
    {
        #region Client

        public static ClientModel ToClientModel(Client entity)
        {
            var model = new ClientModel();

            model.Birthday = entity.Birthday;
            model.CreatedOn = entity.CreatedOn;
            model.Id = entity.Id;
            model.FirstName = entity.FirstName;
            model.LastName = entity.LastName;

            model.Teachers = ToTeacherModels(entity.Teachers);

            return model;
        }

        public static List<ClientModel> ToClientModels(IList<Client> entities)
        {
            var models = new List<ClientModel>();

            if (entities != null && entities.Count > 0)
            {
                foreach(var entity in entities)
                {
                    models.Add(ToClientModel(entity));
                }
            }

            return models;
        }

        public static Client ToClientEntity(ClientModel model)
        {
            var entity = new Client();

            entity.Birthday = model.Birthday;
            entity.CreatedOn = model.CreatedOn;
            entity.Id = model.Id;
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;

            entity.Teachers = ToTeacherEntities(model.Teachers);

            return entity;
        }

        public static List<Client> ToClientEntities(IList<ClientModel> models)
        {
            var entities = new List<Client>();

            if (models != null && models.Count > 0)
            {
                foreach (var model in models)
                {
                    entities.Add(ToClientEntity(model));
                }
            }

            return entities;
        }

        #endregion

        #region Teacher

        public static TeacherModel ToTeacherModel(Teacher entity)
        {
            var model = new TeacherModel();

            model.Birthday = entity.Birthday;
            model.CreatedOn = entity.CreatedOn;
            model.Id = entity.Id;
            model.LastName = entity.LastName;
            model.FirstName = entity.FirstName;
            model.Hired = entity.Hired;

            return model;
        }

        public static List<TeacherModel> ToTeacherModels(IList<Teacher> entities)
        {
            var models = new List<TeacherModel>();

            if (entities != null && entities.Count > 0)
            {
                foreach (var entity in entities)
                {
                    models.Add(ToTeacherModel(entity));
                }
            }

            return models;
        }

        public static Teacher ToTeacherEntity(TeacherModel model)
        {
            var entity = new Teacher();

            entity.Birthday = model.Birthday;
            entity.CreatedOn = model.CreatedOn;
            entity.Id = model.Id;
            entity.LastName = model.LastName;
            entity.FirstName = model.FirstName;
            entity.Hired = model.Hired;

            return entity;
        }

        public static List<Teacher> ToTeacherEntities(IList<TeacherModel> models)
        {
            var entities = new List<Teacher>();

            if (models != null && models.Count > 0)
            {
                foreach (var model in models)
                {
                    entities.Add(ToTeacherEntity(model));
                }
            }

            return entities;
        }

        #endregion

        #region Lesson

        public static LessonModel ToLessonModel(Lesson entity)
        {
            var model = new LessonModel();

            model.Description = entity.Description;
            model.CreatedOn = entity.CreatedOn;
            model.Id = entity.Id;
            model.Title = entity.Title;
            model.Type = entity.Type;

            model.Teacher = ToTeacherModel(entity.Teacher);

            return model;
        }

        public static List<LessonModel> ToLessonModels(IList<Lesson> entities)
        {
            var models = new List<LessonModel>();

            if (entities != null && entities.Count > 0)
            {
                foreach (var entity in entities)
                {
                    models.Add(ToLessonModel(entity));
                }
            }

            return models;
        }

        public static Lesson ToLessonEntity(LessonModel model)
        {
            var entity = new Lesson();

            entity.Description = model.Description;
            entity.CreatedOn = model.CreatedOn;
            entity.Id = model.Id;
            entity.Title = model.Title;
            entity.Type = model.Type;

            entity.Teacher = ToTeacherEntity(model.Teacher);

            return entity;
        }

        public static List<Lesson> ToLessonEntities(IList<LessonModel> models)
        {
            var entities = new List<Lesson>();

            if (models != null && models.Count > 0)
            {
                foreach (var model in models)
                {
                    entities.Add(ToLessonEntity(model));
                }
            }

            return entities;
        }

        #endregion
    }
}
