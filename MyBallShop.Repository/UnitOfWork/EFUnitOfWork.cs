using MyBallShop.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MyBallShop.Repository.UnitOfWork
{
	public class EFUnitOfWork : IEFUnitOfWork, IDisposable
	{

		private readonly BaseDbContext db;
		private bool disposed;
		private Dictionary<string, object> listrepositories;

		public EFUnitOfWork(BaseDbContext context)
		{
			this.db = context;
		}

		public EFUnitOfWork()
		{
			this.db = new BaseDbContext();
		}
		#region IUnitOfWork 

		public bool IsCommitted { get; set; } = false;

		public int Commit()
		{
			if (IsCommitted)
			{
				return 0;
			}
			try
			{
				int result = db.SaveChanges();
				IsCommitted = true;
				return result;
			}
			catch (DbUpdateException e)
			{

				throw e.InnerException.InnerException;
			}
		}

		public void Rollback()
		{
			IsCommitted = false;
		}
		#endregion

		public virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					db.Dispose();
				}
			}
			disposed = true;
		}

		#region IDisposable
		public void Dispose()
		{
			if (!IsCommitted)
			{
				Commit();
			}
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion


		#region Repository
		public dynamic repository<T>()
		{
			if (listrepositories == null)
			{
				listrepositories = new Dictionary<string, object>();
			}

			String repositoryName = typeof(T).Name;
			if (!listrepositories.ContainsKey(repositoryName))
			{
				Assembly asm = Assembly.GetExecutingAssembly();
				var repositoryType = asm.GetTypes()
					  .Where(type => !String.IsNullOrEmpty(type.Namespace))
					  .Where(type => type.IsClass && type.GetInterface(repositoryName) == typeof(T)).FirstOrDefault();
				var repositoryInstance = Activator.CreateInstance(repositoryType, db);
				listrepositories.Add(repositoryName, repositoryInstance);

			}
			return (T)listrepositories[repositoryName];

		}
		#endregion
	}
}