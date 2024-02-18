namespace Yadebs.Models.Dto;

public class JournalDto
{
    public int Id { get; set; }

    public required string Description
    {
        get; set;
    }

    public DateTime Date { get; set; }
    public required List<TransactionDto> Transactions { get; set; }
}