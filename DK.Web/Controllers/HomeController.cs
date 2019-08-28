using DK.BusinessLogic.Models;
using DK.Web.Facades;
using DK.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DK.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var facade = new APIFacade();
            var model = new DataViewModel();

            model.Clients = facade.LoadClients();
            model.Teachers = facade.LoadTeachers();
            model.Lessons = facade.LoadLessons();

            return View(model);
        }
    }
}