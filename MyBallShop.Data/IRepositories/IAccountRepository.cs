using MyBallShop.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBallShop.Data.IRepositories
{
	public interface IAccountRepository : IRepository<AccountEntity>
	{
		void New_Form(AccountEntity ApplyEntity);
		void Update_Form(AccountEntity ApplyEntity);
		void Delete_Form(AccountEntity ApplyEntity);
	}
}