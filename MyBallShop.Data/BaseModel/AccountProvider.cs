using MyBallShop.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBallShop.Data.BaseModel
{

	public class AccountProvider
	{

		public static AccountProvider Provider
		{
			get { return new AccountProvider(); }
		}

		private string LoginUserKey = "FlexWPSupportSite_loginuserkey";
		private string LoginProvider = Configs.GetValue("LoginProvider");

		/// <summary>
		/// 登録者の情報の取得
		/// </summary>
		/// <returns></returns>
		public Account GetCurrent()
		{
			Account logininfo = new Account();
			string LoginUserInfo = "";
			if (LoginProvider == "Cookie")
			{
				LoginUserInfo = WebHelper.GetCookie(LoginUserKey);
			}
			else if (LoginProvider == "Session")
			{
				LoginUserInfo = WebHelper.GetSession(LoginUserKey);
			}
			else if (LoginProvider == "Cache")
			{
				LoginUserInfo = HttpRuntimeCache.Get(LoginUserKey);
			}
			if (LoginUserInfo != null)
			{
				return DESEncrypt.Decrypt(LoginUserInfo).ToObject<Account>();
			}
			return null;
		}

		/// <summary>
		/// 登録者の情報のを作る
		/// </summary>
		/// <param name="logininfo"></param>
		public void AddCurrent(Account logininfo)
		{
			if (LoginProvider == "Cookie")
			{
				WebHelper.SetCookie(LoginUserKey, DESEncrypt.Encrypt(logininfo.ToJson()), 48 * 60);
			}
			else if (LoginProvider == "Session")
			{
				WebHelper.SetSession(LoginUserKey, DESEncrypt.Encrypt(logininfo.ToJson()));
			}
			else if (LoginProvider == "Cache")
			{
				HttpRuntimeCache.Set(LoginUserKey, DESEncrypt.Encrypt(logininfo.ToJson()));
			}
			WebHelper.SetSession("CurrentLoginUser_licence", CoreCommon.GetLicence());
		}

		/// <summary>
		/// 登録者の情報のを削除
		/// </summary>
		public void RemoveCurrent()
		{
			if (LoginProvider == "Cookie")
			{
				WebHelper.RemoveCookie(LoginUserKey.Trim());
			}
			else if (LoginProvider == "Session")
			{
				WebHelper.RemoveSession(LoginUserKey.Trim());
			}
			else if (LoginProvider == "Cache")
			{
				HttpRuntimeCache.Remove(LoginUserKey.Trim());
			}
		}
	}
}