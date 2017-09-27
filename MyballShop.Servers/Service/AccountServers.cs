using MyBallShop.Data.Entity;
using MyBallShop.Data.IRepositories;
using MyBallShop.Model.Model;
using MyBallShop.Repository.Repositories;
using MyBallShop.Repository.UnitOfWork;
using AutoMapper;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace MyballShop.Servers.Service
{
	public class AccountServers
	{
		/// <summary>
		/// 新用户登录
		/// </summary>
		/// <param name=""></param>
		public Task<String> NewAccount(DTO_Account model)
		{
			try
			{
				using (var unitofwork = new EFUnitOfWork())
				{
					AccountRepository _accountRepository = unitofwork.repository<IAccountRepository>();
					_accountRepository.New_Form(AutoMapper.Mapper.Map<AccountEntity>(model));
					if (!unitofwork.IsCommitted)
						unitofwork.Commit();
				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("\nError getting Users. {0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : ""));
			}
			return Task.FromResult<String>("ok");
		}

		/// <summary>
		/// 用户信息更新
		/// </summary>
		/// <param name=""></param>
		public String UpdateAccount(DTO_Account model)
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
				throw new Exception(string.Format("\nError getting Users. {0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : ""));
			}
			return ("ok");
		}

		/// <summary>
		/// 获取全部用户信息更新
		/// </summary>
		/// <param name=""></param>
		public String GetAllAccountInfo(DTO_Account model)
		{
			try
			{
				    AccountRepository _accountRepository = new AccountRepository();
					List<AccountEntity> x = _accountRepository.IQueryable().ToList();
					var t = x.Where(z => z.EMAILID.Equals(model.EMAILID)).ToList();
					if (t.Count() == 0)
					{
						return ("邮箱未注册，请先注册吧！");
					}
					else
					{
						if (x.Where(z => z.EMAILID.Equals(model.EMAILID) && z.PASSWORD.Equals(model.PASSWORD)).ToList().Count() > 0)
						{
							return ("ok");
						}
						else {
							return ("密码不正确，忘记密码了吗？");
						}
					}

				}
			catch (Exception e)
			{
				throw new Exception(string.Format("\nError getting Users. {0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : ""));
			}
		}
	}

	public class Account
	{
		/// <summary>
		/// EMAILID
		/// </summary>
		public String EMAILID { get; set; }
		/// <summary>
		/// PASSWORD
		/// </summary>
		public String PASSWORD { get; set; }
	}
	public class AccountComparer : EqualityComparer<Account>
	{
		public override bool Equals(Account s1, Account s2)
		{
			//这里写你要去除重复的条件。
			return s1.EMAILID == s2.EMAILID && s1.PASSWORD == s2.PASSWORD;
		}
		public override int GetHashCode(Account a)
		{
			return a.EMAILID.GetHashCode();
		}
	}
}