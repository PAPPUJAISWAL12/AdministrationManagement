using System;
using System.Collections.Generic;

namespace FeemanagementSystem.Models;

public partial class ReceiptPrint
{
    public int PrintId { get; set; }

    public int? Rid { get; set; }

    public string PrintTime { get; set; } = null!;

    public DateTime PrintDate { get; set; }

    public int PrintUserId { get; set; }

    public virtual UserList PrintUser { get; set; } = null!;

    public virtual Receipt? RidNavigation { get; set; }
}
