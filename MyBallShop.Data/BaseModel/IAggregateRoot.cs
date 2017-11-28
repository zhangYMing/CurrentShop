using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBallShop.Data.BaseModel
{
	public interface IAggregateRoot : IEntity
	{
		/// <summary>
		/// 主キー
		/// </summary>
		String M_GUID { get; set; }
	}
}