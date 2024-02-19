using Yadebs.Bll.Repository;

namespace Yadebs.Models.Dto;

public class AccountDto : AccountAddDto, IEntity
{
    public int Id { get; set; }
}