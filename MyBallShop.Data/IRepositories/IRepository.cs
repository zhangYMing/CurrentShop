using MyBallShop.Data.BaseModel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MyBallShop.Data.IRepositories
{
	public interface IRepository<TEntity> where TEntity : class, new()
	{
		IDbSet<TEntity> Entities { get; }
		TEntity GetById(params string[] keys);
		void Insert(TEntity entity);
		void Insert(IEnumerable<TEntity> entitys);
		void Update(TEntity entity);
		void Delete(TEntity entity);
		void Delete(Expression<Func<TEntity, bool>> predicate);
		TEntity FindEntity(Expression<Func<TEntity, bool>> predicate);
		IQueryable<TEntity> IQueryable();
		IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate);
		List<TEntity> FindList(string strSql);
		List<TEntity> FindList(string strSql, DbParameter[] dbParameter);
		List<TEntity> FindList(Pagination pagination);
		List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate);
		List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, Pagination pagination);
	}
}