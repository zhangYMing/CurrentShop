using MyBallShop.Data.BaseModel;
using MyBallShop.Data.DBContext;
using MyBallShop.Data.Entity;
using MyBallShop.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBallShop.Repository.Repositories
{
	public class AccountRepository : EFBaseRepository<AccountEntity>, IAccountRepository
	{
		public AccountRepository()
		{
			base.db = new BaseDbContext();
		}
		public AccountRepository(BaseDbContext context)
		{
			base.db = context;
		}

		public void New_Form(AccountEntity AccountEntity)
		{
			AccountEntity.M_GUID = Guid.NewGuid().ToString();
			AccountEntity.CREATETIME = DateTime.Now;
			Insert(AccountEntity);
		}

		public void Update_Form(AccountEntity AccountEntity)
		{
			AccountEntity.MODIFYTIME = DateTime.Now;
			Update(AccountEntity);
		}

		public void Delete_Form(AccountEntity AccountEntity)
		{
			AccountEntity.MODIFYTIME = DateTime.Now;
			Delete(AccountEntity);
		}
	}
}