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
    /// API Controller for Teacher's data access
    /// </summary>  
    public class TeacherController : ApiController
    {
        // GET api/documentation
        /// <summary>
        /// Gets Teacher entity data by it's Id value
        /// </summary>  
        /// <param name="id">Teacher Id value</param>
        /// <returns>The Teacher data model</returns>
        [HttpGet]
        [Route("get_teacher")]
        [ResponseType(typeof(TeacherModel))]
        public IHttpActionResult GetTeacher(string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var models = new List<TeacherModel>();
                var service = new TeacherService();

                return Ok(BindingManager.ToTeacherModel(service.Get(int.Parse(id))));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return InternalServerError(ex);
            }
        }

        // GET api/documentation
        /// <summary>
        /// Gets all Teacher entities data
        /// </summary>  
        /// <returns>The Teacher data collection</returns>
        [HttpGet]
        [Route("get_teachers")]
        [ResponseType(typeof(List<TeacherModel>))]
        public IHttpActionResult GetTeachers()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var models = new List<TeacherModel>();
                var service = new TeacherService();

                return Ok(BindingManager.ToTeacherModels(service.GetAll()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return InternalServerError(ex);
            }
        }

        // POST api/documentation
        /// <summary>
        /// Add new Teacher entity to database
        /// </summary>  
        /// <param name="model">Teacher data model</param>
        /// <returns>Response model data</returns>
        [Route("add_teacher")]
        [HttpPost]
        [ResponseType(typeof(ResponseModel))]
        public IHttpActionResult AddTeacher(TeacherModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var service = new TeacherService();

                int id = service.Add(BindingManager.ToTeacherEntity(model));
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
        /// Update existing Teacher entity with new data
        /// </summary>  
        /// <param name="model">Teacher data model</param>
        /// <returns>Response model data</returns>
        [Route("update_teacher")]
        [HttpPost]
        [ResponseType(typeof(ResponseModel))]
        public IHttpActionResult UpdateTeacher(TeacherModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var service = new TeacherService();

                bool isSuccess = service.Update(BindingManager.ToTeacherEntity(model));
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
        /// Delete existing Teacher entity from database
        /// </summary>  
        /// <param name="id">Teacher Id value</param>
        /// <returns>Response model data</returns>
        [Route("delete_teacher")]
        [HttpDelete]
        [ResponseType(typeof(ResponseModel))]
        public IHttpActionResult DeleteTeacher(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var service = new TeacherService();

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