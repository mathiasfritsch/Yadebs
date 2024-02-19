using Yadebs.Bll.Repository;

namespace Yadebs.Models.Dto;

public class AccountUpdateDto : AccountAddDto, IEntity
{
    public int Id { get; set; }
}