using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JdCat.Basketball.Model.Enums
{
    /// <summary>
    /// 比赛日志类别
    /// </summary>
    public enum MatchLogCategory
    {
        /// <summary>
        /// 三分
        /// </summary>
        ThreePoint = 1,
        /// <summary>
        /// 三分不中
        /// </summary>
        UnThreePoint = 2,
        /// <summary>
        /// 两分
        /// </summary>
        TwoPoint = 3,
        /// <summary>
        /// 两分不中
        /// </summary>
        UnTwoPoint = 4,
        /// <summary>
        /// 一分
        /// </summary>
        OnePoint = 5,
        /// <summary>
        /// 一分不中
        /// </summary>
        UnOnePoint = 6,
        /// <summary>
        /// 犯规
        /// </summary>
        Foul = 7,
        /// <summary>
        /// 篮板
        /// </summary>
        Backboard = 8,
        /// <summary>
        /// 盖帽
        /// </summary>
        BlockShot = 9,
        /// <summary>
        /// 助攻
        /// </summary>
        Assists = 10,
        /// <summary>
        /// 抢断
        /// </summary>
        Steals = 11,
        /// <summary>
        /// 失误
        /// </summary>
        Fault = 12,
        /// <summary>
        /// 更换球员
        /// </summary>
        Change = 13,
        /// <summary>
        /// 更换统计员
        /// </summary>
        ChangeStatistician = 14,
    }
}
