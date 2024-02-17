using Mapster;

namespace Yadebs.Models.Dto.Journal;

public class JournalUpdateDto : JournalAddDto
{
    public int Id { get; set; }


    [AdaptIgnore(MemberSide.Source)]
    public new TransactionAddDto[] Transactions
    {
        get; set;
    }
}