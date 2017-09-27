using MyballShop.Servers.Service;
using MyBallShop.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MyBallShop.Controllers.CreateAcountMsgController;

namespace MyBallShop.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Account = Request.QueryString["id"];
			return View();
		}
		public ActionResult Login()
		{
			return View();
		}
	}
}