using System;
using System.Collections.Generic;

namespace FeemanagementSystem.Models;

public partial class ReceiptDetail
{
    public int DetailId { get; set; }

    public int? Rid { get; set; }

    public int? SheetId { get; set; }

    public decimal Amount { get; set; }

    public virtual Receipt? RidNavigation { get; set; }

    public virtual FeeSheet? Sheet { get; set; }
}
