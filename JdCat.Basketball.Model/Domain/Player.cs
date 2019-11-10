using JdCat.Basketball.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JdCat.Basketball.Model.Domain
{
    /// <summary>
    /// 球员
    /// </summary>
    [Table("Player")]
    public class Player : BaseEntity
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 号码
        /// </summary>
        public string PlayNumber { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public string FaceUrl { get; set; }
        /// <summary>
        /// 上场时间
        /// </summary>
        public long TakeupTime { get; set; }
        /// <summary>
        /// 继续比赛时间
        /// </summary>
        public long ContinueTime { get; set; }

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
        /// 正负值
        /// </summary>
        public int GetLost { get; set; }

        /// <summary>
        /// 球员状态
        /// </summary>
        public PlayerStatus Status { get; set; }

        /// <summary>
        /// 所属队伍id
        /// </summary>
        public int TeamId { get; set; }
        /// <summary>
        /// 所属队伍实体
        /// </summary>
        [NotMapped]
        public virtual Team Team { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public int UserInfoId { get; set; }
        /// <summary>
        /// 用户实体
        /// </summary>
        [NotMapped]
        public virtual UserInfo UserInfo{ get; set; }

        /// <summary>
        /// 比赛id
        /// </summary>
        public int MatchId { get; set; }
        /// <summary>
        /// 比赛实体
        /// </summary>
        [NotMapped]
        public virtual Match Match { get; set; }

    }
}
