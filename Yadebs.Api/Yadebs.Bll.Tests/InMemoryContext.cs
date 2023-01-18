using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Yadebs.Db;

namespace Yadebs.Bll.Tests
{
    public class InMemoryContext
    {
        public static AccountingContext GetContext()
        {
            DbContextOptions<AccountingContext> contextOptions = new DbContextOptionsBuilder<AccountingContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                    .Options;
            var context = new AccountingContext(contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }
    }
}