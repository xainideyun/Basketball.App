using StackExchange.Redis;
using JdCat.Basketball.IService;
using JdCat.Basketball.Model.Domain;
using JdCat.Basketball.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NLog;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.IO;
using JdCat.Basketball.Model.Enums;
using JdCat.Basketball.Model;
using Microsoft.EntityFrameworkCore;

namespace JdCat.Basketball.RedisService
{
    public class MatchRedisService : BaseRedisService, IMatchService
    {
        public MatchRedisService(IConnectionMultiplexer cache, BasketballDbContext context) : base(cache, context)
        {
        }


        private Logger Logger => LogManager.GetCurrentClassLogger();

        // 球队更新字段
        private static List<string> _teamFields = new List<string> { "Score", "ThreePoint", "UnThreePoint", "TwoPoint", "UnTwoPoint", "OnePoint", "UnOnePoint", "Foul", "Backboard", "BlockShot", "Assists", "Steals", "Fault", "Suspend" };
        // 比赛更新字段
        private static List<string> _matchFields = new List<string> { "TakeupTime", "HostScore", "VisitorScore", "EndTime", "Status" };
        // 单节更新字段
        private static List<string> _sectionFields = new List<string> { "HostScore", "VisitorScore", "TakeupTime", "EndTime", "Status" };
        // 球员更新字段
        private static List<string> _playerFields = new List<string> { "TakeupTime", "Score", "ThreePoint", "UnThreePoint", "TwoPoint", "UnTwoPoint", "OnePoint", "UnOnePoint", "Foul", "Backboard", "BlockShot", "Assists", "Steals", "Fault" };

        public async Task<List<Match>> GetUserCreateMatchsAsync(int userId, PagingQuery paging)
        {
            return await GetRelativeEntitysAsync<Match, UserInfo>(userId, "CreateMatch", paging);
        }

        public async Task<List<Match>> GetJoinMatchsAsync(int userId, PagingQuery paging)
        {
            return await GetRelativeEntitysAsync<Match, UserInfo>(userId, paging: paging);
        }

        public async Task<Match> GetMatchDetailAsync(int matchId)
        {
            var match = await GetAsync<Match>(matchId);
            var teams = await GetRelativeEntitysAsync<Team, Match>(matchId, order: Order.Ascending);
            var sections = await GetRelativeEntitysAsync<Section, Match>(matchId, order: Order.Ascending);
            await teams.ForEachAsync(async team =>
            {
                team.Players = await GetRelativeEntitysAsync<Player, Team>(team.ID);
            });
            match.Teams = teams;
            match.Sections = sections ?? new List<Section>();
            return match;
        }

        public async Task<List<Team>> GetMatchTeamsAsync(int matchId)
        {
            return await GetRelativeEntitysAsync<Team, Match>(matchId);
        }

        public async Task<Match> CreateMatchAsync(Match match)
        {
            //if (match.Teams.Count == 1)
            //{
            //    match.Teams.Add(new Team
            //    {
            //        Name = match.VisitorName,
            //        UserInfoId = match.UserInfoId,
            //        Players = new List<Player> { new Player { Name = "球员1", PlayNumber = "1", Status = PlayerStatus.Up, UserInfoId = 1, MatchId = match.ID }
            //        }
            //    });
            //}
            //var hostTeam = match.Teams.First();
            //var visitorTeam = match.Teams.Last();
            //match.HostName = hostTeam.Name;
            //match.VisitorName = visitorTeam.Name;
            await AddAsync(match);
            match.Teams.ForEach(team => team.MatchId = match.ID);
            await AddAsync<Team>(match.Teams);
            if (match.Mode == MatchMode.Self)
            {
                var vistorTeam = match.Teams.FirstOrDefault(a => a.Name == "系统队");
                var player = new Player { Name = "admin", PlayNumber = "X", Status = PlayerStatus.Up, TeamId = vistorTeam.ID, UserInfoId = 1, MatchId = match.ID };
                await AddAsync(player);
                await SetRelativeEntitysAsync<Player, Team>(vistorTeam.ID, player);         // 保存球员与球队关系
            }
            await SetRelativeEntitysAsync<Team, Match>(match.ID, match.Teams.ToArray());                // 保存比赛与队伍的关系
            await SetRelativeEntitysAsync<Match, UserInfo>(match.UserInfoId, "CreateMatch", match);     // 保存用户创建的比赛

            return match;
        }

        public async Task<Player> JoinPlayerAsync(Player player)
        {
            player.Status = PlayerStatus.Down;
            await AddAsync(player);
            await SetRelativeEntitysAsync<Player, Team>(player.TeamId, player);                     // 保存球队与球员关系
            await SetRelativeEntitysAsync<Match, UserInfo>(player.UserInfoId, player.MatchId);      // 保存用户参与过的比赛
            await SetRelativeEntitysAsync<Player, UserInfo>(player.UserInfoId, player);             // 保存用户与参赛者关系

            var tempKey = KeyForOther($"Temp:JoinTeamPlayers:{player.TeamId}");
            await Database.ListRightPushAsync(tempKey, player.ToJson());        // 新加入成员记录
            await Database.KeyExpireAsync(tempKey, new TimeSpan(0, 0, 10));     // 设置10秒后过期
            return player;
        }

        public async Task<List<Player>> GetNewJoinPlayersAsync(int teamId)
        {
            var tempKey = KeyForOther($"Temp:JoinTeamPlayers:{teamId}");
            var list = await Database.ListRangeAsync<Player>(tempKey);
            //await Database.KeyDeleteAsync(tempKey);
            return list;
        }

        public async Task<MatchLog> ChangeStatisticianAsync(int teamId, UserInfo user)
        {
            var team = await GetAsync<Team>(teamId);
            var oldUser = team.UserInfoId;
            team.UserInfoId = user.ID;
            await UpdateAsync(team, nameof(team.UserInfoId));
            await SetRelativeEntitysAsync<Match, UserInfo>(user.ID, team.MatchId);      // 保存用户参与过的比赛
            var log = new MatchLog { LogTime = DateTime.Now, Content = $"{team.Name}更换统计员：{user.Name ?? user.NickName}", Category = MatchLogCategory.ChangeStatistician, MatchId = team.MatchId, UserInfoId = oldUser };
            await AddAsync(log);
            await SetRelativeEntitysAsync<MatchLog, Match>(team.MatchId, log);      // 保存比赛日志与比赛之间的关系
            return null;
        }

        public async Task<Match> StartMatchAsync(int matchId)
        {
            var match = await GetAsync<Match>(matchId);
            var timestamp = DateTime.Now.ToTimestamp();
            var section = new Section { PartNumber = 1, StartTime = DateTime.Now, ContinueTime = timestamp, Status = MatchStatus.Working, MatchId = match.ID };
            await AddAsync(section);
            await SetRelativeEntitysAsync<Section, Match>(matchId, section);
            match.StartTime = DateTime.Now;
            match.Status = MatchStatus.Working;
            match.ContinueTime = timestamp;
            await UpdateAsync(match, nameof(match.StartTime), nameof(match.Status));
            match.Sections = new List<Section> { section };
            return match;
        }

        public async Task<bool> PauseAsync(int matchId, int? teamId)
        {
            var match = await GetAsync<Match>(matchId);
            match.Status = MatchStatus.Pause;
            await UpdateAsync(match, nameof(match.Status));
            if (teamId.HasValue)
            {
                var team = await GetAsync<Team>(teamId.Value);
                team.Suspend++;
                await UpdateAsync(team, nameof(team.Suspend));
            }

            return true;
        }

        public async Task<long> ContinueAsync(int sectionId)
        {
            var timestamp = DateTime.Now.ToTimestamp();
            var section = await GetAsync<Section>(sectionId);
            section.ContinueTime = timestamp;
            await UpdateAsync(section, "ContinueTime");

            var match = await GetAsync<Match>(section.MatchId);
            match.Status = MatchStatus.Working;
            match.ContinueTime = timestamp;
            await UpdateAsync(match, "Status", "ContinueTime");

            await UpPlayersContinueAsync(match.ID);

            return timestamp;
        }

        public async Task RecordMatchAsync(MatchLog log)
        {
            await AddAsync(log);
            await SetRelativeEntitysAsync<MatchLog, Match>(log.MatchId, log);
        }

        public async Task<List<MatchLog>> GetLogsAsync(int matchId, PagingQuery paging)
        {
            return await GetRelativeEntitysAsync<MatchLog, Match>(matchId, paging: paging);
        }

        public async Task SaveMatchAsync(Match match)
        {
            var teams = match.Teams;
            var section = match.Sections.FirstOrDefault(a => a.Status != MatchStatus.End);
            var players = teams.SelectMany(a => a.Players).ToList();

            // 保存比赛信息
            if (match.Status == MatchStatus.End)
            {
                match.EndTime = DateTime.Now;
            }
            Context.Attach(match);
            foreach (var field in _matchFields)
            {
                Context.Entry(match).Property(field).IsModified = true;
            }
            // 保存球队信息
            foreach (var team in teams)
            {
                Context.Attach(team);
                foreach (var field in _teamFields)
                {
                    Context.Entry(team).Property(field).IsModified = true;
                }
            }
            // 保存单节数据
            Context.Attach(section);
            foreach (var field in _sectionFields)
            {
                Context.Entry(section).Property(field).IsModified = true;
            }
            // 保存球员数据
            foreach (var player in players)
            {
                Context.Attach(player);
                foreach (var field in _playerFields)
                {
                    Context.Entry(player).Property(field).IsModified = true;
                }
            }

            await Context.SaveChangesAsync();
            await SetObjectAsync(match);
            await teams.ForEachAsync(async team => await SetObjectAsync(team));
            await SetObjectAsync(section);
            await players.ForEachAsync(async player => await SetObjectAsync(player));
        }

        public async Task EndSectionAsync(int sectionId)
        {
            var section = await GetAsync<Section>(sectionId);
            section.Status = MatchStatus.End;
            section.EndTime = DateTime.Now;
            await UpdateAsync(section, nameof(section.Status), nameof(section.EndTime));

            var match = await GetAsync<Match>(section.MatchId);
            match.Status = MatchStatus.Halftime;
            await UpdateAsync(section, nameof(match.Status));
        }

        public async Task<Section> NextSectionAsync(int sectionId)
        {
            var lastSection = await GetAsync<Section>(sectionId);
            var timestamp = DateTime.Now.ToTimestamp();
            var section = new Section { PartNumber = lastSection.PartNumber + 1, StartTime = DateTime.Now, Status = MatchStatus.Working, MatchId = lastSection.MatchId, ContinueTime = timestamp };
            await AddAsync(section);
            await SetRelativeEntitysAsync<Section, Match>(lastSection.MatchId, section);

            var match = await GetAsync<Match>(lastSection.MatchId);
            match.Status = MatchStatus.Working;
            match.ContinueTime = timestamp;
            await UpdateAsync(match, nameof(match.Status), nameof(match.ContinueTime));

            await UpPlayersContinueAsync(match.ID);

            return section;
        }

        public async Task EndMatchAsync(int matchId)
        {
            var match = await GetAsync<Match>(matchId);
            match.EndTime = DateTime.Now;
            match.Status = MatchStatus.End;
            await UpdateAsync(match, nameof(match.EndTime), nameof(match.Status));
        }

        public async Task<List<Player>> GetTeamPlayersAsync(int teamId)
        {
            return await GetRelativeEntitysAsync<Player, Team>(teamId, order: Order.Ascending);
        }

        public async Task<bool> ChangeTeamRecordPeopleAsync(int teamId, int userId)
        {
            var team = await GetAsync<Team>(teamId);
            team.UserInfoId = userId;
            await UpdateAsync(team, nameof(team.UserInfoId));
            await SetRelativeEntitysAsync<Match, UserInfo>(userId, team.MatchId);
            return true;
        }

        public async Task RecordMatchLogsAsync(List<MatchLog> logs)
        {
            var matchId = logs.First().MatchId;
            await AddAsync<MatchLog>(logs);
            await SetRelativeEntitysAsync<MatchLog, Match>(matchId, logs.ToArray());
        }



        /// <summary>
        /// 场上球员继续比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        private async Task UpPlayersContinueAsync(int matchId)
        {
            var timestamp = DateTime.Now.ToTimestamp();
            var players = await Context.Players.Where(a => a.MatchId == matchId && a.Status == PlayerStatus.Up).ToListAsync();
            players.ForEach(a => a.ContinueTime = timestamp);
            await Context.SaveChangesAsync();
            await SetObjectAsync<Player>(players);
        }

    }
}
