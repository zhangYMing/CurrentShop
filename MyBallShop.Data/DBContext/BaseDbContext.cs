using MyBallShop.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MyBallShop.Data.DBContext
{
	/// <summary>
	/// 共通DBオブジェクト
	/// </summary>
	public class BaseDbContext : DbContext
	{
		public BaseDbContext()
			: base("MyBallShopDbContext")
		{
			this.Configuration.AutoDetectChangesEnabled = false;
			this.Configuration.ValidateOnSaveEnabled = false;

			#region LazyLoadの設定、TRUEの場合、LazyLoadを行う、且つvirtualに追加する

			this.Configuration.LazyLoadingEnabled = false;
			this.Configuration.ProxyCreationEnabled = false;

			#endregion

			//変更する場合、データ状態の表示、
			//例えば新規(Added State),修正(Modified State),
			//削除(Deleted State),Unchanged State
			this.Configuration.AutoDetectChangesEnabled = true;

		}

		/// <summary>
		/// DB=>Mapping設定
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//default設定
			//modelBuilder.Entity<User>().HasKey(t => t.Id);

			// override EntityTypeConfiguration<EntityType>

			//全部テーブルのschemaを設定する
			//modelBuilder.HasDefaultSchema("newdbo");

			modelBuilder.Configurations.Add(new AccountMap());

			//string assembleFileName = Assembly.GetExecutingAssembly().CodeBase.Replace("MyBallShop.Data.DLL", "MyBallShop.Mapping.DLL").Replace("file:///", "");
			//Assembly asm = Assembly.LoadFile(assembleFileName);
			//var typesToRegister = asm.GetTypes()
			//	.Where(type => !String.IsNullOrEmpty(type.Namespace))
			//	.Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
			//foreach (var type in typesToRegister)
			//{
			//	dynamic configurationInstance = Activator.CreateInstance(type);
			//	modelBuilder.Configurations.Add(configurationInstance);
			//}
		}
	}
}