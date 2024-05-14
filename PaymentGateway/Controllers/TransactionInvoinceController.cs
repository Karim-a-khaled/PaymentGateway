using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Entities.DTOs;
using PaymentGateway.Services;

namespace PaymentGateway.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TransactionInvoinceController : ControllerBase
{
    private readonly TransactionInvoiceService _transactionInvoice;
    public TransactionInvoinceController(TransactionInvoiceService transactionInvoice)
    {
        _transactionInvoice = transactionInvoice;
    }


    [AllowAnonymous]
    [HttpPost("login")] 
    public async Task<ActionResult<string>> Login(LoginDto loginDto)
    {
        var user = _transactionInvoice.Login(loginDto);

        if (user is null)
            return Unauthorized();

        return Ok(user);
    }

 
    [HttpPost("bill/generate")]
    public async Task<ActionResult<string>> Pay(GenerateBillDto generateBillDto)
    {
        var tranactionBill = _transactionInvoice.Pay(generateBillDto);

        if (tranactionBill is null)
            return BadRequest();

        return Ok(tranactionBill);
    }


    [HttpPost("refund/generate")]
    public async Task<ActionResult<string>> Refund(RefundDto refundDto)
    {
        var tranactionBillRefund = _transactionInvoice.Refund(refundDto);

        if (tranactionBillRefund is null)
            return BadRequest();

        return Ok(tranactionBillRefund);
    }
}