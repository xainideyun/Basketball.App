using JdCat.Basketball.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JdCat.Basketball.Model.Domain
{
    [Table("MatchLog")]
    public class MatchLog : BaseEntity
    {
        /// <summary>
        /// 记录者名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime LogTime { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public MatchLogCategory Category { get; set; }
        /// <summary>
        /// 比赛id
        /// </summary>
        public int MatchId { get; set; }
        /// <summary>
        /// 比赛实体
        /// </summary>
        public virtual Match Match { get; set; }
        /// <summary>
        /// 记录者id
        /// </summary>
        public int UserInfoId { get; set; }
        /// <summary>
        /// 记录者实体
        /// </summary>
        public UserInfo UserInfo { get; set; }
    }
}
