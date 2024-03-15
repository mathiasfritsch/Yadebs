using Yadebs.Bll.Repository;

namespace Yadebs.Models.Dto;

public class AccountDto : IEntity
{
    public string Name { get; set; } = default!;
    public int Number { get; set; }
    public int BookId { get; set; }
    public int? ParentId { get; set; }
    public bool IncreasesDebitWhenMoneyAdded { get; set; }
    public int Id { get; set; }
}