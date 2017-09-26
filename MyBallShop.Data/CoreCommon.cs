using MyBallShop.Data.BaseModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MyBallShop.Data
{
	/// <summary>
	/// サイト共通方法
	/// </summary>
	public class CoreCommon
	{
		#region Stopwatch

		public static Stopwatch ComWatch { get; set; } = new Stopwatch();

		/// <summary>
		/// 時間の計測が開始される
		/// </summary>
		/// <returns></returns>
		public static Stopwatch TimerStart()
		{
			Stopwatch watch = new Stopwatch();
			ComWatch.Reset();
			ComWatch.Start();
			return watch;
		}

		/// <summary>
		/// 時間の計測が終了される
		/// </summary>
		/// <param name="watch"></param>
		/// <returns></returns>
		public static string TimerEnd(Stopwatch watch)
		{
			ComWatch.Stop();
			double costtime = ComWatch.ElapsedMilliseconds;
			return costtime.ToString();
		}
		#endregion

		#region GUID

		/// <summary>
		/// GuIdを取得
		/// </summary>
		/// <returns></returns>
		public static string GuId()
		{
			return Guid.NewGuid().ToString();
		}

		/// <summary>
		/// 番号の新規（非同期の場合、重複の可能性がある）
		/// </summary>
		/// <returns>2016102055123</returns>
		public static string CreateNo()
		{
			string strRandom = new Random().Next(1000, 10000).ToString();
			string code = DateTime.Now.ToString("yyyyMMddHHmmssfff") + strRandom;
			return code;
		}
		#endregion

		#region Licence
		/// <summary>
		/// Licenceの検証
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool IsLicence(string key)
		{
			string host = HttpContext.Current.Request.Url.Host.ToLower();
			if (host.Equals("localhost"))
				return true;
			string licence = ConfigurationManager.AppSettings["LicenceKey"];
			if (licence != null && licence == Md5.Md5Hash(key))
				return true;

			return false;
		}

		/// <summary>
		/// Licence作成
		/// </summary>
		/// <returns></returns>
		public static string GetLicence()
		{
			var licence = Configs.GetValue("LicenceKey");
			if (string.IsNullOrEmpty(licence))
			{
				licence = GuId();
				Configs.SetValue("LicenceKey", licence);
			}
			return Md5.Md5Hash(licence);
		}
		#endregion


		public static string FileSize(long Size)
		{
			string m_strSize = "";
			long FactSize = 0;
			FactSize = Size;
			if (FactSize < 1024.00)
				m_strSize = FactSize.ToString("F2") + " Byte";
			else if (FactSize >= 1024.00 && FactSize < 1048576)
				m_strSize = (FactSize / 1024.00).ToString("F2") + " K";
			else if (FactSize >= 1048576 && FactSize < 1073741824)
				m_strSize = (FactSize / 1024.00 / 1024.00).ToString("F2") + " M";
			else if (FactSize >= 1073741824)
				m_strSize = (FactSize / 1024.00 / 1024.00 / 1024.00).ToString("F2") + " G";
			return m_strSize;
		}
	}
}