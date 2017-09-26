using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace MyBallShop.Data.BaseModel
{
	/// <summary>
	/// ページを処理する共通クラス
	/// </summary>
	public class WebHelper
	{
		#region 絶対的なパス
		/// <summary>
		/// 相対的なパスを絶対的なパスに変更する
		/// </summary>
		/// <param name="relativeUrl"></param>
		/// <returns></returns>
		public static string ResolveUrl(string relativeUrl)
		{
			if (string.IsNullOrWhiteSpace(relativeUrl))
				return string.Empty;
			relativeUrl = relativeUrl.Replace("\\", "/");
			if (relativeUrl.StartsWith("/"))
				return relativeUrl;
			if (relativeUrl.Contains("://"))
				return relativeUrl;
			return VirtualPathUtility.ToAbsolute(relativeUrl);
		}

		#endregion

		#region HtmlString文字化

		/// <summary>
		/// HtmlString文字列のエンコード
		/// </summary>
		/// <param name="html"></param>
		/// <returns></returns>
		public static string HtmlEncode(string HtmlString)
		{
			return HttpUtility.HtmlEncode(HtmlString);
		}

		/// <summary>
		/// HtmlString文字列のデコード
		/// </summary>
		/// <param name="html"></param>
		/// <returns></returns>
		public static string HtmlDecode(string HtmlString)
		{
			return HttpUtility.HtmlDecode(HtmlString);
		}

		#endregion

		#region URLのエンコード・デコード

		/// <summary>
		/// URLのエンコード
		/// </summary>
		/// <param name="url"></param>
		/// <param name="isUpper"></param>
		/// <returns></returns>
		public static string UrlEncode(string url, bool isUpper = false)
		{
			var result = HttpUtility.UrlEncode(url, Encoding.UTF8);
			if (!isUpper)
				return result;
			return GetUpperEncode(result);
		}

		/// <summary>
		/// URLのエンコードは大文字に変更する
		/// </summary>
		/// <param name="encode"></param>
		/// <returns></returns>
		private static string GetUpperEncode(string encode)
		{
			var result = new StringBuilder();
			int index = int.MinValue;
			for (int i = 0; i < encode.Length; i++)
			{
				string character = encode[i].ToString();
				if (character == "%")
					index = i;
				if (i - index == 1 || i - index == 2)
					character = character.ToUpper();
				result.Append(character);
			}
			return result.ToString();
		}

		/// <summary>
		/// URLのデコード
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static string UrlDecode(string url)
		{
			return HttpUtility.UrlDecode(url);
		}

		/// <summary>
		/// URLのデコード
		/// </summary>
		/// <param name="url"></param>
		/// <param name="encoding"></param>
		/// <returns></returns>
		public static string UrlDecode(string url, Encoding encoding)
		{
			return HttpUtility.UrlDecode(url, encoding);
		}

		#endregion

		#region　cacheの操作
		/// <summary>
		/// cacheの追加
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public static void SetCache(string key, dynamic value)
		{
			if (key.IsEmpty())
				return;
			System.Web.Caching.Cache objCache = HttpRuntime.Cache;
			objCache.Insert(key, value);
		}

		/// <summary>
		/// cacheの取得
		/// </summary>
		/// <param name="key"></param>
		public static T GetCache<T>(string key)
		{
			if (key.IsEmpty())
				return default(T);
			System.Web.Caching.Cache objCache = HttpRuntime.Cache;
			return (T)objCache.Get(key);
		}

		/// <summary>
		/// cacheの更新
		/// </summary>
		/// <param name="key"></param>
		public static void UpdateCache(string key, dynamic value)
		{
			if (key.IsEmpty())
				return;
			System.Web.Caching.Cache objCache = HttpRuntime.Cache;
			RemoveCache(key);
			SetCache(key, value);
		}

		/// <summary>
		/// cacheの削除
		/// </summary>
		/// <param name="key"></param>
		public static void RemoveCache(string key)
		{
			if (key.IsEmpty())
				return;
			System.Web.Caching.Cache _cache = HttpRuntime.Cache;
			_cache.Remove(key);
		}

		/// <summary>
		/// 全部Cacheを削除
		/// </summary>
		public static void RemoveAllCache()
		{
			System.Web.Caching.Cache _cache = HttpRuntime.Cache;
			IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
			while (CacheEnum.MoveNext())
			{
				_cache.Remove(CacheEnum.Key.ToString());
			}
		}
		#endregion

		#region　Sessionの操作
		/// <summary>
		/// Sessionの追加
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public static void SetSession<T>(string key, T value)
		{
			if (key.IsEmpty())
				return;
			HttpContext.Current.Session[key] = value;

		}

		/// <summary>
		/// Sessionの追加
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public static void SetSession(string key, string value)
		{
			SetSession<string>(key, value);
		}

		/// <summary>
		/// Sessionの取得
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetSession(string key)
		{
			if (key.IsEmpty())
				return string.Empty;
			return HttpContext.Current.Session[key] as string;
		}

		/// <summary>
		/// Sessionの取得
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static T GetSession<T>(string key)
		{
			if (key.IsEmpty())
				return default(T);
			return (T)HttpContext.Current.Session[key];
		}

		/// <summary>
		/// Sessionの削除
		/// </summary>
		/// <param name="key"></param>
		public static void RemoveSession(string key)
		{
			if (key.IsEmpty())
				return;
			HttpContext.Current.Session.Contents.Remove(key);
		}
		#endregion

		#region Cookieの操作
		/// <summary>
		/// Cookieの追加
		/// </summary>
		/// <param name="strName"></param>
		/// <param name="strValue"></param>
		public static void SetCookie(string strName, string strValue)
		{
			HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
			if (cookie == null)
			{
				cookie = new HttpCookie(strName);
			}
			cookie.Value = strValue;
			HttpContext.Current.Response.AppendCookie(cookie);
		}

		/// <summary>
		/// Cookieの追加(TimeOut設定できる)
		/// </summary>
		/// <param name="strName"></param>
		/// <param name="strValue"></param>
		/// <param name="expires">TimeOut(Minute)</param>
		public static void SetCookie(string strName, string strValue, int expires)
		{
			HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
			if (cookie == null)
			{
				cookie = new HttpCookie(strName);
			}
			cookie.Value = strValue;
			cookie.Expires = DateTime.Now.AddMinutes(expires);
			HttpContext.Current.Response.Cookies.Add(cookie);
		}

		/// <summary>
		/// Cookieの取得
		/// </summary>
		/// <param name="strName"></param>
		/// <returns></returns>
		public static string GetCookie(string strName)
		{
			if (HttpContext.Current != null &&
				HttpContext.Current.Request.Cookies != null &&
				HttpContext.Current.Request.Cookies[strName] != null)
			{
				return Convert.ToString(HttpContext.Current.Request.Cookies[strName].Value);
			}
			return null;
		}

		/// <summary>
		/// Cookieの削除
		/// </summary>
		/// <param name="CookiesName"></param>
		public static void RemoveCookie(string CookiesName)
		{
			HttpCookie objCookie = new HttpCookie(CookiesName.Trim());
			objCookie.Expires = DateTime.Now.AddYears(-5);
			HttpContext.Current.Response.Cookies.Add(objCookie);
		}

		#endregion

		#region HttpRequestの操作

		/// <summary>
		/// URLによりHttpRequestを取得
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static string HttpWebRequest(string url)
		{
			return HttpWebRequest(url, string.Empty, Encoding.Default);
		}

		/// <summary>
		/// URLによりHttpRequestを取得
		/// </summary>
		/// <param name="url"></param>
		/// <param name="parameters">parm1=a&(&amp;)parm2=b</param>
		/// <returns></returns>
		public static string HttpWebRequest(string url, string parameters)
		{
			return HttpWebRequest(url, parameters, Encoding.GetEncoding("utf-8"), true);
		}

		/// <summary>
		/// URLによりHttpRequestを取得
		/// </summary>
		/// <param name="url"></param>
		/// <param name="parameters">parm1=a&(&amp;)parm2=b</param>
		/// <param name="encoding"></param>
		/// <param name="isPost"></param>
		/// <param name="contentType">HTML内容タイプ</param>
		/// <param name="cookie">CookieContainer</param>
		/// <param name="timeout"></param>
		/// <returns></returns>
		public static string HttpWebRequest(string url, string parameters, Encoding encoding, bool isPost = false,
			 string contentType = "application/x-www-form-urlencoded", CookieContainer cookie = null, int timeout = 120000)
		{
			var request = (HttpWebRequest)WebRequest.Create(url);
			request.Timeout = timeout;
			request.CookieContainer = cookie;
			if (isPost)
			{
				byte[] postData = encoding.GetBytes(parameters);
				request.Method = "POST";
				request.ContentType = contentType;
				request.ContentLength = postData.Length;
				using (Stream stream = request.GetRequestStream())
				{
					stream.Write(postData, 0, postData.Length);
				}
			}
			var response = (HttpWebResponse)request.GetResponse();
			string result;
			using (Stream stream = response.GetResponseStream())
			{
				if (stream == null)
					return string.Empty;
				using (var reader = new StreamReader(stream, encoding))
				{
					result = reader.ReadToEnd();
				}
			}
			return result;
		}
		#endregion

		#region HTMLタグを削除

		/// <summary>
		/// HTMLタグを削除
		/// </summary>
		/// <param name="Htmlstring"></param>
		/// <returns></returns>
		public static string NoHtml(string Htmlstring)
		{
			Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&hellip;", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&mdash;", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&ldquo;", "", RegexOptions.IgnoreCase);
			Htmlstring.Replace("<", "");
			Htmlstring = Regex.Replace(Htmlstring, @"&rdquo;", "", RegexOptions.IgnoreCase);
			Htmlstring.Replace(">", "");
			Htmlstring.Replace("\r\n", "");
			Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
			return Htmlstring;
		}

		#endregion
	}

	public static class uni {
		/// <summary>
		/// 文字列が空白かどうか判断
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool IsEmpty(this String value)
		{
			return string.IsNullOrWhiteSpace(value);
		}

		/// <summary>
		/// 文字列が空白かどうか判断
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool IsEmpty(this object value)
		{
			if (value != null && !string.IsNullOrEmpty(value.ToString()))
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}