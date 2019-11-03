using JdCat.Basketball.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JdCat.Basketball.Model.Domain
{
    [Table("Match")]
    public class Match : BaseEntity
    {
        /// <summary>
        /// 比赛编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 比赛地点
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 比赛地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Lng { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Lat { get; set; }
        /// <summary>
        /// 比赛耗时（毫秒）
        /// </summary>
        public long TakeupTime { get; set; }
        /// <summary>
        /// 主队得分
        /// </summary>
        public double HostScore { get; set; }
        /// <summary>
        /// 主队名称
        /// </summary>
        public string HostName { get; set; }
        /// <summary>
        /// 客队得分
        /// </summary>
        public double VisitorScore { get; set; }
        /// <summary>
        /// 客队名称
        /// </summary>
        public string VisitorName { get; set; }
        /// <summary>
        /// 比赛开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 比赛结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        ///// <summary>
        ///// 暂停时间
        ///// </summary>
        //public long PauseTime { get; set; }
        /// <summary>
        /// 比赛继续时间（如比赛暂停后继续比赛的时间）
        /// </summary>
        public long ContinueTime { get; set; }
        /// <summary>
        /// 比赛模式
        /// </summary>
        public MatchMode Mode { get; set; }
        /// <summary>
        /// 比赛状态
        /// </summary>
        public MatchStatus Status { get; set; }

        ///// <summary>
        ///// 主队id
        ///// </summary>
        //public int TeamHostId { get; set; }
        ///// <summary>
        ///// 主队实体
        ///// </summary>
        ////[ForeignKey("TeamHostId")]
        //public virtual Team TeamHost { get; set; }
        ///// <summary>
        ///// 客队id
        ///// </summary>
        //public int TeamVisitorId { get; set; }
        ///// <summary>
        ///// 客队实体
        ///// </summary>
        ////[ForeignKey("TeamVisitorId")]
        //public virtual Team TeamVisitor { get; set; }
        /// <summary>
        /// 比赛球队
        /// </summary>
        public virtual ICollection<Team> Teams { get; set; }

        /// <summary>
        /// 单节
        /// </summary>
        public virtual ICollection<Section> Sections { get; set; }

        /// <summary>
        /// 创建者id
        /// </summary>
        public int UserInfoId { get; set; }
        /// <summary>
        /// 创建者实体
        /// </summary>
        public virtual UserInfo UserInfo{ get; set; }
    }
}
