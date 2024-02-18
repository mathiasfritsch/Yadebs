using Mapster;

namespace Yadebs.Models.Dto;

public class JournalUpdateDto : JournalAddDto
{ 
    public int Id { get; }
    
    [AdaptIgnore(MemberSide.Source)]
    public new TransactionAddDto[] Transactions
    {
        get;
    }
}