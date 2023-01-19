using Mapster;

namespace Yadebs.Models.Dto.Journal
{
    public class JournalUpdateDto
    {
        public int Id { get; set; }

        public string Description
        {
            get; set;
        }

        public DateTime Date { get; set; }

        [AdaptIgnore(MemberSide.Source)]
        public TransactionUpdateDto[] Transactions
        {
            get; set;
        }
    }
}