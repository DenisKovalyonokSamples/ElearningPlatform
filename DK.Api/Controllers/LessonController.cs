using DK.Api.Services;
using DK.BusinessLogic.Enums;
using DK.BusinessLogic.Managers;
using DK.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace DK.Api.Controllers
{
    /// <summary>
    /// API Controller for Lesson's data access
    /// </summary>  
    public class LessonController : ApiController
    {
        // GET api/documentation
        /// <summary>
        /// Gets Lesson entity data by it's Id value
        /// </summary>  
        /// <param name="id">Lesson Id value</param>
        /// <returns>The Lesson data model</returns>
        [HttpGet]
        [Route("get_lesson")]
        [ResponseType(typeof(LessonModel))]
        public IHttpActionResult GetLesson(string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var models = new List<LessonModel>();
                var service = new LessonService();

                return Ok(BindingManager.ToLessonModel(service.Get(int.Parse(id))));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return InternalServerError(ex);
            }
        }

        // GET api/documentation
        /// <summary>
        /// Gets all Lesson entities data
        /// </summary>  
        /// <returns>The Lesson data collection</returns>
        [HttpGet]
        [Route("get_lessons")]
        [ResponseType(typeof(List<LessonModel>))]
        public IHttpActionResult GetLessons()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var models = new List<LessonModel>();
                var service = new LessonService();

                return Ok(BindingManager.ToLessonModels(service.GetAll()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return InternalServerError(ex);
            }
        }

        // POST api/documentation
        /// <summary>
        /// Add new Lesson entity to database
        /// </summary>  
        /// <param name="model">Lesson data model</param>
        /// <returns>Response model data</returns>
        [Route("add_lesson")]
        [HttpPost]
        [ResponseType(typeof(ResponseModel))]
        public IHttpActionResult AddLesson(LessonModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var service = new LessonService();

                int id = service.Add(BindingManager.ToLessonEntity(model));
                if (id == 0)
                {
                    return Ok(new ResponseModel() { Result = ResponseType.Error, Description = "Entity was not created." });
                }

                return Ok(new ResponseModel() { Result = ResponseType.Success });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return InternalServerError(ex);
            }
        }

        // POST api/documentation
        /// <summary>
        /// Update existing Lesson entity with new data
        /// </summary>  
        /// <param name="model">Lesson data model</param>
        /// <returns>Response model data</returns>
        [Route("update_lesson")]
        [HttpPost]
        [ResponseType(typeof(ResponseModel))]
        public IHttpActionResult UpdateLesson(LessonModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var service = new LessonService();

                bool isSuccess = service.Update(BindingManager.ToLessonEntity(model));
                if (!isSuccess)
                {
                    return Ok(new ResponseModel() { Result = ResponseType.Error, Description = "Entity was not created." });
                }

                return Ok(new ResponseModel() { Result = ResponseType.Success });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return InternalServerError(ex);
            }
        }

        // DELETE api/documentation
        /// <summary>
        /// Delete existing Lesson entity from database
        /// </summary>  
        /// <param name="id">Lesson Id value</param>
        /// <returns>Response model data</returns>
        [Route("delete_lesson")]
        [HttpDelete]
        [ResponseType(typeof(ResponseModel))]
        public IHttpActionResult DeleteLesson(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var service = new LessonService();

                bool isSuccess = service.Delete(id);
                if (!isSuccess)
                {
                    return Ok(new ResponseModel() { Result = ResponseType.Error, Description = "Entity was not deleted." });
                }

                return Ok(new ResponseModel() { Result = ResponseType.Success });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return InternalServerError(ex);
            }
        }
    }
}
