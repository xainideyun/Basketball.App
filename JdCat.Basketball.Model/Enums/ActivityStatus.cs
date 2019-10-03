using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JdCat.Basketball.Model.Enums
{
    /// <summary>
    /// 活动状态
    /// </summary>
    public enum ActivityStatus
    {
        /// <summary>
        /// 未定义
        /// </summary>
        [Description("未定义")]
        None = 0,
        /// <summary>
        /// 进行中
        /// </summary>
        [Description("进行中")]
        Doing = 1,
        /// <summary>
        /// 已截止
        /// </summary>
        [Description("已截止")]
        End = 2,
        /// <summary>
        /// 已截止
        /// </summary>
        [Description("已删除")]
        Delete = 4,
    }
}
