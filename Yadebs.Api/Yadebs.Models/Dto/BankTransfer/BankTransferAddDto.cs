﻿namespace Yadebs.Models.Dto;

public class BankTransferAddDto
{
    public decimal? NetAmount { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal GrossAmount { get; set; }
    public decimal Tax { get; set; }
    public bool IsIncome { get; set; }
    public DateTime PaymentDate { get; set; }
}