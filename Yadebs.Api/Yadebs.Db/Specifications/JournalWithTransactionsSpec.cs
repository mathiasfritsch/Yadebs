using Ardalis.Specification;

namespace Yadebs.Db.Specifications;

public class JournalWithTransactionsSpec : Specification<Journal>
{
    public JournalWithTransactionsSpec()
        => Query.Include(j => j.Transactions);
}