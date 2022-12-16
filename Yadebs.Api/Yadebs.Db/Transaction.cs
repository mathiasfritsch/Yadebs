namespace Yadebs.Db
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public Journal Journal { get; set; }
        public int JournalId { get; set; }


        /// <summary>
        /// soll -debit
        /// haben - credit
        /// </summary>
        public bool IsDebit { get; set; }
    }
}