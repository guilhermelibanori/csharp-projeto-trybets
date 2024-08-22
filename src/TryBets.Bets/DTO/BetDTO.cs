namespace TryBets.Bets.DTO;

public class BetDTORequest
{
  public int matchId { get; set; }
  public int teamId { get; set; }
  public decimal betValue { get; set; }
}

public class BetDTOResponse
{
  public int betId { get; set; }
  public int matchId { get; set; }
  public int teamId { get; set; }
  public decimal betValue { get; set; }
  public DateTime matchDate { get; set; }
  public string? teamName { get; set; }
  public string? email { get; set; }
}
