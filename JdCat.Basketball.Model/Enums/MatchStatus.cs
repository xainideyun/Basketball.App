using System;
using System.Collections.Generic;
using System.Text;

namespace JdCat.Basketball.Model.Enums
{
    /// <summary>
    /// 比赛状态
    /// </summary>
    public enum MatchStatus
    {
        /// <summary>
        /// 初始化
        /// </summary>
        Init = 0,
        /// <summary>
        /// 比赛进行中
        /// </summary>
        Working = 1,
        /// <summary>
        /// 比赛结束
        /// </summary>
        End = 2,
        /// <summary>
        /// 比赛暂停
        /// </summary>
        Pause = 3,
        /// <summary>
        /// 中场休息
        /// </summary>
        Halftime = 4
    }
}
