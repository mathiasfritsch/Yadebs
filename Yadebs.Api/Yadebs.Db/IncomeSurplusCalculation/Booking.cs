namespace Yadebs.Db.IncomeSurplusCalculation;

public class Booking
{
    public int Id { get; set; }
    public BankTransfer? BankTransfer { get; set; }
    public Document? Document { get; set; }
    public Category? Category { get; set; }
}