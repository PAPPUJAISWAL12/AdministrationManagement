using System;
using System.Collections.Generic;

namespace FeemanagementSystem.Models;

public partial class Reception
{
    public int Rid { get; set; }

    public string PersonName { get; set; } = null!;

    public DateTime EntryDate { get; set; }

    public string EntryTime { get; set; } = null!;

    public string Purpose { get; set; } = null!;

    public string PersonAddress { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime? CancelledDate { get; set; }

    public int? CancelledUserId { get; set; }

    public string FiscalYear { get; set; } = null!;

    public string? ResonForCancell { get; set; }

    public string? ReceptionStatus { get; set; }

    public virtual UserList? CancelledUser { get; set; }

    public virtual UserList User { get; set; } = null!;
}
