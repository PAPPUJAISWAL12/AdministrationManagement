using System;
using System.Collections.Generic;

namespace FeemanagementSystem.Models;

public partial class PrograInfoView
{
    public int Pid { get; set; }

    public string Pname { get; set; } = null!;

    public string Pdescription { get; set; } = null!;

    public string Venue { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public string StartTime { get; set; } = null!;

    public DateTime EndDate { get; set; }

    public string EndTime { get; set; } = null!;

    public int UserId { get; set; }

    public string? EntryUser { get; set; }

    public DateTime? CancelledDate { get; set; }

    public int? CancelledUserId { get; set; }

    public string? CancelledBy { get; set; }

    public string? ReasonForCancell { get; set; }
}
