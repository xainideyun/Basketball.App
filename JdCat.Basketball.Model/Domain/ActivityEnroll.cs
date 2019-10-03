using JdCat.Basketball.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JdCat.Basketball.Model.Domain
{
    [Table("ActivityEnroll")]
    public class ActivityEnroll : BaseEntity
    {
        /// <summary>
        /// 活动标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 活动时间
        /// </summary>
        public DateTime ActivityTime { get; set; }
        /// <summary>
        /// 活动地点
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 活动地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Lng { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Lat { get; set; }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 活动状态
        /// </summary>
        public ActivityStatus Status { get; set; }

        /// <summary>
        /// 报名人数
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 参加人数
        /// </summary>
        public int JoinQuantity { get; set; }
        /// <summary>
        /// 缺席人数
        /// </summary>
        public int AbsentQuantity { get; set; }
        /// <summary>
        /// 待定人数
        /// </summary>
        public int PendingQuantity { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 关联的用户id
        /// </summary>
        public int UserInfoId { get; set; }
        /// <summary>
        /// 关联的用户实体
        /// </summary>
        public virtual UserInfo UserInfo { get; set; }
        /// <summary>
        /// 活动参与人
        /// </summary>
        public virtual ICollection<ActivityParticipant> ActivityParticipants { get; set; }

    }
}
