using NodaTime;
using Yadebs.Bll.Repository;

namespace Yadebs.Models.Dto;

public class JournalUpdateDto : IEntity
{
    public int Id { get; set; }
    public string Description { get; set; } = default!;
    public LocalDate Date { get; set; }

    public TransactionUpdateDto[] Transactions { get; init; } = default!;
}