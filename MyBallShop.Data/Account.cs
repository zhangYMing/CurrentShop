using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBallShop.Data
{
	/// <summary>
	/// 登録ユーザ情報作成
	/// </summary>
	public class Account
	{
		private string _objectid = "SYSTEM";

		/// <summary>
		/// 登録ID
		/// </summary>
		public string objectId
		{
			get
			{
				return _objectid;
			}
			set
			{
				_objectid = value;
			}
		}

		/// <summary>
		/// 登録者アカウント
		/// </summary>
		public string userPrincipalName { get; set; }
		/// <summary>
		/// 登録者ユーザ名
		/// </summary>
		public string displayName { get; set; }
		/// <summary>
		/// tenantID
		/// </summary>
		public string tenantID { get; set; }
		/// <summary>
		/// Account有効化
		/// </summary>
		public string accountEnabled { get; set; }
		/// <summary>
		/// ユーザの類型
		/// </summary>
		public string userType { get; set; }
		/// <summary>
		/// ユーザ登録したTokenを格納
		/// </summary>
		public string AccessToken { get; set; }
		/// <summary>
		/// 企業コード
		/// </summary>
		public string CompanyId { get; set; }
		/// <summary>
		/// TIMESPAN
		/// </summary>
		public string TimeSpan { get; set; }
		/// <summary>
		/// 登録者区分
		/// </summary>
		public string UserKubun { get; set; }
		/// <summary>
		/// 登録者種別
		/// </summary>
		public string UserSyubetu { get; set; }
		/// <summary>
		/// 担当者ID
		/// </summary>
		public string TanTouSyaId { get; set; }
		///// <summary>
		///// 登録者パスワード
		///// </summary>
		//public string UserPwd { get; set; }
		///// <summary>
		///// 登録者ユーザ権限情報
		///// </summary>
		//public string AuthorityINfo { get; set; }
		///// <summary>
		///// 登録者Token
		///// </summary>
		//public string LoginToken { get; set; }
		///// <summary>
		///// 登録時間
		///// </summary>
		//public DateTime LoginTime { get; set; }
		///// <summary>
		///// ユーザ区分
		///// </summary>
		//public UserType Usertype { get; set; }
		///// <summary>
		///// 管理者区分
		///// </summary>
		//public bool IsSystem { get; set; }
	}

	/// <summary>
	/// 登録ユーザ区分
	/// </summary>
	public enum UserType
	{
		User = 0,
		Customer
	}

	/// <summary>
	/// 製品の権限
	/// </summary>
	public class ItemProductAuthority
	{
		/// <summary>
		/// 製品区分
		/// </summary>
		public String SEIHINKUBUN { get; set; }
		/// <summary>
		/// 型番
		/// </summary>
		public String KATABAN { get; set; }
		/// <summary>
		/// ファイル種別
		/// </summary>
		public String FILESYUBETU { get; set; }
		/// <summary>
		/// ファイル名
		/// </summary>
		public String FILENAME { get; set; }
	}
}