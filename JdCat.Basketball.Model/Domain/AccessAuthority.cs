using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JdCat.Basketball.Model.Domain
{
    /// <summary>
    /// 权限描述项（既可绑定角色，可以绑定用户）
    /// </summary>
    [Table("AccessAuthority")]
    public class AccessAuthority: BaseEntity
    {
        /// <summary>
        /// 路由名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 绑定的用户
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// 用户实体
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// 绑定的角色
        /// </summary>
        public int? RoleId { get; set; }
        /// <summary>
        /// 角色实体
        /// </summary>
        public virtual Role Role { get; set; }
    }
}
