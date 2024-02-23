using Ardalis.Specification;

namespace Yadebs.Db.Specifications;

public class AccountListOrderedByNumberSpec : Specification<Account>
{
    public AccountListOrderedByNumberSpec() => Query.OrderBy(a => a.Number);
}