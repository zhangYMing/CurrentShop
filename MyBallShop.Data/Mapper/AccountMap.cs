using MyBallShop.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyBallShop.Mapping
{
	class AccountMap : EntityTypeConfiguration<AccountEntity>
	{
		public AccountMap()
		{
			this.ToTable("MYBALLSHOP_ACCOUNT");

			//DBの主キー設定
			this.HasKey(t => new { t.GUID , t.EMAILID});
			//列設定
			this.Property(p => p.M_GUID).IsRequired().HasMaxLength(64).HasColumnType("VARCHAR");
			this.Property(p => p.EMAILID).IsRequired().HasMaxLength(64).HasColumnType("VARCHAR");
			this.Property(p => p.PASSWORD).IsRequired().HasColumnType("VARCHAR");
			this.Property(p => p.PHONENUMBER).HasColumnType("VARCHAR");
			this.Property(p => p.ADDRESS).HasColumnType("NVARCHAR");
			this.Property(p => p.NAME).HasColumnType("NVARCHAR");

			this.Property(p => p.CREATEUSERID).HasMaxLength(64).HasColumnType("VARCHAR");
			this.Property(p => p.CREATETIME).HasColumnType("datetime");
			this.Property(p => p.MODIFYUSERID).HasMaxLength(64).HasColumnType("VARCHAR");
			this.Property(p => p.MODIFYTIME).HasColumnType("datetime");
		}
	}
}