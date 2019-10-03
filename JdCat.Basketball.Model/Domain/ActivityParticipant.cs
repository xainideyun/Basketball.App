using JdCat.Basketball.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JdCat.Basketball.Model.Domain
{
    [Table("ActivityParticipant")]
    public class ActivityParticipant : BaseEntity
    {
        /// <summary>
        /// 参与者姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 报名时间
        /// </summary>
        public DateTime JoinTime { get; set; }
        /// <summary>
        /// 参与者头像地址
        /// </summary>
        public string FaceUrl { get; set; }
        /// <summary>
        /// 号码
        /// </summary>
        public string PlayNumber { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 参与者状态
        /// </summary>
        public JoinStatus Status { get; set; }

        /// <summary>
        /// 关联的用户id
        /// </summary>
        public int UserInfoId { get; set; }
        /// <summary>
        /// 关联的用户实体
        /// </summary>
        public virtual UserInfo UserInfo { get; set; }
        /// <summary>
        /// 报名活动的id
        /// </summary>
        public int ActivityEnrollId { get; set; }
        /// <summary>
        /// 报名活动实体
        /// </summary>
        public virtual ActivityEnroll ActivityEnroll { get; set; }

    }
}
