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
    [Table("Log")]
    public class Log : BaseEntity
    {
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string Application { get; set; }
        /// <summary>
        /// 日志级别
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 记录日志的类
        /// </summary>
        public string Logger { get; set; }
        /// <summary>
        /// 打印位置
        /// </summary>
        public string Callsite { get; set; }
        /// <summary>
        /// 错误详情
        /// </summary>
        public string Exception { get; set; }

    }
}
