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
		public async Task<ActionResult> CreateAccount(DTO_Account model)
		{
			if (model.GUID == null)
			{
				model.GUID = Guid.NewGuid().ToString();
			}
		    var flg = await new AccountServers().NewAccount(model);
			if (flg == "ok")
			{
				return Json(model, JsonRequestBehavior.AllowGet);
			}
			return Content("error");
		}

		[HttpPost]
		public ActionResult UpdateAccount(DTO_Account model)
		{
			if (model.GUID == null)
			{
				model.GUID = Guid.NewGuid().ToString();
			}
			var flg =  new AccountServers().UpdateAccount(model);
			if (flg == "ok")
			{
				return Json(model, JsonRequestBehavior.AllowGet);
			}
			return Json(new DTO_Account(), JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult LoginCheck(DTO_Account model)
		{
			AccountServers a = new AccountServers();
			var flg = a.GetAllAccountInfo(model);

			if (flg == "ok") {
				var msg = new returnMsg()
				{
					EMAILID = model.EMAILID,
					NAME = model.NAME,
					GUID = model.GUID,
					STATUSMSG = "登录成功",
				};

				return Json(msg, JsonRequestBehavior.AllowGet);
			}
			return Json(new returnMsg { STATUSMSG= flg }, JsonRequestBehavior.AllowGet);
		}

		public class returnMsg {
			/// <summary>
			/// GUID
			/// </summary>
			public String GUID { get; set; }
			/// <summary>
			/// EMAILID
			/// </summary>
			public String EMAILID { get; set; }
			/// <summary>
			/// PASSWORD
			/// </summary>
			public String PASSWORD { get; set; }
			/// <summary>
			/// PHONENUMBER
			/// </summary>
			public String PHONENUMBER { get; set; }
			/// <summary>
			/// NAME
			/// </summary>
			public String NAME { get; set; }
			/// <summary>
			/// ADDRESS
			/// </summary>
			public String ADDRESS { get; set; }
			/// <summary>
			/// STATUSMSG
			/// </summary>
			public String STATUSMSG { get; set; }
		}
	}
}