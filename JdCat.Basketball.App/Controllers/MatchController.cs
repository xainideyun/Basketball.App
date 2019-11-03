using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JdCat.Basketball.App.Models;
using JdCat.Basketball.Common;
using JdCat.Basketball.IService;
using JdCat.Basketball.Model.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace JdCat.Basketball.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : BaseController<IMatchService>
    {
        public MatchController(IMatchService service) : base(service)
        {
        }

        /// <summary>
        /// 创建比赛
        /// </summary>
        /// <param name="match"></param>
        /// <param name="util"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResult<Match>>> CreateMatch([FromBody]Match match, [FromServices]IUtilService util)
        {
            match.Code = await util.GetNextMatchCodeAsync();
            var result = await Service.CreateMatchAsync(match);
            return new ApiResult<Match> { Result = result };
        }

        /// <summary>
        /// 获取比赛详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("detail/{id}")]
        public async Task<ActionResult<Match>> GetMatch(int id)
        {
            var match = await Service.GetMatchDetailAsync(id);
            return match;
        }

        /// <summary>
        /// 加入比赛
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        [HttpPost("join")]
        public async Task<ActionResult<ApiResult<Player>>> JoinMatch([FromBody]Player player)
        {
            var players = await Service.GetTeamPlayersAsync(player.TeamId);
            if (players != null && players.Any(a => a.UserInfoId == player.UserInfoId))
            {
                return new ApiResult<Player> { Code = 301, Message = "已经加入过比赛" };
            }
            var team = await Service.GetAsync<Team>(player.TeamId);
            player.MatchId = team.MatchId;
            await Service.JoinPlayerAsync(player);
            return new ApiResult<Player> { Result = player };
        }

        /// <summary>
        /// 获取队伍成员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("team/players/{id}")]
        public async Task<ActionResult<List<Player>>> GetTeamPlayers(int id)
        {
            return await Service.GetTeamPlayersAsync(id) ?? new List<Player>();
        }

        /// <summary>
        /// 获取队伍新加入的成员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("team/newplayers/{id}")]
        public async Task<ActionResult<List<Player>>> GetTeamNewJoinPlayers(int id)
        {
            return await Service.GetNewJoinPlayersAsync(id);
        }

        /// <summary>
        /// 获取用户创建的比赛
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("create/{id}")]
        public async Task<ActionResult<List<Match>>> GetCreateMatchs(int id, [FromQuery]PagingQuery paging)
        {
            return await Service.GetUserCreateMatchsAsync(id, paging) ?? new List<Match>();
        }

        /// <summary>
        /// 获取用户参与过的比赛
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet("join/{id}")]
        public async Task<ActionResult<List<Match>>> GetJoinMatchs(int id, [FromQuery]PagingQuery paging)
        {
            return await Service.GetJoinMatchsAsync(id, paging) ?? new List<Match>();
        }

        /// <summary>
        /// 获取比赛队伍列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("teams/{id}")]
        public async Task<ActionResult<List<Team>>> GetMatchTeams(int id)
        {
            return await Service.GetMatchTeamsAsync(id);
        }

        /// <summary>
        /// 开始比赛
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("start/{id}")]
        public async Task<ActionResult<Match>> StartMatch(int id)
        {
            var match = await Service.StartMatchAsync(id);
            return match;
        }

        /// <summary>
        /// 暂停比赛
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("pause/{id}")]
        public async Task<ActionResult<ApiResult<bool>>> Pause(int id, [FromQuery]int? teamId)
        {
            var result = new ApiResult<bool>
            {
                Result = await Service.PauseAsync(id, teamId)
            };
            return result;
        }

        /// <summary>
        /// 比赛继续
        /// </summary>
        /// <param name="id">单节实体id</param>
        /// <returns></returns>
        [HttpPut("continue/{id}")]
        public async Task<ActionResult<ApiResult<long>>> Continue(int id)
        {
            return new ApiResult<long> { Result = await Service.ContinueAsync(id) };
        }

        /// <summary>
        /// 下一节
        /// </summary>
        /// <param name="id">上一节id</param>
        /// <returns></returns>
        [HttpPut("next/{id}")]
        public async Task<ActionResult<ApiResult<Section>>> Next(int id)
        {
            var section = await Service.NextSectionAsync(id);
            return new ApiResult<Section> { Result = section };
        }

        /// <summary>
        /// 更换球员
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        [HttpPost("change")]
        public async Task<ActionResult<ApiResult<string>>> ChangePlayer([FromBody]IEnumerable<Player> players)
        {
            await Service.UpdateAsync(players, new[] { "Status", "ContinueTime", "TakeupTime" });
            return new ApiResult<string> { Message = "ok" };
        }

        /// <summary>
        /// 比赛记录
        /// </summary>
        /// <param name="log"></param>
        /// <param name="hostScore"></param>
        /// <param name="visitorScore"></param>
        /// <returns></returns>
        [HttpPost("record")]
        public async Task<ActionResult<ApiResult<string>>> Record([FromBody]MatchLog log, [FromQuery]double? hostScore, [FromQuery]double? visitorScore)
        {
            await Service.RecordMatchAsync(log);
            var match = await Service.GetAsync<Match>(log.MatchId);
            if (hostScore.HasValue && visitorScore.HasValue)
            {
                match.HostScore = hostScore.Value;
                match.VisitorScore = visitorScore.Value;
                await Service.UpdateAsync(match, nameof(match.HostScore), nameof(match.VisitorScore));
            }
            return new ApiResult<string> { Result = "ok" };
        }

        [HttpPost("upload")]
        public async Task<ActionResult<ApiResult<bool>>> Upload([FromBody]MatchData data)
        {
            if (data.Match != null)
            {
                await Service.UpdateAsync(data.Match);
            }
            if (data.Section != null)
            {
                await Service.UpdateAsync(data.Section);
            }
            if (data.Teams != null && data.Teams.Count > 0)
            {
                await Service.UpdateAsync<Team>(data.Teams);
            }
            if (data.Players != null && data.Players.Count > 0)
            {
                await Service.UpdateAsync<Player>(data.Players);
            }
            return new ApiResult<bool> { Result = true };
        }

        /// <summary>
        /// 获取比赛记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet("record/{id}")]
        public async Task<ActionResult<List<MatchLog>>> GetLog(int id, [FromQuery]PagingQuery paging)
        {
            return await Service.GetLogsAsync(id, paging) ?? new List<MatchLog>();
        }

    }
}