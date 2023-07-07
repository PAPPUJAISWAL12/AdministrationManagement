using System;
using System.Collections.Generic;

namespace FeemanagementSystem.Models;

public partial class ReceiptView
{
    public int Rid { get; set; }

    public DateTime ReceiptDate { get; set; }

    public int? StdId { get; set; }

    public string DestinationAddress { get; set; } = null!;

    public decimal BusFee { get; set; }

    public string FullName { get; set; } = null!;

    public string UserAddress { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string Cname { get; set; } = null!;

    public int Cid { get; set; }

    public string ReceiptTime { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public decimal? Discount { get; set; }

    public int EntryUserId { get; set; }

    public string? EntryBy { get; set; }

    public int? CancelledUserId { get; set; }

    public string? CancelledBy { get; set; }

    public DateTime? CancelledDate { get; set; }

    public string? ReasonForCancelled { get; set; }
}
