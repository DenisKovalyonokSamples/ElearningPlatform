using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;
using System;
using System.Configuration;
using System.IO;
using DK.BusinessLogic.Models;
using System.Collections.Generic;
using System.Text;
using DK.BusinessLogic.Enums;
using System.Linq;

namespace DK.Api.Tests.Controllers
{
    [TestClass]
    public class ClientControllerTest
    {
        [TestMethod]
        public void GetAll()
        {
            // Arrange
            List<ClientModel> result = null;

            // Act
            HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}get_clients", ConfigurationManager.AppSettings["server_url"])));
            request.Method = "GET";
            request.ContentType = "application/json";

            var response = request.GetResponse();
            string json;
            using (Stream responseStream = response.GetResponseStream())
            {
                json = new StreamReader(responseStream).ReadToEnd();
            }
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
            };
            response.Close();

            result = JsonConvert.DeserializeObject<List<ClientModel>>(json, settings);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            ClientModel result = null;

            // Get clients
            HttpWebRequest itemsRequest = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}get_clients", ConfigurationManager.AppSettings["server_url"])));
            itemsRequest.Method = "GET";
            itemsRequest.ContentType = "application/json";

            var itemsResponse = itemsRequest.GetResponse();
            string json;
            using (Stream responseStream = itemsResponse.GetResponseStream())
            {
                json = new StreamReader(responseStream).ReadToEnd();
            }
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
            };
            itemsResponse.Close();

            var entities = JsonConvert.DeserializeObject<List<ClientModel>>(json, settings);

            // Assert
            Assert.IsNotNull(entities);

            //Update existing item
            if (entities != null && entities.Count > 0)
            {
                ClientModel model = entities.FirstOrDefault();

                if (model != null)
                {
                    // Act
                    HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}get_client?id=" + model.Id.ToString(), ConfigurationManager.AppSettings["server_url"])));
                    request.Method = "GET";
                    request.ContentType = "application/json";

                    var response = request.GetResponse();
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        json = new StreamReader(responseStream).ReadToEnd();
                    }
                    settings = new JsonSerializerSettings()
                    {
                        TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
                    };
                    response.Close();

                    result = JsonConvert.DeserializeObject<ClientModel>(json, settings);

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.AreEqual(model.Id, result.Id);
                }
            }
        }

        [TestMethod]
        public void Add()
        {
            // Arrange
            var model = new ClientModel();
            model.FirstName = "New";
            model.LastName = "Client";
            model.Birthday = DateTime.UtcNow.AddYears(-20).Date;

            // Act
            string jsonObj = JsonConvert.SerializeObject(model);
            byte[] body = Encoding.UTF8.GetBytes(jsonObj);

            HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}add_client", ConfigurationManager.AppSettings["server_url"])));
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = body.Length;
            
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
                stream.Close();
            }

            var response = request.GetResponse();
            string json;
            using (Stream responseStream = response.GetResponseStream())
            {
                json = new StreamReader(responseStream).ReadToEnd();
            }
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
            };
            response.Close();
            var result = JsonConvert.DeserializeObject<ResponseModel>(json, settings);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseType.Success, result.Result);
        }

        [TestMethod]
        public void Update()
        {
            string marker = "Upd.";

            // Get clients
            HttpWebRequest itemsRequest = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}get_clients", ConfigurationManager.AppSettings["server_url"])));
            itemsRequest.Method = "GET";
            itemsRequest.ContentType = "application/json";

            var itemsResponse = itemsRequest.GetResponse();
            string json;
            using (Stream responseStream = itemsResponse.GetResponseStream())
            {
                json = new StreamReader(responseStream).ReadToEnd();
            }
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
            };
            itemsResponse.Close();

            var entities = JsonConvert.DeserializeObject<List<ClientModel>>(json, settings);

            // Assert
            Assert.IsNotNull(entities);

            //Update existing item
            if (entities != null && entities.Count > 0)
            {
                ClientModel model = entities.Where(e => !e.FirstName.Contains(marker)).FirstOrDefault();

                if (model != null)
                {
                    model.Birthday = model.Birthday.AddYears(2);
                    model.FirstName = model.FirstName + " " + marker;
                    model.LastName = model.LastName + " " + marker;

                    // Act
                    string jsonObj = JsonConvert.SerializeObject(model);
                    byte[] body = Encoding.UTF8.GetBytes(jsonObj);

                    HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}update_client", ConfigurationManager.AppSettings["server_url"])));
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = body.Length;

                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(body, 0, body.Length);
                        stream.Close();
                    }

                    var response = request.GetResponse();
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        json = new StreamReader(responseStream).ReadToEnd();
                    }
                    settings = new JsonSerializerSettings()
                    {
                        TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
                    };
                    response.Close();
                    var result = JsonConvert.DeserializeObject<ResponseModel>(json, settings);

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.AreEqual(ResponseType.Success, result.Result);
                }
            }
        }

        [TestMethod]
        public void Delete()
        {
            // Get clients
            HttpWebRequest itemsRequest = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}get_clients", ConfigurationManager.AppSettings["server_url"])));
            itemsRequest.Method = "GET";
            itemsRequest.ContentType = "application/json";

            var itemsResponse = itemsRequest.GetResponse();
            string json;
            using (Stream responseStream = itemsResponse.GetResponseStream())
            {
                json = new StreamReader(responseStream).ReadToEnd();
            }
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
            };
            itemsResponse.Close();

            var entities = JsonConvert.DeserializeObject<List<ClientModel>>(json, settings);

            // Assert
            Assert.IsNotNull(entities);

            //Update existing item
            if (entities != null && entities.Count > 0)
            {
                ClientModel model = entities.FirstOrDefault();

                if (model != null)
                {
                    HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}delete_client?id=" + model.Id, ConfigurationManager.AppSettings["server_url"])));
                    request.Method = "DELETE";
                    request.ContentType = "application/json";

                    var response = request.GetResponse();
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        json = new StreamReader(responseStream).ReadToEnd();
                    }
                    settings = new JsonSerializerSettings()
                    {
                        TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
                    };
                    response.Close();

                    var result = JsonConvert.DeserializeObject<ResponseModel>(json, settings);

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.AreEqual(ResponseType.Success, result.Result);
                }
            }
        }
    }
}
