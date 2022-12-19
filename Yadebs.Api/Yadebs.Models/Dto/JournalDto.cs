namespace Yadebs.Models.Dto
{
    public class JournalDto
    {
        public int Id { get; set; }


        public string  Name { get => "SomeString"; }

        public DateTime Date { get; set; }
        public List<TransactionDto> Transactions { get; set; }
    }
}