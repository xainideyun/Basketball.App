using JdCat.Basketball.Common;
using JdCat.Basketball.Model.Domain;
using JdCat.Basketball.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JdCat.Basketball.IService
{
    public interface IMatchService : IBaseService
    {
        #region MyRegion

        ///// <summary>
        ///// 创建比赛
        ///// </summary>
        ///// <param name="match"></param>
        ///// <returns></returns>
        //Task<Match> CreateMatchAsync(Match match);
        ///// <summary>
        ///// 球队更换统计人
        ///// </summary>
        ///// <param name="team"></param>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //Task<MatchLog> ChangeStatisticianAsync(Team team, UserInfo user);
        ///// <summary>
        ///// 球员加入
        ///// </summary>
        ///// <param name="player"></param>
        ///// <returns></returns>
        //Task<bool> JoinPlayerAsync(Player player);
        ///// <summary>
        ///// 获取球队队员
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //Task<List<Player>> GetTeamPlayersAsync(int id);
        ///// <summary>
        ///// 更换球员
        ///// </summary>
        ///// <param name="team">更换球员的球队</param>
        ///// <param name="oldPlayers">正在场上的球员</param>
        ///// <param name="newPlayers">新上场的球员</param>
        ///// <returns></returns>
        //Task<MatchLog> ChangePlayerAsync(Team team, IEnumerable<Player> oldPlayers, IEnumerable<Player> newPlayers);
        ///// <summary>
        ///// 开始比赛
        ///// </summary>
        ///// <param name="match"></param>
        ///// <returns>返回第一节实体对象</returns>
        //Task<Section> StartMatchAsync(Match match);
        ///// <summary>
        ///// 记录
        ///// </summary>
        ///// <param name="player">球员</param>
        ///// <param name="category">记录类别</param>
        ///// <returns></returns>
        //Task RecordAsync(Player player, MatchLogCategory category);
        ///// <summary>
        ///// 下一节
        ///// </summary>
        ///// <param name="match"></param>
        ///// <returns></returns>
        //Task NextSectionAsync(Match match);
        ///// <summary>
        ///// 结束比赛
        ///// </summary>
        ///// <param name="match"></param>
        ///// <returns></returns>
        //Task CloseMatchAsync(Match match);

        #endregion
        /// <summary>
        /// 获取用户创建的比赛
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Match>> GetUserCreateMatchsAsync(int userId, PagingQuery paging);
        /// <summary>
        /// 获取用户参与过的比赛
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        Task<List<Match>> GetJoinMatchsAsync(int userId, PagingQuery paging);
        /// <summary>
        /// 获取比赛详情
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        Task<Match> GetMatchDetailAsync(int matchId);
        /// <summary>
        /// 获取比赛队伍
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        Task<List<Team>> GetMatchTeamsAsync(int matchId);
        /// <summary>
        /// 创建比赛
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        Task<Match> CreateMatchAsync(Match match);
        /// <summary>
        /// 球员加入
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        Task<Player> JoinPlayerAsync(Player player);
        /// <summary>
        /// 获取新加入成员
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        Task<List<Player>> GetNewJoinPlayersAsync(int teamId);
        /// <summary>
        /// 更换统计员
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<MatchLog> ChangeStatisticianAsync(int teamId, UserInfo user);
        /// <summary>
        /// 开始比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        Task<Match> StartMatchAsync(int matchId);
        /// <summary>
        /// 暂停比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        Task<bool> PauseAsync(int matchId, int? teamId);
        /// <summary>
        /// 比赛继续
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns>返回继续比赛时间戳</returns>
        Task<long> ContinueAsync(int sectionId);
        /// <summary>
        /// 记录比赛日志
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        Task RecordMatchAsync(MatchLog log);
        /// <summary>
        /// 获取比赛日志
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        Task<List<MatchLog>> GetLogsAsync(int matchId, PagingQuery paging);
        /// <summary>
        /// 保存比赛数据（发生在更换统计员、单节结束、比赛结束之前）
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        Task SaveMatchAsync(Match match);
        /// <summary>
        /// 单节结束
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        Task EndSectionAsync(int sectionId);
        /// <summary>
        /// 下一节
        /// </summary>
        /// <param name="sectionId">上一节id</param>
        /// <returns></returns>
        Task<Section> NextSectionAsync(int sectionId);
        /// <summary>
        /// 结束比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        Task EndMatchAsync(int matchId);
        /// <summary>
        /// 获取队伍的参与者
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        Task<List<Player>> GetTeamPlayersAsync(int teamId);
        /// <summary>
        /// 更换球队统计员
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> ChangeTeamRecordPeopleAsync(int teamId, int userId);
    }
}
