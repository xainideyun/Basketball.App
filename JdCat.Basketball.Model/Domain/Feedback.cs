using JdCat.Basketball.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JdCat.Basketball.Model.Domain
{
    /// <summary>
    /// 日志类
    /// </summary>
    [Table("Feedback")]
    public class Feedback : BaseEntity
    {
        /// <summary>
        /// 提交内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 提交用户id
        /// </summary>
        public int UserInfoId { get; set; }
        /// <summary>
        /// 提交用户
        /// </summary>
        public virtual UserInfo UserInfo { get; set; }

    }
}
