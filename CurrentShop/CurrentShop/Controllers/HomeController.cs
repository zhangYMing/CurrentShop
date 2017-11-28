using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CurrentShop.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public ActionResult Service()
		{
			ViewBag.Message = "Your contact service.";

			return View();
		}
		public ActionResult Serviced()
		{
			ViewBag.Message = "Your contact servicedetail.";

			return View();
		}
	}
}