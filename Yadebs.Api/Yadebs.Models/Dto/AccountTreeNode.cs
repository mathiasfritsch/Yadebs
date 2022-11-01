namespace Yadebs.Models.Dto
{
    public class AccountTreeNode
    {
        public int? ParentId { get; set; }
        public int Number { get; set; }
        public string? Name { get; set; }
        public int Id { get; set; }
        public AccountTreeNode[]? Children { get; set; }
    }
}