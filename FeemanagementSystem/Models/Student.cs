using System;
using System.Collections.Generic;

namespace FeemanagementSystem.Models;

public partial class Student
{
    public int StdId { get; set; }

    public int Cid { get; set; }

    public int? Bid { get; set; }

    public int UserId { get; set; }

    public short RollNo { get; set; }

    public virtual BusInfo? BidNavigation { get; set; }

    public virtual Class CidNavigation { get; set; } = null!;

    public virtual ICollection<FeeSheet> FeeSheets { get; set; } = new List<FeeSheet>();

    public virtual UserList User { get; set; } = null!;
}
