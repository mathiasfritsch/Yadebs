using System.ComponentModel.DataAnnotations;

namespace Yadebs.Db.IncomeSurplusCalculation;

public class Document
{
    public int Id { get; set; }

    [MaxLength(150)]
    public string Description { get; set; }

    [MaxLength(150)]
    public string InvoiceNumber { get; set; }

    [MaxLength(150)]
    public string DocumentReference { get; set; }
}