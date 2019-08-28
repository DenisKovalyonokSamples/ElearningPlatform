using DK.BusinessLogic.Models;
using DK.Api.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using DK.BusinessLogic.Managers;
using DK.BusinessLogic.Enums;

namespace DK.Api.Controllers
{
    public class ClientController : ApiController
    {
        [HttpGet]
        [Route("get_client")]
        [ResponseType(typeof(ClientModel))]
        public IHttpActionResult GetClient(string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var models = new List<ClientModel>();
                var service = new ClientService();

                return Ok(BindingManager.ToClientModel(service.Get(int.Parse(id))));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("get_clients")]
        [ResponseType(typeof(List<ClientModel>))]
        public IHttpActionResult GetClients()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var models = new List<ClientModel>();
                var service = new ClientService();

                return Ok(BindingManager.ToClientModels(service.GetAll()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return InternalServerError(ex);
            }
        }

        [Route("add_client")]
        [HttpPost]
        [ResponseType(typeof(ResponseModel))]
        public IHttpActionResult AddClient(ClientModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var service = new ClientService();

                int id = service.Add(BindingManager.ToClientEntity(model));
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

        [Route("update_client")]
        [HttpPost]
        [ResponseType(typeof(ResponseModel))]
        public IHttpActionResult UpdateClient(ClientModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var service = new ClientService();

                bool isSuccess = service.Update(BindingManager.ToClientEntity(model));
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

        [Route("delete_client")]
        [HttpDelete]
        [ResponseType(typeof(ResponseModel))]
        public IHttpActionResult DeleteClient(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var service = new ClientService();

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