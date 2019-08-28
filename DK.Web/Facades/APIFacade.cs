using DK.BusinessLogic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace DK.Web.Facades
{
    public class APIFacade
    {
        public List<ClientModel> LoadClients()
        {
            var result = new List<ClientModel>();

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

            return result;
        }

        public List<TeacherModel> LoadTeachers()
        {
            var result = new List<TeacherModel>();

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

            return result;
        }

        public List<LessonModel> LoadLessons()
        {
            var result = new List<LessonModel>();

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

            return result;
        }
    }
}