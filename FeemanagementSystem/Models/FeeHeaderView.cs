using System;
using System.Collections.Generic;

namespace FeemanagementSystem.Models;

public partial class FeeHeaderView
{
    public int Fid { get; set; }

    public string Title { get; set; } = null!;

    public int EntryUserId { get; set; }

    public string? EntryBy { get; set; }

    public DateTime? CancelledDate { get; set; }

    public int? CancelledUserId { get; set; }

    public string? CancelledBy { get; set; }

    public string? ReasonForCancelled { get; set; }
}
