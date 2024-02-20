using Yadebs.Bll.Repository;

namespace Yadebs.Models.Dto;

public class JournalDto : IEntity
{
    public int Id { get; set; }
    public TransactionDto[] Transactions { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime Date { get; set; }
}