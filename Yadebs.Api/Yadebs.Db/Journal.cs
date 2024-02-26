using NodaTime;
using System.ComponentModel.DataAnnotations;
using Yadebs.Bll.Repository;

namespace Yadebs.Db;

public class Journal : IEntity
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Description { get; set; } = default!;
    public LocalDate Date { get; set; }
    public IList<Transaction> Transactions { get; set; } = default!;
}