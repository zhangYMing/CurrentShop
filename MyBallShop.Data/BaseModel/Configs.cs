using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MyBallShop.Data.BaseModel
{
	/// <summary>
	/// WebConfigでAppset関数の操作
	/// </summary>
	public class Configs
	{

		const string appSetPath = "~/Configs/systemapp.config";

		/// <summary>
		/// AppSettings指定キーを取得
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetValue(string key)
		{
			ConfigurationManager.RefreshSection("appSettings");
			return ConfigurationManager.AppSettings[key] ?? string.Empty;
		}

		/// <summary>
		/// AppSettings指定キーを設定
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool SetValue(string key, string value)
		{
			try
			{
				System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();
				xDoc.Load(HttpContext.Current.Server.MapPath(appSetPath));
				System.Xml.XmlNode xNode;
				System.Xml.XmlElement xElem1;
				System.Xml.XmlElement xElem2;
				xNode = xDoc.SelectSingleNode("//appSettings");

				xElem1 = (System.Xml.XmlElement)xNode.SelectSingleNode("//add[@key='" + key + "']");
				if (xElem1 != null)
				{
					xElem1.SetAttribute("value", value);
				}
				else
				{
					xElem2 = xDoc.CreateElement("add");
					xElem2.SetAttribute("key", key);
					xElem2.SetAttribute("value", value);
					xNode.AppendChild(xElem2);
				}
				xDoc.Save(HttpContext.Current.Server.MapPath(appSetPath));
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}