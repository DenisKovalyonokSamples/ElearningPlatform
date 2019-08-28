using DK.Api.Services;
using DK.BusinessLogic.Enums;
using DK.BusinessLogic.Managers;
using DK.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace DK.Api.Controllers
{
    public class TeacherController : ApiController
    {
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