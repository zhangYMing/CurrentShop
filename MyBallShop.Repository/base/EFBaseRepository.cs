using MyBallShop.Data.BaseModel;
using MyBallShop.Data.DBContext;
using MyBallShop.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace MyBallShop.Repository
{
    public class EFBaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
{
	protected BaseDbContext db { get; set; }
	string errorMessage = string.Empty;

	//protected IEFUnitOfWork _unitufwork = null;
	//public IEFUnitOfWork UnitOfWork
	//{
	//    get
	//    {
	//        return _unitufwork;
	//    }
	//}

	//public EFBaseRepository()
	//{
	//    if (this._unitufwork == null)
	//        this._unitufwork = new EFUnitOfWork();
	//}

	//public EFBaseRepository(BaseDbContext context)
	//{
	//    this.db = context;
	//}

	/// <summary>
	/// 指定対象のデータを取得
	/// </summary>
	private IDbSet<TEntity> _entities = null;
	public IDbSet<TEntity> Entities
	{
		get
		{
			if (_entities == null)
			{
				return _entities = db.Set<TEntity>();
			}
			else
			{
				return _entities;
			}
		}
	}

	/// <summary>
	/// キーより該当対象のエンティティを返す
	/// </summary>
	/// <param name="keys"></param>
	/// <returns></returns>
	public TEntity GetById(params string[] keys)
	{
		return this.Entities.Find(keys);
	}

	/// <summary>
	/// データを登録する
	/// </summary>
	/// <param name="entity"></param>
	/// <returns></returns>
	public virtual void Insert(TEntity entity)
	{
		try
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			else
			{
				//this.Entities.Add(entity);
				//this.db.SaveChanges();
				this.db.Entry<TEntity>(entity).State = EntityState.Added;
			}
		}
		catch (DbEntityValidationException dbEx)
		{
			foreach (var validationErrors in dbEx.EntityValidationErrors)
			{
				foreach (var item in validationErrors.ValidationErrors)
				{
					errorMessage += string.Format("Property:{0} Error:{1}", item.PropertyName, item.ErrorMessage) + Environment.NewLine;
				}
			}
			throw new Exception(errorMessage, dbEx);
		}
		//this.db.Entry<TEntity>(entity).State = EntityState.Added;
		//return this.db.SaveChanges();
	}

	/// <summary>
	/// たくさんデータを登録する
	/// </summary>
	/// <param name="entitys"></param>
	/// <returns></returns>
	public virtual void Insert(IEnumerable<TEntity> entitys)
	{
		try
		{
			if (entitys == null || entitys.Count() == 0)
			{
				throw new ArgumentNullException("entity");
			}
			else
			{
				foreach (var entity in entitys)
				{
					this.db.Entry<TEntity>(entity).State = EntityState.Added;
				}
			}
		}
		catch (DbEntityValidationException dbEx)
		{
			foreach (var validationErrors in dbEx.EntityValidationErrors)
			{
				foreach (var item in validationErrors.ValidationErrors)
				{
					errorMessage += string.Format("Property:{0} Error:{1}", item.PropertyName, item.ErrorMessage) + Environment.NewLine;
				}
			}
			throw new Exception(errorMessage, dbEx);
		}

		//return this.db.SaveChanges();
	}

	/// <summary>
	/// データを更新
	/// </summary>
	/// <param name="entity"></param>
	/// <returns></returns>
	public virtual void Update(TEntity entity)
	{
		try
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			else
			{
				this.Entities.Attach(entity);
				PropertyInfo[] props = entity.GetType().GetProperties();
				foreach (PropertyInfo prop in props)
				{
					if (prop.GetValue(entity, null) != null)
					{
						if (prop.GetValue(entity, null).ToString() == "&nbsp;")
							this.db.Entry(entity).Property(prop.Name).CurrentValue = null;
						this.db.Entry(entity).Property(prop.Name).IsModified = true;
					}
				}
			}
		}
		catch (DbEntityValidationException dbEx)
		{
			foreach (var validationErrors in dbEx.EntityValidationErrors)
			{
				foreach (var item in validationErrors.ValidationErrors)
				{
					errorMessage += string.Format("Property:{0} Error:{1}", item.PropertyName, item.ErrorMessage) + Environment.NewLine;
				}
			}
			throw new Exception(errorMessage, dbEx);
		}

		//return this.db.SaveChanges();
	}

	/// <summary>
	/// データを更新
	/// </summary>
	/// <param name="entity"></param>
	/// <returns></returns>
	public virtual void Update(IEnumerable<TEntity> entity)
	{
		try
		{
			if (entity == null || entity.Count() == 0)
			{
				throw new ArgumentNullException("entity");
			}
			else
			{
				foreach (var item in entity)
				{
					this.Entities.Attach(item);
					PropertyInfo[] props = entity.GetType().GetProperties();
					foreach (PropertyInfo prop in props)
					{
						if (prop.GetValue(entity, null) != null)
						{
							if (prop.GetValue(entity, null).ToString() == "&nbsp;")
								this.db.Entry(entity).Property(prop.Name).CurrentValue = null;
							this.db.Entry(entity).Property(prop.Name).IsModified = true;
						}
					}
				}
			}
		}
		catch (DbEntityValidationException dbEx)
		{
			foreach (var validationErrors in dbEx.EntityValidationErrors)
			{
				foreach (var item in validationErrors.ValidationErrors)
				{
					errorMessage += string.Format("Property:{0} Error:{1}", item.PropertyName, item.ErrorMessage) + Environment.NewLine;
				}
			}
			throw new Exception(errorMessage, dbEx);
		}


		//return this.db.SaveChanges();
	}

	/// <summary>
	/// 更新指定の列
	/// </summary>
	/// <param name="entity"></param>
	/// <returns></returns>
	public virtual void UpdateEntityValue(TEntity entity, Dictionary<string, Object> updatefilevalue)
	{
		try
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			else
			{
				int tempInt = 0;
				this.Entities.Attach(entity);
				PropertyInfo[] props = entity.GetType().GetProperties();
				foreach (PropertyInfo prop in props)
				{
					if (updatefilevalue.ContainsKey(prop.Name))
					{
						switch (prop.PropertyType.Name.ToLower())
						{
							case "int32":
								int.TryParse(updatefilevalue[prop.Name].ToString(), out tempInt);
								this.db.Entry(entity).Property(prop.Name).CurrentValue = tempInt;
								break;
							//case "string":
							//    this.db.Entry(entity).Property(prop.Name).CurrentValue = updatefilevalue[prop.Name];
							//    break;
							//case "datetime":
							//    this.db.Entry(entity).Property(prop.Name).CurrentValue = updatefilevalue[prop.Name];
							//    break;
							default:
								this.db.Entry(entity).Property(prop.Name).CurrentValue = updatefilevalue[prop.Name];
								break;
						}
						this.db.Entry(entity).Property(prop.Name).IsModified = true;
					}
				}
			}
		}
		catch (DbEntityValidationException dbEx)
		{
			foreach (var validationErrors in dbEx.EntityValidationErrors)
			{
				foreach (var item in validationErrors.ValidationErrors)
				{
					errorMessage += string.Format("Property:{0} Error:{1}", item.PropertyName, item.ErrorMessage) + Environment.NewLine;
				}
			}
			throw new Exception(errorMessage, dbEx);
		}

		//return this.db.SaveChanges();
	}

	/// <summary>
	/// 更新指定の列
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <param name="entity"></param>
	/// <param name="userInfoDic"></param>
	/// <returns></returns>
	public virtual void setEntityValue(TEntity entity, Dictionary<string, Object> updatefilevalue)
	{
		int tempInt = 0;
		foreach (var pro in entity.GetType().GetProperties())
		{
			if (updatefilevalue.ContainsKey(pro.Name))
			{
				switch (pro.PropertyType.Name.ToLower())
				{
					case "int32":
						int.TryParse(updatefilevalue[pro.Name].ToString(), out tempInt);
						pro.SetValue(entity, tempInt);
						break;
					case "string":
						pro.SetValue(entity, updatefilevalue[pro.Name].ToString());
						break;
					case "datetime":
						pro.SetValue(entity, updatefilevalue[pro.Name]);
						break;
					default:
						pro.SetValue(entity, updatefilevalue[pro.Name].ToString());
						break;
				}
			}
		}
	}

	/// <summary>
	/// データを削除する
	/// </summary>
	/// <param name="entity"></param>
	/// <returns></returns>
	public virtual void Delete(TEntity entity)
	{
		try
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			else
			{
				this.Entities.Attach(entity);
				this.db.Entry<TEntity>(entity).State = EntityState.Deleted;
			}
		}
		catch (DbEntityValidationException dbEx)
		{
			foreach (var validationErrors in dbEx.EntityValidationErrors)
			{
				foreach (var item in validationErrors.ValidationErrors)
				{
					errorMessage += string.Format("Property:{0} Error:{1}", item.PropertyName, item.ErrorMessage) + Environment.NewLine;
				}
			}
			throw new Exception(errorMessage, dbEx);
		}

		//return this.db.SaveChanges();
	}

	/// <summary>
	/// データを削除する
	/// </summary>
	/// <param name="predicate"></param>
	/// <returns></returns>
	public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
	{
		try
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("entity");
			}
			else
			{
				var entitys = this.Entities.Where(predicate).ToList();
				entitys.ForEach(m => this.db.Entry<TEntity>(m).State = EntityState.Deleted);
			}
		}
		catch (DbEntityValidationException dbEx)
		{
			foreach (var validationErrors in dbEx.EntityValidationErrors)
			{
				foreach (var item in validationErrors.ValidationErrors)
				{
					errorMessage += string.Format("Property:{0} Error:{1}", item.PropertyName, item.ErrorMessage) + Environment.NewLine;
				}
			}
			throw new Exception(errorMessage, dbEx);
		}

		//return this.db.SaveChanges();
	}

	public virtual TEntity FindEntity(Expression<Func<TEntity, bool>> predicate)
	{
		try
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("entity");
			}
			else
			{
				return this.Entities.FirstOrDefault(predicate);
			}
		}
		catch (DbEntityValidationException dbEx)
		{
			foreach (var validationErrors in dbEx.EntityValidationErrors)
			{
				foreach (var item in validationErrors.ValidationErrors)
				{
					errorMessage += string.Format("Property:{0} Error:{1}", item.PropertyName, item.ErrorMessage) + Environment.NewLine;
				}
			}
			throw new Exception(errorMessage, dbEx);
		}
	}

	/// <summary>
	/// Set方法でDbSet対象を取得する
	/// </summary>
	/// <returns></returns>
	public virtual IQueryable<TEntity> IQueryable()
	{
		return this.Entities.AsNoTracking();
	}

	public virtual IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate)
	{
		return this.Entities.Where(predicate);
	}

	public virtual List<TEntity> FindList(string strSql)
	{
		return this.db.Database.SqlQuery<TEntity>(strSql).ToList<TEntity>();
	}

	public virtual List<TEntity> FindList(string strSql, DbParameter[] dbParameter)
	{
		return this.db.Database.SqlQuery<TEntity>(strSql, dbParameter).ToList<TEntity>();
	}

	public virtual List<TEntity> FindList(Pagination pagination)
	{
		bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
		string[] _order = pagination.sidx.Split(',');
		MethodCallExpression resultExp = null;
		var tempData = this.Entities.AsQueryable();
		foreach (string item in _order)
		{
			string _orderPart = item;
			_orderPart = Regex.Replace(_orderPart, @"\s+", " ");
			string[] _orderArry = _orderPart.Split(' ');
			string _orderField = _orderArry[0];
			bool sort = isAsc;
			if (_orderArry.Length == 2)
			{
				isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
			}
			var parameter = Expression.Parameter(typeof(TEntity), "t");
			var property = typeof(TEntity).GetProperty(_orderField);
			var propertyAccess = Expression.MakeMemberAccess(parameter, property);
			var orderByExp = Expression.Lambda(propertyAccess, parameter);
			resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
		}
		tempData = tempData.Provider.CreateQuery<TEntity>(resultExp);
		pagination.records = tempData.Count();
		tempData = tempData.Skip<TEntity>(pagination.rows * (pagination.page - 1)).Take<TEntity>(pagination.rows).AsQueryable();
		return tempData.ToList();
	}

	public virtual List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate)
	{
		var tempData = this.Entities.Where(predicate);
		return tempData.ToList();
	}

	public virtual List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, Pagination pagination)
	{
		bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
		string[] _order = pagination.sidx.Split(',');
		MethodCallExpression resultExp = null;
		var tempData = this.Entities.Where(predicate);
		foreach (string item in _order)
		{
			string _orderPart = item;
			_orderPart = Regex.Replace(_orderPart, @"\s+", " ");
			string[] _orderArry = _orderPart.Split(' ');
			string _orderField = _orderArry[0];
			bool sort = isAsc;
			if (_orderArry.Length == 2)
			{
				isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
			}
			var parameter = Expression.Parameter(typeof(TEntity), "t");
			var property = typeof(TEntity).GetProperty(_orderField);
			var propertyAccess = Expression.MakeMemberAccess(parameter, property);
			var orderByExp = Expression.Lambda(propertyAccess, parameter);
			resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
		}
		tempData = tempData.Provider.CreateQuery<TEntity>(resultExp);
		pagination.records = tempData.Count();
		tempData = tempData.Skip<TEntity>(pagination.rows * (pagination.page - 1)).Take<TEntity>(pagination.rows).AsQueryable();
		return tempData.ToList();
	}
}
}