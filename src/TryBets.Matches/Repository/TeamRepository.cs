using TryBets.Matches.DTO;

namespace TryBets.Matches.Repository;

public class TeamRepository : ITeamRepository
{
    protected readonly ITryBetsContext _context;
    public TeamRepository(ITryBetsContext context)
    {
        _context = context;
    }

    public IEnumerable<TeamDTOResponse> Get()
    {
        try
        {
            return _context.Teams.Select(x => new TeamDTOResponse
            {
                teamId = x.TeamId,
                teamName = x.TeamName
            }).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}