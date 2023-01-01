using Microsoft.AspNetCore.Mvc;
using Yadebs.Bll;
using Yadebs.Bll.Interfaces;
using Yadebs.Models.Dto;

namespace Yadebs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalsController : ControllerBase
    {
        private ITransactionService transactionService;


        public JournalsController(ITransactionService accountingService)
        {
            this.transactionService = accountingService;
        }
        [HttpPut("{id}")]
        public async Task PutAsync(int id, [FromBody] JournalDto value) => await this.transactionService.UpdateJournalAsync(id, value);

        [HttpGet()]
        public async Task<List<JournalDto>> Get() => await this.transactionService.GetJournalsAsync();


        [HttpGet("{id}")]
        public async Task<JournalDto> Get(int id) => await this.transactionService.GetJournalAsync(id);

        [HttpPost]
        public async Task<JournalDto> PostAsync([FromBody] JournalDto value) => await this.transactionService.AddJournalAsync(value);

        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id) => await this.transactionService.DeleteJournalAsync(id);

    }
}
