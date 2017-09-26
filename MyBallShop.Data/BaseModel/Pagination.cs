using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBallShop.Data.BaseModel
{
	/// <summary>
	/// ページング対象
	/// </summary>
	public class Pagination
	{
		/// <summary>
		/// 各ページ数
		/// </summary>
		public int rows { get; set; }
		/// <summary>
		/// 当ページ
		/// </summary>
		public int page { get; set; }
		/// <summary>
		/// 順番列
		/// </summary>
		public string sidx { get; set; }
		/// <summary>
		/// 順番タイプ
		/// </summary>
		public string sord { get; set; }
		/// <summary>
		/// レコード数
		/// </summary>
		public int records { get; set; }
		/// <summary>
		/// 総ページ数
		/// </summary>
		public int total
		{
			get
			{
				if (records > 0)
				{
					return records % this.rows == 0 ? records / this.rows : records / this.rows + 1;
				}
				else
				{
					return 0;
				}
			}
		}
	}
}