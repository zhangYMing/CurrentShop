using Microsoft.AspNet.Identity.Owin;
using MyballShop.Servers.Service;
using MyBallShop.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyBallShop.Controllers
{
    public class CreateAcountMsgController : Controller
    {
        // GET: CreateAcountMsg
        public ActionResult CreateAcount()
        {
            return View();
        }

		[HttpPost]
		public string  CreateAccount(DTO_Account model)
		{
				if (model.GUID == null)
				{
					model.GUID = Guid.NewGuid().ToString();
				}
				AccountServers _accountServers = new AccountServers();
				var flg = _accountServers.NewAccount(model);
			if (flg == "ok") {
				return $"{model.GUID}&{model.EMAILID}";
			}
			return "error";
		}

		[HttpPost]
		public string UpdateAccount(DTO_Account model)
		{
			if (model.GUID == null)
			{
				model.GUID = Guid.NewGuid().ToString();
			}
			AccountServers _accountServers = new AccountServers();
			var flg = _accountServers.UpdateAccount(model);
			if (flg == "ok")
			{
				return "ok";
			}
			return "error";
		}
	}
}