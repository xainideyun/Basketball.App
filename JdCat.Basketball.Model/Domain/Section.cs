using JdCat.Basketball.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JdCat.Basketball.Model.Domain
{
    /// <summary>
    /// 比赛每一小节
    /// </summary>
    [Table("Section")]
    public class Section : BaseEntity
    {
        /// <summary>
        /// 分节序号
        /// </summary>
        public int PartNumber { get; set; }
        /// <summary>
        /// 主队得分
        /// </summary>
        public int HostScore { get; set; }
        /// <summary>
        /// 客队得分
        /// </summary>
        public int VisitorScore { get; set; }
        /// <summary>
        /// 单节耗时
        /// </summary>
        public long TakeupTime { get; set; }
        /// <summary>
        /// 比赛开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 比赛结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 暂停时间
        /// </summary>
        public long PauseTime { get; set; }
        /// <summary>
        /// 比赛继续时间（如比赛暂停后继续比赛的时间）
        /// </summary>
        public long ContinueTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public MatchStatus Status { get; set; }

        /// <summary>
        /// 归属比赛
        /// </summary>
        public int MatchId { get; set; }
        /// <summary>
        /// 比赛实体
        /// </summary>
        public virtual Match Match { get; set; }
    }
}
