using Microsoft.AspNetCore.Mvc;
using Yadebs.Bll;
using Yadebs.Models.Dto;

namespace Yadebs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private  IAccountingService accountingService;
        public AccountsController(IAccountingService accountingService)
        {
            this.accountingService = accountingService;
        }

        private static AccountDto[] accounts = {
            new AccountDto { Id = 1, Name = "Lieferungen und Leistungen",Number = 2333 } ,
            new AccountDto { Id = 2, Name = "Erträge",Number = 2334 } ,
            new AccountDto { Id = 3, Name = "Bankkonto",Number = 2335 } ,
            new AccountDto { Id = 4, Name = "Umsatzssteuer",Number = 2336 }
        };

        [HttpGet]
        public async Task<IEnumerable<AccountDto>> GetAsync()
        {
            return await this.accountingService.GetAccountsAsync();
        }

        [HttpGet("{id}")]
        public async Task<AccountDto> Get(int id)
        {
            return await this.accountingService.GetAccountAsync(id);
        }

        [HttpPost]
        public async Task<AccountDto> PostAsync([FromBody] AccountDto value)
        {
            return await this.accountingService.AddAccountAsync(value);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] AccountDto value)
        {
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
             await this.accountingService.DeleteAccountAsync(id);
        }
    }
}