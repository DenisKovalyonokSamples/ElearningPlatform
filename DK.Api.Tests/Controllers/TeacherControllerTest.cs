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
    public class TeacherControllerTest
    {
        [TestMethod]
        public void GetAll()
        {
            // Arrange
            List<TeacherModel> result = null;

            // Act
            HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}get_teachers", ConfigurationManager.AppSettings["server_url"])));
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

            result = JsonConvert.DeserializeObject<List<TeacherModel>>(json, settings);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            TeacherModel result = null;

            // Get teachers
            HttpWebRequest itemsRequest = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}get_teachers", ConfigurationManager.AppSettings["server_url"])));
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

            var entities = JsonConvert.DeserializeObject<List<TeacherModel>>(json, settings);

            // Assert
            Assert.IsNotNull(entities);

            //Update existing item
            if (entities != null && entities.Count > 0)
            {
                TeacherModel model = entities.FirstOrDefault();

                if (model != null)
                {
                    // Act
                    HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}get_teacher?id=" + model.Id.ToString(), ConfigurationManager.AppSettings["server_url"])));
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

                    result = JsonConvert.DeserializeObject<TeacherModel>(json, settings);

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
            var model = new TeacherModel();
            model.FirstName = "New";
            model.LastName = "Teacher";
            model.Birthday = DateTime.UtcNow.AddYears(-20).Date;
            model.Hired = DateTime.UtcNow.AddMonths(-8).Date;

            // Act
            string jsonObj = JsonConvert.SerializeObject(model);
            byte[] body = Encoding.UTF8.GetBytes(jsonObj);

            HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}add_teacher", ConfigurationManager.AppSettings["server_url"])));
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

            // Get teachers
            HttpWebRequest itemsRequest = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}get_teachers", ConfigurationManager.AppSettings["server_url"])));
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

            var entities = JsonConvert.DeserializeObject<List<TeacherModel>>(json, settings);

            // Assert
            Assert.IsNotNull(entities);

            //Update existing item
            if (entities != null && entities.Count > 0)
            {
                TeacherModel model = entities.Where(e => !e.FirstName.Contains(marker)).FirstOrDefault();

                if (model != null)
                {
                    model.Birthday = model.Birthday.AddYears(2);
                    model.FirstName = model.FirstName + " " + marker;
                    model.LastName = model.LastName + " " + marker;

                    // Act
                    string jsonObj = JsonConvert.SerializeObject(model);
                    byte[] body = Encoding.UTF8.GetBytes(jsonObj);

                    HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}update_teacher", ConfigurationManager.AppSettings["server_url"])));
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
            // Get teachers
            HttpWebRequest itemsRequest = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}get_teachers", ConfigurationManager.AppSettings["server_url"])));
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

            var entities = JsonConvert.DeserializeObject<List<TeacherModel>>(json, settings);

            // Assert
            Assert.IsNotNull(entities);

            //Update existing item
            if (entities != null && entities.Count > 0)
            {
                TeacherModel model = entities.FirstOrDefault();

                if (model != null)
                {
                    HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}delete_teacher?id=" + model.Id, ConfigurationManager.AppSettings["server_url"])));
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
