using System;
using System.Collections.Generic;

namespace FeemanagementSystem.Models;

public partial class ReceiptDetailView
{
    public string Title { get; set; } = null!;

    public string Cname { get; set; } = null!;

    public int SheetEntryUserId { get; set; }

    public string? SheetEntryUser { get; set; }

    public int Cid { get; set; }

    public string? FeeSheetStatus { get; set; }

    public DateTime? SheetCanceledDate { get; set; }

    public string FullName { get; set; } = null!;

    public int StdId { get; set; }

    public int SheetId { get; set; }

    public decimal BusFee { get; set; }

    public decimal Amount { get; set; }

    public int? Feesheetcancelled { get; set; }

    public string? FeeSheetCancelledUser { get; set; }

    public string? DestinationAddress { get; set; }

    public int? DetailId { get; set; }

    public int? Rid { get; set; }

    public int? ReceiptFeeSheetId { get; set; }

    public decimal? Discount { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? ReceiptTime { get; set; }

    public string? ReceiptEntryUser { get; set; }

    public int? PrintCount { get; set; }

    public DateTime? ReceiptDate { get; set; }

    public string? CancelledBy { get; set; }

    public string? ReasonForCancelled { get; set; }

    public DateTime? CancelledDate { get; set; }

    public int? CancelledUserId { get; set; }
}
