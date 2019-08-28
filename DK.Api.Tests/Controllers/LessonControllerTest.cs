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
using DK.Dal.Enums;

namespace DK.Api.Tests.Controllers
{
    [TestClass]
    public class LessonControllerTest
    {
        [TestMethod]
        public void GetAll()
        {
            // Arrange
            List<LessonModel> result = null;

            // Act
            HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}get_lessons", ConfigurationManager.AppSettings["server_url"])));
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

            result = JsonConvert.DeserializeObject<List<LessonModel>>(json, settings);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            LessonModel result = null;

            // Get lessons
            HttpWebRequest itemsRequest = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}get_lessons", ConfigurationManager.AppSettings["server_url"])));
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

            var entities = JsonConvert.DeserializeObject<List<LessonModel>>(json, settings);

            // Assert
            Assert.IsNotNull(entities);

            //Update existing item
            if (entities != null && entities.Count > 0)
            {
                LessonModel model = entities.FirstOrDefault();

                if (model != null)
                {
                    // Act
                    HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}get_lesson?id=" + model.Id.ToString(), ConfigurationManager.AppSettings["server_url"])));
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

                    result = JsonConvert.DeserializeObject<LessonModel>(json, settings);

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.AreEqual(model.Id, result.Id);
                }
            }
        }

        [TestMethod]
        public void Add()
        {
            // Get lessons
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
                TeacherModel parent = entities.Where(e => !e.FirstName.Contains("Upd.")).FirstOrDefault();

                if (parent != null)
                {
                    var model = new LessonModel();
                    model.Title = "Some Title";
                    model.Description = "Some lesson description";
                    model.Type = LessonType.Theoretical;
                    model.Teacher = parent;

                    string jsonObj = JsonConvert.SerializeObject(model);
                    byte[] body = Encoding.UTF8.GetBytes(jsonObj);

                    HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}add_lesson", ConfigurationManager.AppSettings["server_url"])));
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
        public void Update()
        {
            string marker = "Upd.";

            // Get lessons
            HttpWebRequest itemsRequest = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}get_lessons", ConfigurationManager.AppSettings["server_url"])));
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

            var entities = JsonConvert.DeserializeObject<List<LessonModel>>(json, settings);

            // Assert
            Assert.IsNotNull(entities);

            //Update existing item
            if (entities != null && entities.Count > 0)
            {
                LessonModel model = entities.Where(e => !e.Title.Contains(marker)).FirstOrDefault();

                if (model != null)
                {
                    model.Type = LessonType.Practical;
                    model.Title = model.Title + " " + marker;
                    model.Description = model.Description + " " + marker;

                    // Act
                    string jsonObj = JsonConvert.SerializeObject(model);
                    byte[] body = Encoding.UTF8.GetBytes(jsonObj);

                    HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}update_lesson", ConfigurationManager.AppSettings["server_url"])));
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
            // Get lessons
            HttpWebRequest itemsRequest = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}get_lessons", ConfigurationManager.AppSettings["server_url"])));
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

            var entities = JsonConvert.DeserializeObject<List<LessonModel>>(json, settings);

            // Assert
            Assert.IsNotNull(entities);

            //Update existing item
            if (entities != null && entities.Count > 0)
            {
                LessonModel model = entities.FirstOrDefault();

                if (model != null)
                {
                    HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(String.Format("{0}delete_lesson?id=" + model.Id, ConfigurationManager.AppSettings["server_url"])));
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
