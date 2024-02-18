using Mapster;

namespace Yadebs.Models.Dto;

public class JournalUpdateDto : JournalAddDto
{
    public int Id { get; set; }
    [AdaptIgnore(MemberSide.Source)]
    public new TransactionUpdateDto[] Transactions { get; set; } = default!;
}