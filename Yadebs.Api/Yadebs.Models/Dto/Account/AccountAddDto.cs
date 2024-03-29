﻿namespace Yadebs.Models.Dto;

public class AccountAddDto
{
    public string Name { get; set; } = default!;
    public int Number { get; set; }
    public int BookId { get; set; }
    public int? ParentId { get; set; }
    public bool IncreasesDebitWhenMoneyAdded { get; set; }
}