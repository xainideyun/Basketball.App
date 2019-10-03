using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JdCat.Basketball.Model.Enums
{
    /// <summary>
    /// 报名者状态
    /// </summary>
    public enum JoinStatus
    {
        /// <summary>
        /// 参加
        /// </summary>
        [Description("参加")]
        Join = 1,
        /// <summary>
        /// 缺席
        /// </summary>
        [Description("缺席")]
        Absent = 2,
        /// <summary>
        /// 待定
        /// </summary>
        [Description("待定")]
        Pending = 4
    }
}
