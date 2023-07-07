using System;
using System.Collections.Generic;

namespace FeemanagementSystem.Models;

public partial class FeeHeader
{
    public int Fid { get; set; }

    public string Title { get; set; } = null!;

    public int EntryUserId { get; set; }

    public DateTime? CancelledDate { get; set; }

    public int? CancelledUserId { get; set; }

    public string? ReasonForCancelled { get; set; }

    public virtual UserList? CancelledUser { get; set; }

    public virtual UserList EntryUser { get; set; } = null!;

    public virtual ICollection<FeeSheet> FeeSheets { get; set; } = new List<FeeSheet>();

    public virtual ICollection<FeeStructure> FeeStructures { get; set; } = new List<FeeStructure>();
}
