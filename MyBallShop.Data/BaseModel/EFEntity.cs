using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBallShop.Data.BaseModel
{
	public class EFEntity
	{
		/// <summary>
		/// 作成者
		/// </summary>
		public String CREATEUSERID { get; set; }
		/// <summary>
		/// 作成時間
		/// </summary>
		public DateTime? CREATETIME { get; set; }
		/// <summary>
		/// 更新者
		/// </summary>
		public String MODIFYUSERID { get; set; }
		/// <summary>
		/// 更新時間
		/// </summary>
		public DateTime? MODIFYTIME { get; set; }
	}
}