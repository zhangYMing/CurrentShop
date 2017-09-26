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

		public void New_Form(AccountEntity AccsessLogEntity)
		{
			var LoginInfo = AccountProvider.Provider.GetCurrent();
			AccsessLogEntity.M_GUID = Guid.NewGuid().ToString();
			if (LoginInfo != null)
			{
				AccsessLogEntity.CREATEUSERID = LoginInfo.objectId;
			}
			AccsessLogEntity.CREATETIME = DateTime.Now;
			Insert(AccsessLogEntity);
		}

		public void Update_Form(AccountEntity AccsessLogEntity)
		{
			var LoginInfo = AccountProvider.Provider.GetCurrent();
			if (LoginInfo != null)
			{
				AccsessLogEntity.MODIFYUSERID = LoginInfo.objectId;
			}
			AccsessLogEntity.MODIFYTIME = DateTime.Now;
			Update(AccsessLogEntity);
		}

		public void Delete_Form(AccountEntity AccsessLogEntity)
		{
			var LoginInfo = AccountProvider.Provider.GetCurrent();
			if (LoginInfo != null)
			{
				AccsessLogEntity.MODIFYUSERID = LoginInfo.objectId;
			}
			AccsessLogEntity.MODIFYTIME = DateTime.Now;
			Delete(AccsessLogEntity);
		}
	}
}