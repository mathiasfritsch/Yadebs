namespace Yadebs.Models.Dto.Journal
{
    public class TransactionUpdateDto
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public int AccountId { get; set; }
    }
}