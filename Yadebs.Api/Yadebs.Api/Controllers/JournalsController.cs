using Microsoft.AspNetCore.Mvc;
using Yadebs.Bll.Interfaces;
using Yadebs.Models.Dto;

namespace Yadebs.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JournalsController : ControllerBase
{
    private readonly ITransactionService _transactionService;


    public JournalsController(ITransactionService accountingService)
    {
        this._transactionService = accountingService;
    }
    [HttpPut("{id}")]
    public async Task PutAsync(int id, [FromBody] JournalUpdateDto value)
    {
        await this._transactionService.UpdateJournalAsync(id, value);
    }

    [HttpGet()]
    public async Task<List<JournalDto>> Get() => await this._transactionService.GetJournalsAsync();


    [HttpGet("{id}")]
    public async Task<JournalDto> Get(int id) => await this._transactionService.GetJournalAsync(id);

    [HttpPost]
    public async Task<JournalDto> PostAsync([FromBody] JournalAddDto journalAdd)
    {
        return await this._transactionService.AddJournalAsync(journalAdd);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(int id) => await this._transactionService.DeleteJournalAsync(id);

}