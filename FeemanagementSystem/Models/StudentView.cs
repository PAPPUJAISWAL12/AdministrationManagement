using System;
using System.Collections.Generic;

namespace FeemanagementSystem.Models;

public partial class StudentView
{
    public int StdId { get; set; }

    public int Cid { get; set; }

    public int? Bid { get; set; }

    public string DestinationAddress { get; set; } = null!;

    public decimal BusFee { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Cname { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string UserAddress { get; set; } = null!;

    public string Phone { get; set; } = null!;
}
