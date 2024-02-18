using System.ComponentModel.DataAnnotations;

namespace Yadebs.Db;

public class Journal
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Description { get; set; }

    public DateTime Date { get; set; }

    public IList<Transaction> Transactions { get; set; }
}