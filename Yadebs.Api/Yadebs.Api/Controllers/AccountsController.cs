using Microsoft.AspNetCore.Mvc;
using Yadebs.Models.Dto;

namespace Yadebs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private static Account[] accounts = {
            new Account { Id = 1, Name = "Lieferungen und Leistungen",Number = 2333 } ,
            new Account { Id = 2, Name = "Erträge",Number = 2334 } ,
            new Account { Id = 3, Name = "Bankkonto",Number = 2335 } ,
            new Account { Id = 4, Name = "Umsatzssteuer",Number = 2336 }
        };

        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return accounts;
        }

        [HttpGet("{id}")]
        public Account Get(int id)
        {
            return accounts[0];
        }

        [HttpPost]
        public void Post([FromBody] Account value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Account value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}