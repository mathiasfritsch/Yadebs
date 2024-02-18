using Microsoft.AspNetCore.Mvc;
using Yadebs.Bll.Interfaces;
using Yadebs.Models.Dto;

namespace Yadebs.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BankTransfersController : ControllerBase
{
    private readonly IIncomeSurplusService _incomeSurplusService;

    public BankTransfersController(IIncomeSurplusService incomeSurplusService)
    {
        this._incomeSurplusService = incomeSurplusService;
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id) => await this._incomeSurplusService.DeleteBankTransferAsync(id);

    [HttpGet]
    public async Task<IEnumerable<BankTransferDto>> GetAsync() => await this._incomeSurplusService.GetBankTransfersAsync();

    [HttpGet("{id}")]
    public async Task<BankTransferDto> Get(int id) => await this._incomeSurplusService.GetBankTransferAsync(id);

    [HttpPost]
    public async Task<BankTransferDto> PostAsync([FromBody] BankTransferAddDto value) => await this._incomeSurplusService.AddBankTransfer(value);

    [HttpPut("{id}")]
    public async Task PutAsync(int id, [FromBody] BankTransferUpdateDto value) => await this._incomeSurplusService.UpdateBankTransferAsync(id, value);
}