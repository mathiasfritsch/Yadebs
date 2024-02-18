using Microsoft.AspNetCore.Mvc;
using Yadebs.Bll;
using Yadebs.Models.Dto;

namespace Yadebs.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private IAccountingService accountingService;

    public AccountsController(IAccountingService accountingService)
    {
        this.accountingService = accountingService;
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id) => await this.accountingService.DeleteAccountAsync(id);

    [HttpGet]
    public async Task<IEnumerable<AccountDto>> GetAsync() => await this.accountingService.GetAccountsAsync();

    [HttpGet("{id}")]
    public async Task<AccountDto> Get(int id) => await this.accountingService.GetAccountAsync(id);

    [HttpPost]
    public async Task<AccountDto> PostAsync([FromBody] AccountDto value) => await this.accountingService.AddAccountAsync(value);

    [HttpPut("{id}")]
    public async Task PutAsync(int id, [FromBody] AccountDto value) => await this.accountingService.UpdateAccountAsync(id, value);
}