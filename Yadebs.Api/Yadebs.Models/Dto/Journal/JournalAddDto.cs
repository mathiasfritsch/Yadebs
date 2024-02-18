namespace Yadebs.Models.Dto;

public class JournalAddDto
{
    public string Description { get; set; } = default!;
    public DateTime Date { get; set; }
    public TransactionAddDto[] Transactions { get; init; } = default!;
}