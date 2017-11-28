using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBallShop.Data.IRepositories
{
	public interface IUnitOfWork
	{
		bool IsCommitted { get; set; }

		int Commit();

		void Rollback();
	}
}