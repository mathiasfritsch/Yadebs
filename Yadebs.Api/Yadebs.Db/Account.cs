using System.ComponentModel.DataAnnotations;

namespace Yadebs.Db
{
    public class Account
    {
        public Account()
        {
            this.Children = new List<Account>();
        }

        public int Id { get; set; }
        public int BookId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public int Number { get; set; }

        public Account Parent { get; set; }

        public int? ParentId { get; set; }

        public IEnumerable<Account> Children { get; set; }
    }
}