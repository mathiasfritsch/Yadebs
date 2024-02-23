using Ardalis.Specification;

namespace Yadebs.Db.IncomeSurplusCalculation.Specifications;

public class BankTransfersOrderedByDateSpec : Specification<BankTransfer>
{
    public BankTransfersOrderedByDateSpec()
        => Query.OrderBy(j => j.PaymentDate);
}