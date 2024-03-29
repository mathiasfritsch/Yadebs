﻿using Yadebs.Bll.Repository;

namespace Yadebs.Models.Dto;

public class BankTransferUpdateDto :  IEntity
{
    public int Id { get; set; }
    public decimal? NetAmount { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal GrossAmount { get; set; }
    public decimal Tax { get; set; }
    public bool IsIncome { get; set; }
    public DateTime PaymentDate { get; set; }
}