using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yadebs.Bll.Repository;

namespace Yadebs.Db;

public class Account : IEntity
{
    public Account()
    {
        this.Children = new List<Account>();
    }
    public int Id { get; set; }

    public Book Book { get; set; }

    [ForeignKey(nameof(Book))]
    public int BookId { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = default!;
    public int? Number { get; set; }
    public Account Parent { get; set; } = default!;
    public int? ParentId { get; set; }
    /// <summary>
    /// Just a node in the chart of accounts
    /// </summary>
    public bool IsPlaceholder { get; set; }

    /// <summary>
    /// also for children
    /// Zugänge  auf  Sollseite
    /// zB true für Aktivkonto 
    ///     - Betriebsaausstattung (Laptop)
    ///     - Bankkonto
    /// zB false für Passivkonto 
    ///     - Verbindlichkeiten aus Lieferungen und Leistungen
    ///     - Umsatzsteuer
    /// zB true für Aufwandskonten
    ///     - Versicherungen
    ///     - Bürobedarf
    /// zB false für Ertragskonten
    ///     - Erlöse (zB Forderung 3000 an Erlöse 3000)
    /// </summary>
    public bool IncreasesDebitWhenMoneyAdded { get; set; }
    public IEnumerable<Account> Children { get; set; }
}