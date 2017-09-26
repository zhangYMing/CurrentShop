using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBallShop.Model.Model
{
	public class DTO_Account
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
		/// PHONENUMBER
		/// </summary>
		public String PHONENUMBER { get; set; }
		/// <summary>
		/// ADDRESS
		/// </summary>
		public String[] ADDRESS { get; set; }
	}
}