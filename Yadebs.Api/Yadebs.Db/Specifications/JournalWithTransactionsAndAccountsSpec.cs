using Ardalis.Specification;

namespace Yadebs.Db.Specifications;

public class JournalWithTransactionsAndAccountsSpec : Specification<Journal>
{
    public JournalWithTransactionsAndAccountsSpec() =>
        Query
            .Include(j => j.Transactions)
            .ThenInclude(t => t.Account);
}