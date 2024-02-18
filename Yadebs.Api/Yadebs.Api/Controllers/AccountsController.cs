using Microsoft.AspNetCore.Mvc;
using Yadebs.Bll;
using Yadebs.Models.Dto;

namespace Yadebs.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountingService _accountingService;

    public AccountsController(IAccountingService accountingService)
    {
        this._accountingService = accountingService;
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id) => await this._accountingService.DeleteAccountAsync(id);

    [HttpGet]
    public async Task<IEnumerable<AccountDto>> GetAsync() => await this._accountingService.GetAccountsAsync();

    [HttpGet("{id}")]
    public async Task<AccountDto> Get(int id) => await this._accountingService.GetAccountAsync(id);

    [HttpPost]
    public async Task<AccountDto> PostAsync([FromBody] AccountAddDto value) => await this._accountingService.AddAccountAsync(value);

    [HttpPut("{id}")]
    public async Task PutAsync(int id, [FromBody] AccountUpdateDto value) => await this._accountingService.UpdateAccountAsync(id, value);
}