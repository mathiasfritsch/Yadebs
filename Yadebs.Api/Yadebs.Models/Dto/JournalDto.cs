namespace Yadebs.Models.Dto
{
    public class JournalDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<TransactionDto> Transactions { get; set; }
    }
}