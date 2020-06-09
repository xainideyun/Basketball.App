using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JdCat.Basketball.Model.Domain
{
    /// <summary>
    /// 角色表
    /// </summary>
    [Table("Role")]
    public class Role: BaseEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 角色标识
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 角色创建人id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 角色创建人
        /// </summary>
        public virtual User User { get; set; }
        ///// <summary>
        ///// 是否可以被删除
        ///// </summary>
        //public bool CanDelete { get; set; }
        /// <summary>
        /// 角色与用户的关系
        /// </summary>
        public virtual ICollection<RoleUser> RoleUsers { get; set; }
        ///// <summary>
        ///// 角色与系统功能的关系
        ///// </summary>
        //public virtual ICollection<MenuRole> SysFuncRoles { get; set; }
        /// <summary>
        /// 绑定的路由列表
        /// </summary>
        public virtual ICollection<AccessAuthority> Routes { get; set; }
    }

    /// <summary>
    /// Fluent Api
    /// </summary>
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(a => a.Routes)
                .WithOne(a => a.Role)
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
