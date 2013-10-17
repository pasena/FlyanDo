using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlyanDo.Service.Abstract;
using FlyanDo.Entity;

namespace FlyanDo.Web.Controllers
{
    public class HomeController : Controller
    {
        private IFlyService flyService;

        public HomeController(IFlyService flyServ)
        {
            flyService = flyServ;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View(flyService.GetAll().ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
