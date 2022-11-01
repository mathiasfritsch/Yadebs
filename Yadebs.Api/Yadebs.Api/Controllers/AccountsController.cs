using Microsoft.AspNetCore.Mvc;
using Yadebs.Bll;
using Yadebs.Models.Dto;

namespace Yadebs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IAccountingService accountingService;

        public AccountsController(IAccountingService accountingService)
        {
            this.accountingService = accountingService;
        }

        [HttpGet]
        public async Task<IEnumerable<AccountDto>> GetAsync() => await this.accountingService.GetAccountsAsync();

        [HttpGet]
        [Route("AccountTree")]
        public async Task<IEnumerable<AccountTreeNode>> AccountTreeAsync() => await this.accountingService.GetAccountTreeAsync();

        [HttpGet("{id}")]
        public async Task<AccountDto> Get(int id) => await this.accountingService.GetAccountAsync(id);

        [HttpPost]
        public async Task<AccountDto> PostAsync([FromBody] AccountDto value) => await this.accountingService.AddAccountAsync(value);

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] AccountDto value) => throw new NotImplementedException();

        [HttpDelete("{id}")]
        public async Task Delete(int id) => await this.accountingService.DeleteAccountAsync(id);
    }
}