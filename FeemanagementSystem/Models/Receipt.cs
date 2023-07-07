using System;
using System.Collections.Generic;

namespace FeemanagementSystem.Models;

public partial class Receipt
{
    public int Rid { get; set; }

    public DateTime ReceiptDate { get; set; }

    public int? StdId { get; set; }

    public string ReceiptTime { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public decimal? Discount { get; set; }

    public int EntryUserId { get; set; }

    public DateTime? CancelledDate { get; set; }

    public int? CancelledUserId { get; set; }

    public string? ReasonForCancelled { get; set; }

    public virtual UserList? CancelledUser { get; set; }

    public virtual UserList EntryUser { get; set; } = null!;

    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();

    public virtual ICollection<ReceiptPrint> ReceiptPrints { get; set; } = new List<ReceiptPrint>();

    public virtual UserList? Std { get; set; }
}
