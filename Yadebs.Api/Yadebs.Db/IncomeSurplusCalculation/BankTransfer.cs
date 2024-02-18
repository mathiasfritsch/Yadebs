namespace Yadebs.Db.IncomeSurplusCalculation;

public class BankTransfer
{
    public int Id { get; set; }
    public decimal? NetAmount { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal GrossAmount { get; set; }
    public decimal Tax { get; set; }
    public bool IsIncome { get; set; }
    public DateTime PaymentDate { get; set; }
}