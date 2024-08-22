using TryBets.Matches.DTO;

namespace TryBets.Matches.Repository;

public class MatchRepository : IMatchRepository
{
    protected readonly ITryBetsContext _context;
    public MatchRepository(ITryBetsContext context)
    {
        _context = context;
    }

    public IEnumerable<MatchDTOResponse> Get(bool matchFinished)
    {
        try
        {
            return _context.Matches.Where(x => x.MatchFinished == matchFinished).OrderBy(x => x.MatchId).Select(x => new MatchDTOResponse
            {
                matchId = x.MatchId,
                matchDate = x.MatchDate,
                matchTeamAId = x.MatchTeamAId,
                matchTeamBId = x.MatchTeamBId,
                teamAName = x.MatchTeamA.TeamName,
                teamBName = x.MatchTeamB.TeamName,
                matchTeamAOdds = Math.Round((x.MatchTeamAValue + x.MatchTeamBValue) / x.MatchTeamAValue, 2).ToString("0.00"),
                matchTeamBOdds = Math.Round((x.MatchTeamAValue + x.MatchTeamBValue) / x.MatchTeamBValue, 2).ToString("0.00"),
                matchFinished = x.MatchFinished,
                matchWinnerId = x.MatchWinnerId
            }).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}