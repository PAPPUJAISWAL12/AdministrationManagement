using System;
using System.Collections.Generic;

namespace FeemanagementSystem.Models;

public partial class FeeSheet
{
    public int SheetId { get; set; }

    public int StdId { get; set; }

    public int Fid { get; set; }

    public decimal Amount { get; set; }

    public DateTime DueDate { get; set; }

    public int EntryUserId { get; set; }

    public string EntryTime { get; set; } = null!;

    public DateTime? CancelledDate { get; set; }

    public int? CancelledUserId { get; set; }

    public string? ReasonForCancelled { get; set; }

    public string? FeeSheetStatus { get; set; }

    public virtual UserList? CancelledUser { get; set; }

    public virtual UserList EntryUser { get; set; } = null!;

    public virtual FeeHeader FidNavigation { get; set; } = null!;

    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();

    public virtual Student Std { get; set; } = null!;
}
