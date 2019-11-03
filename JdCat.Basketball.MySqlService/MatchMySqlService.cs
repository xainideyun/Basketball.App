using JdCat.Basketball.Model;
using JdCat.Basketball.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JdCat.Basketball.Model.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NLog;
using JdCat.Basketball.Model.Enums;
using JdCat.Basketball.Common;

namespace JdCat.Basketball.MySqlService
{
    public class MatchMySqlService : BaseMySqlService, IMatchService
    {
        public MatchMySqlService(BasketballDbContext context) : base(context)
        {
        }
        private Logger Logger => LogManager.GetCurrentClassLogger();

        #region MyRegion

        //public async Task<Match> CreateMatchAsync(Match match)
        //{
        //    if (match.ID > 0) throw new Exception("比赛已存在，请勿重复创建");
        //    var teams = match.Teams;
        //    match.Teams = null;
        //    if (teams == null || teams.Count == 0) throw new Exception("至少需要一个参赛队伍");
        //    await Context.Matchs.AddAsync(match);
        //    await Context.SaveChangesAsync();
        //    match.Teams = new List<Team> { teams.First() };
        //    if (match.Mode == MatchMode.Self)
        //    {
        //        var team2 = new Team
        //        {
        //            Name = "敌方队伍",
        //            UserInfoId = match.UserInfoId,
        //            Players = new List<Player> { new Player { Name = "球员1", PlayNumber = "1", Status = PlayerStatus.Up, UserInfoId = 1, MatchId = match.ID }
        //            }
        //        };
        //        match.Teams.Add(team2);
        //    }
        //    else
        //    {
        //        match.Teams.Add(teams.Last());
        //    }
        //    await Context.SaveChangesAsync();
        //    return match;
        //}

        //public async Task<MatchLog> ChangeStatisticianAsync(Team team, UserInfo user)
        //{
        //    team.UserInfoId = user.ID;
        //    await UpdateAsync(team, new[] { nameof(team.UserInfoId) });
        //    var log = new MatchLog { LogTime = DateTime.Now, Content = $"更换统计员：{user.Name ?? user.NickName}", Category = MatchLogCategory.ChangeStatistician, MatchId = team.MatchId, UserInfoId = team.UserInfoId };
        //    await Context.AddAsync(log);
        //    await Context.SaveChangesAsync();
        //    return log;
        //}

        //public async Task<bool> JoinPlayerAsync(Player player)
        //{
        //    player.Status = PlayerStatus.Down;
        //    await Context.AddAsync(player);
        //    await Context.SaveChangesAsync();
        //    return true;
        //}

        //public async Task<List<Player>> GetTeamPlayersAsync(int id)
        //{
        //    return await Context.Players.Where(a => a.TeamId == id).ToListAsync();
        //}

        //public async Task<MatchLog> ChangePlayerAsync(Team team, IEnumerable<Player> oldPlayers, IEnumerable<Player> newPlayers)
        //{
        //    oldPlayers.ForEach(a => a.Status = PlayerStatus.Down);
        //    newPlayers.ForEach(a => a.Status = PlayerStatus.Up);
        //    oldPlayers.ForEach(a =>
        //    {
        //        Context.Attach(a);
        //        Context.Entry(a).Property(nameof(a.Status)).IsModified = true;
        //    });
        //    newPlayers.ForEach(a =>
        //    {
        //        Context.Attach(a);
        //        Context.Entry(a).Property(nameof(a.Status)).IsModified = true;
        //    });
        //    await Context.SaveChangesAsync();
        //    var log = new MatchLog { LogTime = DateTime.Now, Category = MatchLogCategory.Change, MatchId = team.MatchId, UserInfoId = team.UserInfoId };
        //    await Context.AddAsync(log);
        //    await Context.SaveChangesAsync();
        //    return log;
        //}

        //public async Task<Section> StartMatchAsync(Match match)
        //{
        //    var section = new Section { PartNumber = 1, StartTime = DateTime.Now, MatchId = match.ID, Status = MatchStatus.Working };
        //    match.StartTime = DateTime.Now;
        //    match.Status = MatchStatus.Working;
        //    Context.Attach(match);
        //    Context.Entry(match).Property(nameof(match.StartTime)).IsModified = true;
        //    Context.Entry(match).Property(nameof(match.Status)).IsModified = true;
        //    await Context.AddAsync(section);
        //    await Context.SaveChangesAsync();
        //    return section;
        //}

        //public async Task RecordAsync(Player player, MatchLogCategory category)
        //{
        //    throw new NotImplementedException("未实现mysql记录比赛数据");
        //}

        #endregion

        public Task<Match> CreateMatchAsync(Match match)
        {
            throw new NotImplementedException("未实现mysql创建比赛接口[CreateMatchAsync]。");
        }

        public Task<Player> JoinPlayerAsync(Player player)
        {
            throw new NotImplementedException("未实现mysql球员加入接口[JoinPlayerAsync]。");
        }

        public Task<MatchLog> ChangeStatisticianAsync(Team team, UserInfo user)
        {
            throw new NotImplementedException("未实现mysql更换统计员接口[ChangeStatisticianAsync]。");
        }

        public Task<Section> StartMatchAsync(Match match)
        {
            throw new NotImplementedException("未实现mysql开始比赛接口[StartMatchAsync]。");
        }


    }
}
