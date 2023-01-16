namespace Yadebs.Models.Dto.Journal
{
    public class JournalDto
    {
        public int Id { get; set; }


        public string Description
        {
            get; set;
        }

        public DateTime Date { get; set; }
        public List<TransactionDto> Transactions { get; set; }
    }
}