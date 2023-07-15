using System;
using System.Collections.Generic;

namespace FeemanagementSystem.Models;

public partial class BusInfo
{
    public int Bid { get; set; }

    public string? DestinationAddress { get; set; }

    public decimal BusFee { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
