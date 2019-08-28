using DK.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DK.Web.Models
{
    public class DataViewModel
    {
        public List<ClientModel> Clients { get; set; }

        public List<TeacherModel> Teachers { get; set; }

        public List<LessonModel> Lessons { get; set; }
    }
}