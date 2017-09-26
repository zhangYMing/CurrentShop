using MyBallShop.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBallShop.Repository.UnitOfWork
{
	public interface IEFUnitOfWork : IUnitOfWork, IDisposable
	{
		dynamic repository<T>();
	}
}