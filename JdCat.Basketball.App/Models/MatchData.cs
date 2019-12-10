using JdCat.Basketball.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JdCat.Basketball.App.Models
{
    public class MatchData
    {
        public Match Match { get; set; }
        public Section Section { get; set; }
        public List<Team> Teams { get; set; }
        public List<Player> Players { get; set; }
        public List<MatchLog> Logs { get; set; }
    }
}
