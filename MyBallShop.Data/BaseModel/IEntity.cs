using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBallShop.Data.BaseModel
{
	/// <summary>
	/// データモジュールのインタフェース定義
	/// </summary>
	public interface IEntity
	{
		/// <summary>
		/// 作成者
		/// </summary>
		String CREATEUSERID { get; set; }
		/// <summary>
		/// 作成時間
		/// </summary>
		DateTime? CREATETIME { get; set; }
		/// <summary>
		/// 更新者
		/// </summary>
		String MODIFYUSERID { get; set; }
		/// <summary>
		/// 更新時間
		/// </summary>
		DateTime? MODIFYTIME { get; set; }
	}
}