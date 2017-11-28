using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyballShop.Servers.AutoMappe
{
	public class AutoMapperProfileRegister
	{
		public static void Register()
		{

			// マッピング定義の初期化
			Mapper.Initialize(config =>
			{
				// 複数のプロファイルがあれば、AddProfileで追加していく。
				config.AddProfile<ToDTOProfile>();

				config.AddProfile<ToEntityProfile>();
				// ここでは、マッピング名称ルールのカスタマイズ設定なども実施できる。
			});

			// マッピング設定を検証する。
			// マッピングするデータの過不足があれば、ここで例外が発生する。
			// ただし、キャストの失敗などは検知できないので注意すること。
			//Mapper.AssertConfigurationIsValid();
		}
	}
}