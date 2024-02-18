namespace Yadebs.Models.Dto;

public class TransactionDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public AccountDto Account { get; set; }
    public int JournalId { get; set; }
    public JournalDto Journal { get; set; }
    public bool IsDebit { get; set; }
    public int AccountId { get; set; }
}