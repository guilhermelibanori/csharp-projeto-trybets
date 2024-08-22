using TryBets.Bets.DTO;
using TryBets.Bets.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TryBets.Bets.Repository;

public class BetRepository : IBetRepository
{
    protected readonly ITryBetsContext _context;
    public BetRepository(ITryBetsContext context)
    {
        _context = context;
    }

    public BetDTOResponse Post(BetDTORequest betRequest, string email)
    {
        try
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new Exception("User not founded");
            }
            var match = _context.Matches.FirstOrDefault(m => m.MatchId == betRequest.matchId);
            if (match == null)
            {
                throw new Exception("Match not founded");
            }
            if (match.MatchFinished)
            {
                throw new Exception("Match finished");
            }
            var team = _context.Teams.FirstOrDefault(t => t.TeamId == betRequest.teamId);
            if (team == null)
            {
                throw new Exception("Team not founded");
            }
            if (match.MatchTeamAId != betRequest.teamId && match.MatchTeamBId != betRequest.teamId)
            {
                throw new Exception("Team is not in this match");
            }
            var bet = new Bet
            {
                MatchId = betRequest.matchId,
                TeamId = betRequest.teamId,
                BetValue = betRequest.betValue,
                UserId = user.UserId
            };
            _context.Bets.Add(bet);
            _context.SaveChanges();
            return new BetDTOResponse
            {
                betId = bet.BetId,
                matchId = bet.MatchId,
                teamId = bet.TeamId,
                betValue = bet.BetValue,
                matchDate = match.MatchDate,
                teamName = team.TeamName,
                email = user.Email
            };
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }
    public BetDTOResponse Get(int BetId, string email)
    {
        try
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new Exception("User not founded");
            }
            var bet = _context.Bets.Include(b => b.Match).Include(b => b.Team).FirstOrDefault(b => b.BetId == BetId);
            if (bet == null)
            {
                throw new Exception("Bet not founded");
            }
            return new BetDTOResponse
            {
                betId = bet.BetId,
                matchId = bet.MatchId,
                teamId = bet.TeamId,
                betValue = bet.BetValue,
                matchDate = bet.Match.MatchDate,
                teamName = bet.Team.TeamName,
                email = user.Email
            };
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}