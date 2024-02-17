namespace Yadebs.Models.Dto.Journal;

public class JournalAddDto
{
    public required string Description
    {
        get; set;
    }

    public DateTime Date { get; set; }

    public TransactionAddDto[] Transactions
    {
        get; set;
    }
}