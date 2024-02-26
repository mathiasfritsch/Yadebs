namespace Yadebs.Models.Dto;

public class TransactionUpdateDto
{
    int Id { get; set; }
    public decimal Amount { get; set; }
    public int AccountId { get; set; }
}