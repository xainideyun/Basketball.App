using JdCat.Basketball.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JdCat.Basketball.Model.Domain
{
    /// <summary>
    /// 比赛队伍
    /// </summary>
    [Table("Team")]
    public class Team : BaseEntity
    {
        /// <summary>
        /// 队名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 得分
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 三分
        /// </summary>
        public int ThreePoint { get; set; }
        /// <summary>
        /// 三分不中
        /// </summary>
        public int UnThreePoint { get; set; }
        /// <summary>
        /// 两分
        /// </summary>
        public int TwoPoint { get; set; }
        /// <summary>
        /// 两分不中
        /// </summary>
        public int UnTwoPoint { get; set; }
        /// <summary>
        /// 一分
        /// </summary>
        public int OnePoint { get; set; }
        /// <summary>
        /// 一分不中
        /// </summary>
        public int UnOnePoint { get; set; }
        /// <summary>
        /// 犯规
        /// </summary>
        public int Foul { get; set; }
        /// <summary>
        /// 篮板
        /// </summary>
        public int Backboard { get; set; }
        /// <summary>
        /// 盖帽
        /// </summary>
        public int BlockShot { get; set; }
        /// <summary>
        /// 助攻
        /// </summary>
        public int Assists { get; set; }
        /// <summary>
        /// 抢断
        /// </summary>
        public int Steals { get; set; }
        /// <summary>
        /// 失误
        /// </summary>
        public int Fault { get; set; }
        /// <summary>
        /// 暂停
        /// </summary>
        public int Suspend { get; set; }


        /// <summary>
        /// 比赛记录人id
        /// </summary>
        public int UserInfoId { get; set; }
        /// <summary>
        /// 比赛记录人实体
        /// </summary>
        [NotMapped]
        public virtual UserInfo UserInfo { get; set; }

        /// <summary>
        /// 所属比赛id
        /// </summary>
        public int MatchId { get; set; }
        /// <summary>
        /// 所属比赛实体
        /// </summary>
        [NotMapped]
        public virtual Match Match { get; set; }

        /// <summary>
        /// 队伍球员
        /// </summary>
        [NotMapped]
        public virtual ICollection<Player> Players { get; set; }
    }
}
