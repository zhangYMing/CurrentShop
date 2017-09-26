using MyBallShop.Data.Entity;
using MyBallShop.Data.IRepositories;
using MyBallShop.Model.Model;
using MyBallShop.Repository.Repositories;
using MyBallShop.Repository.UnitOfWork;
using AutoMapper;
using System;

namespace MyballShop.Servers.Service
{
	public class AccountServers
	{
		/// <summary>
		/// 新用户登录
		/// </summary>
		/// <param name=""></param>
		public string NewAccount(DTO_Account model)
		{
			try {
				using (var unitofwork = new EFUnitOfWork())
				{
					AccountRepository _accountRepository = unitofwork.repository<IAccountRepository>();
					_accountRepository.New_Form(AutoMapper.Mapper.Map<AccountEntity>(model));
					if (!unitofwork.IsCommitted)
						unitofwork.Commit();
				}
			} catch (Exception e) {
				return "err";
			}
			return "ok";
		}

		/// <summary>
		/// 用户信息更新
		/// </summary>
		/// <param name=""></param>
		public string UpdateAccount(DTO_Account model)
		{
			try
			{
				using (var unitofwork = new EFUnitOfWork())
				{
					AccountRepository _accountRepository = unitofwork.repository<IAccountRepository>();
					_accountRepository.Update_Form(AutoMapper.Mapper.Map<AccountEntity>(model));
					if (!unitofwork.IsCommitted)
						unitofwork.Commit();
				}
			}
			catch (Exception e)
			{
				return "err";
			}
			return "ok";
		}
	}
}