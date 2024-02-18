using System.ComponentModel.DataAnnotations;

namespace Yadebs.Db;

public class Book
{
    public Book()
    {
        this.Accounts = new List<Account>();
    }
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; }
    public IEnumerable<Account> Accounts { get; set; }
}