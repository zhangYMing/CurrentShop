﻿using MyBallShop.Data.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBallShop.Data.Entity
{
	/// <summary>
	/// 登録アカウント
	/// </summary>
	public class AccountEntity : AggregateRoot
	{
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
		/// NAME
		/// </summary>
		public String NAME { get; set; }
		/// <summary>
		/// PHONENUMBER
		/// </summary>
		public String PHONENUMBER { get; set; }
		/// <summary>
		/// ADDRESS
		/// </summary>
		public String ADDRESS { get; set; }
	}
}