namespace Yadebs.Models.Dto;

public class AccountDto
{
    public int Id { get; init; }
    public string Name { get; set; }
    public int Number { get; set; }
    public int BookId { get; set; }
    public int? ParentId { get; set; }
    public bool IncreasesDebitWhenMoneyAdded { get; set; }
}