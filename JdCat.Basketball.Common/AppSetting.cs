using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace JdCat.Basketball.Common
{
    public class AppSetting
    {
        public static JsonSerializerSettings JsonSetting { get; } = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            DateFormatString = "yyyy-MM-dd HH:mm:ss",
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static AppSetting Setting { get; } = new AppSetting();

        /// <summary>
        /// 篮球助手AppId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 篮球助手Secret
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// 文件域名
        /// </summary>
        public string FileDomain { get; set; }

        /// <summary>
        /// JWT验证密钥
        /// </summary>
        public string JwtSecret { get; set; }
        /// <summary>
        /// JWT过期时间
        /// </summary>
        public int JwtExpiryDuration { get; set; }

    }
}

