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
    /// <summary>
    /// API Controller for Client's data access
    /// </summary>  
    public class ClientController : ApiController
    {
        // GET api/documentation
        /// <summary>
        /// Gets Client entity data by it's Id value
        /// </summary>  
        /// <param name="id">Client Id value</param>
        /// <returns>The Client data model</returns>
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

        // GET api/documentation
        /// <summary>
        /// Gets all Client entities data
        /// </summary>  
        /// <returns>The Client data collection</returns>
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

        // POST api/documentation
        /// <summary>
        /// Add new Client entity to database
        /// </summary>  
        /// <param name="model">Client data model</param>
        /// <returns>Response model data</returns>
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

        // POST api/documentation
        /// <summary>
        /// Update existing Client entity with new data
        /// </summary>  
        /// <param name="model">Client data model</param>
        /// <returns>Response model data</returns>
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

        // DELETE api/documentation
        /// <summary>
        /// Delete existing Client entity from database
        /// </summary>  
        /// <param name="id">Client Id value</param>
        /// <returns>Response model data</returns>
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