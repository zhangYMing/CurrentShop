using AutoMapper;
using MyBallShop.Data.Entity;
using MyBallShop.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyballShop.Servers.AutoMappe
{
	public class ToEntityProfile : Profile
	{
		public override string ProfileName
		{
			get
			{
				return "ToDTOProfile";
			}
		}

		public ToEntityProfile()
		{
			CreateMap<DTO_Account,AccountEntity>();
		}
	}
}