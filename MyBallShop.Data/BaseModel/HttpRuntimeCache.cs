using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace MyBallShop.Data.BaseModel
{
	/// <summary>
	/// 非同期の場合、Cacheを使用する
	/// </summary>
	public class HttpRuntimeCache
	{
		/// <summary>
		/// CacheのTime
		/// </summary>
		private const double Seconds = 30 * 24 * 60 * 60;

		/// <summary>
		/// Cacheを設置
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool Set(string key, object value)
		{
			return Set(key, value, null, DateTime.Now.AddSeconds(Seconds), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
		}

		/// <summary>
		/// Cacheを設置
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="path"></param>
		/// <returns></returns>
		public static bool Set(string key, object value, string path)
		{
			try
			{
				var cacheDependency = new CacheDependency(path);
				return Set(key, value, cacheDependency);
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Cacheを設置
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="cacheDependency"></param>
		/// <returns></returns>
		public static bool Set(string key, object value, CacheDependency cacheDependency)
		{
			return Set(key, value, cacheDependency, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
		}

		/// <summary>
		/// Cacheを設置
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="seconds"></param>
		/// <param name="isAbsulute"></param>
		/// <returns></returns>
		public static bool Set(string key, object value, double seconds, bool isAbsulute)
		{
			return Set(key, value, null, (isAbsulute ? DateTime.Now.AddSeconds(seconds) : Cache.NoAbsoluteExpiration),
				(isAbsulute ? Cache.NoSlidingExpiration : TimeSpan.FromSeconds(seconds)), CacheItemPriority.Default, null);
		}

		/// <summary>
		/// Cacheを取る
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static T Get<T>(string key)
		{
			if (String.IsNullOrEmpty(key))
				return default(T);
			System.Web.Caching.Cache objCache = HttpRuntime.Cache;
			//return objCache.Get(key);
			return (T)GetPrivate(key);
		}

		/// <summary>
		/// Cacheを取る
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static String Get(string key)
		{
			if (String.IsNullOrEmpty(key))
				return default(String);
			System.Web.Caching.Cache objCache = HttpRuntime.Cache;
			return (String)GetPrivate(key);
		}

		/// <summary>
		/// Cache存在かどうか判断
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool Exists(string key)
		{
			return (GetPrivate(key) != null);
		}

		/// <summary>
		/// Cacheを削除
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool Remove(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				return false;
			}
			HttpRuntime.Cache.Remove(key);
			return true;
		}

		/// <summary>
		/// Cacheを削除
		/// </summary>
		/// <returns></returns>
		public static bool RemoveAll()
		{
			IDictionaryEnumerator iDictionaryEnumerator = HttpRuntime.Cache.GetEnumerator();
			while (iDictionaryEnumerator.MoveNext())
			{
				HttpRuntime.Cache.Remove(Convert.ToString(iDictionaryEnumerator.Key));
			}
			return true;
		}

		/// <summary>
		/// Cacheを設置
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="cacheDependency"></param>
		/// <param name="dateTime"></param>
		/// <param name="timeSpan"></param>
		/// <param name="cacheItemPriority"></param>
		/// <param name="cacheItemRemovedCallback"></param>
		/// <returns></returns>
		public static bool Set(string key, object value, CacheDependency cacheDependency, DateTime dateTime,
			TimeSpan timeSpan, CacheItemPriority cacheItemPriority, CacheItemRemovedCallback cacheItemRemovedCallback)
		{
			if (string.IsNullOrEmpty(key) || value == null)
			{
				return false;
			}
			HttpRuntime.Cache.Insert(key, value, cacheDependency, dateTime, timeSpan, cacheItemPriority, cacheItemRemovedCallback);
			return true;
		}

		/// <summary>
		/// Cacheを取り
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		private static object GetPrivate(string key)
		{
			return string.IsNullOrEmpty(key) ? null : HttpRuntime.Cache.Get(key);
		}
	}
}