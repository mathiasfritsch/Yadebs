using Microsoft.AspNetCore.Mvc;
using Yadebs.Bll;
using Yadebs.Bll.Interfaces;
using Yadebs.Models.Dto;

namespace Yadebs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalController : ControllerBase
    {
        private ITransactionService transactionService;


        public JournalController(ITransactionService accountingService)
        {
            this.transactionService = accountingService;
        }
        [HttpGet("{id}")]
        public async Task<JournalDto> Get(int id) => await this.transactionService.GetJournalAsync(id);

    }
}
