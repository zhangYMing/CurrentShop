using AutoMapper;
using MyBallShop.Data.Entity;
using MyBallShop.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyballShop.Servers.AutoMappe
{
	public class ToDTOProfile : Profile
	{
		public override string ProfileName
		{
			get
			{
				return "ToDTOProfile";
			}
		}

		public ToDTOProfile()
		{
			CreateMap<AccountEntity, DTO_Account>();
		}
	}
}