namespace Yadebs.Models.Dto;

public class JournalDto
{
    public int Id { get; set; }
    public string Description { get; set; } = default!;
    public DateTime Date { get; set; }
    public List<TransactionDto> Transactions { get; set; } = default!;
}