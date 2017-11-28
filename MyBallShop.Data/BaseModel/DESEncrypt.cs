using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MyBallShop.Data.BaseModel
{
	/// <summary>
	/// DESC暗号化、復号
	/// </summary>
	public class DESEncrypt
	{
		private static string DESKey
		{
			get
			{
				return "MyBallShop_desencrypt_xxx@nm";
			}
		}

		/// <summary>
		/// 暗号化
		/// </summary>
		/// <param name="Text"></param>
		/// <returns></returns>
		public static string Encrypt(string Text)
		{
			return Encrypt(Text, DESKey);
		}

		/// <summary>
		/// データ暗号化
		/// </summary>
		/// <param name="Text"></param>
		/// <param name="sKey"></param>
		/// <returns></returns>
		private static string Encrypt(string Text, string sKey)
		{
			DESCryptoServiceProvider des = new DESCryptoServiceProvider();
			byte[] inputByteArray;
			inputByteArray = Encoding.UTF8.GetBytes(Text);
			des.Key = ASCIIEncoding.ASCII.GetBytes(Md5.Md5Hash(sKey).Substring(0, 8));
			des.IV = ASCIIEncoding.ASCII.GetBytes(Md5.Md5Hash(sKey).Substring(0, 8));
			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
			cs.Write(inputByteArray, 0, inputByteArray.Length);
			cs.FlushFinalBlock();
			StringBuilder ret = new StringBuilder();
			foreach (byte b in ms.ToArray())
			{
				ret.AppendFormat("{0:X2}", b);
			}
			return ret.ToString();
		}

		/// <summary>
		/// DESC復号
		/// </summary>
		/// <param name="Text"></param>
		/// <returns></returns>
		public static string Decrypt(string Text)
		{
			if (!string.IsNullOrEmpty(Text))
			{
				return Decrypt(Text, DESKey);
			}
			else
			{
				return "";
			}
		}

		/// <summary>
		/// データ暗号化
		/// </summary>
		/// <param name="Text"></param>
		/// <param name="sKey"></param>
		/// <returns></returns>
		private static string Decrypt(string Text, string sKey)
		{
			DESCryptoServiceProvider des = new DESCryptoServiceProvider();
			int len = 0;
			len = Text.Length / 2;
			byte[] inputByteArray = new byte[len];
			int x, i;
			for (x = 0; x < len; x++)
			{
				i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
				inputByteArray[x] = (byte)i;
			}
			des.Key = ASCIIEncoding.ASCII.GetBytes(Md5.Md5Hash(sKey).Substring(0, 8));
			des.IV = ASCIIEncoding.ASCII.GetBytes(Md5.Md5Hash(sKey).Substring(0, 8));
			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
			cs.Write(inputByteArray, 0, inputByteArray.Length);
			cs.FlushFinalBlock();
			return Encoding.UTF8.GetString(ms.ToArray());
		}
	}
	/// <summary>
	/// MD5暗号化
	/// </summary>
	public class Md5
	{
		public static string Md5Hash(string input)
		{
			MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
			byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
			StringBuilder sBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}
			return sBuilder.ToString();
		}
	}
}