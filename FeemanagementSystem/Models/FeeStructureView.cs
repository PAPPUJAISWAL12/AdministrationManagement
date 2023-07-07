using System;
using System.Collections.Generic;

namespace FeemanagementSystem.Models;

public partial class FeeStructureView
{
    public int FsId { get; set; }

    public int? Cid { get; set; }

    public string Cname { get; set; } = null!;

    public int? Fid { get; set; }

    public string Title { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime DueDate { get; set; }

    public int EntryUserId { get; set; }

    public string? EntryBy { get; set; }

    public string EntryTime { get; set; } = null!;

    public DateTime? CancelledDate { get; set; }

    public int? CancelledUserId { get; set; }

    public string? CancelledBy { get; set; }

    public string? ReasonForCancelled { get; set; }
}
