using Yadebs.Bll.Repository;

namespace Yadebs.Models.Dto;

public class TransactionDto : IEntity
{
    public int Id { get; set; }
    public AccountDto Account { get; set; } = default!;
    public int JournalId { get; set; }
    public JournalDto Journal { get; set; } = default!;
    public bool IsDebit { get; set; }
    public decimal Amount { get; set; }
    public int AccountId { get; set; }
}