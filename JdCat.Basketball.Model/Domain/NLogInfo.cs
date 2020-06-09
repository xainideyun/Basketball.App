using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JdCat.Basketball.Model.Domain
{
    /// <summary>
    /// 日志信息表
    /// </summary>
    [Table("NLogInfo")]
    public class NLogInfo: BaseEntity
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 日志信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 记录的类名
        /// </summary>
        public string Logger { get; set; }
        /// <summary>
        /// 调用的网站
        /// </summary>
        public string Callsite { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string Exception { get; set; }

    }
}
